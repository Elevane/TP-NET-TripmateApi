using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Services.Authentification.Interfaces;

namespace TripmateApi.Application.Services.Authentification
{
    public class UserService : IUserService
    {
        public Task<Result<LoginResponseDto>> Authenticate(LoginRequestDto model)
        {
            throw new NotImplementedException();
        }
    }
}
