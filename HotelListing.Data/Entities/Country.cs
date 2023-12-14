namespace HotelListing.API.Data.Entities
{
    public class Country
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public virtual IEnumerable<Hotel>? Hotels { get; set; }
    }
}