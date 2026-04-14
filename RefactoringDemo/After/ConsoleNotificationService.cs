using RefactoringDemo.Models;

namespace RefactoringDemo.After;

/// <summary>
/// Jednostavna implementacija za demo — ispisuje na konzolu.
/// U produkciji bi slala email, SMS, push notifikaciju itd.
/// </summary>
public class ConsoleNotificationService : INotificationService
{
	public void NotifyCustomer(Order order)
	{
		Console.WriteLine($"  [Obaveštenje] Kupac: {order.CustomerName}, iznos: {order.TotalAmount:N2} RSD");
	}
}
