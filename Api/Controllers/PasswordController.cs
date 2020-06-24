using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Domain;
using Domain.Adapter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : Controller
    {
        private readonly AuthAdapter _congnitoInstance;


        private readonly ILogger<AuthController> _logger;
        
        public PasswordController(AuthAdapter cognitoInstance, ILogger<AuthController> logger)

        {
            _congnitoInstance = cognitoInstance;

            _logger = logger;
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