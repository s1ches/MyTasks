using System.Security.Claims;
using Notes.Application.Interfaces;

namespace Notes.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id);
        }
    }

    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor) 
        => _httpContextAccessor = httpContextAccessor;
    
    
}