using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("Reservation")]
    public partial class Reservation
    {

        #region [ Expressions ]

        /// <summary>
        /// Expression to get the entity's primary-key
        /// </summary>
        public static readonly Expression<Func<Reservation, Guid>> PrimaryKeyExpression = r => r.Id;

        #endregion

        #region [ Properties ]

        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(HotelId))]
        public int HotelId { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        [StringLength(1000)]
        public string? AdditionalInfo { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion

        #region [ Navigations ]

        [ForeignKey("HotelId")]
        [InverseProperty("Reservation")]
        public virtual Hotel Hotel { get; set; } = null!;

        #endregion
    }
}
