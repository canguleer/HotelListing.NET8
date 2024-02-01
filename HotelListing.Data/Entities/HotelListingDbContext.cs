using HotelListing.API.Data.Confirgurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    public partial class HotelListingDbContext : IdentityDbContext<User, Role, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
        {
            _contextAccessor = contextAccessor;
        }
        public virtual DbSet<Hotel> Hotel { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_hotel");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.CountryId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.LastChangedOn).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.LastChangedBy).UseCollation("Latin1_General_CI_AS");
                entity.Property(e => e.Name).UseCollation("Latin1_General_CI_AI");

                entity.HasOne(c => c.Country).WithMany(h => h.Hotel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hotel_country");

            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(c => c.Id).HasName("PK_country");

                entity.Property(c => c.Id).HasDefaultValueSql("(newid())");
                entity.Property(c => c.Name).UseCollation("Latin1_General_CI_AI");
                entity.Property(c => c.ShortName).UseCollation("Latin1_General_CS_AI");

                entity.HasMany(c => c.Hotel).WithOne(h => h.Country).HasForeignKey(h => h.CountryId).IsRequired();
            });

            
            modelBuilder.Entity<IdentityUserClaim<Guid>>(b =>
            {
                b.ToTable("UserClaim");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(b =>
            {
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(b =>
            {
                b.ToTable("UserToken");
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(b =>
            {
                b.ToTable("RoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>(b =>
            {
                b.ToTable("UserRole");
            });

        }
    }
}
