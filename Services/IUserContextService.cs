using System.Security.Claims;

namespace SimpleBackendGame.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}