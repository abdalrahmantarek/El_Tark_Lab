using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
	public class VisitServiceServices:IBaseService<VisitService>
	{
		private readonly HMISContext context;

		public VisitServiceServices(HMISContext context)
        {
			this.context = context;
		}

		public void Add(VisitService t)
		{
			context.Add(t);
		}

		public IEnumerable<VisitService> GetAll()
		{
			return context.VisitServices.Include(x=>x.Visit).Include(x=>x.Service).Include(x=>x.Lab2labNavigation).ToList();
		}

		public VisitService GetByPK(int serviceId , int serial , int visitId)
		{
			return context.VisitServices.Include(x=>x.Service).FirstOrDefault(x => x.ServiceId == serviceId && x.ServiceSerial == serial && x.VisitId == visitId);	
		}

		public  List<VisitService> GetByServiceIdAndVisitId(int serviceId,int visitId)
		{
			return context.VisitServices.Where(x => x.VisitId == visitId && x.ServiceId == serviceId).ToList();
		}

		public VisitService Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(VisitService t)
		{
			throw new NotImplementedException();
		}



		public List<VisitService> GetServicesByVisitId(int visitId)
		{
			return context.VisitServices.Where(x => x.VisitId == visitId ).Include(s=>s.Service).ToList();
		}

		public VisitService GetById(int id)
		{
			throw new NotImplementedException();
		}
	}
}
