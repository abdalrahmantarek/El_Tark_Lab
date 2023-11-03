using LAB.Models;

namespace LAB.ViewModel
{
	public class ServicesPricelistViewModel
	{
		public int ServiceId { get; set; }

		public string? ServiceName { get; set; }

		public string? ServiceNameAr { get; set; }

		public double ServicePrice { get; set; }

		public int ServiceCat { get; set; }

		public int ContractId { get; set; }

		public int? StandRef { get; set; }

		public string LastUpdateBy { get; set; } = null!;

		public string LastUpdateFrom { get; set; } = null!;

		public DateTime LastUpdateDate { get; set; }
		public List<StandardService> standardServices { get; set; } = new List<StandardService>();
		public List<ServicesCategory> categories { get; set; } = new List<ServicesCategory>();

	}
}
