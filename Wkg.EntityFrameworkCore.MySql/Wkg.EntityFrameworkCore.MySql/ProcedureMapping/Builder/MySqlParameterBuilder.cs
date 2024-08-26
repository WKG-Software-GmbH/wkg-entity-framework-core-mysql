using MySql.Data.MySqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.Output;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Builder.ThrowHelpers;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;

/// <summary>
/// Provides a simple API for configuring a parameter of a stored procedure in a MySQL database.
/// </summary>
internal interface IMySqlParameterBuilder : IParameterBuilder
{
    /// <summary>
    /// The <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> associated with the parameter of this MySQL procedure.
    /// </summary>
    MySqlDbType? MySqlDbType { get; }
}

/// <summary>
/// Provides a simple API for configuring a parameter of a stored procedure in a MySQL database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the Input/Output container object used to pass arguments to and from the stored procedure.</typeparam>
/// <typeparam name="TParameter">The CLR type of the parameter being mapped.</typeparam>
public class MySqlParameterBuilder<TIOContainer, TParameter>
    : ParameterBuilder<
        TIOContainer,
        TParameter,
        MySqlCompiledParameter,
        MySqlParameterBuilder<TIOContainer, TParameter>>,
    IMySqlParameterBuilder
    where TIOContainer : class
{
    private static readonly MySqlTypeMap s_typeMap = new();

    private MySqlDbType? MySqlDbType { get; set; } = null;

    MySqlDbType? IMySqlParameterBuilder.MySqlDbType => MySqlDbType;

    /// <summary>
    /// Creates a new instance of <see cref="MySqlParameterBuilder{TIOContainer, TParameter}"/>.
    /// </summary>
    /// <param name="parameterSelector">An expression that selects the parameter from the <typeparamref name="TIOContainer"/> object.</param>
    /// <param name="throwHelper">The <see cref="IProcedureThrowHelper"/> to be used for throwing exceptions.</param>
    public MySqlParameterBuilder(Expression<Func<TIOContainer, TParameter>> parameterSelector, IProcedureThrowHelper throwHelper) : base(parameterSelector, throwHelper)
    {
        MySqlDbType = s_typeMap.GetDbTypeOrDefault(Context.PropertyInfo.PropertyType);
    }

    /// <summary>
    /// Specifies the <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> associated with the parameter of this MySQL procedure.
    /// </summary>
    /// <param name="mySqlDbType">The <see cref="global::MySql.Data.MySqlClient.MySqlDbType"/> associated with the parameter of this MySQL procedure.</param>
    /// <returns>The current <see cref="MySqlParameterBuilder{TIOContainer, TParameter}"/> instance for fluent configuration.</returns>
    public MySqlParameterBuilder<TIOContainer, TParameter> HasDbType(MySqlDbType mySqlDbType)
    {
        MySqlDbType = mySqlDbType;
        return this;
    }

    /// <inheritdoc/>
    [MemberNotNull(nameof(MySqlDbType))]
    protected override void AssertIsValid()
    {
        base.AssertIsValid();

        _ = MySqlDbType ?? Context.ThrowHelper.Throw<ArgumentNullException, MySqlDbType>("Attempted to build Parameter with undefined database type. Either no database type was specified or the type could not be inferred from the parameter type.");
    }

    /// <inheritdoc/>
    protected override IParameterCompiler<MySqlCompiledParameter> Build() =>
        new MySqlParameterCompiler(this);
}