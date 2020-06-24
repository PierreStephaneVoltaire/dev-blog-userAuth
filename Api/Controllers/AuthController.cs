using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Amazon.CognitoIdentityProvider.Model;
using Domain;
using Domain.Adapter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthAdapter _congnitoInstance;


        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthAdapter cognitoInstance, ILogger<AuthController> logger)

        {
            _congnitoInstance = cognitoInstance;

            _logger = logger;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]

        public ObjectResult InitiateAuth([Microsoft.AspNetCore.Mvc.FromBody] SigninCredentialDto signinCredential)
        {
            try
            {
                var loginAction =
                    _congnitoInstance.InitiateAuthAsync(new SigninCredentialImp(signinCredential.Username,
                        signinCredential.Password));
                Response.Cookies.Append(
                    "accessToken",
                    loginAction.Result.AuthenticationResult.AccessToken,
                    new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        Path = "/",
                        Domain = "despairdrivendevelopment.net",
                        IsEssential = true,
                        Expires = new DateTimeOffset(
                            DateTime.Now.AddSeconds(loginAction.Result.AuthenticationResult.ExpiresIn))

                    }
                );
                Response.StatusCode = (int) HttpStatusCode.NoContent;
                return StatusCode((int) HttpStatusCode.NoContent, null);
            }
            catch (AggregateException e)
            {
                return StatusCode((int) HttpStatusCode.Unauthorized, "Invalid credentials");
            }



        }

    }
}