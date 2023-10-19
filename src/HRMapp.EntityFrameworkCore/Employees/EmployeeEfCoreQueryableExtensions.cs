using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Employees;

public static class EmployeeEfCoreQueryableExtensions
{
    public static IQueryable<Employee> IncludeDetails(this IQueryable<Employee> queryable, bool include = true)
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
