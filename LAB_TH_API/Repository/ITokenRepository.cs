using Microsoft.AspNetCore.Identity;

namespace LAB_TH_API.Repository
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
