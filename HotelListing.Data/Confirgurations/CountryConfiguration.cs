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
                    Id = new Guid(),
                    Name = "Jamaica",
                    ShortName = "JM"
                },
                new Country
                {
                    Id = new Guid(),
                    Name = "Bahamas",
                    ShortName = "BS"
                },
                new Country
                {
                    Id = new Guid(),
                    Name = "Cayman Island",
                    ShortName = "CI"
                }
            );
        }
    }
}
