using Services.Models.Trello;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
    public interface ITrelloCommandService
    {
        Task CreateCheckListAsync(CreateCheckListRequest request);
        Task UpdateCheckListAsync(string checkListId, UpdateCheckListRequest request);

		Task CreateCheckListItemsAsync(int shoppingListId, bool deleteListFirst);
		Task DeleteCheckListItemsAsync(string checkListId, IEnumerable<string> checkListItemIds);
	}
}
