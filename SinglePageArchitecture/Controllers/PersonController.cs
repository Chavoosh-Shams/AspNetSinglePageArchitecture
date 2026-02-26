using Microsoft.AspNetCore.Mvc;
using SinglePageArchitecture.ApplicationServices.Services.Contracts;

namespace SinglePageArchitecture.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonApplicationService _personApplicationService;

        public PersonController(IPersonApplicationService personApplicationService)
        {
            _personApplicationService = personApplicationService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _personApplicationService.GetAllAsync();
            if (!result.IsSuccessful)
                return StatusCode((int)result.Status, result);
            return Ok(result);
        }

    }
}
