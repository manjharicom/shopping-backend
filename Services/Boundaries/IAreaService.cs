using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IAreaService
	{
		Task<AddUpdateResponse> AddAreaAsync(string name);
		Task<AreaModel> GetAreaAsync(int areaId);
		Task<IEnumerable<AreaModel>> GetAreasAsync(string areaIds = null, bool incldueProducts = false);
		Task<AddUpdateResponse> UpdateAreaAsync(UpdateRequest request);
	}
}
