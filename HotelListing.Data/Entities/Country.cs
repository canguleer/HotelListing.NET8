using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelListing.API.Data.Contracts;

namespace HotelListing.API.Data.Entities
{
    [Table("Country")]
    public class Country : IAuditableEntity
    {
        #region [ Properties ] 

        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = null!;

        [StringLength(50)]
        public string? ShortName { get; set; }
        public string LastChangedBy { get; set; } = null!;
        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;

        #endregion

        #region [ Navigations ]


        [InverseProperty("Country")]
        public virtual IEnumerable<Hotel> Hotel { get; set; } = new List<Hotel>();

        #endregion

    }
}