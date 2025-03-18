using Dapper;
using Data.Boundaries;
using Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class RecipeRepository : SqlRepository, IRecipeRepository
	{
		public RecipeRepository(IDbConnection connection) : base(connection)
		{
		}

		public async Task<int> AddAsync(string name
			, string cookingInstructions)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("CookingInstructions", cookingInstructions);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);
			parameters.Add("RecipeId", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddRecipe]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			var recipeId = parameters.Get<int>("RecipeId");
			if (returnCode == 0)
				return recipeId;
			return returnCode;
		}

		public async Task AddProductAsync(
			  int recipeId
			, int productId
			, string measurement)
		{
			var parameters = new DynamicParameters();
			parameters.Add("recipeId", recipeId);
			parameters.Add("productId", productId);
			parameters.Add("Measurement", measurement);

			await ExecuteAsync("[dbo].[AddProductToRecipe]", parameters);
		}

		public async Task DeleteProductAsync(int recipeId, int productId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("recipeId", recipeId);
			parameters.Add("productId", productId);

			await ExecuteAsync("[dbo].[RemoveProductFromRecipe]", parameters);
		}

		public async Task<IEnumerable<Recipe>> GetAsync(string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("SearchText", name);
			return await QueryAsync<Recipe>("[dbo].[GetRecipes]", parameters);
		}

		public async Task<Recipe> GetAsync(int recipeId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("recipeId", recipeId);
			return await QuerySingleAsync<Recipe>("[dbo].[GetRecipe]", parameters);
		}

		public async Task<int> UpdateAsync(int recipeId
			, string name
			, string cookingInstructions)
		{
			var parameters = new DynamicParameters();
			parameters.Add("recipeId", recipeId);
			parameters.Add("Name", name);
			parameters.Add("CookingInstructions", cookingInstructions);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateRecipe]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task<IEnumerable<Recipe>> SearchAsync(string searchText)
		{
			var parameters = new DynamicParameters();
			parameters.Add("searchText", searchText);
			return await QueryAsync<Recipe>("[dbo].[SearchRecipes]", parameters);
		}
	}
}
