using System;
using System.Collections.Generic;

namespace Services.Models
{
	public class AddShoppingListPriceRequestModel
	{
		public int ShoppingListId { get; set; }
		public DateTime ShoppingDate { get; set; }
		public IEnumerable<ShoppingListPriceRequest> Prices { get; set; }
	}
}
