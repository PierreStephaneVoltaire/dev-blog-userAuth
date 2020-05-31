using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
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

        public User_Infrstructure_Cognito(string clientId, string poolId, string poolSecret, string region)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _poolId = poolId ?? throw new ArgumentNullException(nameof(poolId));
            _poolSecret = poolSecret ?? throw new ArgumentNullException(nameof(poolSecret));
            _region = region ?? throw new ArgumentNullException(nameof(region));

            _client = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.CACentral1);
        }


        public async Task<SignUpResponse> SignUp(UserCredential userCredential)
        {
            var signUpRequest = new SignUpRequest
            {
                ClientId = _clientId,
                Password = userCredential.Password,
                Username = userCredential.Username
            };
            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = userCredential.Email
            };
            var nicknameAttribute = new AttributeType
            {
                Name = "nickname",
                Value = userCredential.Username
            };

            signUpRequest.UserAttributes.Add(nicknameAttribute);
            signUpRequest.UserAttributes.Add(emailAttribute);
            return await _client.SignUpAsync(signUpRequest);
        }


        public async Task<ChangePasswordResponse> ChangePasswordAsync(
            ChangePasswordCredentials changePasswordCredentials,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new ChangePasswordRequest();
            request.AccessToken = changePasswordCredentials.AccessToken;
            request.PreviousPassword = changePasswordCredentials.PreviousPassword;
            request.ProposedPassword = changePasswordCredentials.NewPassword;
            return await _client.ChangePasswordAsync(request, cancellationToken);
        }

        public async Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(
            ConfirmForgotPassword confirmForgotPassword,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var confirmForgotPasswordRequest = new ConfirmForgotPasswordRequest();
            confirmForgotPasswordRequest.Username = confirmForgotPassword.UserName;
            confirmForgotPasswordRequest.ClientId = _clientId;
            confirmForgotPasswordRequest.Password = confirmForgotPassword.Password;
            confirmForgotPasswordRequest.ConfirmationCode = confirmForgotPassword.ConfirmationCode;

            return await _client.ConfirmForgotPasswordAsync(confirmForgotPasswordRequest, cancellationToken);
        }

        public async Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmSignupCredentials confirmSignupCredentials,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new ConfirmSignUpRequest
            {
                Username = confirmSignupCredentials.Username,
                ClientId = _clientId,
                ConfirmationCode = confirmSignupCredentials.ConfirmationCode
            };


            return await _client.ConfirmSignUpAsync(request, cancellationToken);
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(string AccessToken,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new DeleteUserRequest {AccessToken = AccessToken};
            return await _client.DeleteUserAsync(request, cancellationToken);
        }


        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(string username,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new ForgotPasswordRequest
            {
                ClientId = _clientId,
                Username = username
            };
            return await _client.ForgotPasswordAsync(request, cancellationToken);
        }

        public async Task<InitiateAuthResponse> InitiateAuthAsync(SigninCredentials signinCredentials,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new InitiateAuthRequest
            {
                ClientId = _clientId, AuthParameters = new Dictionary<string, string>
                {
                    {"USERNAME", signinCredentials.Username},
                    {"PASSWORD", signinCredentials.Password}
                },

                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH
            };
            return await _client.InitiateAuthAsync(request, cancellationToken);
        }


        public async Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(string username,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var request = new ResendConfirmationCodeRequest
            {
                Username = username,
                ClientId = _clientId
            };
            {
            }
            ;
            return await _client.ResendConfirmationCodeAsync(request, cancellationToken);
        }
    }
}