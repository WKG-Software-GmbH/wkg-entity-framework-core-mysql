using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wkg.EntityFrameworkCore.DataTypes.Uuids;

namespace Ntb.Portal.Database.Compatibility.UuidSupport;

/// <summary>
/// Provides extension methods for <see cref="PropertyBuilder{TProperty}"/> to configure UUID properties.
/// </summary>
public static class PropertyBuilderExtensions
{
    /// <summary>
    /// Configures this <see cref="Uuid"/> property to use a <c>binary(16)</c> column type with the required coversions.
    /// </summary>
    /// <param name="propertyBuilder">The property builder to configure.</param>
    /// <returns>The same property builder instance for method chaining.</returns>
    public static PropertyBuilder<Uuid> IsUuid(this PropertyBuilder<Uuid> propertyBuilder) => propertyBuilder
        .HasColumnType("binary(16)")
        .HasConversion(
            // the uuid is a big endian wrapper around mixed endian guid
            uuid => uuid.ToGuid(),
            guid => new Uuid(guid));

    /// <summary>
    /// Configures this <see cref="Uuid"/> property to use a <c>varchar(36)</c> column type with the required coversions.
    /// </summary>
    /// <param name="propertyBuilder">The property builder to configure.</param>
    /// <param name="validateDatabaseValues">Whether to validate the database values when converting, or to parse them without validation.</param>
    /// <returns>The same property builder instance for method chaining.</returns>
    public static PropertyBuilder<Uuid> IsVarcharUuid(this PropertyBuilder<Uuid> propertyBuilder, bool validateDatabaseValues = false)
    {
        propertyBuilder.HasColumnType("varchar(36)");
        if (validateDatabaseValues)
        {
            propertyBuilder.HasConversion(
                uuid => uuid.ToString(),
                s => Uuid.Parse(s));
        }
        else
        {
            propertyBuilder.HasConversion(
                uuid => uuid.ToString(),
                s => Uuid.ParseUnsafe(s));
        }
        return propertyBuilder;
    }
}
