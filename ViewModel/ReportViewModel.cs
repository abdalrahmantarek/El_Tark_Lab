using LAB.Models;

namespace LAB.ViewModel
{
	public class ReportViewModel
	{
		public ServiceBarcode ServiceBarcode { get; set; }
		public List<RoportResult> RoportResults { get; set; } = new List<RoportResult>();
		public List<NormalRange> NormalRanges { get; set; } = new();

		
	}
}
