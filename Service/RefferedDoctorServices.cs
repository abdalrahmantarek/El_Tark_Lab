using LAB.IService;
using LAB.Models;

namespace LAB.Service
{
	public class RefferedDoctorServices:IBaseService<RefferedDoctor>
	{
		private readonly HMISContext _context;

		public RefferedDoctorServices(HMISContext context) 
		{
			_context=context;
		}

		public void Add(RefferedDoctor t)
		{
			_context.Add(t);
		}

		public IEnumerable<RefferedDoctor> GetAll()
		{
			var model = _context.RefferedDoctors.ToList();

			return model;
		}

		public RefferedDoctor GetById(int id)
		{
			var model = _context.RefferedDoctors.Where(d => d.DrId == id).FirstOrDefault();
			return model;
		}

		public RefferedDoctor Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(RefferedDoctor t)
		{
			_context.Update(t);
			_context.SaveChanges();
		}
	}
}
