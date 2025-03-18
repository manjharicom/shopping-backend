using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IRecipeRepository
	{
		Task<IEnumerable<Recipe>> GetAsync(string name);
		Task<Recipe> GetAsync(int recipeId);
		Task<int> AddAsync(
			  string name
			, string cookingInstructions);
		Task<int> UpdateAsync(
			  int recipeId
			, string name
			, string cookingInstructions);
		Task AddProductAsync(
			  int recipeId
			, int productId
			, string measurement);
		Task DeleteProductAsync(
		  int recipeId
		, int productId);

		Task<IEnumerable<Recipe>> SearchAsync(string searchText);
	}
}
