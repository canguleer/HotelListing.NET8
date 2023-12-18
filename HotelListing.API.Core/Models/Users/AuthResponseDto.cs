namespace HotelListing.API.Core.Models.Users
{
    public class AuthResponseDto
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }
}
