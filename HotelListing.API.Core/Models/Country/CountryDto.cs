using HotelListing.API.Core.Models.Hotel;

namespace HotelListing.API.Core.Models.Country
{
    public class CountryDto : BaseCountryDto, IBaseDto
    {
        public Guid Id { get; set; }

        public IEnumerable<HotelDto> Hotels { get; set; }
    }
}
