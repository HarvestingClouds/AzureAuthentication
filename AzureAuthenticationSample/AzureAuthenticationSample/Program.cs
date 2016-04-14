using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationSample
{
    //#error Please update the appSettings section in app.config, then remove this statement
    class Program
    {
        //This is a sample console application that shows you how to grab a token from AAD using 3 different methods
        static void Main(string[] args)
        {
            #region Fetching configuration data
            //Configuration Needed for All Methods
            string ClientID = ConfigurationManager.AppSettings["ClientID"];

            //Configuration Needed for Method 1 only
            string ADALServiceURL = ConfigurationManager.AppSettings["ADALServiceURL"];
            string ADALRedirectURL = ConfigurationManager.AppSettings["ADALRedirectURL"];
            string ARMBillingServiceURL = ConfigurationManager.AppSettings["ARMBillingServiceURL"];
            string TenantDomain = ConfigurationManager.AppSettings["TenantDomain"];

            //Configuration Needed for Method 2 and 3
            string TenantID = ConfigurationManager.AppSettings["TenantID"];

            //Configuration Needed for Method 2 only
            string Password = ConfigurationManager.AppSettings["Password"];

            //Configuration Needed for Method 3 only
            string CertificateName = ConfigurationManager.AppSettings["CertificateName"];
            #endregion

            #region Invoking methods and printing the Authentication Token
            //Instantiating the Authentication Helper class
            AuthenticationHelper authHelperObject = new AuthenticationHelper();

            //Method 1 Invocation
            string token1 = string.Empty;
            token1 = authHelperObject.GetOAuthTokenFromAAD_ByPrompting(ADALServiceURL, TenantDomain, ARMBillingServiceURL, ClientID, ADALRedirectURL);
            Console.WriteLine("The Authentication token from first method i.e. by prompting is: ");
            Console.WriteLine(token1);
            Console.WriteLine();

            //Method 2 Invocation
            string token2 = string.Empty;
            token2 = authHelperObject.GetOAuthTokenFromAAD_ByCredentials(TenantID, ClientID, Password);
            Console.WriteLine("The Authentication token from second method i.e. by crednetial is: ");
            Console.WriteLine(token2);
            Console.WriteLine();

            //Method 3 Invocation
            string token3 = string.Empty;
            token3 = authHelperObject.GetOAuthTokenFromAAD_ByCertificate(TenantID, ClientID, CertificateName);
            Console.WriteLine("The Authentication token from third method i.e. by certificate is: ");
            Console.WriteLine(token3);
            Console.WriteLine();

            Console.ReadLine();
            #endregion

        }
    }
}
