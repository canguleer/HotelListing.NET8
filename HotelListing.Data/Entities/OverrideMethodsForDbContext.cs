using HotelListing.API.Data.Contracts;
using HotelListing.API.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.API.Data.Entities
{
    public partial class HotelListingDbContext
    {
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
    }
}
