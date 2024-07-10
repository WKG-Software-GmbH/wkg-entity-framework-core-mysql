using Wkg.Versioning;

namespace Wkg.EntityFrameworkCore.MySql;

/// <summary>
/// Provides version information for the Wkg.EntityFrameworkCore.MySql framework.
/// </summary>
public class WkgEntityFrameworkCoreMySql : DeploymentVersionInfo
{
    private const string __CI_DEPLOYMENT_VERSION_PREFIX = "0.0.0";
    private const string __CI_DEPLOYMENT_VERSION_SUFFIX = "CI-INJECTED";
    private const string __CI_DEPLOYMENT_DATETIME_UTC = "1970-01-01 00:00:00";

    private WkgEntityFrameworkCoreMySql() : base
    (
        __CI_DEPLOYMENT_VERSION_PREFIX,
        __CI_DEPLOYMENT_VERSION_SUFFIX,
        __CI_DEPLOYMENT_DATETIME_UTC
    ) => Pass();

    /// <summary>
    /// Provides version information for the Wkg.EntityFrameworkCore.MySql framework.
    /// </summary>
    public static WkgEntityFrameworkCoreMySql VersionInfo { get; } = new WkgEntityFrameworkCoreMySql();
}
