using HotelListing.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Confirgurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = new Guid("68a16966-4bd7-4d69-8d6f-3464910a829f"),
                    Name = "Jamaica",
                    ShortName = "JM",
                    LastChanged = DateTime.UtcNow,
                    LastChangedBy = "ZZZ"
                },
                new Country
                {
                    Id = new Guid("1228d541-26e2-4b10-9560-29a9c92532b1"),
                    Name = "Bahamas",
                    ShortName = "BS",
                    LastChanged = DateTime.UtcNow,
                    LastChangedBy = "ZZZ"
                },
                new Country
                {
                    Id = new Guid("0b5682f0-8a72-47aa-88c3-7dcbc3dd53c1"),
                    Name = "Cayman Island",
                    ShortName = "CI",
                    LastChanged = DateTime.UtcNow,
                    LastChangedBy = "ZZZ"
                }
            );
        }
    }
}
