using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder LoadProcedure<TProcedure, TIOContainer>(this ModelBuilder builder)
        where TProcedure : StoredProcedure<TIOContainer>,
            IProcedureConfiguration<TProcedure, TIOContainer>, IMySqlStoredProcedure<TIOContainer>
        where TIOContainer : class
    {
        MySqlProcedureBuilder<TProcedure, TIOContainer> procedure = builder.Procedure<TProcedure, TIOContainer>();
        TProcedure.Configure(procedure);
        MySqlProcedureBuildPipeline.Execute(procedure);
        return builder;
    }

    public static MySqlProcedureBuilder<TProcedure, TIOContainer> Procedure<TProcedure, TIOContainer>(this ModelBuilder _)
        where TProcedure : StoredProcedure<TIOContainer>, IMySqlStoredProcedure<TIOContainer>
        where TIOContainer : class =>
            new();

    public static ModelBuilder LoadReflectiveProcedures(this ModelBuilder builder)
    {
        _ = builder ?? throw new ArgumentNullException(nameof(builder));
        ReflectiveProcedureConfigurationLoader.LoadAll(builder);
        return builder;
    }
}
