namespace HotelListing.API.Core.Models.Hotel
{
    public abstract class BaseDto
    {
        public string LastChangedBy { get; set; } = "ZZZ";
        public DateTime LastChanged { get; set; }= DateTime.UtcNow;
    }
    
}
