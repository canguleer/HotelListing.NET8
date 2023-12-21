using HotelListing.API.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HotelListing.API.Data.Extensions
{
    /// <summary>
    /// Class holding the extensions for the <see cref="HttpContext"/> type
    /// </summary>
    public static class HttpContextExtensions
    {
        /// <summary>
        /// Retrieves the authenticated claim user information 
        /// </summary>
        public static ClaimUserInfo GetClaimsUser(this HttpContext? httpContext)
        {
            // get the authenticated user over http context
            var user = httpContext?.User;

            if (!(bool)user?.Identity?.IsAuthenticated)
                return new ClaimUserInfo();

            return new ClaimUserInfo
            {
                UserName = user?.FindFirstValue(ClaimTypes.NameIdentifier),
                Role = user?.FindFirstValue(ClaimTypes.Role),
                UserId = user?.Claims.First(c => c.Type == "uid").Value
            };
        }
    }

    /// <summary>
    /// Get or sets the claim user information
    /// </summary>
    public class ClaimUserInfo
    {

        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
    }
}
