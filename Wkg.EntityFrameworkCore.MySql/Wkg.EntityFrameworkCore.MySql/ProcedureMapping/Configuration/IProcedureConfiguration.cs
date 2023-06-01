using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration;

/// <summary>
/// Represents the configuration of a stored procedure.
/// </summary>
/// <typeparam name="TProcedure">The type of the stored procedure command object to load and configure.</typeparam>
/// <typeparam name="TIOContainer">The type of the input/output container used by <typeparamref name="TProcedure"/>.</typeparam>
public interface IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : StoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
    /// <summary>
    /// Configures the stored procedure.
    /// </summary>
    /// <param name="self">The <see cref="MySqlProcedureBuilder{TProcedure,TIOContainer}"/> to use for the configuration.</param>
    static abstract void Configure(MySqlProcedureBuilder<TProcedure, TIOContainer> self);
}