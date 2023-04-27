using Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Builder;
using Wkg.EntityFrameworkCore.ProcedureMapping;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration;

public interface IProcedureConfiguration<TProcedure, TIOContainer>
    where TProcedure : StoredProcedure<TIOContainer>, IProcedureConfiguration<TProcedure, TIOContainer>, IMySqlStoredProcedure<TIOContainer>
    where TIOContainer : class
{
    static abstract void Configure(MySqlProcedureBuilder<TProcedure, TIOContainer> self);
}