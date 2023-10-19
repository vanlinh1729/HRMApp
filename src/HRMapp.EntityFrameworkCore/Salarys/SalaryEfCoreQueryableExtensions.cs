using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Salarys;

public static class SalaryEfCoreQueryableExtensions
{
    public static IQueryable<Salary> IncludeDetails(this IQueryable<Salary> queryable, bool include = true)
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
