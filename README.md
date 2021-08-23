# Identity Issuer Validation

Extends Azure AD authentication to allow for a list of allowed tenants to be specified.   Based on the [Azure Samples](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/blob/master/1-WebApp-OIDC/1-2-AnyOrg/README-1-1-to-1-2.md#how-to-restrict-users-from-specific-organizations-from-signing-in-your-web-app) documentation.

## Usage

Add an `AllowedTenants` section to your settings.  This can be embedded in the standard AzureAd settings if desired:

```json
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "[Enter the domain of your tenant, e.g. contoso.onmicrosoft.com]",
    "ClientId": "Enter_the_Application_Id_here",
    "TenantId": "common",
    "CallbackPath": "/signin-oidc",
    "AllowedTenants": [
      "GUID1",
      "GUID2"
    ]
  }
```

The GUIDs should be the ids of the tenants authorized to access the application.

After calling `AddMicrosoftIdentityWebApp` in your startup method, chain a call to `AddIssuerValidation`, passing the configuration section which contains the allowed tenants.

```cs
var configurationSection = Configuration.GetSection("AzureAd");
services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(configurationSection)
    .AddIssuerValidation(configurationSection);

```

When an unauthorized tenant attempts to access the application, a `SecurityTokenInvalidIssuerException` will be thrown.  This should be handled by the owning application.
