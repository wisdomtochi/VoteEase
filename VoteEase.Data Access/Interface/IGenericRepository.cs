namespace VoteEase.Data_Access.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        Task<T> ReadSingle(Guid id);
        Task<T> ReadSingle(Guid id, Guid Id);
        Task<IEnumerable<T>> ReadAll();
        Task Create(T entity);
        void Update(T entity);
        Task Delete(Guid id);
        Task Delete(Guid id, Guid Id);
        Task<bool> SaveChanges();
    }
}
