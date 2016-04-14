// <copyright file="AuthenticationHelper.cs">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Aman Sharma (@20aman)</author>
// <date>04/14/2016</date>
// <summary>Class with methods for Authenticating the Azure API requests</summary>
// <website>http://HarvestingClouds.com</website>
//Pre-Requisites: Follow the corresponding steps from URL: https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationSample
{
    public class AuthenticationHelper : IAuthenticationHelper
    {
        /// <summary>
        /// Method 1 - Fetches the Azure Authentication Token From Azure Active Directory by Prompting the end user for authentication credentials
        /// Note: This is the only authentication method which requires manual user intervention
        /// This same method is provided with the samples like Azure Billing Usage and RateCard samples
        /// </summary>
        /// <param name="ADALServiceURL">Service root URL for ADAL authentication service WITH NO TRAILING SLASH!</param>
        /// <param name="TenantDomain">DNS name for your Azure AD tenant</param>
        /// <param name="ARMBillingServiceURL">Service root URL for ARM/Billing service WITH NO TRAILING SLASH!</param>
        /// <param name="ClientID">GUID for AAD application configured as Native Client App in AAD tenant specified above</param>
        /// <param name="ADALRedirectURL">Redirect URL for ADAL authentication service MUST MATCH YOUR AAD APP CONFIGURATION!</param>
        /// <returns>Authentication Token</returns>
        public string GetOAuthTokenFromAAD_ByPrompting(string ADALServiceURL, string TenantDomain, string ARMBillingServiceURL, string ClientID, string ADALRedirectURL)
        {
            //Creating the Authentication Context
            var authenticationContext = new AuthenticationContext(String.Format("{0}/{1}", ADALServiceURL, TenantDomain));

            //Ask the logged in user to authenticate, so that this client app can get a token on his behalf
            var result = authenticationContext.AcquireToken(String.Format("{0}/", ARMBillingServiceURL), ClientID,new Uri(ADALRedirectURL));

            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            return result.AccessToken;
        }

        /// <summary>
        /// Method 2 - Fetches the Azure Authentication Token From Azure Active Directory using credentials
        /// Note: For this method to work follow the section "Authenticate with password - PowerShell" from the below URL
        /// https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/
        /// </summary>
        /// <param name="TenanatID">Tenanat ID from your Azure Subscription</param>
        /// <param name="ClientID">GUID for AAD application configured as Native Client App in AAD tenant specified above</param>
        /// <param name="UserName">User Name</param>
        /// <param name="Password">Password</param>
        /// <returns>Authentication Token</returns>
        public string GetOAuthTokenFromAAD_ByCredentials(string TenanatID, string ClientID, string UserName, string Password)
        {
            //Creating the variable for result
            string token = string.Empty;

            //Creating the Authentication Context
            var authenticationContext = new AuthenticationContext("https://login.windows.net/" + TenanatID);
            //Creating Credentials
            var credential = new ClientCredential(clientId: ClientID, clientSecret: Password);
            //Fetching Token from Azure AD
            var result = authenticationContext.AcquireToken(resource: "https://management.core.windows.net/", clientCredential: credential);

            //Checking if data recieved from Azure AD
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            //Getting token
            token = result.AccessToken;

            //Returning the token
            return token;
        }

        /// <summary>
        /// Method 3 - Fetches the Azure Authentication Token From Azure Active Directory using a Certificate
        /// Note: For this method to work follow the section "Authenticate with certificate - PowerShell" from the below URL
        /// https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/
        /// </summary>
        /// <param name="TenanatID">Tenanat ID from your Azure Subscription</param>
        /// <param name="ClientID">GUID for AAD application configured as Native Client App in AAD tenant specified above</param>
        /// <param name="CertificateName">Name of certificate. This should be in your local user store on the computer where this tool is run.</param>
        /// <returns>Authentication Token</returns>
        public string GetOAuthTokenFromAAD_ByCertificate(string TenanatID, string ClientID, string CertificateName)
        {
            //Creating the Authentication Context
            var authContext = new AuthenticationContext(string.Format("https://login.windows.net/{0}", TenanatID));

            //Creating the certificate object. This will be used to authenticate
            X509Certificate2 cert = null;

            //The Certificate should be already installed in personal store of the current user under 
            //the context of which the application is running.
            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                //Trying to open and fetch the certificate
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates;
                var certs = certCollection.Find(X509FindType.FindBySubjectName, CertificateName, false);
                //Checking if certificate found
                if (certs == null || certs.Count <= 0)
                {
                    //Throwing error if certificate not found
                    throw new Exception("Certificate " + CertificateName + " not found.");
                }
                cert = certs[0];
            }
            finally
            {
                //Closing the certificate store
                store.Close();
            }

            //Creating Client Assertion Certificate object
            var certCred = new ClientAssertionCertificate(ClientID, cert);

            //Fetching the actual token for authentication of every request from Azure using the certificate
            var token = authContext.AcquireToken("https://management.core.windows.net/", certCred);

            //Optional steps if you need more than just a token from Azure AD
            //var creds = new TokenCloudCredentials(subscriptionId, token.AccessToken);
            //var client = new ResourceManagementClient(creds); 

            //Returning the token
            return token.AccessToken;
        }
    }
}
