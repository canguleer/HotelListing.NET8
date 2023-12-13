using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("User")]
    public partial class User
    {
        #region [ Properties ]

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        [Column(TypeName = "datetime")]

        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion

        #region [ Navigations ]

        [ForeignKey("Id")]
        [InverseProperty("Hotel")]
        public virtual Country Country { get; set; } = null!;

        #endregion
    }
}
