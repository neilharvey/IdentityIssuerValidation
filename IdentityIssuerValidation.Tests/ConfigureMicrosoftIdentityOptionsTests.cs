using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IdentityIssuerValidation.Tests
{
    public class ConfigureMicrosoftIdentityOptionsTests
    {
        [Fact]
        public void Configure_SetsValidatorAsIssuerValidator()
        {
            var mock = new Mock<IIssuerTokenValidator>();
            var configurer = new ConfigureMicrosoftIdentityOptions(mock.Object);

            var options = new MicrosoftIdentityOptions();
            options.TokenValidationParameters = new TokenValidationParameters();
            configurer.Configure(options);

            options.TokenValidationParameters.IssuerValidator(
                issuer: "issuer",
                securityToken: null,
                validationParameters: null);

            mock.Verify(x => x.Validate("issuer"), Times.Once);
        }
    }
}
