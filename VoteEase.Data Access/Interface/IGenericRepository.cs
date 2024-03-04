namespace VoteEase.Data_Access.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        Task<T> ReadSingle(Guid id);
        Task<IEnumerable<T>> ReadAll();
        Task Create(T entity);
        void Update(T entity);
        Task Delete(Guid id);
        Task<bool> SaveChanges();
    }
}
