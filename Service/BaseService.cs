using LAB.IService;
using LAB.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB.Service
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected HMISContext _Context;


        public BaseService(HMISContext Context)
        {
            _Context = Context;
        }

        public void Add(T t)
        {
            _Context.Add(t);
            _Context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
           return _Context.Set<T>().ToList();
          
        }

        public T GetById(int id)
        {
            return _Context.Set<T>().Find(id);
        }

        public T Search(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T t)
        {
            _Context.Update(t);
        }
    }
}
