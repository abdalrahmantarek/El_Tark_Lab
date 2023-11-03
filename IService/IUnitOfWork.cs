
using LAB.Models;
using LAB.Service;

namespace LAB.IService
{
    public interface IUnitOfWork : IDisposable
	{
		public HMISContext _Context { get; }
		public VendorService VendorService { get;  }
		public VendorCatService VendorCat { get;  }
		public ServiceStanderService StandardServices { get; }
		public ServicesPricelistService servicesPricelistService { get; }
		public PatientService PatientService { get; }
		public VendorContactService VendorContactService { get; }
		public ContractService ContractService { get;  }
		public VisitServices VisitServices { get; }
		public RefferedDoctorServices RefferedDoctorServices { get; }
		public VisitServiceServices VisitServiceServices { get;  }
		public PatientInvoiceService PatientInvoiceServices { get; }

		void Save();
    }
}
