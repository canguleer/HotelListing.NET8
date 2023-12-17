using HotelListing.API.Data.Confirgurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    public class HotelListingDbContext : IdentityDbContext<ApiUser>
    {
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Country> Country { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());

            ///////////////////////////////////////////////////////////////////
            //CaseSensitivity
            //CI specifies case -insensitive, CS specifies case -sensitive.

            //AccentSensitivity
            //AI specifies accent-insensitive, AS specifies accent - sensitive.
            ///////////////////////////////////////////////////////////////////

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_country");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Name).UseCollation("Latin1_General_CI_AI");
                entity.Property(e => e.ShortName).UseCollation("Latin1_General_CS_AI");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_hotel");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.CountryId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.LastChanged).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.LastChangedBy).UseCollation("Latin1_General_CI_AS");
                entity.Property(e => e.Name).UseCollation("Latin1_General_CI_AI");

                entity.HasOne(c => c.Country).WithMany(h => h.Hotel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hotel_country");
            });


        }
    }
}
