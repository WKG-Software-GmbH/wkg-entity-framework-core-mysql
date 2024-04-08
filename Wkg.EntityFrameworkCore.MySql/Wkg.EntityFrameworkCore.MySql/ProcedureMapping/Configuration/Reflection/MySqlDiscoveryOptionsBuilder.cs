using Wkg.EntityFrameworkCore.Configuration.Reflection.Discovery;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class MySqlDiscoveryOptionsBuilder : DiscoveryOptionsBuilder
{
    public override IDiscoveryOptionsBuilder AddTargetDatabaseEngine<TTargetEngine>() => 
        throw new NotSupportedException("Multiple database engine support is not available for stored procedures. The target database engine is automatically determined through the implemented interfaces.");
}
