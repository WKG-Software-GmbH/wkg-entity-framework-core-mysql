using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;

public interface IMySqlResultBuilder : IResultBuilder
{
}

public class MySqlResultBuilder<TResult> : ResultBuilder<TResult, MySqlDataReader, MySqlResultBuilder<TResult>>, IMySqlResultBuilder
    where TResult : class
{
    public MySqlResultBuilder(IProcedureThrowHelper throwHelper) : base(throwHelper, typeof(TResult))
    {
    }

    public MySqlResultColumnBuilder<TResult, TProperty> Column<TProperty>(Expression<Func<TResult, TProperty>> propertySelector)
    {
        MySqlResultColumnBuilder<TResult, TProperty> columnBuilder = new(propertySelector, ThrowHelper);
        ColumnBuilders.Add(columnBuilder);
        return columnBuilder;
    }

    protected override IResultCompiler<MySqlDataReader> Build() =>
        new MySqlResultCompiler(this);
}
