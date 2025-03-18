using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
    public interface IProductRepository
    {
        Task<IEnumerable<FullProduct>> GetProductsAsync(int orderBy, string searchText = null, int? categoryId = null, int? areaId = null);
        Task<FullProduct> GetProductAsync(int productId);
        Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId);
		Task<IEnumerable<Product>> SearchProductsAsync(string searchText, int? categoryId = null, int? areaId = null);

        Task AddProductToListAsync(int shoppingListId, int productId, int quantity = 1, bool purchased = false);
		Task UpdateProductQuanityListAsync(int shoppingListId, int productId, int quantity);
		Task RemoveProductFromListAsync(int shoppingListId, int productId);

		Task<IEnumerable<Product>> GetShoppingListProductsAsync(int? shoppingListId = null);
		Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> productIds);
        Task<int> AddProductAsync(int categoryId, int areaId, string name, int uomId, int? priceUomId);
		Task<int> UpdateProductAsync(int productId, int categoryId, int areaId, string name, int uomId, int? priceUomId);
	}
}
