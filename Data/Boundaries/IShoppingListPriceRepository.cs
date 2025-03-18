using Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IShoppingListPriceRepository
	{
		Task<ShoppingListPrice> GetShoppingListPriceAsync(int shoppingListPriceId);
		Task<IEnumerable<ShoppingListPrice>> GetShoppingListPricesAsync(IEnumerable<int> ids = null, bool includePrices = false);
		Task<IEnumerable<ShoppingListProductPrice>> GetShoppingListProductPricesAsync(int shoppingListPriceId);
		Task<int> AddShoppingListPriceAsync(int shoppingListId, DateTime shoppingDate
			, IEnumerable<AddShoppingListPriceRequest> prices);
		Task<int> UpdateShoppingListPriceAsync(int shoppingListPriceId, IEnumerable<AddShoppingListPriceRequest> prices);
	}
}
