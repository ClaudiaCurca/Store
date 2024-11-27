using Store.Data;

namespace Store.Services
{
    public interface IServices<T>
    {
        public T Create(T value);
        public bool Update(int id, T value);
        public T GetById(int id);
        public List<T> GetAll();
        public bool DeleteById(int id);
    }
}
