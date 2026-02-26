using SinglePageArchitecture.Frameworks.ResponseFrameworks.Contracts;

namespace SinglePageArchitecture.ApplicationServices.Services.Contracts
{
    public interface IApplicationService<TPost,TPut,TDelete,TGet,TGetAll>
    {
        Task<IResponse<TPost>> PostAsync(TPost post); //Post

        Task<IResponse<TPut>> PutAsync(TPut put); //Put

        Task<IResponse<TDelete>> DeleteAsync(TDelete delete); //Delete

        Task<IResponse<TGet>> GetAsync(TGet get); //Get

        Task<IResponse<List<TGetAll>>> GetAllAsync(); //GetAll

    }
}
