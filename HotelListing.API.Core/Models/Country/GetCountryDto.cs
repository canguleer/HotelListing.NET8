using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Core.Models.Country
{
    public class GetCountryDto : BaseCountryDto, IBaseDto
    {
        public Guid Id { get; set; }
    }
}
