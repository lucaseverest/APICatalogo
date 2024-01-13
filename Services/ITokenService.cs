using APICatalogo.Models;

namespace APICatalogo.Services
{
    public interface ITokenService
    {
        string GerarToken(string key, string issuer, string audience, UserModel user);

    }
}
