using RefactoringDemo.Models;

namespace RefactoringDemo.Before;

/// <summary>
/// "Pre" verzija — mnogo prilika za vežbanje refactoring-a.
/// Uporedite sa RefactoringDemo.After.OrderProcessor.
/// </summary>
public class OrderProcessor
{
	// Enkapsulacija: javno izložena List<Item> — pozivalac može da menja sadržaj
	public List<Item> Items = new();

	// Loše ime: "Process" ne govori šta se obrađuje
	public bool Process(Order order)
	{
		// SRP narušen — sve u jednom metodu

		// --- validacija (prvi put) ---
		if (order == null) return false;
		if (order.Items == null || order.Items.Count == 0) return false;
		if (string.IsNullOrEmpty(order.CustomerName)) return false;

		// Loše ime promenljive, magični brojevi
		decimal x = 0;
		foreach (var item in order.Items)
		{
			x += item.Price * item.Quantity;
		}

		if (x > 100)        // Magični broj: prag za popust
			x = x * 0.9m;   // Magični broj: procenat popusta

		x = x * 1.2m;       // Magični broj: stopa poreza

		order.TotalAmount = x;

		// Mrtav kod (Boy Scout pravilo)
		// var oldTotal = x;
		// if (order.IsProcessed) return false;

		// Sporedan efekat unutar računske logike
		Console.WriteLine($"Porudžbina {order.Id}: {x} RSD");

		// Duplirani kod — ista validacija ponovo
		if (order.Items == null || order.Items.Count == 0)
		{
			Console.WriteLine("Greška: nema stavki");
			return false;
		}

		// Magični broj, loše ime "flag"
		var flag = false;
		var elapsed = (DateTime.Now - order.CreatedAt).TotalMilliseconds;
		if (elapsed > 5000)
		{
			Console.WriteLine("Upozorenje: porudžbina je stara");
			flag = true;
		}

		// Bez interfejsa — direktan ispis (tight coupling)
		Console.WriteLine($"Slanje u magacin: {order.CustomerName}, stavki: {order.Items.Count}");

		// Bez interfejsa — direktno obaveštenje
		Console.WriteLine($"Obaveštenje za: {order.CustomerName}");

		// Sporedan efekat pomešan sa ostatkom
		order.IsProcessed = true;
		Console.WriteLine($"Porudžbina {order.Id} sačuvana.");

		return !flag;
	}

	// Loše ime: "DoWork" ne govori ništa
	public void DoWork(List<Order> orders)
	{
		foreach (var o in orders)
		{
			// Duplirani kod — validacija treći put
			if (o == null) continue;
			if (o.Items == null || o.Items.Count == 0) continue;
			if (string.IsNullOrEmpty(o.CustomerName)) continue;

			Process(o);
		}
	}
}
