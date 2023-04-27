using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping;

public interface IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
}

public abstract class MySqlStoredProcedure<TIOContainer> : StoredProcedure<TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
}

public abstract class MySqlStoredProcedure<TIOContainer, TResult> : StoredProcedure<TIOContainer, TResult>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
    where TResult : class
{
}