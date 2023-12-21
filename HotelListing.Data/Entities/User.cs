﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelListing.API.Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    [Table("User")]
    public partial class User : IdentityUser<Guid>, IAuditableEntity
    {
        #region [ Properties ]

        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [StringLength(100)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        [Unicode(false)]
        public string LastChangedBy { get; set; } = null!;

        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;

        #endregion

    }
}
