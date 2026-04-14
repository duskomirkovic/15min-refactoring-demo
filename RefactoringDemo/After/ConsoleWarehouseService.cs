using RefactoringDemo.Models;

namespace RefactoringDemo.After;

/// <summary>
/// Jednostavna implementacija za demo — ispisuje na konzolu.
/// U produkciji bi komunicirala sa pravim magacinskim sistemom.
/// </summary>
public class ConsoleWarehouseService : IWarehouseService
{
	public void Send(Order order)
	{
		Console.WriteLine($"  [Magacin] Porudžbina #{order.Id} za {order.CustomerName}, stavki: {order.Items.Count}");
	}
}
