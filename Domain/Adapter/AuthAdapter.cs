using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;

namespace Domain.Adapter
{
    public interface AuthAdapter
    {
        Task<SignUpResponse> SignUp(UserCredential userCredential);

        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordCredentials changePasswordCredentials,
            CancellationToken cancellationToken = new CancellationToken());

        Task<ConfirmForgotPasswordResponse> ConfirmForgotPasswordAsync(ConfirmForgotPassword confirmForgotPassword,
            CancellationToken cancellationToken = new CancellationToken());

        Task<ConfirmSignUpResponse> ConfirmSignUpAsync(ConfirmSignupCredentials confirmSignupCredentials,
            CancellationToken cancellationToken = new CancellationToken());

        Task<DeleteUserResponse> DeleteUserAsync(string AccessToken,
            CancellationToken cancellationToken = new CancellationToken());

        Task<ForgotPasswordResponse> ForgotPasswordAsync(string username,
            CancellationToken cancellationToken = new CancellationToken());

        Task<InitiateAuthResponse> InitiateAuthAsync(SigninCredentials signinCredentials,
            CancellationToken cancellationToken = new CancellationToken());

        Task<ResendConfirmationCodeResponse> ResendConfirmationCodeAsync(string username,
            CancellationToken cancellationToken = new CancellationToken());
    }
}