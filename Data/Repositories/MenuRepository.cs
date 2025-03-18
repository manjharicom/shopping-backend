using Dapper;
using Data.Boundaries;
using Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class MenuRepository : SqlRepository, IMenuRepository
	{
		public MenuRepository(IDbConnection connection) : base(connection)
		{
		}

		public async Task<int> AddAsync(string name, string cookingInstructions)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("CookingInstructions", cookingInstructions);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);
			parameters.Add("MenuId", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddMenu]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			var MenuId = parameters.Get<int>("MenuId");
			if (returnCode == 0)
				return MenuId;
			return returnCode;
		}

		public async Task AddProductAsync(int menuId, int productId, string measurement)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			parameters.Add("productId", productId);
			parameters.Add("Measurement", measurement);

			await ExecuteAsync("[dbo].[AddProductToMenu]", parameters);
		}

		public async Task AddRecipeAsync(int menuId, int recipeId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			parameters.Add("recipeId", recipeId);

			await ExecuteAsync("[dbo].[AddRecipeToMenu]", parameters);
		}

		public async Task DeleteProductAsync(int menuId, int productId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			parameters.Add("productId", productId);

			await ExecuteAsync("[dbo].[RemoveProductFromMenu]", parameters);
		}

		public async Task DeleteRecipeAsync(int menuId, int recipeId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			parameters.Add("recipeId", recipeId);

			await ExecuteAsync("[dbo].[RemoveRecipeFromMenu]", parameters);
		}

		public async Task<IEnumerable<Menu>> GetAsync(string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SearchText", name);
			return await QueryAsync<Menu>("[dbo].[GetMenus]", parameters);
		}

		public async Task<Menu> GetAsync(int menuId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			return await QuerySingleAsync<Menu>("[dbo].[GetMenu]", parameters);
		}

		public async Task<int> UpdateAsync(int menuId, string name, string cookingInstructions)
		{
			var parameters = new DynamicParameters();
			parameters.Add("menuId", menuId);
			parameters.Add("Name", name);
			parameters.Add("CookingInstructions", cookingInstructions);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateMenu]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}
	}
}
