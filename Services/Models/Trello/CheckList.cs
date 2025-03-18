using System.Collections.Generic;

namespace Services.Models.Trello
{
    public class CheckList : CheckListNoItems
	{
        public IEnumerable<CheckListItem> CheckItems { get; set; }
    }
}
