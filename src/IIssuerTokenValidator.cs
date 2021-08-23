namespace IdentityIssuerValidation
{
    public interface IIssuerTokenValidator
    {
        string Validate(string issuer);
    }
}
