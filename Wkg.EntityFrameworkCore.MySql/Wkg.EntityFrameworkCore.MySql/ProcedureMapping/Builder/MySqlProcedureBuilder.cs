using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.ProcedureMapping.Runtime;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;

internal interface IMySqlProcedureBuilder : IProcedureBuilder
{
}

public class MySqlProcedureBuilder<TProcedure, TIOContainer>
    : ProcedureBuilder<
        TProcedure,
        TIOContainer,
        MySqlCompiledParameter,
        MySqlDataReader,
        MySqlProcedureBuilder<TProcedure, TIOContainer>>,
    IMySqlProcedureBuilder
    where TProcedure : StoredProcedure<TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
    internal MySqlProcedureBuilder()
    {
    }

    public MySqlParameterBuilder<TIOContainer, TParameter> Parameter<TParameter>(Expression<Func<TIOContainer, TParameter>> parameterExpression)
    {
        MySqlParameterBuilder<TIOContainer, TParameter> paramBuilder = new(parameterExpression, ThrowHelper);
        ParameterBuilders.Add(paramBuilder);
        return paramBuilder;
    }

    public MySqlResultBuilder<TResult> Returns<TResult>() where TResult : class
    {
        MySqlResultBuilder<TResult> resultBuilder = new(ThrowHelper);
        ResultBuilder = resultBuilder;
        return resultBuilder;
    }

    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        string? name = null;
        if (IsFunctionValue && (name = ParameterBuilders.Where(param => param.ParameterDirection is ParameterDirection.Output or ParameterDirection.InputOutput).Select(param => param.ParameterName).FirstOrDefault()) is not null)
        {
            ThrowHelper.Throw<ArgumentException>("MySql functions cannot have output parameters!", name);
        }
    }
    
    protected override IProcedureCompiler<MySqlCompiledParameter> Build() =>
        new MySqlProcedureCompiler(this, typeof(TProcedure));
}
