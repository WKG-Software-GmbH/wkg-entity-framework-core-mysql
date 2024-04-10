using Wkg.EntityFrameworkCore.Configuration.Reflection.Discovery;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class MySqlDiscoveryOptionsBuilder : DiscoveryOptionsBuilder
{
    public override IDiscoveryOptionsBuilder AddTargetDatabaseEngine<TTargetEngine>() => 
        throw new NotSupportedException("Multiple database engine support is currently not available for stored procedures in this version of the library.");
}
