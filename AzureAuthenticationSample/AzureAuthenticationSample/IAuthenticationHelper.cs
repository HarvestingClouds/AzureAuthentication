// <copyright file="IAuthenticationHelper.cs">
// Copyright (c) 2016 All Rights Reserved
// </copyright>
// <author>Aman Sharma (@20aman)</author>
// <date>04/14/2016</date>
// <summary>Class with methods for Authenticating the Azure API requests</summary>
// <website>http://HarvestingClouds.com</website>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureAuthenticationSample
{
    interface IAuthenticationHelper
    {
        string GetOAuthTokenFromAAD_ByPrompting(string ADALServiceURL, string TenantDomain, string ARMBillingServiceURL, string ClientID, string ADALRedirectURL);

        string GetOAuthTokenFromAAD_ByCredentials(string TenanatID, string ClientID, string UserName, string Password);

        string GetOAuthTokenFromAAD_ByCertificate(string TenanatID, string ClientID, string CertificateName);
    }
}
