namespace Services.Models
{
	public class ExportProductsRequest
	{
		public string SearchText { get; set; }
		public int? CategoryId { get; set; }
		public int? AreaId { get; set; }
	}
}
