using LAB.IService;
using LAB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
    public class VendorService : IBaseService<Vendor>
    {
        private readonly HMISContext _context;

        public VendorService(HMISContext context) 
        {
			_context = context;
        }

      

		public Vendor GetById(int id)
		{
			return _context.Vendors.Include(x => x.VendorCatNavigation).Include(x=>x.VendorContracts).SingleOrDefault(x => x.VendorId == id);
		}

		public void Add(Vendor t)
		{
			_context.Add(t);
			_context.SaveChanges();
		}

		public void Update(Vendor t)
		{
			_context.Vendors.Update(t);
			_context.SaveChanges();
		}

		public Vendor Search(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Vendor> GetAll()
		{
			return _context.Vendors.Include(x=>x.VendorContracts).ThenInclude(x => x.ServicesPricelists).Include(x=>x.VendorCatNavigation).ToList();
		}

		public List<Vendor> GlobalSearch(string name) 
		{
			var vendors = _context.Vendors
				.Where(v => v.VendorName.Contains(name) || v.OfficialEmail.Contains(name))
				.ToList();
			return vendors;
		}
		public List<Vendor> GetLab2LabVendor() 
		{
			return _context.Vendors.Where(x => x.VendorCat == 9).ToList();
		}
	}
}

