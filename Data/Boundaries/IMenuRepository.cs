using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IMenuRepository
	{
		Task<IEnumerable<Menu>> GetAsync(string name);
		Task<Menu> GetAsync(int menuId);
		Task<int> AddAsync(
			  string name
			, string cookingInstructions);
		Task AddProductAsync(
			  int menuId
			, int productId
			, string measurement);
		Task AddRecipeAsync(
			  int menuId
			, int recipeId);
		Task<int> UpdateAsync(
			  int menuId
			, string name
			, string cookingInstructions);
		Task DeleteProductAsync(
		  int menuId
		, int productId);
		Task DeleteRecipeAsync(
		  int menuId
		, int recipeId);
	}
}
