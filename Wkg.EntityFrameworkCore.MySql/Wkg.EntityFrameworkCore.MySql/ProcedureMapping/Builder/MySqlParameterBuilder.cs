using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;

public interface IMySqlParameterBuilder : IParameterBuilder
{
    MySqlDbType? MySqlDbType { get; }
}

public class MySqlParameterBuilder<TIOContainer, TParameter>
    : ParameterBuilder<
        TIOContainer,
        TParameter,
        MySqlCompiledParameter,
        MySqlParameterBuilder<TIOContainer, TParameter>>,
    IMySqlParameterBuilder
    where TIOContainer : class
{
    private static readonly MySqlTypeMap _typeMap = new();

    private MySqlDbType? MySqlDbType { get; set; } = null;

    MySqlDbType? IMySqlParameterBuilder.MySqlDbType => MySqlDbType;

    public MySqlParameterBuilder(Expression<Func<TIOContainer, TParameter>> parameterSelector, IProcedureThrowHelper throwHelper) : base(parameterSelector, throwHelper)
    {
        MySqlDbType = _typeMap.GetDbTypeOrDefault(Context.PropertyInfo.PropertyType);
    }

    public MySqlParameterBuilder<TIOContainer, TParameter> HasDbType(MySqlDbType mySqlDbType)
    {
        MySqlDbType = mySqlDbType;
        return this;
    }

    [MemberNotNull(nameof(MySqlDbType))]
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = MySqlDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, MySqlDbType>("Attempted to build Parameter with undefined database type. Either no database type was specified or the type could not be inferred from the parameter type.");
    }

    protected override IParameterCompiler<MySqlCompiledParameter> Build() =>
        new MySqlParameterCompiler(this);
}