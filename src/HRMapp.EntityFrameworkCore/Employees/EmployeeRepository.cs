using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HRMapp.Employees;

public class EmployeeRepository : EfCoreRepository<HRMappDbContext, Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(IDbContextProvider<HRMappDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Employee>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
    public async Task<int> UpdateDepartment(Guid Id, Guid? newDepartmentId)
    {
        var dbContext = await GetDbContextAsync();
        if (newDepartmentId == null)
        {
            newDepartmentId = Guid.Empty;
        }
        return await dbContext.Employees.Where(x=> x.Id == Id)
            .ExecuteUpdateAsync(
                x => x.SetProperty(
                    b => b.DepartmentId, newDepartmentId));


        /*return  await dbContext.Database.ExecuteSqlRawAsync($"UPDATE 'Employees' SET DepartmentId = '{newDepartmentId}' WHERE Id = '{Id}'");*/
    }  
}