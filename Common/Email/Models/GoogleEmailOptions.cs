namespace Common.Email.Models;

public class GoogleEmailOptions : EmailOptions
{
    public readonly string CertificatePassword;
    public readonly string[] Scopes;
    public readonly string Certificate;
    public readonly string ServiceAccountEmail;

    public GoogleEmailOptions(string certificate, string certificatePassword, string[] scopes,string serviceAccountEmail)
    {
        Certificate = certificate;
        CertificatePassword = certificatePassword;
        Scopes = scopes;
        ServiceAccountEmail = serviceAccountEmail;
    }
}