using Services.Models;

namespace Services.Helpers
{
	internal static class ChecklistItemNameHelper
	{
		internal static string ChecklistItemName(ProductModel s)
		{
			return $"{s.Name} ({s.AisleLabel}) x {s.Quantity}";
		}
	}
}
