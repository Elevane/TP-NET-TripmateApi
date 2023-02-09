using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TripmateApi.Application.Common.Models.Authentification;
using TripmateApi.Application.Common.Options;
using TripmateApi.Application.Services.Authentification.Interfaces;
using TripmateApi.Entities.Entities;
using TripmateApi.Infrastructure.Repositories;

namespace TripmateApi.Application.Services.Authentification
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<AuthSettings> _options;

        public UserService(UserRepository repo, IMapper mapper, IOptions<AuthSettings> options)
        {
            _mapper = mapper;
            _options = options;
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
            LoginResponseDto response = new LoginResponseDto()
            {
                Token = GenerateJwtToken(exist)
            };
            return Result.Success(response);
        }

        private string GenerateJwtToken(User user)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_options.Value.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            tokenDescriptor.Claims.Add("firstname", user.Firstname);
            tokenDescriptor.Claims.Add("lastname", user.Lastname);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
