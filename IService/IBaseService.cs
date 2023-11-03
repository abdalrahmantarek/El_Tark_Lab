namespace LAB.IService
{
    public interface IBaseService<T>where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T t);
        void Update(T t);
        T Search(int id);
    }
}
