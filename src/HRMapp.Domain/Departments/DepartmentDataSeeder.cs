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

namespace HRMapp.Departments;

public class DepartmentDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IEmployeeHistoryRepository _employeeHistoryRepository;

    

    public DepartmentDataSeeder(
        IEmployeeRepository employeeRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IDepartmentRepository departmentRepository,
        IContactRepository contactRepository,
        IEmployeeHistoryRepository employeeHistoryRepository
    )
    {
        _employeeHistoryRepository = employeeHistoryRepository;
        _contactRepository = contactRepository;
        _employeeRepository = employeeRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _departmentRepository = departmentRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddDepartmentSeeder(context);
    }
    public async Task AddDepartmentSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _departmentRepository.GetCountAsync() > 0)
            {
                return;
            }
            var tenantId = context?.TenantId;
            List<Department> departments =new List<Department>();
            var department_it = new Department(_guidGenerator.Create(), tenantId, "Phòng IT", null, null);
            var department_hr = new Department(_guidGenerator.Create(), tenantId, "Phòng Nhân sự", null, null);
            var department_kt = new Department(_guidGenerator.Create(), tenantId, "Phòng Kế toán", null, null);
            var department_marketing = new Department(_guidGenerator.Create(), tenantId, "Phòng Marketing", null, null);
            var department_sales = new Department(_guidGenerator.Create(), tenantId, "Phòng Kinh doanh", null, null);
            var department_cs = new Department(_guidGenerator.Create(), tenantId, "Phòng CSKH", null, null);
            var department_rd = new Department(_guidGenerator.Create(), tenantId, "Phòng Nghiên cứu phát triển", null, null);
            var department_operations = new Department(_guidGenerator.Create(), tenantId, "Phòng Điều hành", null, null);
            departments.Add(department_it);
            departments.Add(department_hr);
            departments.Add(department_kt);
            departments.Add(department_marketing);
            departments.Add(department_sales);
            departments.Add(department_cs);
            departments.Add(department_rd);
            departments.Add(department_operations);
            await _departmentRepository.InsertManyAsync(departments);
        }
    }
   
}