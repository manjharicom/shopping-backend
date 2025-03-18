namespace Data.Entities
{
	public class ShoppingListProductPrice
	{
		public int ShoppingListPriceId { get; set; }
		public int ProductId { get; set; }
		public string Comment { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total { get; set; }
	}
}
