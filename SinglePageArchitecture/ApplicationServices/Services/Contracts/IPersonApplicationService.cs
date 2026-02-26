using SinglePageArchitecture.ApplicationServices.Dtos.PersonDtos;

namespace SinglePageArchitecture.ApplicationServices.Services.Contracts
{
    public interface IPersonApplicationService:
        IApplicationService
        <PostPersonDto, PutPersonDto, DeletePersonDto, GetPersonDto, GetAllPeronDto>
    {

    }
}
