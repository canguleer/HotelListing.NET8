using HotelListing.API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Confirgurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role
                {
                    Id = new Guid("158c3c5d-063b-4531-8553-6fc5e5a8c7c2"),
                    Name = nameof(Role.RoleName.Admin),
                    NormalizedName = nameof(Role.RoleName.Admin).ToUpper(),
                    Description = "Indicates that the role is an admin of System",
                    LastChangedOn = DateTime.UtcNow,
                    LastChangedBy = "ZZZ",
                    CreatedBy = "ZZZ",
                    CreatedOn = DateTime.UtcNow
                },
                new Role
                {
                    Id = new Guid("2f13b959-78dc-47b4-b714-7ecbc862e0da"),
                    Name = nameof(Role.RoleName.User),
                    NormalizedName = nameof(Role.RoleName.User).ToUpper(),
                    Description = "Indicates that the role is an user of System",
                    LastChangedOn = DateTime.UtcNow,
                    LastChangedBy = "ZZZ",
                    CreatedBy = "ZZZ",
                    CreatedOn = DateTime.UtcNow
                }
            );
        }
    }
}
