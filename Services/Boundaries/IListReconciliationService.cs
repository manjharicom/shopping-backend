using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IListReconciliationService
	{
		Task ReconcileListAsync(int shoppingListId);

	}
}
