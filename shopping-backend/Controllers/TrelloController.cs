using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models.Trello;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrelloController : ControllerBase
    {
        private readonly ITrelloQueryService _trelloQueryService;
        private readonly ITrelloCommandService _trelloCommandService;

        public TrelloController(ITrelloQueryService trelloQueryService, ITrelloCommandService trelloCommandService)
        {
            _trelloQueryService = trelloQueryService;
            _trelloCommandService = trelloCommandService;
        }

        [HttpGet]
        [Route("GetBoards")]
        public async Task<ActionResult> GetBoardsListAsync()
        {
            var list = await _trelloQueryService.GetBoardsListAsync();
            return Ok(list);
        }

		[HttpGet]
		[Route("GetLists")]
		public async Task<ActionResult> GetListsInABoardAsync([FromQuery] string boardId)
		{
			var list = await _trelloQueryService.GetListsInABoardAsync(boardId);
			return Ok(list);
		}

		[HttpGet]
		[Route("GetCards")]
		public async Task<ActionResult> GetCardsInAListAsync([FromQuery] string listId)
		{
			var list = await _trelloQueryService.GetCardsInAListAsync(listId);
			return Ok(list);
		}

		[HttpGet]
        [Route("GetBoardChecklists")]
        public async Task<ActionResult> GetBoardChecklistsAsync([FromQuery] string boardId)
        {
            var list = await _trelloQueryService.GetChecklistsInABoardAsync(boardId);
            return Ok(list);
        }

		[HttpGet]
		[Route("GetCardChecklists")]
		public async Task<ActionResult> GetChecklistsInACardAsync([FromQuery] string cardId)
		{
			var list = await _trelloQueryService.GetChecklistsInACardAsync(cardId);
			return Ok(list);
		}

		[HttpPost]
		[Route("CreateCheckList")]
		public async Task<ActionResult> CreateCheckListAsync([FromBody] CreateCheckListRequest request)
		{
			await _trelloCommandService.CreateCheckListAsync(request);
			return Ok();
		}

		[HttpPost]
		[Route("CreateCheckListItems/{shoppingListId}/{deleteListFirst}")]
		public async Task<ActionResult> CreateCheckListItemsAsync(int shoppingListId, bool deleteListFirst)
		{
			await _trelloCommandService.CreateCheckListItemsAsync(shoppingListId, deleteListFirst);
			return Ok();
		}

		[HttpPut]
		[Route("{checkListId}")]
		public async Task<ActionResult> UpdateCheckListAsync(string checkListId, UpdateCheckListRequest request)
		{
			await _trelloCommandService.UpdateCheckListAsync(checkListId, request);
			return Ok();
		}






		#region unsued
		//[HttpGet]
		//[Route("GetBoard")]
		//public async Task<ActionResult> GetBoardAsync([FromQuery] string boardId)
		//{
		//	var board = await _trelloQueryService.GetBoardAsync(boardId);
		//	return Ok(board);
		//}

		//[HttpGet]
		//[Route("GetBoardCards")]
		//public async Task<ActionResult> GetBoardCardsAsync([FromQuery] string boardId)
		//{
		//	var list = await _trelloQueryService.GetCardsInABoardAsync(boardId);
		//	return Ok(list);
		//}

		//[HttpGet]
		//[Route("GetCard")]
		//public async Task<ActionResult> GetCardAsync([FromQuery] string cardId)
		//{
		//	var card = await _trelloQueryService.GetCardAsync(cardId);
		//	return Ok(card);
		//}

		//[HttpGet]
		//[Route("GetChecklist")]
		//public async Task<ActionResult> GetChecklistAsync([FromQuery] string checkListId)
		//{
		//	var checkList = await _trelloQueryService.GetChecklistAsync(checkListId);
		//	return Ok(checkList);
		//}

		//[HttpGet]
		//[Route("GetChecklistItems")]
		//public async Task<ActionResult> GetChecklistItemsAsync([FromQuery] string checkListId)
		//{
		//	var checkList = await _trelloQueryService.GetChecklistItemsAsync(checkListId);
		//	return Ok(checkList);
		//}

		//[HttpPost]
		//[Route("CreateCheckListItems")]
		//public async Task<ActionResult> CreateCheckListItemsAsync([FromBody] CreateChecklistItemsRequest request)
		//{
		//	await _trelloCommandService.CreateCheckListItemsAsync(request);
		//	return Ok();
		//}

		//[HttpDelete]
		//[Route("DeleteCheckListItems")]
		//public async Task<ActionResult> DeleteCheckListItemsAsync(string checkListItemId)
		//{
		//	await _trelloCommandService.DeleteCheckListItemsAsync(checkListItemId);
		//	return Ok();
		//}
		#endregion
	}
}
