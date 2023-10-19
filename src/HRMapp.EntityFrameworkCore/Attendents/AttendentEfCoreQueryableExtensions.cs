using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Attendents;

public static class AttendentEfCoreQueryableExtensions
{
    public static IQueryable<Attendent> IncludeDetails(this IQueryable<Attendent> queryable, bool include = true)
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
