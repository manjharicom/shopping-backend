using System;

namespace Data.Entities
{
	public class ShoppingListPrice
	{
		public int ShoppingListPriceId { get; set; }
		public int ShoppingListId { get; set; }
		public DateTime ShoppingDate { get; set; }
		public string PricesJson { get; set; }
	}
}
