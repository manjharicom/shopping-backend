using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class CategorySuperMarketModel : IMapFrom<CategorySuperMarket>
	{
		public int CategoryId { get; set; }
		public string Category { get; set; }
		public string AisleLabel { get; set; }
		public int? Sequence { get; set; }
	}
}
