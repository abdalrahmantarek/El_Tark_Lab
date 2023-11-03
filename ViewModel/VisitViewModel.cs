using LAB.Models;

namespace LAB.ViewModel
{
	public class VisitViewModel
	{

		public Visit Visit { get; set; } = new Visit();
		public Patient? Patient { get; set; } = new Patient();
		public List<RefferedDoctor> RefDoctor
		{ get; set; } = new();
		public List<Patient> Patients { get; set; } = new();
		public List<Vendor> Vendors { get; set; } = new();
		public List<ServicesPricelist> ServicesPricelists { get; set; } = new();
		public List<Vendor> L2LVendor { get; set; } = new();
		public List<VisitserviceViewModel> VisitserviceViewModel { get; set; } = new();

	}
}
