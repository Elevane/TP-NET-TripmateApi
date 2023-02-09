using Microsoft.AspNetCore.Mvc;
using TripmateApi.Common;
using TripmateApi.Entities.Entities;

namespace TripmateApi.Controllers
{
    public class UsersController : BaseApiController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            User user = new User();
            return Ok();
        }
    }
}
