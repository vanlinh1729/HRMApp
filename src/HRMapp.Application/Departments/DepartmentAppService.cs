using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Contacts;
using HRMapp.Permissions;
using HRMapp.Departments.Dtos;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.AuditLogging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace HRMapp.Departments;


public class DepartmentAppService : CrudAppService<Department, DepartmentDto, Guid, DepartmentGetListInput, CreateUpdateDepartmentDto, CreateUpdateDepartmentDto>,
    IDepartmentAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Department.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Department.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Department.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Department.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Department.Delete;

    private readonly IDepartmentRepository _repository;
    private readonly IAuditLogRepository _auditLogRepository;
    private readonly IContactRepository _contactRepository;
    private readonly IEmployeeRepository _ownerRepository;
    private readonly IEmployeeHistoryRepository _employeeHistoryRepository
        ;

    public DepartmentAppService(IDepartmentRepository repository
        , IEmployeeRepository ownerRepository,
        IContactRepository contactRepository,
        IAuditLogRepository auditLogRepository,
        IEmployeeHistoryRepository employeeHistoryRepository) : base(repository)
    {
        _employeeHistoryRepository = employeeHistoryRepository;
        _auditLogRepository = auditLogRepository;
        _repository = repository;
        _ownerRepository = ownerRepository;
        _contactRepository = contactRepository;
    }

    /*protected override async Task<IQueryable<Department>> CreateFilteredQueryAsync(DepartmentGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.OwnerId != null, x => x.OwnerId == input.OwnerId)
            .WhereIf(input.ParentId != null, x => x.ParentId == input.ParentId)
            /*
            .WhereIf(input.Employees != null, x => x.Employees == input.Employees)
            #1#
            ;
    }*/
    [Authorize(HRMappPermissions.Department.Default)]
    public override async Task<PagedResultDto<DepartmentDto>> GetListAsync(DepartmentGetListInput input)
    {
        
        var queryable = await Repository.WithDetailsAsync(x=>x.Employees);
        var query = from department in queryable
            join owner in await _ownerRepository.GetQueryableAsync() on department.OwnerId equals owner.Id into
                departmentowner
            from owner in departmentowner.DefaultIfEmpty()
            join parent in await _repository.GetQueryableAsync() on department.ParentId equals parent.Id into
                departmentparent
            from parent in departmentparent.DefaultIfEmpty()
            select new
            {
                count = department.Employees.Count() ,
                department, OwnerName = owner.Name ?? string.Empty, ParentName = parent.Name ?? string.Empty
            };
       var listDepartment = query
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.department.Name.ToLower().Contains(input.Name.ToLower()))
            .WhereIf(!input.OwnerName.IsNullOrWhiteSpace(),
                x => x.OwnerName.ToLower().Contains(input.OwnerName.ToLower()))
            .WhereIf(!input.ParentName.IsNullOrWhiteSpace(),
                x => x.ParentName.ToLower().Contains(input.ParentName.ToLower()))
            .OrderBy((x) => NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount).ToList();
        /*
        var queryResult = await AsyncExecuter.ToListAsync(query);
        */
        var DepartmentDtos = listDepartment.Select(x =>
        {
            
            var DepartmentDtoDto = ObjectMapper.Map<Department, DepartmentDto>(x.department);
            DepartmentDtoDto.OwnerName = x.OwnerName;
            DepartmentDtoDto.ParentName = x.ParentName;
            DepartmentDtoDto.Count = x.count;
            return DepartmentDtoDto;
        }).ToList();
        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<DepartmentDto>(
            totalCount,
            DepartmentDtos
        );
    }

    public async Task<DepartmentDto> GetDepartmentDetail(Guid departmentId)
    {
        var queryable = await Repository.GetQueryableAsync();
        var query = from department in queryable
            join employee in await _ownerRepository.GetQueryableAsync() on department.Id equals employee.DepartmentId into
                departmentemployee
            from employee in departmentemployee.DefaultIfEmpty()
            join owner in await _ownerRepository.GetQueryableAsync() on department.OwnerId equals owner.Id into
                departmentowner
            from owner in departmentowner.DefaultIfEmpty()
            join parent in await _repository.GetQueryableAsync() on department.ParentId equals parent.Id into
                departmentparent
            from parent in departmentparent.DefaultIfEmpty()
            where (department.Id==departmentId)
            select new DepartmentDto
            {
                Id = department.Id,
                OwnerId = owner.Id,
                ParentId = parent.Id,
                Name = department.Name ?? String.Empty,
                OwnerName = owner.Name ?? String.Empty,
                ParentName = parent.Name?? String.Empty
            };

        var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
        return queryResult;
    }
    [Authorize(HRMappPermissions.Employee.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListOwnerAsync()
    {
        var obj = await _ownerRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    }

    [Authorize(HRMappPermissions.Department.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListParentAsync()
    {
        var obj = await _repository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Department>, List<SelectResultDto>>(obj));
    }
    public async Task<List<EmployeeWithName>> GetListEmployeeNameDepartment(Guid departmentId)
    {
        var employees = await _ownerRepository.GetQueryableAsync();
        var employeesDepartment = from employee in employees
            join department in await _repository.GetQueryableAsync() on employee.DepartmentId equals department.Id into
                emmployedepartment
            from departments in emmployedepartment.DefaultIfEmpty()
            where departments.Id == departmentId /*&& employee.Id != departments.OwnerId*/
            select new
            {
                employee.Id,
                EmployeeName = employee.Name
            };
        var resultObj = employeesDepartment.Select(x => new EmployeeWithName
        {
            Id = x.Id,
            Name = x.EmployeeName
        });
        return resultObj.ToList();
    }

    [UnitOfWork]
    public async Task<DepartmentDto> CreateDepartmentWithManyEmployeeAsync(CreateDepartmentAndAddEmployee input)
    {
        var department =  await _repository.InsertAsync(new Department(GuidGenerator.Create(), CurrentTenant.Id, input.Name, input.OwnerId,
            input.ParentId));
        var otherdepartment = (await _repository.GetQueryableAsync()).Where(x => x.OwnerId == input.OwnerId).Where(x=>x.Id != department.Id).ToList();
        foreach (var de in otherdepartment)
        {
            de.OwnerId = null;
            await _repository.UpdateAsync(de);
        }
        if (input.OwnerId != null)
        {
            var chiefOfDepartment = await _ownerRepository.GetAsync(input.OwnerId.GetValueOrDefault());
            chiefOfDepartment.DepartmentId = department.Id;
            await _ownerRepository.UpdateAsync(chiefOfDepartment);
        }
        List<Employee> employees = (await _ownerRepository.GetQueryableAsync())
            .Where(x => input.employeeId.Contains(x.Id)).ToList();        
        foreach (var em in employees)
        {
            em.DepartmentId = department.Id;
            await _ownerRepository.UpdateAsync(em);
            var newDepartmentName = (await _repository.GetQueryableAsync())
                .Where(x => x.Id == department.Id)
                .FirstOrDefault();

            if (newDepartmentName != null)
            {
                var departmentName = newDepartmentName.Name;
                var emHistory = new EmployeeHistory(GuidGenerator.Create(),CurrentTenant.Id,em.Id,DateTime.Now, DateTime.Now, "Điều chuyển phòng ban","TH Group", "Chuyển đến " +departmentName);
                await _employeeHistoryRepository.InsertAsync(emHistory);
            }
            else
            {
                
            }
        }
            

        return ObjectMapper.Map<Department,DepartmentDto>(department);
    } 

    [Authorize(HRMappPermissions.Department.Default)]
    public async Task<int> DepartmentCountAsync()
    {
        return await _repository.CountAsync();
    }
    [UnitOfWork]
    [Authorize(HRMappPermissions.Department.Update)]

    public async Task<string> UpdateDepartmentWithManyEmployeeAsync(Guid departmentId,CreateDepartmentAndAddEmployee input)
    {
        var department = await _repository.GetAsync(departmentId);
        var otherdepartment = (await _repository.GetQueryableAsync()).Where(x => x.OwnerId == input.OwnerId).Where(x=>x.Id != department.Id).ToList();
        foreach (var de in otherdepartment)
        {
            de.OwnerId = null;
            await _repository.UpdateAsync(de);
        }
        if (input.OwnerId != null)
        {
            var chiefOfDepartment = await _ownerRepository.GetAsync(input.OwnerId.GetValueOrDefault());
            var oldChief = await _ownerRepository.GetAsync(department.OwnerId.GetValueOrDefault());
            oldChief.EmployeePosition = EmployeePosition.Employee;
            chiefOfDepartment.DepartmentId = department.Id;
            await _ownerRepository.UpdateAsync(chiefOfDepartment);
            await _ownerRepository.UpdateAsync(oldChief);
        }
        if (department == null)
        {
            department.Name = input.Name;
            department.OwnerId = input.OwnerId;
            department.ParentId = input.ParentId;
            await _repository.UpdateAsync(department);
        }
        
  
        List<Employee> employeeinDepartment = (await _ownerRepository.GetQueryableAsync())
            .Where(x => x.DepartmentId== departmentId && !input.employeeId.Contains(x.Id)).ToList();
        List<Employee> employees = (await _ownerRepository.GetQueryableAsync())
            .Where(x => input.employeeId.Contains(x.Id)).ToList();
        /*if (departmentId != employees[0].DepartmentId)
        {*/
        foreach (var em in employees)
        {
            if (em.DepartmentId != departmentId)
            {
                em.DepartmentId = departmentId;
                var newDepartmentName = (await _repository.GetQueryableAsync()).Where(x=>x.Id == departmentId).First().Name;
                var emHistory = new EmployeeHistory(GuidGenerator.Create(),CurrentTenant.Id,em.Id,DateTime.Now, DateTime.Now, "Điều chuyển phòng ban","TH Group", "Chuyển đến " +newDepartmentName);
                await _employeeHistoryRepository.InsertAsync(emHistory);
            }
        }              
        foreach (var em in employeeinDepartment)
        {
            em.DepartmentId = null;
        }        
        employees.AddRange(employeeinDepartment);
        await _ownerRepository.UpdateManyAsync(employeeinDepartment);
        /*}*/
        

        // return ObjectMapper.Map<Department,DepartmentDto>(department);
        return "ok";
    }

    public async Task<List<DepartmentChangeOwnerDto>> GetDepartmentChangeListAsync(Guid departmentId)
    {
        var employee = await _ownerRepository.GetListAsync();
        var entityChange =
            await _auditLogRepository.GetEntityChangesWithUsernameAsync(departmentId.ToString(),
                "HRMapp.Departments.Department");
        var ownerchange = entityChange.Select(x => new
        {
            x.UserName,
            x.EntityChange.ChangeTime,
            /*NewValue = Guid.Empty,
            OriginalValue = Guid.Empty*/
            NewValue =
                !x.EntityChange.PropertyChanges
                    .Select(a => ParseToGuidCheckException(a.NewValue)).ToList().IsNullOrEmpty() ?x.EntityChange.PropertyChanges
                    .Select(a => ParseToGuidCheckException(a.NewValue)).ToList()[0]: Guid.Empty,
            OriginalValue =
                !x.EntityChange.PropertyChanges
                    .Select(a => ParseToGuidCheckException(a.OriginalValue)).ToList().IsNullOrEmpty()?x.EntityChange.PropertyChanges
                    .Select(a => ParseToGuidCheckException(a.OriginalValue)).ToList()[0]: Guid.Empty
        }).ToList();
        

        var result = ownerchange.Join
            (employee,
                x => x.NewValue,
                x => x.Id,
                (x, y) => new
                {
                    username = x.UserName, changetime = x.ChangeTime, newvalue = y.Name, originvalue = x.OriginalValue
                })
            .Join(employee, x => x.originvalue, x => x.Id,
                (x, y) => new DepartmentChangeOwnerDto
                    { UserName = x.username, ChangeTime = x.changetime, NewValue = x.newvalue, OriginalValue = y.Name })
            .ToList();
        return result;
    }
    
    public async Task<PagedResultDto<DepartmentWithDetailDto>> GetListUsersDepartment(DepartmentDetailById input)
    {
        var employees = await _ownerRepository.GetQueryableAsync();
        var employeesDepartment = from employee in employees
            join department in await _repository.GetQueryableAsync() on employee.DepartmentId equals department.Id into
                emmployedepartment
            from departments in emmployedepartment.DefaultIfEmpty()
            join contact in await _contactRepository.GetQueryableAsync() on employee.ContactId equals contact.Id into
                emmployedcontact
            from contacts in emmployedcontact.DefaultIfEmpty()
            /*into  departmentowner

            from  owner in departmentowner.DefaultIfEmpty()
            */
            where departments.Id == input.Id/* && departments.OwnerId != employee.Id*/
            select new
            {
                employee.Id,
                EmployeeName = employee.Name,
                EmployeePosition = employee.EmployeePosition,
                contacts.Email,
                contacts.PhoneNumber
            };
        var resultObj = employeesDepartment.Select(x => new DepartmentWithDetailDto
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            EmployeePosition = x.EmployeePosition,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber
        });
        /*.OrderBy(NormalizeSorting(input.Sorting))
        .Skip(input.SkipCount)
        .Take(input.MaxResultCount);;*/
        var result = resultObj.Skip(input.SkipCount)
            .Take(input.MaxResultCount).ToList();
        var totalcount = resultObj.Count();
        return new PagedResultDto<DepartmentWithDetailDto>(totalcount, result);
    }
     public async Task<PagedResultDto<DepartmentWithDetailDto>> GetListUsersDepartmentEdit(DepartmentDetailById input)
    {
      

        var employees = await _ownerRepository.GetQueryableAsync();
        var employeesDepartment = from employee in employees
            join department in await _repository.GetQueryableAsync() on employee.DepartmentId equals department.Id into
                emmployedepartment
            from departments in emmployedepartment.DefaultIfEmpty()
            join contact in await _contactRepository.GetQueryableAsync() on employee.ContactId equals contact.Id into
                emmployedcontact
            from contacts in emmployedcontact.DefaultIfEmpty()
            /*into  departmentowner
            
            from  owner in departmentowner.DefaultIfEmpty()
            */
            /*where input.Id != Guid.Empty
                ? employee.DepartmentId != input.Id && departments.OwnerId != employee.Id
                : 1 == 1*/
            select new
            {
                employee.Id,
                EmployeeName = employee.Name,
                EmployeePosition = employee.EmployeePosition,
                contacts.Email,
                contacts.PhoneNumber
            };

        var iQueryable = employeesDepartment
            .WhereIf(!input.EmployeeName.IsNullOrEmpty(),
                x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .Select(x => new DepartmentWithDetailDto
            {
                Id = x.Id,
                Email = x.Email,
                EmployeeName = x.EmployeeName,
                EmployeePosition = x.EmployeePosition,
                PhoneNumber = x.PhoneNumber
            });
        var totalcount = iQueryable.Count();

        var result = iQueryable
                .OrderBy((x) => x.EmployeeName)
                .Skip(input.SkipCount)
            .Take(input.MaxResultCount).ToList();
        ;

        return new PagedResultDto<DepartmentWithDetailDto>(totalcount, result);
    }

    public static Guid ParseToGuidCheckException(string guid)
    {
        var resultguid = Guid.Empty;
        try
        {
            resultguid = Guid.Parse(guid.Trim('"'));
        }
        catch (Exception e)
        {
            return resultguid;
        }

        return resultguid;
    }
    
    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"department.{nameof(Department.Id)}";
        }
        // custom contain sorting 
        if (sorting.Contains("OwnerName", StringComparison.OrdinalIgnoreCase) || sorting.Contains("count", StringComparison.OrdinalIgnoreCase) || sorting.Contains("ParentName", StringComparison.OrdinalIgnoreCase))
        {
            return sorting;
        }
        return $"department.{sorting}";
    }


}
