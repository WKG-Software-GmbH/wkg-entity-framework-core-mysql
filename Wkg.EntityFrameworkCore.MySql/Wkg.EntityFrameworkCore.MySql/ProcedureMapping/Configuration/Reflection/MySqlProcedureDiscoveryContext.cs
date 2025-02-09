using Microsoft.EntityFrameworkCore;
using Wkg.EntityFrameworkCore.Configuration.Discovery;

namespace Wkg.EntityFrameworkCore.MySql.ProcedureMapping.Configuration.Reflection;

internal class MySqlProcedureDiscoveryContext : ProcedureDiscoveryContext
{
    private static readonly List<WeakReference<ModelBuilder>> s_configuredModelBuilders = [];

    protected override List<WeakReference<ModelBuilder>> StaticModelBuilderCache => s_configuredModelBuilders;
}
