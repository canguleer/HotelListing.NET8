namespace HotelListing.API.Core.Models.Hotel
{
    public class HotelDto : BaseHotelDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string? LastChangedBy { get; set; }
        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
