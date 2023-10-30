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

namespace HRMapp.Contracts;

public class ContractDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IContractRepository _contractRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;

    

    public ContractDataSeeder(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IContractRepository contractRepository,
        IEmployeeRepository employeeRepository
        
    )
    {
        _employeeRepository = employeeRepository;
        _contractRepository = contractRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddContractSeeder(context);
    }
    
    public async Task AddContractSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _contractRepository.GetCountAsync() > 0)
            {
                return;
            }
            var tenantId = context?.TenantId;
            var employees = await _employeeRepository.GetListAsync();
            List<Contract> contracts =new List<Contract>();

            foreach (var employee in employees)
            {
                // Create a contract for each employee
                var contract = new Contract(
                    _guidGenerator.Create(),
                    tenantId,
                    employee.Id, // Assuming employee ID is used
                    TimeContract.OneYear, // Example: Set the contract type
                    DateTime.UtcNow, // Example: Set the sign date to the current time
                    150000.0m // Example: Set a default salary coefficient
                );
                contracts.Add(contract);
            }
            await _contractRepository.InsertManyAsync(contracts);
        }
    }
}