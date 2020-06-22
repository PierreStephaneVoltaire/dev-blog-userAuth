using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Amazon.CognitoIdentityProvider.Model;
using Api.utils.swagger;
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

        public ObjectResult InitiateAuth( [Microsoft.AspNetCore.Mvc.FromBody] SigninCredentialDto signinCredential)
        {
            try
            {
                var loginAction= _congnitoInstance.InitiateAuthAsync(new SigninCredentialImp(signinCredential.Username,signinCredential.Password));
                Response.Cookies.Append(
                    "accessToken",
                    loginAction.Result.AuthenticationResult.AccessToken,
                    new Microsoft.AspNetCore.Http.CookieOptions()
                    {
                        Path = "/",
                        Domain = "despairdrivendevelopment.net",
                        IsEssential = true,
                        Expires = new DateTimeOffset( DateTime.Now.AddSeconds(loginAction.Result.AuthenticationResult.ExpiresIn))
                        
                    }
                );
                Response.StatusCode = (int) HttpStatusCode.NoContent;
                return StatusCode((int) HttpStatusCode.NoContent,null);
            }
            catch (AggregateException e)
            {
                return StatusCode((int) HttpStatusCode.Unauthorized, "Invalid credentials");
            }
          
        
            
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("forgotpassword")]
       
        public async Task<OkObjectResult> ForgotPassword([Required]string userName)

        {
          _congnitoInstance.ForgotPasswordAsync(
                userName);
         return Ok("password reset successfully we will send you an email with your confirmation code if the account exists");
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("confirmforgotpassword")]
        public async Task<ActionResult> ConfirmForgotPassword(string ConfirmationCode, string password,
            string userName)
        {
            try
            {
                await _congnitoInstance.ConfirmForgotPasswordAsync(new ConfirmForgotPasswordCredentialsImp(password,
                    ConfirmationCode,
                    userName));
                return Ok("password reset");
            }
            catch (ExpiredCodeException e)
            {
                return Unauthorized("the confirmation code and/or username is invalid");
            }
          
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("changepassword")]
        public Task<ChangePasswordResponse> ChangePassword(ChangePasswordCredentialDto changePasswordCredentials)
        {
            
            return _congnitoInstance.ChangePasswordAsync(new ChangePasswordCredentialImp(changePasswordCredentials.AccessToken, changePasswordCredentials.PreviousPassword,
                changePasswordCredentials.NewPassword));
        }
    }
}