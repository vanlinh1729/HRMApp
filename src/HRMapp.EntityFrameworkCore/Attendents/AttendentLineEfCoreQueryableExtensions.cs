using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Attendents;

public static class AttendentLineEfCoreQueryableExtensions
{
    public static IQueryable<AttendentLine> IncludeDetails(this IQueryable<AttendentLine> queryable, bool include = true)
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
