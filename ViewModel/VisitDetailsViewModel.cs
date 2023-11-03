using LAB.Models;

namespace LAB.ViewModel
{
	public class VisitDetailsViewModel
	{
		public Visit Visit { get; set; }

		public List<ServiceStatusViewModel> ServiceStatuses { get; set; } = new List<ServiceStatusViewModel>();
	}
}
