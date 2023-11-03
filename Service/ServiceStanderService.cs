using LAB.IService;
using LAB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
    public class ServiceStanderService : IBaseService<StandardService>
    {
        private HMISContext _context;

        public ServiceStanderService(HMISContext context)
        {
            _context = context;
        }

        public void Add(StandardService t)
        {
            _context.Add(t);    
        }
        public IEnumerable<StandardService> GetAll()
        {
            
            
            
               return _context.StandardServices.Include(l=>l.ServicesPricelists).Include(c=>c.ServiceAlphaCodeNavigation).AsSplitQuery().ToList();
        }

	

		public StandardService GetById(int id)
        {
            return _context.StandardServices.Include(x => x.Packages).FirstOrDefault(x => x.ServiceId == id);
        }

        public StandardService Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(StandardService t)
        {
            throw new NotImplementedException();
        }
    }
}
