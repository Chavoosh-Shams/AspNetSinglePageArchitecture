

using Microsoft.EntityFrameworkCore;
using SinglePageArchitecture.Frameworks;
using SinglePageArchitecture.Frameworks.ResponseFrameworks;
using SinglePageArchitecture.Frameworks.ResponseFrameworks.Contracts;
using SinglePageArchitecture.Models.DomainModels.PersonAggregates;
using SinglePageArchitecture.Models.Services.Contracts;
using System.Net;

namespace SinglePageArchitecture.Models.Services.Repositories
{
    public class PersonRepository : IPersonRepository
    {

        #region [- Private Fields -]
        private readonly ProjectDbContext _context; 
        #endregion


        #region [- Ctor() -]
        public PersonRepository(ProjectDbContext context)
        {
            _context = context;
        }
        #endregion


        #region [- InsertAsync() -]
        public async Task<IResponse<Person>> InsertAsync(Person obj)
        {
            try
            {
                if (obj is null)
                {
                    return new Response<Person>(
                        false,
                        HttpStatusCode.BadRequest,
                        ResponseMessages.NullInput,
                        null
                        );
                }
                await _context.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new Response<Person>(
                    true,
                    HttpStatusCode.Created,
                    ResponseMessages.SuccessfullOperation,
                    obj
                    );
            }
            catch (Exception)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion


        #region [- UpdateAsync() -]
        public async Task<IResponse<Person>> UpdateAsync(Person obj)
        {

            if (obj == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            var existingPerson = await _context.Person.FindAsync(obj.Id);
            if (existingPerson == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.NotFound,
                    ResponseMessages.NullInput,
                    null
                    );
            }
            _context.Entry(existingPerson).CurrentValues.SetValues(obj);
            await _context.SaveChangesAsync();
            return new Response<Person>(
                true,
                HttpStatusCode.OK,
                ResponseMessages.SuccessfullOperation,
                obj
                );
        }
        #endregion


        #region [- DeleteAsync() -]
        public async Task<IResponse<Person>> DeleteAsync(Person obj)
        {
            try { 
                var existingPerson = await _context.Person.FindAsync(obj.Id);
                if (existingPerson == null)
                {
                    return new Response<Person>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NotFound,
                        null );
                }
                _context.Person.Remove(existingPerson);
                await _context.SaveChangesAsync();
                return new Response<Person>(
                    true,
                    HttpStatusCode.NoContent,
                    ResponseMessages.SuccessfullOperation,
                    existingPerson);
            }
            catch(Exception) {
                return new Response<Person>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
                }
            
        }

        #endregion


        #region [- SelectAsync() -]
        public async Task<IResponse<Person>> SelectAsync(Person obj)
        {
            if (obj == null)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.BadRequest,
                    ResponseMessages.NullInput,
                    null
                );
            }
            try
            {
                var person = await _context.Person.FindAsync(obj.Id);
                if(person == null)
                {
                    return new Response<Person>(
                        false,
                        HttpStatusCode.NotFound,
                        ResponseMessages.NotFound,
                        null );
                }
                return new Response<Person>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    person);
            }
            catch (Exception)
            {
                return new Response<Person>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null);
            }
        }
        #endregion


        #region [- SelectAllAsync() -]
        public async Task<IResponse<IEnumerable<Person>>> SelectAllAsync()
        {

            try
            {
                var persons = await _context.Person.AsNoTracking().ToListAsync();
                return new Response<IEnumerable<Person>>(
                    true,
                    HttpStatusCode.OK,
                    ResponseMessages.SuccessfullOperation,
                    persons);
            }
            catch (Exception)
            {
                return new Response<IEnumerable<Person>>(
                    false,
                    HttpStatusCode.InternalServerError,
                    ResponseMessages.Error,
                    null );
            }
        } 
        #endregion


    }
}
