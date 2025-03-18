using System.Collections.Generic;

namespace Services.Models
{
	public class Ingredient
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IEnumerable<RecipeMenuProductModel> Products { get; set; }
	}
}
