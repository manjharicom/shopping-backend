using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IUomRepository
	{
		Task<int> AddUomAsync(string name, bool allowDecimalQuantity);
		Task<UnitOfMeasure> GetUomAsync(int uom);
		Task<IEnumerable<UnitOfMeasure>> GetUomsAsync(IEnumerable<int> uoms = null);
		Task<int> UpdateUomAsync(int uomId, string name, bool allowDecimalQuantity);
	}
}
