using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
	public class PatientInvoiceService:IBaseService<PatientInvoice>
	{
		private readonly HMISContext _context;
		

		public PatientInvoiceService(HMISContext context  )
		{
			_context= context;
		
		}

		public void Add(PatientInvoice t)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PatientInvoice> GetAll()
		{
			return _context.PatientInvoices.Include(g => g.Visit).ThenInclude(x=>x.Patient).ToList();
		}


		public IEnumerable<PatientInvoice> GetAllByCreationDate()
		{
			return _context.PatientInvoices.Where(h=>h.CreationDate==DateTime.Now).ToList();
		}

		public PatientInvoice GetById(int id)
		{
			return _context.PatientInvoices.FirstOrDefault(t => t.InvoiceId == id);
		}


		public PatientInvoice GetByVisitId(int id)
		{
			return _context.PatientInvoices.Include(v=>v.Visit).ThenInclude(f=>f.Patient).Include(m=>m.Visit.RefererDoctorNavigation).FirstOrDefault(t => t.VisitId == id);
		}

		


		public PatientInvoice Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(PatientInvoice t)
		{
			throw new NotImplementedException();
		}
	}
}
