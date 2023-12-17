using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("Hotel")]
    public partial class Hotel
    {
        #region [ Properties ]

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(CountryId))]
        public Guid CountryId { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        [StringLength(1000)]
        public string? AdditionalInfo { get; set; }

        public double Rating { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion

        #region [ Navigations ]

        [ForeignKey("CountryId")]
        [InverseProperty("Hotel")]
        public virtual Country Country { get; set; } = null!;

        #endregion
    }
}
