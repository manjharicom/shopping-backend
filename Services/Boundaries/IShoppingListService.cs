using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
    public interface IShoppingListService
    {
        Task<AddUpdateResponse> CreateShoppingListAsync(CreateShoppingListRequest request);
        Task<AddUpdateResponse> UpdateShoppingListAsync(UpdateShoppingListRequest request);
        Task DeleteShoppingListAsync(int shoppingListId);
        Task DeleteShoppingListItemsAsync(int shoppingListId, string shoppingListIds = null);

        Task<ShoppingListModel> GetAsync(int shoppingListId);
        Task<IEnumerable<ShoppingListModel>> GetAsync(string shoppingListIds = null);
    }
}
