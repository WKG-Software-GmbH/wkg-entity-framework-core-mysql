using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration;

public interface IReflectiveProcedureConfiguration<TProcedure, TIOContainer> : IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : StoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
}