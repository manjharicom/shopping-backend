using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface ISuperMarketService
	{
		Task<IEnumerable<SuperMarketModel>> GetSuperMarketsAsync(string superMarketIds = null);
		Task<SuperMarketModel> GetSuperMarketAsync(int superMarketId);
		Task<IEnumerable<CategorySuperMarketModel>> GetCategorySuperMarketsAsync(int superMarketId);
		Task<AddUpdateResponse> AddAsync(string name);
		Task<AddUpdateResponse> UpdateAsync(UpdateRequest request);
		Task DeleteAsync(int superMarketId);
		Task<AddUpdateResponse> MergeCategorySuperMarketAsync(int superMarketId, IEnumerable<CategorySuperMarketRequest> categorySuperMarkets);
		Task SetSuperMarketAsync(int superMarketId);
	}
}
