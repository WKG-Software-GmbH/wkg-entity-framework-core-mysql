using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping;

/// <summary>
/// Represents a stored procedure command object in a MySql database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
public interface IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class;

/// <summary>
/// A stored procedure command object in a MySql database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
public abstract class MySqlStoredProcedure<TIOContainer> : StoredProcedure<TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class;

/// <summary>
/// A stored procedure command object with a result set of type <typeparamref name="TResult"/> in a MySql database.
/// </summary>
/// <typeparam name="TIOContainer">The type of the input/output container used to pass parameters to the stored procedure and to retrieve the result of <see langword="out"/> or <see langword="ref"/> parameters.</typeparam>
/// <typeparam name="TResult">The type of the result set.</typeparam>
public abstract class MySqlStoredProcedure<TIOContainer, TResult> : StoredProcedure<TIOContainer, TResult>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
    where TResult : class;