using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using Wkg.EntityFrameworkCore.ProcedureMapping.Compiler.ResultBinding;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Compiler.ResultBinding;

internal record MySqlResultColumnCompilerHint(Expression ReaderGetExpression, Type ReaderResultType, Expression? AutoConversion) : IResultColumnCompilerHint
{
    public static MySqlResultColumnCompilerHint Create<TResult>(Expression<Func<MySqlDataReader, string, TResult>> readerExpression) =>
        new(readerExpression, typeof(TResult), null);

    public static MySqlResultColumnCompilerHint Create<TResult>(Expression<Func<MySqlDataReader, string, TResult>> readerExpression, Expression conversion) =>
        new(readerExpression, typeof(TResult), conversion);
}
