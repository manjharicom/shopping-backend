namespace Services.Models.Trello
{
	public class CreateCheckListRequest
	{
		public string idCard { get; set; }
		public string name { get; set; }
		public decimal? pos { get; set; }
	}
}
