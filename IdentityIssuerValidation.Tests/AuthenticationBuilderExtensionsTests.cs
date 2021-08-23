using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Xunit;

namespace IdentityIssuerValidation.Tests
{
    public class AuthenticationBuilderExtensionsTests
    {
        [Fact]
        public void AddIssuerValidation_AddWithCorrectLifetime()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder().Build();
            var configurationSection = configuration.GetSection("AzureAD");

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(configurationSection)
                .AddIssuerValidation(configurationSection);

            Assert.Contains(services, x => x.ServiceType == typeof(IIssuerTokenValidator)
                                        && x.ImplementationType == typeof(IssuerTokenValidator)
                                        && x.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(services, x => x.ServiceType == typeof(IConfigureOptions<MicrosoftIdentityOptions>)
                                        && x.ImplementationType == typeof(ConfigureMicrosoftIdentityOptions)
                                        && x.Lifetime == ServiceLifetime.Singleton);
        }
    }
}
