using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("Role")]
    public partial class Role : IdentityRole<Guid>
    {
        #region [ Properties ]
        public string Description { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion


    }
}
