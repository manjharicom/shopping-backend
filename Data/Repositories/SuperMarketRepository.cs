using Dapper;
using Data.Boundaries;
using Data.Entities;
using Data.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class SuperMarketRepository : SqlRepository, ISuperMarketRepository
	{
		public SuperMarketRepository(IDbConnection dbConnection) : base(dbConnection)
		{
		}

		public async Task<IEnumerable<SuperMarket>> GetSuperMarketsAsync(IEnumerable<int> superMarketIds = null)
		{
			var parameters = new DynamicParameters();
			if (superMarketIds?.Any() ?? false)
			{
				var cats = superMarketIds.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("SuperMarkets", cats);
			}
			return await QueryAsync<SuperMarket>("[dbo].[GetSuperMarkets]", parameters);
		}

		public async Task<SuperMarket> GetSuperMarketAsync(int superMarketId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);
			return await QuerySingleAsync<SuperMarket>("[dbo].[GetSuperMarket]", parameters);
		}

		public async Task<IEnumerable<CategorySuperMarket>> GetCategorySuperMarketsAsync(int superMarketId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);
			return await QueryAsync<CategorySuperMarket>("[dbo].[GetCategorySuperMarkets]", parameters);
		}

		public async Task<int> AddAsync(string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddSuperMarket]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;

		}

		public async Task<int> UpdateAsync(int superMarketId, string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateSuperMarket]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task DeleteAsync(int superMarketId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);

			await ExecuteAsync("[dbo].[DeleteSupermarket]", parameters);
		}


		public async Task MergeCategorySuperMarketAsync(int superMarketId, IEnumerable<UdtCategorySuperMarket> categorySuperMarkets)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);

			if (categorySuperMarkets?.Any() ?? false)
			{
				var cats = categorySuperMarkets.GetTableValuedParameter("dbo.UdtCategorySuperMarket");
				parameters.Add("CategorySuperMarkets", cats);
			}

			await ExecuteAsync("[dbo].[MergeCategorySuperMarket]", parameters);
		}

		public async Task SetSuperMarketAsync(int superMarketId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);
			await ExecuteAsync("[dbo].[SetSuperMarket]", parameters);
		}

	}
}
