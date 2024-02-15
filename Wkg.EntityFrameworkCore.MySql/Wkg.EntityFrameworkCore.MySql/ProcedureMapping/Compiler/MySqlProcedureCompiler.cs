using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;

internal class MySqlProcedureCompiler(IMySqlProcedureBuilder builder, Type procedureType) 
    : ProcedureCompiler<IMySqlProcedureBuilder>(builder, procedureType), IProcedureCompiler<MySqlCompiledParameter>
{
    public ICompiledProcedure Compile(MySqlCompiledParameter[] compiledParameters, CompiledResult? compiledResult)
    {
        IMySqlProcedureBuilder b = Builder;
        return new CompiledProcedure<MySqlCompiledParameter>(b.ProcedureName!, b.IsFunction, compiledParameters, ProcedureType, compiledResult);
    }
}