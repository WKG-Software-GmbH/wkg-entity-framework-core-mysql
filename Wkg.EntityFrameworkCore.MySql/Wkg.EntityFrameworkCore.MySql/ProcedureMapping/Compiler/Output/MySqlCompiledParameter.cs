﻿using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;

/// <summary>
/// Represents a parameter that has been compiled for runtime use.
/// </summary>
/// <param name="Name">The name of the parameter.</param>
/// <param name="MySqlDbType">The <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> of the parameter.</param>
/// <param name="Direction">The <see cref="ParameterDirection"/> of the parameter.</param>
/// <param name="IsOutput">Whether the parameter requires output binding.</param>
/// <param name="Getter">The <see cref="PropertyGetter"/> that can be used to retrieve the value of the parameter from the input/output container object.</param>
/// <param name="Setter">The <see cref="PropertySetter"/> that can be used to store the value of the parameter to the input/output container object.</param>
/// <param name="Size">The size of the parameter.</param>
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
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Store(ref DbParameter param, object context) => 
        Setter!.Invoke(context, param.Value!);
}