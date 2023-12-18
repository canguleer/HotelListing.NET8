namespace HotelListing.API.Data.Entities
{
    public partial class Hotel
    {
        #region [ Enums ]

        [Flags]
        public enum Status
        {
            Pending = 1 << 0,

            Confirmed = 1 << 0,

            Reserved = 1 << 0,
        }

        #endregion
    }
}
