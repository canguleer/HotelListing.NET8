using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelListing.API.Data.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("Hotel")]
    public partial class Hotel: IAuditableEntity
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

        [StringLength(100)]
        [Unicode(false)]
        public string? LastChangedBy { get; set; }

        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedBy { get; set; }

        #endregion

        #region [ Navigations ]

        [ForeignKey("CountryId")]
        [InverseProperty("Hotel")]
        public virtual Country Country { get; set; } = null!;

        #endregion
    }
}
