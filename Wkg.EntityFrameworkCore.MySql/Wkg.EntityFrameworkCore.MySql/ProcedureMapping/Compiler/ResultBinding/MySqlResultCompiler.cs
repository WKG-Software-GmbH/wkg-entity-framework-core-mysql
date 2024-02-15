using MySql.Data.MySqlClient;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;

internal class MySqlResultCompiler(IMySqlResultBuilder builder) : ResultCompiler<IMySqlResultBuilder>(builder), IResultCompiler<MySqlDataReader>
{
    public CompiledResult<MySqlDataReader> Compile(CompiledResultColumn[] compiledResultColumns)
    {
        CompiledResultFactory<MySqlDataReader> factory = CompileResultFactoryFor<MySqlDataReader>(compiledResultColumns);
        return new MySqlCompiledResult(Builder.IsCollection, factory);
    }
}
