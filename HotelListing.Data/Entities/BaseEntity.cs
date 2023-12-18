using System.Reflection;

namespace HotelListing.API.Data.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Defines the property-infos for the last-changed properties
        /// </summary>
        private protected readonly (PropertyInfo LastChangedPropertyInfo, PropertyInfo LastChangedByPropertyInfo) LastChangedPropertyInfos;
    }

    public abstract class BaseEntity<TEntity> : BaseEntity where TEntity : BaseEntity<TEntity>, new()

    {

    }
}
