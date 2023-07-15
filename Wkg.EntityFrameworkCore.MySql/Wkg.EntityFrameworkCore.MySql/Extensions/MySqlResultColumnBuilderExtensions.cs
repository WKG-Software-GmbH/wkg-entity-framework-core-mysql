using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Extensions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultConverters;
using Wkg.Extensions.Common;

namespace Wkg.EntityFrameworkCore.MySql.Extensions;

/// <summary>
/// Provides extension methods for <see cref="MySqlResultColumnBuilder{TResult, TProperty}"/> instances.
/// </summary>
public static class MySqlResultColumnBuilderExtensions
{
    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="bool"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, bool> GetAsBoolean<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Byte);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetBoolean(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a string and deserialize it as JSON object matching the property type <typeparamref name="TProperty"/> being configured on the <typeparamref name="TResult"/> entity.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, object> GetAsJson<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.JSON);
        LambdaExpression jsonConversion = JsonResultConverter.For(builder
            .To<IMySqlResultColumnBuilder>().Context.ResultProperty.PropertyType);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint
            .Create((reader, name) => reader.GetString(name), jsonConversion));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="byte"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, byte> GetAsByte<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.UByte);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetByte(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="byte"/> array.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, byte[]> GetAsBytes<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Blob);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetBytes(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="DateTime"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, DateTime> GetAsDateTime<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.DateTime);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetDateTime(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="decimal"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, decimal> GetAsDecimal<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Decimal);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetDecimal(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="double"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, double> GetAsDouble<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Double);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetDouble(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="float"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, float> GetAsFloat<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Float);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetFloat(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="Guid"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <remarks>
    /// This method requires the column to be a UUID string (<c>VARCHAR(36)</c>). For <c>BINARY(16)</c> columns, use <see cref="GetAsBytes{TResult,TProperty}(MySqlResultColumnBuilder{TResult,TProperty})"/> instead in combination with a custom conversion from <see langword="byte"/>[] to <see cref="Guid"/>.
    /// </remarks>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, Guid> GetAsGuid<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Guid);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetGuid(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="short"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, short> GetAsInt16<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Int16);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetInt16(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="int"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, int> GetAsInt32<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Int32);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetInt32(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="long"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, long> GetAsInt64<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Int64);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetInt64(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="MySqlDateTime"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, MySqlDateTime> GetAsMySqlDateTime<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.DateTime);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetMySqlDateTime(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="MySqlDecimal"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, MySqlDecimal> GetAsMySqlDecimal<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Decimal);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetMySqlDecimal(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="MySqlGeometry"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, MySqlGeometry> GetAsMySqlGeometry<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Geometry);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetMySqlGeometry(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="sbyte"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, sbyte> GetAsSByte<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Byte);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetSByte(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="Stream"/> of raw binary data.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <remarks>
    /// The <see cref="Stream"/> returned by the compiler is not guaranteed to be seekable and must be disposed by the caller.
    /// </remarks>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, Stream> GetAsStream<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Blob);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetStream(reader.GetOrdinal(name))));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="string"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, string> GetAsString<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.VarChar);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetString(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="TimeSpan"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, TimeSpan> GetAsTimeSpan<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.Time);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetTimeSpan(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="ushort"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, ushort> GetAsUInt16<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.UInt16);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetUInt16(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="uint"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, uint> GetAsUInt32<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.UInt32);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetUInt32(name)));
        return new(builder);
    }

    /// <summary>
    /// Instructs the compiler to retrieve the column value as a <see cref="ulong"/>.
    /// </summary>
    /// <typeparam name="TResult">The type of the result entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property being configured.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder instance for fluent configuration.</returns>
    public static MySqlResultColumnBuilderProxy<TResult, TProperty, ulong> GetAsUInt64<TResult, TProperty>(this MySqlResultColumnBuilder<TResult, TProperty> builder)
    {
        builder.HasDbType(MySqlDbType.UInt64);
        builder.SetCompilerHint(MySqlResultColumnCompilerHint.Create((reader, name) => reader.GetUInt64(name)));
        return new(builder);
    }
}
