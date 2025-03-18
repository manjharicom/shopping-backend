namespace Data.Entities
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public bool IsShipped { get; set; }
		public string AisleLabel { get; set; }
		public int Sequence { get; set; }
		public int Quantity { get; set; }
		public int? ShoppingListId { get; set; }

		public int? UomId { get; set; }
		public string Uom { get; set; }
		public int? PriceUomId { get; set; }
		public string PriceUom { get; set; }
		public bool AllowDecimalQuantity { get; set; }
		public bool Purchased { get; set; }
	}
}
