using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data.Entities
{
    public class Hotel
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public double Rating { get; set; }

        #region [ Nagivations ]

        public Country? Country { get; set; }

        #endregion
    }
}
