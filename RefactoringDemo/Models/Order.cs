namespace RefactoringDemo.Models;

public class Order
{
	public int Id { get; set; }
	public string CustomerName { get; set; } = string.Empty;
	public List<Item> Items { get; set; } = new();
	public DateTime CreatedAt { get; set; }
	public bool IsProcessed { get; set; }
	public decimal TotalAmount { get; set; }
}
