using MySql.Data.MySqlClient;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping;

internal class MySqlProcedureBuildPipeline : ProcedureBuildPipeline
{
    public static void Execute(IProcedureBuilder<MySqlCompiledParameter, MySqlDataReader> procedureBuilder) => 
        ProcedureBuildPipeline.Execute(procedureBuilder);
}
