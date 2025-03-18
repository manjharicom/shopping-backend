namespace Services.Models.Trello
{
	public class List
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Closed { get; set; }
		public decimal Pos { get; set; }
		public bool? SoftLimit { get; set; }
		public string IdBoard { get; set; }
		public bool Subscribed { get; set; }
		public object Limits { get; set; }
	}
}
