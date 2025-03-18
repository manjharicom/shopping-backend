using Services.Enums;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IProductService
    {
        Task<IEnumerable<FullProductModel>> GetProductsAsync(OrderBy orderBy, string searchText = null, int? categoryId = null, int? areaId = null);
        Task<FullProductModel> GetProductAsync(int productId);
        Task<IEnumerable<ProductModel>> GetProductsForCategoryAsync(int categoryId);
		Task<IEnumerable<ProductModel>> SearchProductsAsync(string searchText, int? categoryId = null, int? areaId = null);

		Task AddProductToListAsync(int shoppingListId, int productId, int quantity = 1, bool purchases = false);
		Task UpdateProductQuanityListAsync(int shoppingListId, int productId, int quantity);
		Task RemoveProductFromListAsync(int shoppingListId, int productId);
		Task<IEnumerable<ProductModel>> GetShoppingListProductsAsync(int? shoppingListId = null, bool isPurchased = false);
		Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> productIds);

		Task<AddUpdateResponse> AddProductAsync(AddProductRequest request);
		Task<AddUpdateResponse> UpdateProductAsync(UpdateProductRequest request);
	}
}
