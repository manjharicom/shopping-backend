using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IShoppingListRepository
	{
		Task<int> CreateShoppingListAsync(int superMarketId, string checkListId, string boardId, string name);
		Task<int> UpdateShoppingListAsync(int shoppingListId, string checkListId, string boardId, string name);
		Task DeleteShoppingListAsync(int shoppingListId);
		Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> shoppingListIds = null);

		Task<ShoppingList> GetAsync(int shoppingListId);
		Task<IEnumerable<ShoppingList>> GetAsync(IEnumerable<int> shoppingListIds = null);
	}
}
