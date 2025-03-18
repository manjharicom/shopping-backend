namespace Data.Entities
{
	public class AddShoppingListPriceRequest
	{
		public int ProductId { get; set; }
		public string Comment { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total { get; set; }
	}
}
