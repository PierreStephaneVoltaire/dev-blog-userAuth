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
    public class AuthController : ControllerBase
    {
        private readonly AuthAdapter _congnitoInstance;


        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthAdapter cognitoInstance, ILogger<AuthController> logger)

        {
            _congnitoInstance = cognitoInstance;

            _logger = logger;
        }

        [HttpPost]
        public Task<InitiateAuthResponse> InitiateAuth(string password, string userName = null, string email = null)
        {
            return _congnitoInstance.InitiateAuthAsync(new SigninCredentials(userName, password, email));
        }

        [HttpPost("forgotpassword")]
        public Task<ForgotPasswordResponse> ForgotPassword(string userName)
        {
            return _congnitoInstance.ForgotPasswordAsync(userName);
        }

        [HttpPost("confirmforgotpassword")]
        public Task<ConfirmForgotPasswordResponse> ConfirmForgotPassword(string ConfirmationCode, string password,
            string userName)
        {
            return _congnitoInstance.ConfirmForgotPasswordAsync(new ConfirmForgotPassword(password, ConfirmationCode,
                userName));
        }

        [HttpPost("changepassword")]
        public Task<ChangePasswordResponse> ChangePassword(string newPassword, string accessToken,
            string previousPassword)
        {
            return _congnitoInstance.ChangePasswordAsync(new ChangePasswordCredentials(accessToken, previousPassword,
                newPassword));
        }
    }
}