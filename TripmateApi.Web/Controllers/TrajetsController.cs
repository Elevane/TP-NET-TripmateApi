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
            if(_contextUser == null || _contextUser.Id == null || _contextUser.Email == null) throw new NullReferenceException("Internal User is set null whil activating TrajetsController");
        }

        [Authorize]
        [HttpPost("query")]
        public async Task<IActionResult> GetAll([FromBody] GetAllTrajetQueryDto query)
        {
            Result<List<GetAllTrajetResponseDto>> res = await _service.FindAll(query, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Result res = await _service.Delete(id, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            Result<List<GetAllTrajetResponseDto>> res = await _service.FindAllUser(_contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok(res.Value);
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

        [Authorize]
        [HttpPost("{trajetId}")]
        public async Task<IActionResult> Inscription(InscriptionTrajetRequestDto request)
        {
            Result res = await _service.Inscription(request, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }

        [Authorize]
        [HttpPost("valider/{inscriptionId}")]
        public async Task<IActionResult> ValidationInscription(int inscriptionId)
        {
            Result res = await _service.Validate(inscriptionId, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTrajetRequestDto request)
        {
            Result res = await _service.Update(request, _contextUser.Id);
            if (res.IsFailure)
                return BadRequest(res.Error);
            return Ok();
        }
    }
}
