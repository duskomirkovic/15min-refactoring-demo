using RefactoringDemo.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// Kreiramo test porudžbinu
var order = new Order
{
	Id = 1,
	CustomerName = "Marko Petrović",
	CreatedAt = DateTime.Now,
	Items =
	[
		new() { Name = "Tastatura", Price = 4500m, Quantity = 1 },
		new() { Name = "Miš",       Price = 2200m, Quantity = 2 }
	]
};

// ── PRE refaktorisanja ──────────────────────────────────────
Console.WriteLine("═══ PRE refaktorisanja ═══");
Console.WriteLine();

var before = new RefactoringDemo.Before.OrderProcessor();
var result1 = before.Process(order);
Console.WriteLine($"  Rezultat: {result1}");
Console.WriteLine();

// Resetujemo porudžbinu za drugi prolaz
order.IsProcessed = false;
order.TotalAmount = 0;

// ── POSLE refaktorisanja ────────────────────────────────────
Console.WriteLine("═══ POSLE refaktorisanja ═══");
Console.WriteLine();

var notificationService = new RefactoringDemo.After.ConsoleNotificationService();
var warehouseService = new RefactoringDemo.After.ConsoleWarehouseService();
var after = new RefactoringDemo.After.OrderProcessor(notificationService, warehouseService);
var result2 = after.ProcessOrder(order);
Console.WriteLine($"  Rezultat: {result2}");
