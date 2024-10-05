using Hangfire.Annotations;
using Hangfire.Dashboard;

internal class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}