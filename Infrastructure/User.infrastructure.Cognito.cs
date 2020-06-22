using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Amazon.Runtime.Internal.Auth;
using Domain;
using Domain.Adapter;
namespace Infrastructure.Adapters

{
    public class User_Infrstructure_Cognito : AuthAdapter
    {
        private readonly AmazonCognitoIdentityProviderClient _client;
        private readonly string _clientId;
        private readonly string _poolId;
        private readonly string _poolSecret;
        private readonly string _region;
        private readonly CognitoUserPool  _userpool;

        public User_Infrstructure_Cognito(string clientId, string poolId, string poolSecret, string region)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _poolId = poolId ?? throw new ArgumentNullException(nameof(poolId));
            _poolSecret = poolSecret ?? throw new ArgumentNullException(nameof(poolSecret));
            _region = region ?? throw new ArgumentNullException(nameof(region));
            _client = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.CACentral1);
            _userpool = new CognitoUserPool(_poolId, _clientId, _client);

        }

        public CognitoUser getCurrentUser( string username)
        {
        
            return new Amazon.Extensions.CognitoAuthentication.CognitoUser(username, _clientId, _userpool, _client);
        }
        public async Task<SignUpResponse> SignUp(IUserCredential userCredentialImp)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Password = userCredentialImp.Password,
                Username = userCredentialImp.Username
            };
            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = userCredentialImp.Email
            };
            var nicknameAttribute = new AttributeType
            {
                Name = "nickname",
                Value = userCredentialImp.Username
            };

            signUpRequest.UserAttributes.Add(nicknameAttribute);
            signUpRequest.UserAttributes.Add(emailAttribute);
            return await _client.SignUpAsync(signUpRequest);
        }


        public async Task<ChangePasswordResponse> ChangePasswordAsync(
            IChangePasswordCredential changePasswordCredentialImp,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new ChangePasswordRequest();
            request.AccessToken = changePasswordCredentialImp.AccessToken;
            request.PreviousPassword = changePasswordCredentialImp.PreviousPassword;
            request.ProposedPassword = changePasswordCredentialImp.NewPassword;
            return await _client.ChangePasswordAsync(request, cancellationToken);
        }

        public async Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(
            IConfirmForgotPasswordCredentials confirmForgotPasswordCredentialsImp,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var confirmForgotPasswordRequest = new ConfirmForgotPasswordRequest();
            confirmForgotPasswordRequest.Username = confirmForgotPasswordCredentialsImp.UserName;
            confirmForgotPasswordRequest.ClientId = _clientId;
            confirmForgotPasswordRequest.Password = confirmForgotPasswordCredentialsImp.Password;
            confirmForgotPasswordRequest.ConfirmationCode = confirmForgotPasswordCredentialsImp.ConfirmationCode;

            return await _client.ConfirmForgotPasswordAsync(confirmForgotPasswordRequest, cancellationToken);
        }

        public async void ConfirmSignUpAsync(IConfirmSignupCredential confirmSignupCredentialImp)
        {
        

           await getCurrentUser(confirmSignupCredentialImp.Username)
                .ConfirmSignUpAsync(confirmSignupCredentialImp.ConfirmationCode, true);

        }

        public async void DeleteUserAsync(string username)
        {
           await getCurrentUser(username).DeleteUserAsync();
        }


        public async void ForgotPasswordAsync(string username)
        {

            var user = getCurrentUser(username);
      await user.ForgotPasswordAsync();
        }

        public async Task<InitiateAuthResponse> InitiateAuthAsync(ISigninCredential signinCredentialImp,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new InitiateAuthRequest
            {
                ClientId = _clientId, AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", signinCredentialImp.Username},
                    {"PASSWORD", signinCredentialImp.Password}
                },

                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH
            };
            return await _client.InitiateAuthAsync(request, cancellationToken);
        }


        public async void ResendConfirmationCodeAsync(string username)
        {
            await getCurrentUser(username).ResendConfirmationCodeAsync();
        }
    }
}