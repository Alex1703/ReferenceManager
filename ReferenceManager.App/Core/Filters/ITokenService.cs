using ReferenceManager.App.Models;

namespace ReferenceManager.App.Core.Filters
{
    public interface ITokenService
    {
        string CreatedToken(Usuario usuario);

        int? ValidateToken(string token);

    }
}
