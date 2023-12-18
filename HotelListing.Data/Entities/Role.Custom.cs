using System.ComponentModel;

namespace HotelListing.API.Data.Entities
{
    public partial class Role
    {
        #region [ Enums ]

        /// <summary>
        /// Defines the role's name
        /// </summary>
        [Flags]
        public enum RoleName
        {
            /// <summary>
            /// Indicates that the role is an admin of system
            /// </summary>
            [Description("Admin")]
            Admin = 1 << 0,

            /// <summary>
            /// Indicates that the role is an user 
            /// </summary>
            [Description("User")]
            User = 1 << 1
        }

        #endregion
    }
}
