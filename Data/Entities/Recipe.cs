namespace Data.Entities
{
	public class Recipe
	{
		public int RecipeId { get;set;}
		public string Name { get; set; }
		public string CookingInstructions { get; set; }
		public string RecipeProductsJson { get; set; }
	}
}
