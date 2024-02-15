using Wkg.EntityFrameworkCore.Configuration.Reflection.Attributes;
using Wkg.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Policies.NamingPolicies;
using Wkg.EntityFrameworkCore.Configuration.Policies.MappingPolicies;

namespace Wkg.EntityFrameworkCore.MySql.Configuration;

/// <summary>
/// Indicates that the decorated class should be loaded by the MySQL configuration loader, if multiple configuration loaders are used by the application.
/// </summary>
/// <remarks>
/// The attribute on its own does not do anything. It must be passed to the <see cref="ModelBuilderExtensions.LoadReflectiveModels{TDatabaseEngineModelAttribute}(ModelBuilder, INamingPolicy?, IMappingPolicy?)"/> method to be effective.
/// </remarks>
public sealed class MySqlModelAttribute : DatabaseEngineModelAttribute;