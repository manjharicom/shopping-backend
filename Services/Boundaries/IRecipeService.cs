using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IRecipeService
	{
		Task<IEnumerable<RecipeModel>> GetAsync(string name);
		Task<RecipeModel> GetAsync(int recipeId);
		Task<AddUpdateResponse> AddAsync(AddRecipeMenuRequest request);
		Task<AddUpdateResponse> UpdateAsync(UpdateRecipeRequest request);
		Task<AddUpdateResponse> AddProductAsync(int recipeId, RecipeMenuProductModel request);
		Task<AddUpdateResponse> DeleteProductAsync(int recipeId, int productId);
		Task<IEnumerable<RecipeModel>> SearchAsync(string searchText);
	}
}
