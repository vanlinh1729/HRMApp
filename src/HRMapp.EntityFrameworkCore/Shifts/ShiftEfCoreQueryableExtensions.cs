using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Shifts;

public static class ShiftEfCoreQueryableExtensions
{
    public static IQueryable<Shift> IncludeDetails(this IQueryable<Shift> queryable, bool include = true)
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
