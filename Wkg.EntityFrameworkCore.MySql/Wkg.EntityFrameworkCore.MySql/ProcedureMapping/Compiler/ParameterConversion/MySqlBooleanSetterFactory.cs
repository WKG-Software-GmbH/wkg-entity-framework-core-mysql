using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.AccessorFactory;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.Output;
using Wkg.Reflection;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ParameterConversion;

internal readonly struct MySqlBooleanSetterFactory
{
    private static readonly MethodInfo _convertToBool = typeof(MySqlBooleanSetterFactory)
        .GetMethod(nameof(ConvertToBool), 1, BindingFlags.NonPublic | BindingFlags.Static, null, TypeArray.Of<object>(), null)!;

    private readonly IMySqlParameterBuilder _builder;
    private readonly IAccessorBuilder _accessorBuilder;

    private MySqlBooleanSetterFactory(IMySqlParameterBuilder builder, IAccessorBuilder accessorBuilder)
    {
        _builder = builder;
        _accessorBuilder = accessorBuilder;
    }

    public static MySqlBooleanSetterFactory CreateFrom(IMySqlParameterBuilder builder, IAccessorBuilder accessorBuilder) => 
        new(builder, accessorBuilder);

    public PropertySetter CreateSetter() => _builder.MySqlDbType switch
    {
        MySqlDbType.Bit 
            or MySqlDbType.UInt64 => CreateSetterInternal<ulong>(),
        MySqlDbType.Byte => CreateSetterInternal<sbyte>(),
        MySqlDbType.Int16 => CreateSetterInternal<short>(),
        MySqlDbType.Int24 => CreateSetterInternal<int>(),
        MySqlDbType.Int32 => CreateSetterInternal<int>(),
        MySqlDbType.Int64 => CreateSetterInternal<long>(),
        MySqlDbType.UInt16 => CreateSetterInternal<ushort>(),
        MySqlDbType.UInt24 => CreateSetterInternal<uint>(),
        MySqlDbType.UInt32 => CreateSetterInternal<uint>(),
        MySqlDbType.UByte => CreateSetterInternal<byte>(),
        _ => _builder.Context.ThrowHelper.Throw<NotSupportedException, PropertySetter>($"Cannot map MySqlDbType {_builder.MySqlDbType} to bool.")
    };

    private PropertySetter CreateSetterInternal<T>() where T : struct, INumber<T>
    {
        MethodInfo converter = _convertToBool.MakeGenericMethod(typeof(T));

        // get a "raw" setter that expects the I/O Container and an unboxed bool.
        PropertySetter<bool> directSetter = _accessorBuilder.BuildSetterDirect<bool>();
        ConstantExpression directSetterExpression = Expression.Constant(directSetter);
        ParameterExpression io = Expression.Parameter(typeof(object), "I/O Container");
        ParameterExpression value = Expression.Parameter(typeof(object), "value");
        MethodCallExpression unboxedValue = Expression.Call(converter, value);
        InvocationExpression setterExpression = Expression.Invoke(directSetterExpression, io, unboxedValue);
        return Expression.Lambda<PropertySetter>(setterExpression, io, value).Compile();
    }

    /// <summary>
    /// Unboxes a numeric value of type <typeparamref name="T"/> and converts it to a bool.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    private static bool ConvertToBool<T>(object o) where T : struct, INumber<T> =>
        Unsafe.Unbox<T>(o) != default;
}
