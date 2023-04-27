using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Runtime;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;

public readonly record struct MySqlCompiledParameter
(
    string Name,
    MySqlDbType MySqlDbType,
    ParameterDirection Direction,
    bool IsOutput,
    PropertyGetter Getter,
    PropertySetter? Setter,
    int Size
) : ICompiledParameter
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Load(ref DbParameter? parameter, object context)
    {
        if (parameter is not null)
        {
            parameter.Value = Getter.Invoke(context);
        }
        else
        {
            parameter = new MySqlParameter(Name, MySqlDbType)
            {
                Direction = Direction,
                Value = Getter.Invoke(context),
                Size = Size,
            };
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Store(ref DbParameter param, object context) => Setter!.Invoke(context, param.Value!);
}