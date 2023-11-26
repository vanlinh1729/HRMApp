using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Attendents;
using HRMapp.Contacts;
using HRMapp.Contracts;
using HRMapp.Departments;
using HRMapp.Salarys;
using HRMapp.Shifts;
using HRMapp.Users;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Users;
using HrmUser = HRMapp.Users.HrmUser;

namespace HRMapp.Employees;

public class EmployeeDataSeeder
    : IDataSeedContributor, ITransientDependency
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IEmployeeHistoryRepository _employeeHistoryRepository;
    private readonly IHrmUserRepository _userRepository;

    

    public EmployeeDataSeeder(
        IEmployeeRepository employeeRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IDepartmentRepository departmentRepository,
        IContactRepository contactRepository,
        IEmployeeHistoryRepository employeeHistoryRepository,
        IHrmUserRepository userRepository
    )
    {
        _userRepository = userRepository;
        _employeeHistoryRepository = employeeHistoryRepository;
        _contactRepository = contactRepository;
        _employeeRepository = employeeRepository;
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _departmentRepository = departmentRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await AddEmployeeSeeder(context);
        await AddEmployeeHistorySeeder(context);
    }
    
    public async Task AddEmployeeSeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _employeeRepository.GetCountAsync() > 0 /*|| await _departmentRepository.GetCountAsync() < 8*/)
            {
                return;
            }
            
            var department = await _departmentRepository.GetListAsync();
            var contact = await _contactRepository.GetListAsync();
            var tenantId = context?.TenantId;
            List<Employee> employee = new List<Employee>();
            var employee_it = new Employee(_guidGenerator.Create(), tenantId, "Đỗ Đức Hùng", "Không có", null, contact[0].Id,
                department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_1 = new Employee(_guidGenerator.Create(), tenantId, "Trần Vũ Hoàn", "Không có", null, contact[2].Id,
                department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_2 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Văn Lĩnh", "Không có", null, contact[3].Id,
                department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_3 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Minh Tuấn", "Không có", null, contact[4].Id,
                department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_4 = new Employee(_guidGenerator.Create(), tenantId, "Trần Quốc Vương", "Không có", null, contact[5].Id,
                department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_5 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Thị Hương", "Không có", null, contact[6].Id, department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_6 = new Employee(_guidGenerator.Create(), tenantId, "Lê Văn Duy", "Không có", null, contact[7].Id, department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_7 = new Employee(_guidGenerator.Create(), tenantId, "Phạm Thị Lan", "Không có", null, contact[8].Id, department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_8 = new Employee(_guidGenerator.Create(), tenantId, "Hoàng Minh Tuấn", "Không có", null, contact[9].Id, department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_it_9 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Thị Mai", "Không có", null, contact[10].Id, department[0].Id, StatusEmployee.Online, EmployeePosition.Employee);
            
            var employee_hr = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Đình Quyết", "Không có", null, contact[11].Id,
                department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_1 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Cao Lâm", "Không có", null, contact[12].Id,
                department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_2 = new Employee(_guidGenerator.Create(), tenantId, "Đặng Văn Quang", "Không có", null, contact[13].Id,
                department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_3 = new Employee(_guidGenerator.Create(), tenantId, "Trần Thị Diễm", "Không có", null, contact[14].Id,
                department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_4 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Hải Dương", "Không có", null, contact[15].Id,
                department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_5 = new Employee(_guidGenerator.Create(), tenantId, "Trần Thanh Tâm", "Không có", null, contact[16].Id, department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_6 = new Employee(_guidGenerator.Create(), tenantId, "Lê Thị Thu", "Không có", null, contact[17].Id, department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_7 = new Employee(_guidGenerator.Create(), tenantId, "Phạm Văn Hùng", "Không có", null, contact[18].Id, department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_8 = new Employee(_guidGenerator.Create(), tenantId, "Hoàng Đức Huy", "Không có", null, contact[19].Id, department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_hr_9 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Thị Thu Hà", "Không có", null, contact[20].Id, department[1].Id, StatusEmployee.Online, EmployeePosition.Employee);
            
            var employee_kt = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Thị Như Quỳnh", "Không có", null, contact[21].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt1 = new Employee(_guidGenerator.Create(), tenantId, "Lê Hoàng Quân", "Không có", null, contact[22].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt2 = new Employee(_guidGenerator.Create(), tenantId, "Đinh Văn Phượng", "Không có", null, contact[23].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt3 = new Employee(_guidGenerator.Create(), tenantId, "Lê Văn Minh Châu", "Không có", null, contact[24].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt4 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Xuân Sang", "Không có", null, contact[25].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt5 = new Employee(_guidGenerator.Create(), tenantId, "Lê Phú Quý", "Không có", null, contact[26].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt6 = new Employee(_guidGenerator.Create(), tenantId, "Lý Quốc Quyền", "Không có", null, contact[27].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt7 = new Employee(_guidGenerator.Create(), tenantId, "Bùi Minh Quân", "Không có", null, contact[28].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt8 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Ngọc Sơn", "Không có", null, contact[29].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_kt9 = new Employee(_guidGenerator.Create(), tenantId, "Bùi Duy Qúy", "Không có", null, contact[30].Id, department[2].Id, StatusEmployee.Online, EmployeePosition.Employee);
            
            var employee_marketing = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Vũ Ngọc Quyên", "Không có", null, contact[31].Id, department[3].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_marketing1 = new Employee(_guidGenerator.Create(), tenantId, "Trương Trọng Quân", "Không có", null, contact[32].Id, department[3].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_marketing2 = new Employee(_guidGenerator.Create(), tenantId, "Phạm Minh Quân", "Không có", null, contact[33].Id, department[3].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_marketing3 = new Employee(_guidGenerator.Create(), tenantId, "Võ Thanh Sanh", "Không có", null, contact[34].Id, department[3].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_marketing4 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Minh Phương", "Không có", null, contact[35].Id, department[3].Id, StatusEmployee.Online, EmployeePosition.Employee);

            var employee_sale = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Viết Sơn", "Không có", null, contact[36].Id, department[4].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_sale1 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Hoàng Hải Sơn", "Không có", null, contact[37].Id, department[4].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_sale2 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Hải Sơn", "Không có", null, contact[38].Id, department[4].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_sale3 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Vũ Trường Sơn", "Không có", null, contact[39].Id, department[4].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_sale4 = new Employee(_guidGenerator.Create(), tenantId, "Trần Minh Sang", "Không có", null, contact[40].Id, department[4].Id, StatusEmployee.Online, EmployeePosition.Employee);

            var employee_cs = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Trung Sơn", "Không có", null, contact[41].Id, department[5].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_cs1 = new Employee(_guidGenerator.Create(), tenantId, "Lê Quang", "Không có", null, contact[42].Id, department[5].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_cs2 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Thị Phương", "Không có", null, contact[43].Id, department[5].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_cs3 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Văn Sơn", "Không có", null, contact[44].Id, department[5].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_cs4 = new Employee(_guidGenerator.Create(), tenantId, "Lê Anh Sơn", "Không có", null, contact[45].Id, department[5].Id, StatusEmployee.Online, EmployeePosition.Employee);

            var employee_rd = new Employee(_guidGenerator.Create(), tenantId, "Võ Tiến Sĩ", "Không có", null, contact[46].Id, department[6].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_rd1 = new Employee(_guidGenerator.Create(), tenantId, "Phan Đức Sơn", "Không có", null, contact[47].Id, department[6].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_rd2 = new Employee(_guidGenerator.Create(), tenantId, "Trần Thế Sơn", "Không có", null, contact[48].Id, department[6].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_rd3 = new Employee(_guidGenerator.Create(), tenantId, "Vũ Hồng Tâm", "Không có", null, contact[49].Id, department[6].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_rd4 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Minh Tân", "Không có", null, contact[50].Id, department[6].Id, StatusEmployee.Online, EmployeePosition.Employee);

            var employee_operations = new Employee(_guidGenerator.Create(), tenantId, "Lê Hà Xuân Thái", "Không có", null, contact[51].Id, department[7].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_operations1 = new Employee(_guidGenerator.Create(), tenantId, "Nguyễn Ngọc Phương", "Không có", null, contact[52].Id, department[7].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_operations2 = new Employee(_guidGenerator.Create(), tenantId, "Bùi Anh Tài", "Không có", null, contact[53].Id, department[7].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_operations3 = new Employee(_guidGenerator.Create(), tenantId, "Dương Chí Tâm", "Không có", null, contact[54].Id, department[7].Id, StatusEmployee.Online, EmployeePosition.Employee);
            var employee_operations4 = new Employee(_guidGenerator.Create(), tenantId, "Phạm Thanh Quốc", "Không có", null, contact[55].Id, department[7].Id, StatusEmployee.Online, EmployeePosition.Employee);

            employee.Add(employee_it);
            employee.Add(employee_it_1);
            employee.Add(employee_it_2);
            employee.Add(employee_it_3);
            employee.Add(employee_it_4);
            employee.Add(employee_hr);
            employee.Add(employee_hr_1);
            employee.Add(employee_hr_2);
            employee.Add(employee_hr_3);
            employee.Add(employee_hr_4);
            // For the HR department
            employee.Add(employee_hr_5);
            employee.Add(employee_hr_6);
            employee.Add(employee_hr_7);
            employee.Add(employee_hr_8);
            employee.Add(employee_hr_9);

// For the IT department
            employee.Add(employee_it_5);
            employee.Add(employee_it_6);
            employee.Add(employee_it_7);
            employee.Add(employee_it_8);
            employee.Add(employee_it_9);

// For the KT department
            employee.Add(employee_kt);
            employee.Add(employee_kt1);
            employee.Add(employee_kt2);
            employee.Add(employee_kt3);
            employee.Add(employee_kt4);
            employee.Add(employee_kt5);
            employee.Add(employee_kt6);
            employee.Add(employee_kt7);
            employee.Add(employee_kt8);
            employee.Add(employee_kt9);

// For the Marketing department
            employee.Add(employee_marketing);
            employee.Add(employee_marketing1);
            employee.Add(employee_marketing2);
            employee.Add(employee_marketing3);
            employee.Add(employee_marketing4);

// For the Sales department
            employee.Add(employee_sale);
            employee.Add(employee_sale1);
            employee.Add(employee_sale2);
            employee.Add(employee_sale3);
            employee.Add(employee_sale4);

// For the Customer Service department
            employee.Add(employee_cs);
            employee.Add(employee_cs1);
            employee.Add(employee_cs2);
            employee.Add(employee_cs3);
            employee.Add(employee_cs4);

// For the Research and Development department
            employee.Add(employee_rd);
            employee.Add(employee_rd1);
            employee.Add(employee_rd2);
            employee.Add(employee_rd4);
            employee.Add(employee_rd3);

// For the Operations department
            employee.Add(employee_operations);
            employee.Add(employee_operations1);
            employee.Add(employee_operations2);
            employee.Add(employee_operations4);
            employee.Add(employee_operations3);


            await _employeeRepository.InsertManyAsync(employee);
        }
    }
    public async Task AddEmployeeHistorySeeder(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (await _employeeHistoryRepository.GetCountAsync() > 0)
            {
                return;
            }
            var tenantId = context?.TenantId;
            var employees = await _employeeRepository.GetListAsync();
            List<EmployeeHistory> employeeHistories =new List<EmployeeHistory>();

            foreach (var employee in employees)
            {
                Random random = new Random();
                DateTime startDate = new DateTime(2022, 04, 04);
                DateTime endDate = new DateTime(2023, 6, 01);
                int range = (endDate - startDate).Days;
                
                DateTime randomDate = startDate.AddDays(random.Next(range));
                var employeehistory1 = new EmployeeHistory(
                    _guidGenerator.Create(),
                    tenantId,
                    employee.Id,
                    new DateTime(2020, 07, 15),
                    new DateTime(2022, 04, 01),
                    "Part-time Freelancer",
                    "*",
                    "Tham gia nhiều dự án freelance cho các công ty nước ngoài."
                ); 
                var employeehistory2 = new EmployeeHistory(
                    _guidGenerator.Create(),
                    tenantId,
                    employee.Id,
                    randomDate,
                    randomDate,
                    "Nhân viên",
                    "THG",
                    "Gia nhập công ty THG."
                );  
               
                employeeHistories.Add(employeehistory1);
                employeeHistories.Add(employeehistory2);
            }
            await _employeeHistoryRepository.InsertManyAsync(employeeHistories);
        }
    }
   

}