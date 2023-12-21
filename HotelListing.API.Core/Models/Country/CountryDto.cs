using HotelListing.API.Core.Models.Hotel;

namespace HotelListing.API.Core.Models.Country
{
    public class CountryDto : BaseCountryDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string? LastChangedBy { get; set; }
        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }

        public IEnumerable<HotelDto> Hotels { get; set; }
    }
}
