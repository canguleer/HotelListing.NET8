using HotelListing.API.Data.Confirgurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data.Contracts;
using HotelListing.API.Data.Extensions;

namespace HotelListing.API.Data.Entities
{
    public class HotelListingDbContext : IdentityDbContext<User, Role, Guid,
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get all the entities that inherit from AuditableEntity
            // and have a state of Added or Modified
            var entries = ChangeTracker
                .Entries()
                .Where(e => e is { Entity: IAuditableEntity, State: EntityState.Added or EntityState.Modified }).ToList();

            // get the authenticated user's claim information
            var userClaim = _contextAccessor.HttpContext.GetClaimsUser();
            
            // For each entity we will set the Audit properties
            foreach (var entityEntry in entries)
            {
                // If the entity state is Added let's set
                // the CreatedAt and CreatedBy properties
                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditableEntity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                    ((IAuditableEntity)entityEntry.Entity).CreatedBy = userClaim.UserName ?? "System Admin";
                }
                else
                {
                    // If the state is Modified then we don't want
                    // to modify the CreatedAt and CreatedBy properties
                    // so we set their state as IsModified to false
                    Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedOn).IsModified = false;
                    Entry((IAuditableEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
                }

                // In any case we always want to set the properties
                // ModifiedAt and ModifiedBy
                ((IAuditableEntity)entityEntry.Entity).LastChangedOn = DateTime.UtcNow;
                ((IAuditableEntity)entityEntry.Entity).LastChangedBy = userClaim.UserName ?? "System Admin";
            }

            // After we set all the needed properties
            // we call the base implementation of SaveChangesAsync
            // to actually save our entities in the database
            return await base.SaveChangesAsync(cancellationToken);
        }

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
