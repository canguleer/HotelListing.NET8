namespace HotelListing.API.Core.Models.Hotel
{
    public class HotelDto : BaseHotelDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
