using Microsoft.IdentityModel.Tokens;
using Services.StaticContent;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.OtherServices
{
    public static class TokenService
    {
        public static string GenerateEightCharacterToken()
        {
            string allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random r = new Random();

            return new string(
                Enumerable.Repeat(allChar, 8)
                .Select(token => token[r.Next(token.Length)]).ToArray()).ToString();
        }

        public static bool IsTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Constants.ISSUER,
                ValidateAudience = true,
                ValidAudience = Constants.AUDIENCE,
                ValidateLifetime = true,
                IssuerSigningKey = Constants.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                if (securityToken.ValidTo < DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SecurityTokenExpiredException ex)
            {
                return true;
            }
            catch (System.ArgumentException ex)
            {
                return false;
            }
        }

        public static string GenerateAccessToken(string id, string email)
        {
            Random r = new Random();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,id),
                new Claim(ClaimTypes.Name,email),
                new Claim(ClaimTypes.Email,email),
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: Constants.ISSUER,
                audience: Constants.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
                signingCredentials: new SigningCredentials(Constants.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = Constants.ISSUER,
                ValidateAudience = true,
                ValidAudience = Constants.AUDIENCE,
                ValidateLifetime = false,
                IssuerSigningKey = Constants.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);

                return principal;
            }
            catch (SecurityTokenException ex)
            {
                return null;
            }
        }

        public static string GenerateRefreshToken()
        {
            Random r = new Random();
            var secretKey = Constants.GetSymmetricSecurityKey();

            List<Claim> claims = new List<Claim>
            {
                new Claim("IsRefreshToken","true"),
                new Claim("tokenId",r.Next().ToString())
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: Constants.ISSUER,
                audience: Constants.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(7)),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return refreshToken;
        }
    }
}
