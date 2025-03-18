using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class FullProductModel : IMapFrom<FullProduct>
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string DisplayName => !string.IsNullOrEmpty(Uom) ? $"{Name} ({Uom})" : Name;

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
