using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents an <see cref="IResultBuilder"/> for result entities of a stored procedure in a MySql database.
/// </summary>
public interface IMySqlResultBuilder : IResultBuilder
{
}

/// <summary>
/// The result entity builder for result type <typeparamref name="TResult"/> of a stored procedure in a MySql database.
/// </summary>
/// <typeparam name="TResult">The type of the result collection.</typeparam>
/// <remarks>
/// Initializes a new instance of the <see cref="MySqlResultBuilder{TResult}"/> class.
/// </remarks>
/// <param name="throwHelper">The <see cref="IProcedureThrowHelper"/> to be used if an error is encountered.</param>
public class MySqlResultBuilder<TResult>(IProcedureThrowHelper throwHelper) 
    : ResultBuilder<TResult, MySqlDataReader, MySqlResultBuilder<TResult>>(throwHelper, typeof(TResult)), IMySqlResultBuilder
    where TResult : class
{

    /// <summary>
    /// Creates a new <see cref="MySqlResultColumnBuilder{TResult, TProperty}"/> to map a result column of the procedure to a property of the result entity.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property to be mapped.</typeparam>
    /// <param name="propertySelector">A lambda expression selecting the property to be mapped.</param>
    /// <returns>A new <see cref="MySqlResultColumnBuilder{TResult, TProperty}"/> to configure the column.</returns>
    public MySqlResultColumnBuilder<TResult, TProperty> Column<TProperty>(Expression<Func<TResult, TProperty>> propertySelector)
    {
        MySqlResultColumnBuilder<TResult, TProperty> columnBuilder = new(propertySelector, ThrowHelper);
        ColumnBuilders.Add(columnBuilder);
        return columnBuilder;
    }

    /// <inheritdoc/>
    protected override IResultCompiler<MySqlDataReader> Build() =>
        new MySqlResultCompiler(this);
}
