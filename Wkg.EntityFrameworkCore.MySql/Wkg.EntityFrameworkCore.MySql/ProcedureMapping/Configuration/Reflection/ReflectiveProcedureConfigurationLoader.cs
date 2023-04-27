using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Reflection;
using Wkg.EntityFrameworkCore.MySql.Extensions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class ReflectiveProcedureConfigurationLoader : ReflectiveLoaderBase
{
    private static object? _reflectiveProcedureLoaderSentinel = new();

    public static ModelBuilder LoadAll(ModelBuilder builder)
    {
        AssertLoadOnce(builder, ref _reflectiveProcedureLoaderSentinel);

        Console.WriteLine("ReflectiveProcedureConfigurationLoader is initializing.");

        LoadAllProceduresInternal(typeof(IStoredProcedure),
            typeof(StoredProcedure<>),
            typeof(IReflectiveProcedureConfiguration<,>),
            typeof(ModelBuilderExtensions),
            nameof(ModelBuilderExtensions.LoadProcedure),
            builder);

        Console.WriteLine("ReflectiveProcedureConfigurationLoader is exiting.");
        return builder;
    }
}