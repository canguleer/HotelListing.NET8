namespace HotelListing.API.Core.Models.Country
{
    public class UpdateCountryDto : BaseCountryDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
