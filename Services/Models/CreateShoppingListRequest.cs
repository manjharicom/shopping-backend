namespace Services.Models
{
	public class CreateShoppingListRequest
	{
		public int SuperMarketId { get; set; }
		public string CheckListId { get; set; }
		public string BoardId { get; set; }
		public string Name { get; set; }
	}
}
