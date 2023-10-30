using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Contacts;
using HRMapp.Contracts;
using HRMapp.Departments;
using HRMapp.Employees;
using HRMapp.Salarys;
using HRMapp.Shifts;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace HRMapp.Salarys;

public class SalaryDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;

    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;

    private readonly IAttendentForMonthRepository _attendentForMonthRepository;

    private readonly ISalaryRepository _salaryRepository;


    public SalaryDataSeeder(
        IEmployeeRepository employeeRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IAttendentForMonthRepository attendentForMonthRepository,
        ISalaryRepository salaryRepository
    )
    {
        _attendentForMonthRepository = attendentForMonthRepository;

        _salaryRepository = salaryRepository;

        _employeeRepository = employeeRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddSalarySeeder(context);
    }

    public async Task AddSalarySeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _salaryRepository.GetCountAsync() > 0)
            {
                return;
            }

            var tenantId = context?.TenantId;
            var employees = await _employeeRepository.GetListAsync();
            List<Salary> salaries = new List<Salary>();

            foreach (var employee in employees)
            {
                var att4m =
                    await _attendentForMonthRepository.GetListAsync(x => x.EmployeeId == employee.Id);
                foreach (var att in att4m)
                {
                    var salary = new Salary(
                        _guidGenerator.Create(),
                        tenantId,
                        employee.Id,
                        att.Id,
                        450000
                    );
                    salaries.Add(salary);
                }
            }

            await _salaryRepository.InsertManyAsync(salaries);
        }
    }
}