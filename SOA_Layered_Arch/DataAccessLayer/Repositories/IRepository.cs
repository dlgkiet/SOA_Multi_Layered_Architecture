using SOA_Layered_Arch.CoreLayer.Entities;


namespace SOA_Layered_Arch.DataAccessLayer.Repositories
{
    // Interface chung cho tất cả các repository
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);

        // Sửa lại GetTopRatedMoviesWithSpAsync để nhận int và trả về IEnumerable<Movie>
        Task<IEnumerable<Movie>> GetTopRatedMoviesWithSpAsync(int topCount);
    }
}