using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Contracts;

public static class ContractEfCoreQueryableExtensions
{
    public static IQueryable<Contract> IncludeDetails(this IQueryable<Contract> queryable, bool include = true)
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
