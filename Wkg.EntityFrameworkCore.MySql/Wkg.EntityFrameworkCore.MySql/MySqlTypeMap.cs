﻿using MySql.Data.MySqlClient;

namespace Wkg.EntityFrameworkCore.MySql;

internal class MySqlTypeMap : DbTypeMap<MySqlDbType>
{
    protected override Dictionary<Type, MySqlDbType> TypeMap { get; } = new()
    {
        { typeof(int), MySqlDbType.Int32 },
        { typeof(long), MySqlDbType.Int64 },
        { typeof(string), MySqlDbType.VarChar },
        { typeof(bool), MySqlDbType.Bit },
        { typeof(DateTime), MySqlDbType.DateTime },
        { typeof(DateTimeOffset), MySqlDbType.DateTime },
        { typeof(TimeOnly), MySqlDbType.Time },
        { typeof(DateOnly), MySqlDbType.Date },
        { typeof(TimeSpan), MySqlDbType.Time },
        { typeof(float), MySqlDbType.Float },
        { typeof(double), MySqlDbType.Double },
        { typeof(decimal), MySqlDbType.Decimal },
        { typeof(byte[]), MySqlDbType.Blob },
        { typeof(Guid), MySqlDbType.Guid },
        { typeof(uint), MySqlDbType.UInt32 },
        { typeof(ulong), MySqlDbType.UInt64 },
        { typeof(short), MySqlDbType.Int16 },
        { typeof(ushort), MySqlDbType.UInt16 },
        { typeof(sbyte), MySqlDbType.Byte },
        { typeof(byte), MySqlDbType.UByte },
        { typeof(char), MySqlDbType.VarChar },
        { typeof(char[]), MySqlDbType.VarChar },
    };
}