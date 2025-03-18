using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface ICategoryService
	{
		Task<IEnumerable<CategoryModel>> GetCategoriesAsync(string categoryIds = null, bool incldueProducts = false);
		Task<CategoryModel> GetCategoryAsync(int categoryId);
		Task<AddUpdateResponse> AddCategoryAsync(string name);
		Task<AddUpdateResponse> UpdateCategoryAsync(UpdateRequest request);
	}
}
