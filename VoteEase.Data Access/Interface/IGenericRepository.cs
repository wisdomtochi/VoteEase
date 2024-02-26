namespace VoteEase.Data_Access.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> ReadSingle(Guid id);
        Task<IEnumerable<T>> ReadAll();
        Task Create(T entity);
        void Update(T entity);
        Task Delete(Guid id);
    }
}
