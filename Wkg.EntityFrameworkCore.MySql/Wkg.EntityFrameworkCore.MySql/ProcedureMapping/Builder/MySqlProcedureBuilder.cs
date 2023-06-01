using MySql.Data.MySqlClient;
using System.Data;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a stored procedure in a MySql database.
/// </summary>
internal interface IMySqlProcedureBuilder : IProcedureBuilder
{
}

/// <summary>
/// Provides a simple API for configuring a stored procedure in a MySql database.
/// </summary>
/// <typeparam name="TProcedure">The concrete type of the stored procedure command object that represents the stored database procedure.</typeparam>
/// <typeparam name="TIOContainer">The type of the Input/Output container object used to pass arguments to and from the stored procedure.</typeparam>
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

    /// <summary>
    /// Creates a new <see cref="MySqlParameterBuilder{TIOContainer, TParameter}"/> instance for configuring a parameter of this MySQL procedure.
    /// </summary>
    /// <typeparam name="TParameter">The CLR type of the parameter being mapped.</typeparam>
    /// <param name="parameterExpression">An lambda expression that identifies the property of the <typeparamref name="TIOContainer"/> that should be mapped to this parameter.</param>
    /// <returns>A new <see cref="MySqlParameterBuilder{TIOContainer, TParameter}"/> instance to configure the parameter.</returns>
    public MySqlParameterBuilder<TIOContainer, TParameter> Parameter<TParameter>(Expression<Func<TIOContainer, TParameter>> parameterExpression)
    {
        MySqlParameterBuilder<TIOContainer, TParameter> paramBuilder = new(parameterExpression, ThrowHelper);
        ParameterBuilders.Add(paramBuilder);
        return paramBuilder;
    }

    /// <summary>
    /// Creates a new <see cref="MySqlResultBuilder{TResult}"/> instance for configuring the result set of this MySQL procedure.
    /// </summary>
    /// <typeparam name="TResult">The CLR type of the result entities within the result set.</typeparam>
    /// <returns>A new <see cref="MySqlResultBuilder{TResult}"/> instance to configure the result set.</returns>
    public MySqlResultBuilder<TResult> Returns<TResult>() where TResult : class
    {
        MySqlResultBuilder<TResult> resultBuilder = new(ThrowHelper);
        ResultBuilder = resultBuilder;
        return resultBuilder;
    }

    /// <inheritdoc/>
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        string? name = null;
        if (IsFunctionValue && (name = ParameterBuilders.Where(param => param.ParameterDirection is ParameterDirection.Output or ParameterDirection.InputOutput).Select(param => param.ParameterName).FirstOrDefault()) is not null)
        {
            ThrowHelper.Throw<ArgumentException>("MySql functions cannot have output parameters!", name);
        }
    }
    
    /// <inheritdoc/>
    protected override IProcedureCompiler<MySqlCompiledParameter> Build() =>
        new MySqlProcedureCompiler(this, typeof(TProcedure));
}
