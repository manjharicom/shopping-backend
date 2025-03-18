using System.Collections.Generic;

namespace Services.Models
{
	public class RecipeModelSmall
	{
		public int RecipeId { get; set; }
		public string Name { get; set; }
		public string CookingInstructions { get; set; }
		public IEnumerable<RecipeMenuProductModel> Products { get; set; }
	}
}
