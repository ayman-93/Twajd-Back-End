using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Twajd_Back_End.Core.Services;

namespace Twajd_Back_End.Api.Middleware
{
    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly IAuthService _authService;

        public TokenManagerMiddleware(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await _authService.IsCurrentActiveToken())
            {
                await next(context);

                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
