using RefactoringDemo.Models;

namespace RefactoringDemo.After;

/// <summary>
/// Servis za slanje porudžbine u magacin.
/// Pattern 8: Interfejs omogućava laku zamenu implementacije i testiranje.
/// </summary>
public interface IWarehouseService
{
	void Send(Order order);
}
