using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Attendents;

public static class AttendentForMonthEfCoreQueryableExtensions
{
    public static IQueryable<AttendentForMonth> IncludeDetails(this IQueryable<AttendentForMonth> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
            // .Include(x => x.xxx) // TODO: AbpHelper generated
            ;
    }
}
