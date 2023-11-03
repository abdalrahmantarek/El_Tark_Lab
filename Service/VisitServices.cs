using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LAB.Service
{
	public class VisitServices : IBaseService<Visit>
	{
		private readonly HMISContext context;

		public VisitServices(HMISContext context)
        {
			this.context = context;
		}
        public void Add(Visit t)
		{
			context.Add(t);
		}

		public IEnumerable<Visit> GetAll()
		{
			var model = context.Visits.Include(v => v.RefererDoctorNavigation).Include(d => d.Patient).ToList();

			return model;
		}

		public Visit GetById(int id)
		{
			Visit model = context.Visits.Include(v => v.RefererDoctorNavigation).Include(d => d.Patient).SingleOrDefault(x => x.VisitId == id);
			return model;
		}
		

		public Visit Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(Visit t)
		{
			context.Update(t);
		}
		
	}
}
