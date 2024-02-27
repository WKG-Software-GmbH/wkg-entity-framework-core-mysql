using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Reflection;
using Wkg.EntityFrameworkCore.MySql.Extensions;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.Logging;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class ReflectiveProcedureConfigurationLoader : ReflectiveLoaderBase
{
    private static object? _reflectiveProcedureLoaderSentinel = new();

    public static ModelBuilder LoadAll(ModelBuilder builder, string[]? targetAssemblies)
    {
        AssertLoadOnce(builder, ref _reflectiveProcedureLoaderSentinel);

        Log.WriteInfo($"{nameof(ReflectiveProcedureConfigurationLoader)} is initializing.");

        LoadAllProceduresInternal(typeof(IStoredProcedure),
            typeof(StoredProcedure<>),
            typeof(IReflectiveProcedureConfiguration<,>),
            typeof(ModelBuilderExtensions),
            nameof(ModelBuilderExtensions.LoadProcedure),
            builder,
            targetAssemblies);

        Log.WriteInfo($"{nameof(ReflectiveProcedureConfigurationLoader)} is exiting.");
        return builder;
    }
}