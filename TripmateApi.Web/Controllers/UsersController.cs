﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Services.Authentification.Interfaces;

namespace TripmateApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserService _service;

        public UsersController(IUserService service) => _service = service;


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {   
            Result<LoginRegsiterResponseDto> result = await _service.Authenticate(request);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            Result<LoginRegsiterResponseDto> result = await _service.Register(request);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}
