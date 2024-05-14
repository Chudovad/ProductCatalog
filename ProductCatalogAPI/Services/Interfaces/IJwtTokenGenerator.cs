using ProductCatalogAPI.Models;

namespace ProductCatalogAPI.Service.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
