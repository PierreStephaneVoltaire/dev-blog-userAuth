using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;

namespace Domain.Adapter
{
    public interface AuthAdapter
    {
        CognitoUser getCurrentUser( string username);
        Task<SignUpResponse> SignUp(IUserCredential userCredentialImp);

        Task<ChangePasswordResponse> ChangePasswordAsync(
            IChangePasswordCredential changePasswordCredentialImp,
            CancellationToken cancellationToken = new CancellationToken());

        Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(
            IConfirmForgotPasswordCredentials confirmForgotPasswordCredentialsImp,
            CancellationToken cancellationToken = new CancellationToken());

        void ConfirmSignUpAsync(IConfirmSignupCredential confirmSignupCredentialImp);
        void DeleteUserAsync(string username);
        void ForgotPasswordAsync(string username);

        Task<InitiateAuthResponse> InitiateAuthAsync(ISigninCredential signinCredentialImp,
            CancellationToken cancellationToken = new CancellationToken());

        void ResendConfirmationCodeAsync(string username);
    }
}