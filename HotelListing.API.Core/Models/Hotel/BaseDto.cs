namespace HotelListing.API.Core.Models.Hotel
{
    public abstract class BaseDto
    {
        public string? LastChangedBy { get; set; } 
        public DateTime LastChanged { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
    
}
