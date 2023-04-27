using MySql.Data.MySqlClient;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ResultBinding;
using Wkg.Extensions.Common;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;

public class MySqlResultColumnBuilderProxy<TResult, TProperty, TColumn> : TypedResultColumnBuilderProxy<TResult, TProperty, TColumn, MySqlResultColumnBuilder<TResult, TProperty>, MySqlResultColumnBuilderProxy<TResult, TProperty, TColumn>>, IMySqlResultColumnBuilder
{
    public MySqlResultColumnBuilderProxy(MySqlResultColumnBuilder<TResult, TProperty> proxiedBuilder) : base(proxiedBuilder)
    {
    }

    MySqlDbType? IMySqlResultColumnBuilder.MySqlDbType => ProxiedBuilder.To<IMySqlResultColumnBuilder>().MySqlDbType;

    protected override void AttemptAutoConfiguration() => throw new NotSupportedException();
}