using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Facebook.CoreKit;
using Facebook.LoginKit;
using PetrolPriceMonitor.Constants;
using PetrolPriceMonitor.iOS.Services;
using PetrolPriceMonitor.Models;
using PetrolPriceMonitor.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthenticationService))]
namespace PetrolPriceMonitor.iOS.Services
{
    public class AuthenticationService : IAuthenticate
    {
        private CognitoAWSCredentials _credentials;
        private LoginManager _loginManager;

        private static readonly List<string> ReadPermissions = new List<string> { "public_profile" };

        public bool IsAuthenticated
        {
            get
            {
                return (AccessToken.CurrentAccessToken != null);
            }
        }

        public AuthenticationService()
        {
            _credentials = new CognitoAWSCredentials(
                Authentication.IdentityPoolId,
                RegionEndpoint.USEast1
            );

            _loginManager = new LoginManager();
        }

        public async Task<IResponse> LogInUsingFacebook()
        {
            if (AccessToken.CurrentAccessToken != null)
            {
                return new SuccessResponse();
            }

            try
            {
                var response = await _loginManager.LogInWithReadPermissionsAsync(ReadPermissions.ToArray(), null);

                if (response.IsCancelled)
                {
                    return new ErrorResponse("");
                }
                else if (response.Token == null)
                {
                    return new ErrorResponse("");
                }

                var accessToken = response.Token;
                _credentials.AddLogin("graph.facebook.com", accessToken.TokenString);

                return new SuccessResponse();
            }
            catch (Exception ex)
            {


                return new ErrorResponse(ErrorMessage.UnknownError);
            }
        }

        public async Task<IResponse> LogInAsGuest()
        {



            //throw new NotImplementedException();

            // AmazonCognitoIdentityRequest sfs = new AmazonCognitoIdentityRequest();

            return new SuccessResponse();
            
        }

        public async Task<IResponse> LogIn(string email, string password)
        {
            await System.Threading.Tasks.Task.Delay(10000);

            return new SuccessResponse();
        }

        public async Task<IResponse> SignUp(string email, string password)
        {
            AmazonCognitoIdentityProviderConfig config = new AmazonCognitoIdentityProviderConfig();
            config.RegionEndpoint = RegionEndpoint.USEast1;

            AmazonCognitoIdentityProviderClient client = new AmazonCognitoIdentityProviderClient(_credentials, config);
            
            var request = new SignUpRequest
            {
                ClientId = Authentication.UserPoolClientId,
                Username = email,
                Password = password,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email", Value = email }
                }
            };

            try
            {
                var response = await client.SignUpAsync(request);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new SuccessResponse();
                }
                else
                {
                    return new ErrorResponse(response.HttpStatusCode, "");
                }
            }
            catch (Amazon.CognitoIdentityProvider.Model.InvalidParameterException ex) when (ex.Message.Contains("email"))
            {
                // email validation error
                return new ErrorResponse("");
            }
            catch (Amazon.CognitoIdentityProvider.Model.InvalidParameterException ex) when (ex.Message.Contains("password"))
            {
                // password validation error
                return new ErrorResponse("");
            }
            catch (InvalidPasswordException ex)
            {
                // invalid password error
                return new ErrorResponse("");
            }
            catch (UsernameExistsException ex)
            {
                // username exists error
                return new ErrorResponse("");
            }
            catch (Exception ex)
            {
                // unknown error
                return new ErrorResponse("");
            }
        }

        public void LogOut()
        {
            if (AccessToken.CurrentAccessToken != null)
            {
                _loginManager.LogOut();
            }

            _credentials.Clear();
        }
    }
}
