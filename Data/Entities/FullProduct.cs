namespace Data.Entities
{
	public class FullProduct
	{
		public int ProductId { get; set; }
		public string Name { get; set; }

		public int CategoryId { get; set; }
		public string Category { get; set; }

		public int AreaId { get; set; }
		public string Area { get; set; }

		public int? ShoppingListId { get; set; }
		public bool IsShipped { get; set; }

		public int? UomId { get; set; }
		public string Uom { get; set; }
		public int? PriceUomId { get; set; }
		public string PriceUom { get; set; }
	}
}
