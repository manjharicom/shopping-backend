namespace Data.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
		public bool IsShipped { get; set; }
		public string ProductsJson { get; set; }
	}
}
