using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Common.Models.Authentification.interfaces;
using TripmateApi.Application.Common.Models.Trajets;
using TripmateApi.Application.Services.Trajets;
using TripmateApi.Common.Authentification;

namespace TripmateApi.Controllers
{
    public class TrajetsController : BaseApiController
    {
        private readonly TrajetService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IInternalUser _contextUser;
        
        public TrajetsController(TrajetService service, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor= httpContextAccessor;
            _service = service;
            _contextUser = (IInternalUser)_contextAccessor.HttpContext.Items["User"];
            if(_contextUser == null ) throw new NullReferenceException("Internal User is set null whil activating TrajetsController");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTrajetRequestDto request)
        {
            Result res = await _service.Create(request, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }
    }
}
