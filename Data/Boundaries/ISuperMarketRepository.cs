using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface ISuperMarketRepository
	{
		Task<IEnumerable<SuperMarket>> GetSuperMarketsAsync(IEnumerable<int> superMarketIds = null);
		Task<SuperMarket> GetSuperMarketAsync(int superMarketId);
		Task<IEnumerable<CategorySuperMarket>> GetCategorySuperMarketsAsync(int superMarketId);
		Task<int> AddAsync(string name);
		Task<int> UpdateAsync(int superMarketId, string name);
		Task DeleteAsync(int superMarketId);
		Task MergeCategorySuperMarketAsync(int superMarketId, IEnumerable<UdtCategorySuperMarket> categorySuperMarkets);
		Task SetSuperMarketAsync(int superMarketId);
	}
}
