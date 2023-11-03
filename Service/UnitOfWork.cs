using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposed = false;

        #region Prop

        public HMISContext _Context { get; private set; }
        public VendorService VendorService { get; private set; }
        public VendorCatService VendorCat {get; private set;}
        public ServiceStanderService StandardServices { get; private set; }
		public ServicesPricelistService servicesPricelistService { get; private set; }
		public PatientService PatientService { get; private set; }
        public VendorContactService VendorContactService { get; private set; }
        public ContractService ContractService { get; private set; }
		public VisitServices VisitServices { get; private set; }
        public RefferedDoctorServices RefferedDoctorServices { get; private set; }
        public VisitServiceServices VisitServiceServices { get; private set; }
        public PatientInvoiceService PatientInvoiceServices { get; private set; }
		#endregion




		#region ctor
		public UnitOfWork(HMISContext context)
        {
            _Context = context;
            VendorService = new VendorService(context);
			VendorCat=new VendorCatService(context);
            StandardServices = new ServiceStanderService (context);
            servicesPricelistService = new(context);
			PatientService = new(context);
            VendorContactService = new VendorContactService(context);
			ContractService = new(context);
            VisitServices = new(context);
            RefferedDoctorServices = new(context);
			VisitServiceServices =new(context);
            PatientInvoiceServices = new(context);

		}
        #endregion



        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _Context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
