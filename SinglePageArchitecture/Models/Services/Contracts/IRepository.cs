using SinglePageArchitecture.Frameworks.ResponseFrameworks.Contracts;

namespace SinglePageArchitecture.Models.Services.Contracts
{
    public interface IRepository<T> where T : class
    {

        Task<IResponse<T>> InsertAsync(T obj);

        Task<IResponse<T>> UpdateAsync(T obj);

        Task<IResponse<T>> DeleteAsync(T obj);

        Task<IResponse<T>> SelectAsync(T obj);

        Task<IResponse<IEnumerable<T>>> SelectAllAsync();

    }
}
