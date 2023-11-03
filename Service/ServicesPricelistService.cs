using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
	public class ServicesPricelistService : IBaseService<ServicesPricelist>
	{
		HMISContext context;

		public ServicesPricelistService(HMISContext _context)
		{
			context = _context;
		}
		public void Add(ServicesPricelist t)
		{
			context.Add(t);
		}

		public IEnumerable<ServicesPricelist> GetAll()
		{
			return context.ServicesPricelists.ToList();
		}
		public IEnumerable<ServicesPricelist> GetAllByContractId(int id) 
		{
			return context.ServicesPricelists.Where(x=>x.ContractId == id).ToList();
		}

		public ServicesPricelist GetById(int id)
		{
			return context.Set<ServicesPricelist>().Find(id);
		}

		public ServicesPricelist Search(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(ServicesPricelist t)
		{
			throw new NotImplementedException();
		}

	}
}
