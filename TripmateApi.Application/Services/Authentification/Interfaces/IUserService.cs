using CSharpFunctionalExtensions;
using TripmateApi.Application.Common.Models.Authentification;

namespace TripmateApi.Application.Services.Authentification.Interfaces
{
    public interface IUserService
    {
        Task<Result<LoginRegsiterResponseDto>> Authenticate(LoginRequestDto model);

        Task<Result<LoginRegsiterResponseDto>> Register(RegisterRequestDto model);

        Result<InternalAuthenticatedUserDto> Get(string email);
    }
}
