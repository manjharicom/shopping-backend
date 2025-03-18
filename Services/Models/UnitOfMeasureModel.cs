using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class UnitOfMeasureModel : IMapFrom<UnitOfMeasure>
	{
		public int UomId { get; set; }
		public string Name { get; set; }
		public bool AllowDecimalQuantity { get; set; }
	}
}
