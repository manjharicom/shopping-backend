namespace Services.Models
{
	public class AddProductRequest
	{
		public int CategoryId { get; set; }
		public int AreaId { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; } = 1;
		public int UomId { get; set; }
		public int? PriceUomId { get; set; }
	}
}
