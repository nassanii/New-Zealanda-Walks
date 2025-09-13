using Microsoft.AspNetCore.Identity;

namespace NZwalks.API.Repository.IRepository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
