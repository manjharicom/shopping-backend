using System.Collections.Generic;

namespace Services.Models.Trello
{
	public class CreateChecklistItemsRequest
	{
		public string CheckListId { get; set; }
		public bool DeleteListFirst { get; set; }
		public IEnumerable<CreateChecklistItemRequest> Items { get; set; }
	}
}
