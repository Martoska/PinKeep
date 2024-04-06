using System.Security.Claims;

namespace Course_project.Services
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                //throw new ArgumentNullException(nameof(principal));
                return string.Empty;

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
