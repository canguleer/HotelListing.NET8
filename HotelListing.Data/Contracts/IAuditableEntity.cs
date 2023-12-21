namespace HotelListing.API.Data.Contracts
{
   public interface IAuditableEntity
    {
        public string LastChangedBy { get; set; }
        public DateTime LastChangedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
