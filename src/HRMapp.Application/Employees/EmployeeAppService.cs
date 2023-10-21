using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Contacts;
using HRMapp.Departments;
using HRMapp.Permissions;
using HRMapp.Employees.Dtos;
using HRMapp.Users;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace HRMapp.Employees;


public class EmployeeAppService : CrudAppService<Employee, EmployeeDto, Guid, EmployeeGetListInput, CreateUpdateEmployeeDto, CreateUpdateEmployeeDto>,
    IEmployeeAppService
{
     private readonly IDepartmentRepository _departmentRepository;
    private readonly IRepository<Contact, Guid> _contactRepository;
    private readonly IRepository<HrmUser, Guid> _userRepository;
    private readonly IRepository<IdentityUser, Guid> _user; 

    private readonly IEmployeeRepository _repository;

    public EmployeeAppService(IEmployeeRepository repository
        , IRepository<HrmUser, Guid> userRepository
        , IRepository<Contact, Guid> contactRepository
        , IDepartmentRepository departmentRepository
        ,IRepository<IdentityUser, Guid> user
    ) : base(repository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _contactRepository = contactRepository;
        _departmentRepository = departmentRepository;
        _user = user;
    }

    protected override string GetPolicyName { get; set; } = HRMappPermissions.Employee.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Employee.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Employee.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Employee.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Employee.Delete;

    //    // protected override async Task<IQueryable<Employee>> CreateFilteredQueryAsync(EmployeeGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //    //    //    //    //         .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
    //    //    //    //    //         .WhereIf(!input.OtherName.IsNullOrWhiteSpace(), x => x.OtherName.Contains(input.OtherName))
    //    //    //    //    //         .WhereIf(input.HrmUserId != null, x => x.HrmUserId == input.HrmUserId)
    //    //    //    //    //         .WhereIf(input.HrmContactId != null, x => x.HrmContactId == input.HrmContactId)
    //    //    //    //    //         .WhereIf(input.DepartmentId != null, x => x.DepartmentId == input.DepartmentId)
    //    //    //    //    //         .WhereIf(input.Status != null, x => x.Status == input.Status)
    //    //    //    //    //         .WhereIf(input.Active != null, x => x.Active == input.Active)
    //    //    //    //    //         .WhereIf(input.NumberTimeOf != null, x => x.NumberTimeOf == input.NumberTimeOf)
    //    //    //         ;
    // }
    //
    [Authorize(HRMappPermissions.Employee.Default)]
    public override async Task<PagedResultDto<EmployeeDto>> GetListAsync(EmployeeGetListInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from employee in queryable
            join user in await _user.GetQueryableAsync() on employee.UserId equals user.Id into
                employeeuser
            from user in employeeuser.DefaultIfEmpty()
            join contact in await _contactRepository.GetQueryableAsync() on employee.ContactId equals
                contact.Id into employeecontact
            from contact in employeecontact.DefaultIfEmpty()
            join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals
                department.Id into employeedepartment
            from department in employeedepartment.DefaultIfEmpty()
            select new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                OtherName = employee.OtherName,
                UserName = user.UserName,
                UserId = employee.UserId,
                ContactName = contact.Name,
                ContactId = employee.ContactId,
                DepartmentName = department.Name,
                DepartmentId = employee.DepartmentId,
                Status = employee.Status,
                Gender = contact.Gender,
                BirthDay = contact.BirthDay,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber
            };
        var listEmployee = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.ToLower().Contains(input.Name.ToLower()))
            .WhereIf(!input.OtherName.IsNullOrWhiteSpace(),
                x => x.OtherName.ToLower().Contains(input.OtherName.ToLower()))
            .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.ToLower().Contains(input.UserName.ToLower()))
            .WhereIf(!input.ContactName.IsNullOrWhiteSpace(),
                x => x.ContactName.ToLower().Contains(input.ContactName.ToLower()))
            .WhereIf(!input.DepartmentName.IsNullOrWhiteSpace(),
                x => x.DepartmentName.ToLower().Contains(input.DepartmentName.ToLower()))
            .WhereIf(!input.DepartmentName.IsNullOrWhiteSpace(),
                x => x.DepartmentName.ToLower().Contains(input.DepartmentName.ToLower()))
            .WhereIf(input.Status != null, x => x.Status == input.Status)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        
        
        var queryResult = await AsyncExecuter.ToListAsync(listEmployee);

        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<EmployeeDto>(
            totalCount,
            queryResult
        );
    }

    public async Task<string> UpdateDepartment(EmployeeInputUpdateOneFieldDto input)
    {
        var rowRecord = await _repository.UpdateDepartment(input.Id, input.DepartmentId);
        if (rowRecord != 0)
        {
            return input.DepartmentId.ToString();
        }

        return "";
    }

    public async Task<EmployeeDto> GetEmployeeDetail(Guid employeeId)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from employee in queryable
            where employee.Id == employeeId
            join user in await _userRepository.GetQueryableAsync() on employee.UserId equals user.Id into
                employeeuser
            from user in employeeuser.DefaultIfEmpty()
            join contact in await _contactRepository.GetQueryableAsync() on employee.ContactId equals
                contact.Id into employeecontact
            from contact in employeecontact.DefaultIfEmpty()
            join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals
                department.Id into employeedepartment
            from department in employeedepartment.DefaultIfEmpty()
            select new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                OtherName = employee.OtherName,
                UserName = user.UserName,
                UserId = employee.UserId,
                ContactName = contact.Name,
                ContactId = employee.ContactId,
                DepartmentName = department.Name,
                DepartmentId = employee.DepartmentId,
                Status = employee.Status,
              
                Gender = contact.Gender,
                BirthDay = contact.BirthDay,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber
            };
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
    }

    [Authorize(HRMappPermissions.Employee.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListHrmUserAsync()
    {
        var obj = await _user.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<IdentityUser>, List<SelectResultDto>>(obj));
    }
    public async Task<ListResultDto<SelectResultDto>> GetListHrmContactAsync()
    {
        var obj = await _contactRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Contact>, List<SelectResultDto>>(obj));
    }

    [Authorize(HRMappPermissions.Department.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListDepartmentAsync()
    {
        var obj = await _departmentRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Department>, List<SelectResultDto>>(obj));
    }

    [Authorize(HRMappPermissions.Employee.Update)]
    public override Task<EmployeeDto> GetAsync(Guid id)
    {
        return base.GetAsync(id);
    }

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"employee.{nameof(Employee.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("hrmUserName", StringComparison.OrdinalIgnoreCase))
        {
            return "hrmUserName";
        }

        if (sorting.Contains("hrmContactName", StringComparison.OrdinalIgnoreCase))
        {
            return "hrmContactName";
        }

        if (sorting.Contains("departmentName", StringComparison.OrdinalIgnoreCase))
        {
            return "departmentName";
        }

        return $"employee.{sorting}";
    }
}
