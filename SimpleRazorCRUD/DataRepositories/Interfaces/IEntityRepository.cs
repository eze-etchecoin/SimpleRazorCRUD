using SimpleRazorCRUD.EntitiesModels;

namespace SimpleRazorCRUD.DataRepositories.Interfaces
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? GetOne(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
