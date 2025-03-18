using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IMenuService
	{
		Task<AddUpdateResponse> AddAsync(AddRecipeMenuRequest request);
		Task<AddUpdateResponse> AddProductAsync(int menuId, RecipeMenuProductModel request);
		Task<AddUpdateResponse> AddRecipeAsync(int menuId, int recipeId);
		Task<AddUpdateResponse> DeleteProductAsync(int menuId, int productId);
		Task<AddUpdateResponse> DeleteRecipeAsync(int menuId, int recipeId);
		Task<IEnumerable<MenuModel>> GetAsync(string name);
		Task<MenuModel> GetAsync(int menuId);
		Task<AddUpdateResponse> UpdateAsync(UpdateMenuRequest request);
	}
}
