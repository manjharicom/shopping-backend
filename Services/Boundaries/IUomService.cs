using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IUomService
	{
		Task<AddUpdateResponse> AddUomAsync(AddUomRequest request);
		Task<UnitOfMeasureModel> GetUomAsync(int uom);
		Task<IEnumerable<UnitOfMeasureModel>> GetUomsAsync(string uoms = null);
		Task<AddUpdateResponse> UpdateUomAsync(UpdateUomRequest request);
	}
}
