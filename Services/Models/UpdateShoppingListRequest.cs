namespace Services.Models
{
	public class UpdateShoppingListRequest
	{
		public int ShoppingListId { get; set; }
		public string CheckListId { get; set; }
		public string BoardId { get; set; }
		public string Name { get; set; }
	}
}
