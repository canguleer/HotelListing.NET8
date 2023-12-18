﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("User")]
    public partial class User : IdentityUser<Guid>
    {
        #region [ Properties ]

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime LastChanged { get; set; }

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        #endregion

    }
}
