using RefactoringDemo.Models;

namespace RefactoringDemo.After;

/// <summary>
/// "Posle" verzija — svih 9 refactoring-a primenjeno.
/// Uporedite sa RefactoringDemo.Before.OrderProcessor.
/// </summary>
public class OrderProcessor
{
	// Refactoring 7: Konstante umesto magičnih brojeva
	private const decimal DiscountThreshold = 100m;
	private const decimal DiscountRate = 0.9m;
	private const decimal TaxMultiplier = 1.2m;
	private const int OrderExpirationMs = 5000;

	// Refactoring 5: Enkapsulacija — IReadOnlyList umesto javne List<T>
	private readonly List<Item> _items = new();
	public IReadOnlyList<Item> Items => _items;

	// Refactoring 8: Zavisnosti preko interfejsa — lako se testira
	private readonly INotificationService _notificationService;
	private readonly IWarehouseService _warehouseService;

	public OrderProcessor(
		INotificationService notificationService,
		IWarehouseService warehouseService)
	{
		_notificationService = notificationService;
		_warehouseService = warehouseService;
	}

	// Refactoring 3: Jasno ime — ProcessOrder umesto Process
	// Refactoring 1: Extract Method — svaki korak izdvojen
	// Refactoring 6: Jedna odgovornost — samo koordinacija
	public bool ProcessOrder(Order order)
	{
		if (!IsValid(order))
			return false;

		order.TotalAmount = CalculateTotal(order);

		var isExpired = IsOrderExpired(order);

		_warehouseService.Send(order);
		_notificationService.NotifyCustomer(order);
		SaveOrder(order);

		return !isExpired;
	}

	// Refactoring 3: Jasno ime — ProcessAllOrders umesto DoWork
	public void ProcessAllOrders(List<Order> orders)
	{
		foreach (var order in orders)
		{
			// Refactoring 2: DRY — koristi istu validacionu metodu
			if (!IsValid(order)) continue;
			ProcessOrder(order);
		}
	}

	// Refactoring 2: DRY — jedna validaciona metoda za ceo projekat
	// Refactoring 1: Extract Method
	private static bool IsValid(Order order)
	{
		return order != null
			&& order.Items != null
			&& order.Items.Count > 0
			&& !string.IsNullOrEmpty(order.CustomerName);
	}

	// Refactoring 1: Extract Method
	// Refactoring 9: Čista funkcija — bez sporednih efekata
	private static decimal CalculateTotal(Order order)
	{
		decimal total = order.Items.Sum(item => item.Price * item.Quantity);

		if (total > DiscountThreshold)
			total *= DiscountRate;

		total *= TaxMultiplier;
		return total;
	}

	// Refactoring 1: Extract Method
	// Refactoring 9: Čista provera — nema sporednih efekata
	private static bool IsOrderExpired(Order order)
	{
		var elapsed = (DateTime.Now - order.CreatedAt).TotalMilliseconds;
		return elapsed > OrderExpirationMs;
	}

	// Refactoring 9: Sporedan efekat izolovan u zasebnu metodu
	private static void SaveOrder(Order order)
	{
		order.IsProcessed = true;
	}
}
