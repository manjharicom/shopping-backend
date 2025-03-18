using AutoMapper;
using Microsoft.Extensions.Options;
using Services.Boundaries;
using Services.Models.Trello;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class TrelloQueryService : ITrelloQueryService
	{
		private readonly ITrelloApiService _trelloApiService;

		public TrelloQueryService(ITrelloApiService trelloApiService)
		{
			_trelloApiService = trelloApiService;
		}

		public async Task<IEnumerable<Board>> GetBoardsListAsync()
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<Board>>("members/me/boards");
			return list.OrderBy(l => l.Name);
		}

		public async Task<Board> GetBoardAsync(string boardId)
		{
			var board = await _trelloApiService.GetDataAsync<Board>($"boards/{boardId}");
			return board;
		}



		public async Task<IEnumerable<List>> GetListsInABoardAsync(string boardId)
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<List>>($"boards/{boardId}/lists");
			return list;
		}

		public async Task<List> GetListAsync(string listId)
		{
			var list = await _trelloApiService.GetDataAsync<List>($"lists/{listId}");
			return list;
		}



		public async Task<IEnumerable<Card>> GetCardsInABoardAsync(string boardId)
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<Card>>($"boards/{boardId}/cards");
			return list;
		}

		public async Task<IEnumerable<Card>> GetCardsInAListAsync(string listId)
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<Card>>($"lists/{listId}/cards");
			return list;
		}

		public async Task<Card> GetCardAsync(string cardId)
		{
			var card = await _trelloApiService.GetDataAsync<Card>($"cards/{cardId}");
			return card;
		}



		public async Task<IEnumerable<CheckListNoItems>> GetChecklistsInABoardAsync(string boardId)
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<CheckListNoItems>>($"boards/{boardId}/checklists");
			return list.OrderBy(l => l.Name);
		}

		public async Task<IEnumerable<CheckListNoItems>> GetChecklistsInACardAsync(string cardId)
		{
			var list = await _trelloApiService.GetDataAsync<IEnumerable<CheckListNoItems>>($"cards/{cardId}/checklists");
			return list.OrderBy(l => l.Name);
		}

		public async Task<CheckList> GetChecklistAsync(string checkListId)
		{
			var checkList = await _trelloApiService.GetDataAsync<CheckList>($"checklists/{checkListId}");
			return checkList;
		}

		public async Task<IEnumerable<CheckListItem>> GetChecklistItemsAsync(string checkListId)
		{
			var items = await _trelloApiService.GetDataAsync<IEnumerable<CheckListItem>>($"checklists/{checkListId}/checkItems");
			return items;
		}
	}
}
