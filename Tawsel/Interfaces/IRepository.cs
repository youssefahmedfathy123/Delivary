namespace Tawsel.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<string> Add(T record);
        string Edit(T newRecord);
        Task<string> Delete(T record);
        Task<List<T>> GetAll();
        Task<bool> Save();
        
    }
}


