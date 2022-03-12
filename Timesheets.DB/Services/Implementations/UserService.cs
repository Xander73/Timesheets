using Core.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Timesheets.DB.DAL.Context;
using Timesheets.DB.DAL.Interfaces;
using Timesheets.DB.Services.Interfaces;

namespace Timesheets.DB.Services.Implementations
{
    public sealed class UserService : IUserService
    {
        MyDbContext _dbContext;
        IUserRepo _repo;

        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba di da ba dee da ba di da d ba dee da ba di da ba dee";


        public UserService(IUserRepo repo)
        {
            _repo = repo;
        }


        public TokenResponse Authenticate(string user, string password, CancellationToken tokenCancellation)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            TokenResponse tokenResponse = new TokenResponse();
            int i = 0;
            foreach (User item in _repo.GetAll(tokenCancellation).Result)
            {
                i++;
                if (string.CompareOrdinal(item.FirstName, user) == 0 && string.CompareOrdinal(item.Password, password) == 0)
                {
                    tokenResponse.Token = GenerateJwtToken(i, 1);
                    RefreshToken refreshToken = GenerateRefreshToken(i);
                    item.RefreshToken = refreshToken.Token;
                    item.TimeExpires = refreshToken.Expires.Ticks;
                    _repo.UpdateItem(item, tokenCancellation);
                    tokenResponse.RefreshToken = refreshToken.Token;
                    return tokenResponse;
                }
            }
            return null;
        }


        public string RefreshToken(string token, CancellationToken tokenCancellation)
        {
            int i = 0;
            foreach (User item in _repo.GetAll(tokenCancellation).Result)
            {
                i++;
                if (string.CompareOrdinal(item.RefreshToken, token) == 0
                    && DateTime.UtcNow.Ticks >= item.TimeExpires is false)
                {
                    RefreshToken refreshToken = GenerateRefreshToken(i);
                    item.RefreshToken = refreshToken.Token;
                    item.TimeExpires = refreshToken.Expires.Ticks;
                    _repo.UpdateItem(item, tokenCancellation);
                    return item.RefreshToken;
                }
            }
            return string.Empty;
        }


        private string GenerateJwtToken(int id, int minutes)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public RefreshToken GenerateRefreshToken(int id)
        {
            RefreshToken refreshToken = new RefreshToken();
            refreshToken.Expires = DateTime.Now.AddMinutes(360);
            refreshToken.Token = GenerateJwtToken(id, 360);
            return refreshToken;
        }
    }
}
