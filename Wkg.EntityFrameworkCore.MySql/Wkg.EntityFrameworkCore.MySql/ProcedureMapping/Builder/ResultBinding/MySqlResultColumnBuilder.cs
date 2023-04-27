using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;

public interface IMySqlResultColumnBuilder : IResultColumnBuilder
{
    MySqlDbType? MySqlDbType { get; }
}

public class MySqlResultColumnBuilder<TResult, TProperty>
    : ResultColumnBuilder<TResult, TProperty, MySqlResultColumnBuilder<TResult, TProperty>>, IMySqlResultColumnBuilder
{
    private static readonly MySqlTypeMap _typeMap = new();

    private MySqlDbType? MySqlDbType { get; set; } = null;

    MySqlDbType? IMySqlResultColumnBuilder.MySqlDbType => MySqlDbType;

    internal MySqlResultColumnBuilder(Expression<Func<TResult, TProperty>> columnSelector, IResultThrowHelper throwHelper) : base(columnSelector, throwHelper)
    {
    }

    public MySqlResultColumnBuilder<TResult, TProperty> HasDbType(MySqlDbType dbType)
    {
        if (MySqlDbType is not null)
        {
            Context.ThrowHelper.Throw<InvalidOperationException>("Attempted to set DB Type on a column that already has a DB Type! This was unexpected at this time.");
        }
        MySqlDbType = dbType;
        return this;
    }

    public override MySqlResultColumnBuilder<TResult, TProperty> RequiresConversion<TColumn>(Expression<Func<TColumn, TProperty>> conversion)
    {
        ParameterExpression columnExpression = conversion.Parameters[0];
        MySqlDbType = _typeMap.GetDbTypeOrDefault(columnExpression.Type);

        return base.RequiresConversion(conversion);
    }

    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = MySqlDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, MySqlDbType>("Attempted to build column with no valid DB Type!", nameof(MySqlDbType));
    }

    protected override void AttemptAutoConfiguration() =>
        MySqlDbType ??= _typeMap.GetDbTypeOrDefault(Context.ResultProperty.PropertyType);

    internal void SetCompilerHint(MySqlResultColumnCompilerHint hint) => 
        CompilerHint = hint;

    protected override IResultColumnCompiler Build() =>
        new MySqlResultColumnCompiler(this);
}