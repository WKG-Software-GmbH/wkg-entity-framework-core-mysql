using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ParameterConversion;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;

internal class MySqlParameterCompiler(IMySqlParameterBuilder builder) : ParameterCompiler<IMySqlParameterBuilder>(builder), IParameterCompiler<MySqlCompiledParameter>
{
    public MySqlCompiledParameter Compile()
    {
        IMySqlParameterBuilder b = Builder;
        return new MySqlCompiledParameter(b.ParameterName!, b.MySqlDbType!.Value, b.ParameterDirection, b.IsOutput, CreateGetter(), CreateSetter(), b.Size);
    }

    protected override PropertySetter? CreateSetter()
    {
        PropertySetter? setter;
        if (Builder.IsOutput && Builder.Context.PropertyInfo.PropertyType == typeof(bool))
        {
            // MySql booleans will be returned as ulong, so we need to convert them to bool
            setter = MySqlBooleanSetterFactory.CreateFrom(Builder, AccessorBuilder).CreateSetter();
        }
        else
        {
            setter = base.CreateSetter();
        }
        return setter;
    }
}
