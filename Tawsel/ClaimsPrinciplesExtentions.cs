using System.Security.Claims;

namespace Tawsel
{
    public static class ClaimsPrinciplesExtentions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }

}
