using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.Hotel
{
    public abstract class BaseHotelDto :BaseDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        public double? Rating { get; set; }

        [Required]
        public Guid CountryId { get; set; }

    }
}
