using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Services.Authentification.Interfaces;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Repositories;

namespace TripmateApi.Application.Services.Authentification
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }
        private string generateHashedPassword(string password)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();         
        }

        public async Task<Result<LoginResponseDto>> Authenticate(LoginRequestDto model)
        {
            string hashedPassword = generateHashedPassword(model.Password);
            User exist = await _repo.FindOne(user => user.Email == model.Email && user.Password == hashedPassword);
            if (exist == null)
                return Result.Failure<LoginResponseDto>("Aucun utilisateur n'a été trouvé avec cet email & ce mot de passe");
            return Result.Success(new LoginResponseDto());
        }
    }
}
