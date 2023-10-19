using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Departments;

public static class DepartmentEfCoreQueryableExtensions
{
    public static IQueryable<Department> IncludeDetails(this IQueryable<Department> queryable, bool include = true)
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
