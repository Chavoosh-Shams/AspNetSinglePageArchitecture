using Microsoft.Extensions.Hosting;
using SinglePageArchitecture.ApplicationServices.Dtos.PersonDtos;
using SinglePageArchitecture.ApplicationServices.Services.Contracts;
using SinglePageArchitecture.Frameworks;
using SinglePageArchitecture.Frameworks.ResponseFrameworks;
using SinglePageArchitecture.Frameworks.ResponseFrameworks.Contracts;
using SinglePageArchitecture.Models.DomainModels.PersonAggregates;
using SinglePageArchitecture.Models.Services.Contracts;
using System.Net;

namespace SinglePageArchitecture.ApplicationServices.Services
{
    public class PersonApplicationService : IPersonApplicationService
    {

        #region [- Private Fields -]
        private readonly IPersonRepository _personRepository;
        #endregion


        #region [- Ctor() -]
        public PersonApplicationService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion


        #region [- PostAsync() -]
        public async Task<IResponse<PostPersonDto>> PostAsync(PostPersonDto post)
        {
            if(post == null)
            {
                return new Response<PostPersonDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            try
            {
                var person = new Person()
                {
                    FirstName = post.FirstName,
                    LastName = post.LastName,
                };
                var result = await _personRepository.InsertAsync( person );
                if (!result.IsSuccessful)
                {
                    return new Response<PostPersonDto>(
                        false,
                        result.Status,
                        result.Message,
                        null );
                }
                return new Response<PostPersonDto>(
                    true,
                    HttpStatusCode.Created,
                    ResponseMessages.SuccessfullOperation,
                    post );
            }
            catch (Exception)
            {
                return new Response<PostPersonDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null );
            }
        }
        #endregion


        #region [- PutAsync() -]
        public async Task<IResponse<PutPersonDto>> PutAsync(PutPersonDto put)
        {
            if (put == null)
            {
                return new Response<PutPersonDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            try
            {
                var person = new Person()
                {
                    Id = put.Id,
                    FirstName = put.FirstName,
                    LastName = put.LastName,
                };
                var result = await _personRepository.UpdateAsync(person);
                if (!result.IsSuccessful)
                {
                    return new Response<PutPersonDto>(
                        false,
                        result.Status,
                        result.Message,
                        null );
                }
                return new Response<PutPersonDto>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    put );
            }
            catch (Exception)
            {
                return new Response<PutPersonDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }

        }
        #endregion


        #region [- DeleteAsync() -]
        public async Task<IResponse<DeletePersonDto>> DeleteAsync(DeletePersonDto delete)
        {
            if (delete == null)
            {
                return new Response<DeletePersonDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            try
            {
                var person = new Person()
                {
                    Id = delete.Id,
                    FirstName = delete.FirstName,
                    LastName = delete.LastName,
                };
                var result = await _personRepository.DeleteAsync(person);
                if (!result.IsSuccessful)
                {
                    return new Response<DeletePersonDto>(
                        false,
                        result.Status,
                        result.Message,
                        null);
                }
                return new Response<DeletePersonDto>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    delete);
            }
            catch (Exception)
            {
                return new Response<DeletePersonDto>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }

        }
        #endregion


        #region [- GetAsync() -]
        public async Task<IResponse<GetPersonDto>> GetAsync(GetPersonDto GetPersonDto)
        {
            if (GetPersonDto == null)
            {
                return new Response<GetPersonDto>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.Error,
                    null
                    );
            }
            var person = new Person()
            {
                Id = GetPersonDto.Id,
            };
            var personDto = await _personRepository.SelectAsync(person);
            if (!personDto.IsSuccessful || personDto.Value == null)
            {
                return new Response<GetPersonDto>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            var responseDto = new GetPersonDto
            {
                Id = personDto.Value.Id,
                FirstName = personDto.Value.FirstName,
                LastName = personDto.Value.LastName
            };
            return new Response<GetPersonDto>(
               true,
               HttpStatusCode.OK,
               ResponseMessages.SuccessfullOperation,
               responseDto );
        }
        #endregion


        #region [- GetAllAsync() -]
        public async Task<IResponse<List<GetAllPeronDto>>> GetAllAsync()
        {

            var person = await _personRepository.SelectAllAsync();
            if(!person.IsSuccessful|| person.Value == null)
            {
                return new Response<List<GetAllPeronDto>>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null);
            }
            var result = person.Value.Select(person => new GetAllPeronDto()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName
            }).ToList();
            return new Response<List<GetAllPeronDto>>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                result );
        }
        #endregion
    }
}
