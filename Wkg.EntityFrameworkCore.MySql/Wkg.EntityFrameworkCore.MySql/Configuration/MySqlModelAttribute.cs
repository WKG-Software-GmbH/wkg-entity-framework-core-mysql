using Wkg.EntityFrameworkCore.Configuration.Reflection.Attributes;
using Wkg.EntityFrameworkCore.Extensions;

namespace Wkg.EntityFrameworkCore.MySql.Configuration;

/// <summary>
/// Indicates that the decorated class should be loaded by the MySQL configuration loader, if multiple configuration loaders are used by the application.
/// </summary>
/// <remarks>
/// The attribute on its own does not do anything. It must be passed to the <see cref="ModelBuilderExtensions.LoadReflectiveModels{TDatabaseEngineModelAttribute}(Microsoft.EntityFrameworkCore.ModelBuilder, Wkg.EntityFrameworkCore.Configuration.Reflection.Policies.NamingPolicies.INamingPolicy?, Wkg.EntityFrameworkCore.Configuration.Reflection.Policies.MappingPolicies.IMappingPolicy?)"/> method to be effective.
/// </remarks>
public sealed class MySqlModelAttribute : DatabaseEngineModelAttribute
{
}
