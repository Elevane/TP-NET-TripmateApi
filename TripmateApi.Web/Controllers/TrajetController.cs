using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TripmateApi.Application.Common.Models.Trajets;
using TripmateApi.Application.Services.Trajets;

namespace TripmateApi.Controllers
{
    public class TrajetController : BaseApiController
    {
        private readonly TrajetService _service;
        
        public TrajetController(TrajetService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Post([FromBody] CreateTrajetRequestDto request)
        {
            Result res = await _service.Create(request);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }
    }
}
