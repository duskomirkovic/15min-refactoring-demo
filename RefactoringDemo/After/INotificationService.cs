using RefactoringDemo.Models;

namespace RefactoringDemo.After;

/// <summary>
/// Servis za slanje obaveštenja kupcu.
/// Pattern 8: Interfejs omogućava laku zamenu implementacije i testiranje.
/// </summary>
public interface INotificationService
{
	void NotifyCustomer(Order order);
}
