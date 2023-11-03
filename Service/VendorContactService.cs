using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;
/*
 Summary
-dont make chang by (Abdelfatah)

 
 
 
 
 */

namespace LAB.Service
{
    public class VendorContactService : IBaseService<VendorContact>
    {
        private readonly HMISContext _context;

        public VendorContactService(HMISContext context) {
            _context = context;
        
        }
        public void Add(VendorContact t)
        {
            _context.Add(t);
            _context.SaveChanges();
        }

        public IEnumerable<VendorContact> GetAll()
        {
            return _context.VendorContacts.Include(v => v.ContactVendor).AsSplitQuery().ToList();
        }


        public IEnumerable<VendorContact> GetAllbyVendorid(int id)
        {
            return _context.VendorContacts.Where(x=>x.ContactVendorId==id).AsSplitQuery().ToList();
        }

        public VendorContact GetById(int id)
        {
            return _context.VendorContacts.Include(v => v.ContactVendor).AsSplitQuery().FirstOrDefault(c=>c.ContactId==id);
        }


		public VendorContact Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(VendorContact t)
        {
            _context.VendorContacts.Update(t);
            _context.SaveChanges();
        }
        
    }
}
