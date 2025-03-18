using Services.Models.Trello;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
    public interface ITrelloQueryService
    {
        Task<IEnumerable<Board>> GetBoardsListAsync();
        Task<Board> GetBoardAsync(string boardId);


        Task<IEnumerable<List>> GetListsInABoardAsync(string boardId);
		Task<List> GetListAsync(string listId);


		Task<IEnumerable<Card>> GetCardsInABoardAsync(string boardId);
		Task<IEnumerable<Card>> GetCardsInAListAsync(string listId);
		Task<Card> GetCardAsync(string cardId);


        Task<IEnumerable<CheckListNoItems>> GetChecklistsInABoardAsync(string boardId);
		Task<IEnumerable<CheckListNoItems>> GetChecklistsInACardAsync(string cardId);
		Task<CheckList> GetChecklistAsync(string checkListId);
		Task<IEnumerable<CheckListItem>> GetChecklistItemsAsync(string checkListId);

	}
}
