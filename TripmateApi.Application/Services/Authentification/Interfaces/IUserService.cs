using CSharpFunctionalExtensions;
using TripmateApi.Application.Common.Models.Authentification;

namespace TripmateApi.Application.Services.Authentification.Interfaces
{
    public interface IUserService
    {
        Task<Result<LoginResponseDto>> Authenticate(LoginRequestDto model);
    }
}
