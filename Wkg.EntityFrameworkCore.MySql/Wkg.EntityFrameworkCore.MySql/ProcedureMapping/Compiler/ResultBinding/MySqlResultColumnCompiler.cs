using MySql.Data.MySqlClient;
using System.Data;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.Extensions;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultConverters;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;

internal class MySqlResultColumnCompiler : ResultColumnCompiler<IMySqlResultColumnBuilder, MySqlDataReader>, IResultColumnCompiler
{
    public MySqlResultColumnCompiler(IMySqlResultColumnBuilder builder) : base(builder)
    {
    }

    protected override Expression? GetColumnConverterOrDefault()
    {
        if (Builder.MySqlDbType is MySqlDbType.JSON 
            && Builder.Conversion is null 
            && Builder.Context.ResultProperty.PropertyType != typeof(string))
        {
            return JsonResultConverter.For(Builder.Context.ResultProperty.PropertyType);
        }
        return null;
    }

    protected override (Type, Expression) GetColumnFactory() => GetColumnFactoryExpression(Builder.MySqlDbType!.Value);

    private static (Type, Expression) GetColumnFactoryExpression(MySqlDbType dbType) => dbType switch
    {
        MySqlDbType.Binary
            or MySqlDbType.VarBinary
            or MySqlDbType.Blob
            or MySqlDbType.TinyBlob
            or MySqlDbType.MediumBlob
            or MySqlDbType.LongBlob => ReadColumn((reader, name) => reader.GetBytes(name)),
        MySqlDbType.VarChar
            or MySqlDbType.String
            or MySqlDbType.JSON
            or MySqlDbType.Text
            or MySqlDbType.TinyText
            or MySqlDbType.LongText
            or MySqlDbType.MediumText
            or MySqlDbType.VarString => ReadColumn((reader, name) => reader.GetString(name)),
        MySqlDbType.Bit => ReadColumn((reader, name) => reader.GetBoolean(name)),
        MySqlDbType.Date
            or MySqlDbType.Timestamp
            or MySqlDbType.DateTime => ReadColumn((reader, name) => reader.GetDateTime(name)),
        MySqlDbType.Byte => ReadColumn((reader, name) => reader.GetSByte(name)),
        MySqlDbType.Decimal
            or MySqlDbType.NewDecimal => ReadColumn((reader, name) => reader.GetDecimal(name)),
        MySqlDbType.Double => ReadColumn((reader, name) => reader.GetDouble(name)),
        MySqlDbType.Float => ReadColumn((reader, name) => reader.GetFloat(name)),
        MySqlDbType.Geometry => ReadColumn((reader, name) => reader.GetMySqlGeometry(name)),
        MySqlDbType.Guid => ReadColumn((reader, name) => reader.GetGuid(name)),
        MySqlDbType.Int16 => ReadColumn((reader, name) => reader.GetInt16(name)),
        MySqlDbType.Int24
            or MySqlDbType.Int32 => ReadColumn((reader, name) => reader.GetInt32(name)),
        MySqlDbType.Int64 => ReadColumn((reader, name) => reader.GetInt64(name)),
        MySqlDbType.Time => ReadColumn((reader, name) => reader.GetTimeSpan(name)),
        MySqlDbType.UByte => ReadColumn((reader, name) => reader.GetByte(name)),
        MySqlDbType.UInt16 => ReadColumn((reader, name) => reader.GetUInt16(name)),
        MySqlDbType.UInt24
            or MySqlDbType.UInt32 => ReadColumn((reader, name) => reader.GetUInt32(name)),
        MySqlDbType.UInt64 => ReadColumn((reader, name) => reader.GetUInt64(name)),
        _ => throw new NotSupportedException($"The type {dbType} is not supported.")
    };
}
