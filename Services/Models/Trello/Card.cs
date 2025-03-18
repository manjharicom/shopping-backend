using System.Collections.Generic;

namespace Services.Models.Trello
{
    public class Card
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Pos { get; set; }
        public bool Closed { get; set; }
        public string Desc { get; set; }
        public string DateLastActivity { get; set; }
        public string Due { get; set; }
        public string DueReminder { get; set; }
        public string Email { get; set; }
        public string IdBoard { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
        public string ShortLink { get; set; }
        public bool Subscribed { get; set; }
        public IEnumerable<string> IdChecklists { get; set; }
    }
}
