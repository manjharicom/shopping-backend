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
	public class ShoppingListRepository : SqlRepository, IShoppingListRepository
	{
		public ShoppingListRepository(IDbConnection dbConnection) : base(dbConnection)
		{
		}

		public async Task<int> CreateShoppingListAsync(int superMarketId, string checkListId, string boardId, string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SuperMarketId", superMarketId);
			parameters.Add("CheckListId", checkListId);
			parameters.Add("BoardId", boardId);
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[CreateShoppingList]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;

		}

		public async Task<int> UpdateShoppingListAsync(int shoppingListId, string checkListId, string boardId, string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("CheckListId", checkListId);
			parameters.Add("BoardId", boardId);
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateShoppingList]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task DeleteShoppingListAsync(int shoppingListId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			await ExecuteAsync("[dbo].[DeleteShoppingList]", parameters);
		}

		public async Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> shoppingListIds = null)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			if (shoppingListIds?.Any() ?? false)
			{
				var items = shoppingListIds.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("ShoppingLists", items);
			}
			await ExecuteAsync("[dbo].[DeleteShoppingListItems]", parameters);
		}

		public async Task<ShoppingList> GetAsync(int shoppingListId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);

			return await QuerySingleAsync<ShoppingList>("[dbo].[GetShoppingList]", parameters);
		}

		public async Task<IEnumerable<ShoppingList>> GetAsync(IEnumerable<int> shoppingListIds = null)
		{
			var parameters = new DynamicParameters();
			if (shoppingListIds?.Any() ?? false)
			{
				var items = shoppingListIds.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("ShoppingLists", items);
			}

			return await QueryAsync<ShoppingList>("[dbo].[GetShoppingLists]", parameters);
		}
	}
}
