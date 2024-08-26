using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;

/// <summary>
/// Represents an <see cref="IResultColumnBuilder"/> for a result column of a stored procedure in a MySql database.
/// </summary>
public interface IMySqlResultColumnBuilder : IResultColumnBuilder
{
    /// <summary>
    /// The <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> of the column, if configured.
    /// </summary>
    MySqlDbType? MySqlDbType { get; }
}

/// <summary>
/// The builder for a result column of stored procedure in a MySql database.
/// </summary>
/// <typeparam name="TResult">The type of the result collection.</typeparam>
/// <typeparam name="TProperty">The type of the property to be mapped.</typeparam>
public class MySqlResultColumnBuilder<TResult, TProperty>
    : ResultColumnBuilder<TResult, TProperty, MySqlResultColumnBuilder<TResult, TProperty>>, IMySqlResultColumnBuilder
{
    private static readonly MySqlTypeMap s_typeMap = new();

    private MySqlDbType? MySqlDbType { get; set; } = null;

    MySqlDbType? IMySqlResultColumnBuilder.MySqlDbType => MySqlDbType;

    internal MySqlResultColumnBuilder(Expression<Func<TResult, TProperty>> columnSelector, IResultThrowHelper throwHelper) : base(columnSelector, throwHelper)
    {
    }

    /// <summary>
    /// Sets the <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> of the column.
    /// </summary>
    /// <param name="dbType">The <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> to set.</param>
    /// <returns>The column builder for fluent configuration.</returns>
    public MySqlResultColumnBuilder<TResult, TProperty> HasDbType(MySqlDbType dbType)
    {
        if (MySqlDbType is not null)
        {
            Context.ThrowHelper.Throw<InvalidOperationException>("Attempted to set DB Type on a column that already has a DB Type! This was unexpected at this time.");
        }
        MySqlDbType = dbType;
        return this;
    }

    /// <inheritdoc/>
    public override MySqlResultColumnBuilder<TResult, TProperty> RequiresConversion<TColumn>(Expression<Func<TColumn, TProperty>> conversion)
    {
        ParameterExpression columnExpression = conversion.Parameters[0];
        MySqlDbType = s_typeMap.GetDbTypeOrDefault(columnExpression.Type);

        return base.RequiresConversion(conversion);
    }

    /// <inheritdoc/>
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = MySqlDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, MySqlDbType>("Attempted to build column with no valid DB Type!", nameof(MySqlDbType));
    }

    /// <inheritdoc/>
    protected override void AttemptAutoConfiguration() =>
        MySqlDbType ??= s_typeMap.GetDbTypeOrDefault(Context.ResultProperty.PropertyType);

    internal void SetCompilerHint(MySqlResultColumnCompilerHint hint) => 
        CompilerHint = hint;

    /// <inheritdoc/>
    protected override IResultColumnCompiler Build() =>
        new MySqlResultColumnCompiler(this);
}