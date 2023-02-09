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

        public async Task<Result<LoginRegsiterResponseDto>> Authenticate(LoginRequestDto model)
        {
            string hashedPassword = generateHashedPassword(model.Password);
            User exist = await _repo.FindOne(user => user.Email == model.Email && user.Password == hashedPassword);
            if (exist == null)
                return Result.Failure<LoginRegsiterResponseDto>("Aucun utilisateur n'a été trouvé avec cet email & ce mot de passe");
            LoginRegsiterResponseDto response = new LoginRegsiterResponseDto()
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
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email.ToString()), new Claim("email", user.Lastname.ToString()), new Claim("email", user.Firstname.ToString())  }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Result<LoginRegsiterResponseDto>> Register(RegisterRequestDto model)
        {
            if(model.Password != model.ConfirmPasword)
                return Result.Failure<LoginRegsiterResponseDto>("Les deux mails ne correspondent pas");
            User exist = await _repo.FindOne(user => user.Email == model.Email);
            if (exist != null)
                return Result.Failure<LoginRegsiterResponseDto>("Cet email est déjà utilisé");
            User toCreate = _mapper.Map<User>(model);
            toCreate.Password = generateHashedPassword(model.Password);
            await _repo.Create(toCreate);
            LoginRegsiterResponseDto response = new LoginRegsiterResponseDto()
            {
                Token = GenerateJwtToken(toCreate)
            };
            return Result.Success(response);
        }
    }
}
