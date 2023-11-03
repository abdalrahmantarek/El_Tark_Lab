using LAB.Models;
using LAB.Service;

namespace LAB.ViewModel
{
	public class PatientInvoiceViewModel
	{
		public List<VisitService> VisitServices { get; set; }= new List<VisitService>();	

		public int Copayment { get; set; }



	}
}
