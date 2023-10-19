using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HRMapp.Contacts;

public static class ContactEfCoreQueryableExtensions
{
    public static IQueryable<Contact> IncludeDetails(this IQueryable<Contact> queryable, bool include = true)
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
