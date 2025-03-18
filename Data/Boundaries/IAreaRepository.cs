using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IAreaRepository
	{
		Task<IEnumerable<Area>> GetAreasAsync(IEnumerable<int> areaIds = null, bool includeProducts = false);
		Task<Area> GetAreaAsync(int areaId);
		Task<int> AddAreaAsync(string name);
		Task<int> UpdateAreaAsync(int areaId, string name);
	}
}
