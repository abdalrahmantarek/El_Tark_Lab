using LAB.Models;

namespace LAB.ViewModel
{
	public class SPLSelectListViewModel
	{
		public List<ServicesPricelist> ServicesPricelists { get; set; } = new();
		public List<VisitserviceViewModel> VisitserviceViewModel { get; set; } = new();
		public List<Vendor> L2LVendor { get; set; } = new();

	}
}
