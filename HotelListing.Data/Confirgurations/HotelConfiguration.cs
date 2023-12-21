using HotelListing.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Confirgurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
                new Hotel
                {
                    Id = new Guid("fff56899-6925-4095-9da9-74a74d79c853"),
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    AdditionalInfo = null,
                    CountryId = new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"),
                    Rating = 4.3,
                    LastChangedBy = "ZZZ",
                    LastChangedOn = DateTime.UtcNow,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = "ZZZ",
            

                });
        }
    }
}
