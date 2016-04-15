# Azure Authentication (Authenticating any Azure API request in your application)

This is a code sample to showcase how you can authenticate your request programatically with Azure. This also contains **a reusable Authentication Helper class** which you can directly use in your code.

### The authentication methods available are:
 - Authenticating by Prompting for Credentials from end user. (This needs end user interaction)
 - Authenticating by Credentials i.e. using a password. (This does not need any end user interaction)
 - Authenticating by using a Certificate ( This also does not need any end user interaction)

I have provided this functionality in a separate class file along with it's interface.
Download and feel free to use the AuthenticationHelper class directly in your projects. Please keept the credits in the file.

**Pre-Requisites**: Follow the corresponding steps from URL: https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/

####Below are the details regarding the implementation

**Note:** You will need only one of these three methods in your project to authenticate any kind of request to Azure.

 - **Method 1 - Authenticating by Prompting for Credentials from end user** (needs user interaction)

This method fetches the Azure Authentication Token From Azure Active Directory by Prompting the end user for authentication credentials. This is the only authentication method which requires manual user intervention. For this to work follow the instructions in this link and create a Service Principal before executing this method: [Create Active Directory application and service principal using portal](https://azure.microsoft.com/en-us/documentation/articles/resource-group-create-service-principal-portal/)

 - **Method 2 - Authenticating by Credentials** (fully automated mechanism)
 
This method fetches the Azure Authentication Token From Azure Active Directory using credentials. For this method to work follow the section "Authenticate with password - PowerShell" from the below URL: [Authenticating a service principal with Azure Resource Manager](https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/)

 - **Method 3 - Authenticating by using a Certificate** (also fully automated mechanism and no password)

This method fetches the Azure Authentication Token From Azure Active Directory using a Certificate. For this method to work follow the section "Authenticate with certificate - PowerShell" from the below URL: [Authenticating a service principal with Azure Resource Manager](https://azure.microsoft.com/en-us/documentation/articles/resource-group-authenticate-service-principal/)

Check the comments for each method in the AuthenticationHelper.cs file, to read on details regarding inputs and outputs for each of these methods.
