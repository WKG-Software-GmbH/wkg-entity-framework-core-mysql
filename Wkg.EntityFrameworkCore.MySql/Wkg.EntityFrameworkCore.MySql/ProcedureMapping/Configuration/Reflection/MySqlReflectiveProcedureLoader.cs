using Wkg.EntityFrameworkCore.Configuration.Reflection;
using Wkg.EntityFrameworkCore.MySql.Extensions;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class MySqlReflectiveProcedureLoader : ReflectiveProcedureLoader
{
    protected override StoredPrecedureLoaderOptions Options { get; } = new
    (
        StoredProcedureInterface: typeof(IStoredProcedure),
        StoredProcedure: typeof(StoredProcedure<>),
        ReflectiveInterface: typeof(IReflectiveProcedureConfiguration<,>),
        ModelBuilderExtensionsType: typeof(ModelBuilderExtensions),
        LoadProcedureMethodName: nameof(ModelBuilderExtensions.LoadProcedure)
    );
}
