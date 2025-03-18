using Dapper;
using Data.Boundaries;
using Data.Entities;
using Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class ShoppingListPriceRepository : SqlRepository, IShoppingListPriceRepository
	{
		public ShoppingListPriceRepository(IDbConnection connection) : base(connection)
		{
		}

		public async Task<ShoppingListPrice> GetShoppingListPriceAsync(int shoppingListPriceId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListPriceId", shoppingListPriceId);
			return await QuerySingleAsync<ShoppingListPrice>("[dbo].[GetShoppingListPrice]", parameters);
		}

		public async Task<IEnumerable<ShoppingListPrice>> GetShoppingListPricesAsync(IEnumerable<int> ids = null, bool includePrices = false)
		{
			var parameters = new DynamicParameters();
			parameters.Add("IncludePrices", includePrices);
			if (ids?.Any() ?? false)
			{
				var idList = ids.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("Ids", idList);
			}
			return await QueryAsync<ShoppingListPrice>("[dbo].[GetShoppingListPrices]", parameters);
		}

		public async Task<IEnumerable<ShoppingListProductPrice>> GetShoppingListProductPricesAsync(int shoppingListPriceId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListPriceId", shoppingListPriceId);
			return await QueryAsync<ShoppingListProductPrice>("[dbo].[GetShoppingListProductPrices]", parameters);
		}

		public async Task<int> AddShoppingListPriceAsync(int shoppingListId
			, DateTime shoppingDate
			, IEnumerable<AddShoppingListPriceRequest> prices)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("ShoppingDate", shoppingDate);
			var pricesList = prices.GetTableValuedParameter("dbo.[UdtShoppingListProductPrice]");
			parameters.Add("Prices", pricesList);

			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddShoppingListPrice]", parameters);
			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task<int> UpdateShoppingListPriceAsync(int shoppingListPriceId
			, IEnumerable<AddShoppingListPriceRequest> prices)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListPriceId", shoppingListPriceId);
			var pricesList = prices.GetTableValuedParameter("dbo.[UdtShoppingListProductPrice]");
			parameters.Add("Prices", pricesList);

			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateShoppingListPrice]", parameters);
			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}
	}
}
