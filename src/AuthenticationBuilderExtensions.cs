using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

namespace IdentityIssuerValidation
{
    public static class AuthenticationBuilderExtensions
    {
        public static MicrosoftIdentityWebAppAuthenticationBuilderWithConfiguration AddIssuerValidation(this MicrosoftIdentityWebAppAuthenticationBuilderWithConfiguration builder, IConfigurationSection configurationSection)
        {
            builder.Services.Configure<IssuerValidationOptions>(configurationSection);
            builder.Services.AddSingleton<IIssuerTokenValidator, IssuerTokenValidator>();
            builder.Services.AddSingleton<IConfigureOptions<MicrosoftIdentityOptions>, ConfigureMicrosoftIdentityOptions>();
            return builder;
        }
    }
}
