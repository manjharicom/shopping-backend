using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class ShoppingListModel : IMapFrom<ShoppingList>
	{
		public int ShoppingListId { get; set; }
		public int SuperMarketId { get; set; }
		public string Name { get; set; }
		public string CheckListId { get; set; }
		public string BoardId { get; set; }
	}
}
