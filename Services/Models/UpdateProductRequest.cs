namespace Services.Models
{
	public class UpdateProductRequest
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public int AreaId { get; set; }
		public string Name { get; set; }
		public int UomId { get; set; }
		public int? PriceUomId { get; set; }
	}
}
