using Dapper;
using Data.Boundaries;
using Data.Entities;
using Data.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : SqlRepository, IProductRepository
    {
        public ProductRepository(IDbConnection dbConnection) : base(dbConnection)
        {
        }

		public async Task<FullProduct> GetProductAsync(int productId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ProductId", productId);
            return await QuerySingleAsync<FullProduct>("[dbo].[GetProduct]", parameters);
        }

        public async Task<IEnumerable<FullProduct>> GetProductsAsync(int orderBy, string searchText = null, int? categoryId = null, int? areaId = null)
        {
			var parameters = new DynamicParameters();
			parameters.Add("OrderBy", orderBy);
			parameters.Add("SearchText", searchText);
			parameters.Add("CategoryId", categoryId);
			parameters.Add("AreaId", areaId);
			return await QueryAsync<FullProduct>("[dbo].[GetProducts]", parameters);
        }

        public async Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("CategoryId", categoryId);
            return await QueryAsync<Product>("[dbo].[GetProductsForCategory]", parameters);
        }

        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchText, int? categoryId = null, int? areaId = null)
        {
            var parameters = new DynamicParameters();
			parameters.Add("SearchText", searchText);
			parameters.Add("CategoryId", categoryId);
			parameters.Add("AreaId", areaId);
			return await QueryAsync<Product>("[dbo].[SearchProducts]", parameters);
		}

        public async Task AddProductToListAsync(int shoppingListId, int productId, int quantity = 1, bool purchased = false)
        {
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("Quantity", quantity);
			parameters.Add("ProductId", productId);
			parameters.Add("Purchased", purchased);
			await ExecuteAsync("[dbo].[AddProductToShoppingList]", parameters);
		}

		public async Task UpdateProductQuanityListAsync(int shoppingListId, int productId, int quantity)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("ProductId", productId);
			parameters.Add("Quantity", quantity);
			await ExecuteAsync("[dbo].[UpdateProductQuantity]", parameters);
		}

		public async Task RemoveProductFromListAsync(int shoppingListId, int productId)
        {
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("ProductId", productId);
			await ExecuteAsync("[dbo].[RemoveProductFromShoppingList]", parameters);
		}


		public async Task<IEnumerable<Product>> GetShoppingListProductsAsync(int? shoppingListId = null)
        {
			var parameters = new DynamicParameters();
			parameters.Add("ShoppingListId", shoppingListId);
			return await QueryAsync<Product>("[dbo].[GetShoppingListProducts]", parameters);
		}

        public async Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> productIds)
        {
			var parameters = new DynamicParameters();
			if (!productIds?.Any() ?? false)
				return;
			var prods = productIds.GetTableValuedParameter("dbo.UdtId");
			parameters.Add("ShoppingListId", shoppingListId);
			parameters.Add("Products", prods);
			await ExecuteAsync("[dbo].[DeleteShoppingListItems]", parameters);
		}

        public async Task<int> AddProductAsync(int categoryId, int areaId, string name, int uomId, int? priceUomId)
        {
			var parameters = new DynamicParameters();
			parameters.Add("CategoryId", categoryId);
			parameters.Add("AreaId", areaId);
			parameters.Add("Name", name);
			parameters.Add("UomId", uomId);
			parameters.Add("PriceUomId", priceUomId);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);
			await ExecuteAsync("[dbo].[AddProduct]", parameters);

            var returnCode = parameters.Get<int>("ReturnCode");
            return returnCode;
		}

		public async Task<int> UpdateProductAsync(int productId, int categoryId, int areaId, string name, int uomId, int? priceUomId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("ProductId", productId);
			parameters.Add("CategoryId", categoryId);
			parameters.Add("AreaId", areaId);
			parameters.Add("Name", name);
			parameters.Add("UomId", uomId);
			parameters.Add("PriceUomId", priceUomId);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);
			await ExecuteAsync("[dbo].[UpdateProduct]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

	}
}

