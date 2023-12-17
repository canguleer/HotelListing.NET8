using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing.API.Data.Entities
{
    [Table("Country")]
    public class Country
    {
        #region [ Properties ] 

        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? ShortName { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion

        #region [ Navigations ]


        [InverseProperty("Country")]
        public virtual IEnumerable<Hotel> Hotel { get; set; } = new List<Hotel>();

        #endregion


    }
}