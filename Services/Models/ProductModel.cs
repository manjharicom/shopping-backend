using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
    public class ProductModel : IMapFrom<Product>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
		public string DisplayName => !string.IsNullOrEmpty(Uom) ? $"{Name} ({Uom})" : Name;
		
		public string AisleLabel { get; set; }
		public int Sequence { get; set; }
		public int Quantity { get; set; }
		public bool IsShipped { get; set; }
		public int? ShoppingListId { get; set; }


		public int? UomId { get; set; }
		public string Uom { get; set; }
		public int? PriceUomId { get; set; }
		public string PriceUom { get; set; }
		public bool AllowDecimalQuantity { get; set; }
	}
}
