using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> GetCategoriesAsync(IEnumerable<int> categoryIds = null, bool includeProducts = false);
		Task<Category> GetCategoryAsync(int categoryId);
		Task<int> AddCategoryAsync(string name);
		Task<int> UpdateCategoryAsync(int categoryId, string name);
	}
}
