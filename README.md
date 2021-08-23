# Hello AAD

AAD authentication sample

### Setup

Add App Registration must be created to enable AAD authentication for the app.

Via the Azure Portal:

1. Search for Azure Active Directory
2. Under Manage, select App registrations > New registration
3. Enter a name for the application
4. Set the Redirect URI to be https://localhost:44321/signin-oidc
5. Select Register
6. Under Manage, select Authentication.
7. For Front-channel logout URL, enter https://localhost:44321/signout-oidc.
8. Under Implicit grant and hybrid flows, select ID tokens.
9. Select Save.

Open appsettings.json and change the configuration to be the following:

```json
"Domain": "[Enter the domain of your tenant, e.g. contoso.onmicrosoft.com]",
"ClientId": "Enter_the_Application_Id_here",
"TenantId": "common",
```

Run the application, then you should be prompted for Microsoft Credentials.
