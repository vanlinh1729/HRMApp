using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Employees;

public static class EmployeeHistoryEfCoreQueryableExtensions
{
    public static IQueryable<EmployeeHistory> IncludeDetails(this IQueryable<EmployeeHistory> queryable, bool include = true)
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
