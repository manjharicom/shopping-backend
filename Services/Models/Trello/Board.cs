namespace Services.Models.Trello
{
    public class Board
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string DescData { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public string ShortLink { get; set; }
        public bool Closed { get; set; }
        public bool? Starred { get; set; }
        public bool? Pinned { get; set; }
        public bool Subscribed { get; set; }
        public string DateLastView { get; set; }
        public string DateLastActivity { get; set; }
    }
}
