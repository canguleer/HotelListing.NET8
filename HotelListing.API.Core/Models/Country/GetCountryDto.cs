using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Core.Models.Country
{
    public class GetCountryDto : BaseCountryDto, IBaseDto
    {
        public Guid Id { get; set; }
        public string? LastChangedBy { get; set; }
        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
    }
}
