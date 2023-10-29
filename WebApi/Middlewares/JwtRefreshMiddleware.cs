
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.OtherServices;
using Services.Services.UserServices;
using System.Net;
using System.Security.Claims;

namespace WebApi.Middlewares
{
    public class JwtRefreshMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly ILogger<JwtRefreshMiddleware> _logger;

        public JwtRefreshMiddleware(RequestDelegate next, IUserService userService, ILogger<JwtRefreshMiddleware> logger)
        {
            _next = next;
            _userService = userService;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request Path: {context.Request.Path}");

            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Request.Headers.TryGetValue("Authorization", out var accessKey);

                if (accessKey.ToString().StartsWith("Bearer", StringComparison.OrdinalIgnoreCase))
                {
                    accessKey = accessKey.ToString().Substring("Bearer ".Length);
                }

                bool isAccessTokenExpired = TokenService.IsTokenExpired(accessKey!);

                if (isAccessTokenExpired)
                {

                    var principal = TokenService.ValidateToken(accessKey);

                    var userEmail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;    

                    if (userEmail != null)
                    {                      
                        UserModel? model = _userService.GetUsersByFilter(new UserModel { Email = userEmail }).FirstOrDefault();

                        if (model != null)
                        {
                            string? refreshToken = model!.RefreshToken;

                            bool isRefreshTokenExpired = TokenService.IsTokenExpired(refreshToken!);

                            if (isRefreshTokenExpired)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                await context.Response.WriteAsJsonAsync(new { error = "Refresh token has expired" });
                                return;
                            }

                            string token = TokenService.GenerateAccessToken(model.Id.ToString()!, userEmail);

                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            await context.Response.WriteAsync(token);
                            return;
                        }
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { error = "User not found" });
                    return;
                }
            }
        }
    }
}
