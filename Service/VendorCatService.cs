using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
	public class VendorCatService : IBaseService<VendorCategory>
	{
        private readonly HMISContext _context;

        public VendorCatService(HMISContext Context)
        {
            _context = Context;
        }

        public void Add(VendorCategory t)
        {
            _context.Add(t);
        }

        public IEnumerable<VendorCategory> GetAll()
        {
           return _context.VendorCategories.ToList();

		}

        public VendorCategory GetById(int id)
        {
            throw new NotImplementedException();
        }

        public VendorCategory Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VendorCategory t)
        {
            throw new NotImplementedException();
        }
    }
}
