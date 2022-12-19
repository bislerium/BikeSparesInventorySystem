using BikeSparesInventorySystem.Shared;

namespace BikeSparesInventorySystem.Pages;

public partial class Dashboard
{
	protected override void OnInitialized()
	{
        MainLayout.Title = "Dashboard";
		foreach (var group in ActivityLogRepository.GetAll().GroupBy(x => x.SpareID).ToList())
		{
			var l = SpareRepository.Get(x => x.Id, group.Key).Name;
			var d = group.Where(x => !x.ApprovedBy.Equals(Guid.Empty)).Sum(x => x.Quantity);
		}
	}
}
