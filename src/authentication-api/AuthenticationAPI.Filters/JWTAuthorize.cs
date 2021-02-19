using AuthenticationAPI.Core.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Mvp24Hours.Core.DTOs;
using Mvp24Hours.Core.Enums;
using Mvp24Hours.Core.ValueObjects.Logic;
using Mvp24Hours.Infrastructure.Extensions;
using Mvp24Hours.Infrastructure.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace AuthenticationAPI.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class JWTAuthorizeAttribute : ActionFilterAttribute, IAsyncActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            IConfiguration configuration = (IConfiguration)context.HttpContext.RequestServices.GetService(typeof(IConfiguration));

            if (configuration == null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(
                    BusinessResult<VoidResult>.Create(
                    "Project settings not found for authentication filter (OAuth2).".ToMessageResult("internalservererror", MessageType.Error)
                ));
                return;
            }

            string authValidadeUrl = configuration.GetSection("JWTAuthorize:ValidateUrl")?.Value;
            string authByPass = configuration.GetSection("JWTAuthorize:ByPass")?.Value;

            if (authByPass?.ToLower() == "true" || string.IsNullOrEmpty(authValidadeUrl))
            {
                await next();
                return;
            }

            bool isAuthorized = true;

            string authKey = context.HttpContext.Request
                .Headers["Authorization"].SingleOrDefault();

            if (string.IsNullOrWhiteSpace(authKey))
                authKey = "Bearer " + context.HttpContext.Request.Query["Token"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(authKey))
                isAuthorized = false;

            if (isAuthorized)
            {
                Hashtable header = new Hashtable();
                header.Add("Authorization", authKey);

                var response = await WebRequestHelper.GetAsync($"{authValidadeUrl}", header);

                if (!string.IsNullOrEmpty(response))
                {
                    try
                    {
                        var result = JsonConvert.DeserializeObject<LoginResult>(response);

                        if (isAuthorized && string.IsNullOrEmpty(result.UserName))
                        {
                            isAuthorized = false;
                        }
                        else
                        {
                            var claims = new List<Claim>();
                            foreach (var role in result.Claims ?? new List<ClaimResult>())
                            {
                                claims.Add(new Claim(role.Type, role.Value));
                            }
                            ClaimsIdentity identity = new ClaimsIdentity(claims);
                            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                            Thread.CurrentPrincipal = principal;
                        }
                    }
                    catch (Exception)
                    {
                        isAuthorized = false;
                    }
                }
                else
                {
                    isAuthorized = false;
                }
            }

            if (isAuthorized)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new JsonResult(
                    BusinessResult<VoidResult>.Create(
                    "Provide a valid access token (OAuth2).".ToMessageResult("unauthorized", MessageType.Error)
                ));
            }
        }
    }
}
