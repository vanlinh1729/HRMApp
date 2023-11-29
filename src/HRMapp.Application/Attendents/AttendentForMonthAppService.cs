using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
using HRMapp.Departments;
using HRMapp.Employees;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace HRMapp.AttendentForMonths;


public class AttendentForMonthAppService : CrudAppService<AttendentForMonth, AttendentForMonthDto, Guid, AttendentForMonthGetListInput, CreateUpdateAttendentForMonthDto, CreateUpdateAttendentForMonthDto>,
    IAttendentForMonthAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Delete;

    private readonly IEmployeeRepository _employeeRepository;
    private readonly IAttendentRepository _attendentRepository;
    private readonly IAttendentLineRepository _attendentLineRepository;
    private readonly IDepartmentRepository _departmentRepository;

    private readonly IAttendentForMonthRepository _repository;

    public AttendentForMonthAppService(IAttendentRepository attendentRepository,
        IDepartmentRepository departmentRepository,
    IAttendentLineRepository attendentLineRepository,
    IAttendentForMonthRepository repository,
        IEmployeeRepository employeeRepository) : base(repository)
    {
        _departmentRepository = departmentRepository;
        _attendentLineRepository = attendentLineRepository;
        _attendentRepository = attendentRepository;
        _employeeRepository = employeeRepository;
        _repository = repository;
    }

    // protected override async Task<IQueryable<AttendentForMonth>> CreateFilteredQueryAsync(AttendentForMonthGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
    //         .WhereIf(input.Month != null, x => x.Month == input.Month)
    //         ;
    // }
    
    
     [Authorize(HRMappPermissions.AttendentForMonth.Default)]
    public override async Task<PagedResultDto<AttendentForMonthDto>> GetListAsync(AttendentForMonthGetListInput input)
    {
        

        var queryable = await _repository.GetQueryableAsync();
        var query = from AttendentForMonth in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on AttendentForMonth.EmployeeId equals
                employee.Id into employeeinAttendentForMonth
            from employeeinAttendentForMonths in employeeinAttendentForMonth.DefaultIfEmpty()
            
            select new AttendentForMonthDto()
            {
                Id = AttendentForMonth.Id,
                EmployeeName = employeeinAttendentForMonths.Name,
                EmployeeId = AttendentForMonth.EmployeeId,
                Month = AttendentForMonth.Month,
                Count = AttendentForMonth.Count
            };
    
        query = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(input.Month != null, x => x.Month.Month == input.Month.GetValueOrDefault().Month && x.Month.Year == input.Month.GetValueOrDefault().Year)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        var AttendentForMonthDtos = queryResult.Select(x => new AttendentForMonthDto()
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            Month = x.Month,
            Count = x.Count
           
        }).ToList();
        var totalCount = await _repository.CountAsync();
        return new PagedResultDto<AttendentForMonthDto>(
            totalCount,
            AttendentForMonthDtos
        );
       
    }


    [Authorize(HRMappPermissions.AttendentForMonth.Default)]
    public async Task<AttendentForMonthDto> GetAttendentForMonthDetail(Guid att4mId)
    {
        
        var queryable = await _repository.GetQueryableAsync();
        var query =from AttendentForMonth in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on AttendentForMonth.EmployeeId equals
                employee.Id into employeeinAttendentForMonth
            from employeeinAttendentForMonths in employeeinAttendentForMonth.DefaultIfEmpty()
            join department in await _departmentRepository.GetQueryableAsync() on employeeinAttendentForMonths.DepartmentId equals department.Id
                into departmentEmployee
            from departmentEmployees in departmentEmployee.DefaultIfEmpty() 
            where AttendentForMonth.Id == att4mId
            select new AttendentForMonthDto()
            {
                Id = AttendentForMonth.Id,
                EmployeeName = employeeinAttendentForMonths.Name,
                DepartmentName = departmentEmployees.Name,
                EmployeeId = AttendentForMonth.EmployeeId,
                Month = AttendentForMonth.Month,
                Count = AttendentForMonth.Count
            };
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
       
    }

    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Create)]
    public async override Task<AttendentForMonthDto> CreateAsync(CreateUpdateAttendentForMonthDto input)
    {
        var attendentLinesCount = (float)(from a in await _attendentRepository.GetQueryableAsync()
            join al in await _attendentLineRepository.GetQueryableAsync() on a.Id equals al.AttendentId
            where a.EmployeeId == input.EmployeeId &&
                  a.Date.Year == input.Month.Year &&
                  a.Date.Month == input.Month.Month 
            select al).Count();
        var attendentformonth = (await _repository.GetQueryableAsync()).Where(x => x.EmployeeId == input.EmployeeId)
            .Where(x => x.Month.Month == input.Month.Month).Where(x=>x.Month.Year==input.Month.Year).ToList().FirstOrDefault();
        if (attendentformonth != null)
        {
            attendentformonth.Count = (attendentLinesCount /4);
            await _repository.UpdateAsync(attendentformonth);
        }
        else
        {
            await _repository.InsertAsync(new AttendentForMonth(GuidGenerator.Create(), CurrentTenant.Id, input.EmployeeId,
                input.Month, attendentLinesCount/4));
        }
       
        return new AttendentForMonthDto();

    }
    
    

    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Create)]
    public async Task<AttendentForMonthDto> CreateManyAttendentForMonthAsync(CreateManyAttendentForMonthDto input)
    {
        var employees = await _employeeRepository.GetListAsync();
        foreach (var em in employees)
        {
            var attendentLinesCount = (float)(from a in await _attendentRepository.GetQueryableAsync()
                join al in await _attendentLineRepository.GetQueryableAsync() on a.Id equals al.AttendentId
                where a.EmployeeId == em.Id &&
                      a.Date.Year == input.Month.Year &&
                      a.Date.Month == input.Month.Month 
                select al).Count();
            var attendentformonth = (await _repository.GetQueryableAsync()).Where(x => x.EmployeeId == em.Id)
                .Where(x => x.Month.Month == input.Month.Month).Where(x=>x.Month.Year==input.Month.Year).ToList().FirstOrDefault();
            if (attendentformonth != null)
            {
                attendentformonth.Count = (attendentLinesCount /4);
                await _repository.UpdateAsync(attendentformonth);
            }
            else
            {
                await _repository.InsertAsync(new AttendentForMonth(GuidGenerator.Create(), CurrentTenant.Id, em.Id,
                    input.Month, attendentLinesCount/4));
            }
            
        }
        
       
        return new AttendentForMonthDto();

    }
    
    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Default)]
    public async Task<AllAttendentForMonthDto> GetListManyAttendentForMonthAsync(CreateManyAttendentForMonthDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from AttendentForMonth in queryable
            join employee in await _employeeRepository.GetQueryableAsync() on AttendentForMonth.EmployeeId equals
                employee.Id into employeeinAttendentForMonth
            from employeeinAttendentForMonths in employeeinAttendentForMonth.DefaultIfEmpty()
            join department in await _departmentRepository.GetQueryableAsync() on employeeinAttendentForMonths.DepartmentId equals department.Id
            into departmentEmployee
            from departmentEmployees in departmentEmployee.DefaultIfEmpty() 
            where AttendentForMonth.Month.Month == input.Month.Month && AttendentForMonth.Month.Year == input.Month.Year
            select new AttendentForMonthDto()
        {
            Id = AttendentForMonth.Id,
            EmployeeName = employeeinAttendentForMonths.Name,
            DepartmentName = departmentEmployees.Name,
            EmployeeId = AttendentForMonth.EmployeeId,
            Month = AttendentForMonth.Month,
            Count = AttendentForMonth.Count
        };
        var queryResult = await AsyncExecuter.ToListAsync(query);
        var result = new AllAttendentForMonthDto();
        result.ListAttendentForMonth = queryResult;
        return result;
    }
    
    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Default)]
    public async Task<List<AttendentForMonthDetailDto>> GetListAttendentForMonthDetailAsync(AttendentForMonthDetailInputDto input)
    {
       
        var query = from attendent in await _attendentRepository.WithDetailsAsync(x => x.AttendentLines)
            join employee in await _employeeRepository.GetQueryableAsync() on attendent.EmployeeId equals employee.Id
                into attendentemployee
            from employees in attendentemployee.DefaultIfEmpty()
            join de in await _departmentRepository.GetQueryableAsync() on employees.DepartmentId equals de.Id
            where attendent.Date.Month == input.Date.Month && attendent.Date.Year == input.Date.Year 
            select new
            {
                Id =  attendent.Id,
                EmployeeName = employees.Name?? string.Empty,
                EmployeeId = employees.Id,
                DepartmentName = de.Name,
                EmployeePosition = employees.EmployeePosition,
                Date = attendent.Date,
                MissingIn = 
                attendent.MissingIn,
                MissingOut = attendent.MissingOut,
                attendent.AttendentLines,
                CountAtt = attendent.AttendentLines.Count
            };
       
        query = query
            .OrderBy(x => x.Date);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        
        var daysInMonth = DateTime.DaysInMonth(input.Date.Year, input.Date.Month);

        int attLinesPerDay = 4;

        var AttendentForMonthDetailDtos = queryResult.GroupBy(x=>new {x.EmployeeId, x.EmployeeName, x.DepartmentName }).Select(x => new AttendentForMonthDetailDto()
        {
            EmployeeName = x.Key.EmployeeName,
            EmployeePosition=x.Select(x=>x.EmployeePosition).FirstOrDefault(),
            DepartmentName = x.Key.DepartmentName,
            EmployeeId = x.Key.EmployeeId,
            CountAtt = x.SelectMany(x=>x.AttendentLines).Count()/(decimal)attLinesPerDay,
            DayCheckDtos = Enumerable.Range(1, daysInMonth)
                .Select(day => new DayCheckDto
                {
                    DayInMonth = day,
                    TotalInDay = x
                        .SelectMany(x => x.AttendentLines)
                        .Count(line => line.TimeCheck.Day == day) / (decimal)attLinesPerDay
                }).ToList(),
            AttendentLines = ObjectMapper.Map<List<AttendentLine>, List<AttendentLineDto>>(
                x.SelectMany(x => x.AttendentLines).ToList()
            )
        }).ToList();

        return AttendentForMonthDetailDtos;
    }

    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Default)]
    public async Task<List<AttendentForMonthDetailDto>> GetListAttendentForMonthForDepartmentDetailAsync(AttendentForMonthDetailForDepartmentInputDto input)
    {
       
        var query = from attendent in await _attendentRepository.WithDetailsAsync(x => x.AttendentLines)
            join employee in await _employeeRepository.GetQueryableAsync() on attendent.EmployeeId equals employee.Id
                into attendentemployee
            from employees in attendentemployee.DefaultIfEmpty()
            join de in await _departmentRepository.GetQueryableAsync() on employees.DepartmentId equals de.Id
            where attendent.Date.Month == input.Date.Month && attendent.Date.Year == input.Date.Year && input.DepartmentName == de.Name
            select new
            {
                Id =  attendent.Id,
                EmployeeName = employees.Name?? string.Empty,
                EmployeeId = employees.Id,
                DepartmentName = de.Name,
                EmployeePosition = employees.EmployeePosition,
                Date = attendent.Date,
                MissingIn = 
                attendent.MissingIn,
                MissingOut = attendent.MissingOut,
                attendent.AttendentLines,
                CountAtt = attendent.AttendentLines.Count
            };
       
        query = query
            .OrderBy(x => x.Date);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        
        var daysInMonth = DateTime.DaysInMonth(input.Date.Year, input.Date.Month);

        int attLinesPerDay = 4;

        var AttendentForMonthDetailDtos = queryResult.GroupBy(x=>new {x.EmployeeId, x.EmployeeName, x.DepartmentName }).Select(x => new AttendentForMonthDetailDto()
        {
            EmployeeName = x.Key.EmployeeName,
            EmployeePosition=x.Select(x=>x.EmployeePosition).FirstOrDefault(),
            DepartmentName = x.Key.DepartmentName,
            EmployeeId = x.Key.EmployeeId,
            CountAtt = x.SelectMany(x=>x.AttendentLines).Count()/(decimal)attLinesPerDay,
            DayCheckDtos = Enumerable.Range(1, daysInMonth)
                .Select(day => new DayCheckDto
                {
                    DayInMonth = day,
                    TotalInDay = x
                        .SelectMany(x => x.AttendentLines)
                        .Count(line => line.TimeCheck.Day == day) / (decimal)attLinesPerDay
                }).ToList(),
            AttendentLines = ObjectMapper.Map<List<AttendentLine>, List<AttendentLineDto>>(
                x.SelectMany(x => x.AttendentLines).ToList()
            )
        }).ToList();

        return AttendentForMonthDetailDtos;
    }

    /*
    public async Task<List<AttendentForMonthDetailDto>> GetListAttendentForMonthDetailAsync(AttendentForMonthDetailInputDto input)
{
    try
    {
        var employeesQuery = await _employeeRepository.GetQueryableAsync();

        var query = from employee in employeesQuery
                    join attendent in (await _attendentRepository.WithDetailsAsync(x => x.AttendentLines))
                        on employee.Id equals attendent.EmployeeId into attendentGroup
                    from attendent in attendentGroup.DefaultIfEmpty()
                    join department in await _departmentRepository.GetQueryableAsync() on employee.DepartmentId equals department.Id
                    where (attendent == null || (attendent.Date.Month == input.Date.Month && attendent.Date.Year == input.Date.Year))
                    select new
                    {
                        Id = attendent?.Id,
                        EmployeeName = employee.Name ?? string.Empty,
                        EmployeeId = employee.Id,
                        DepartmentName = department?.Name,
                        Date = attendent?.Date,
                        MissingIn = attendent?.MissingIn,
                        MissingOut = attendent?.MissingOut,
                        AttendentLines = attendent?.AttendentLines
                    };

        query = query.OrderBy(x => x.Date);
        var queryResult = await AsyncExecuter.ToListAsync(query);

        var daysInMonth = DateTime.DaysInMonth(input.Date.Year, input.Date.Month);
        int attLinesPerDay = 4;

        var AttendentForMonthDetailDtos = queryResult.GroupBy(x => x.EmployeeId).Select(group => new AttendentForMonthDetailDto()
        {
            EmployeeName = group.First().EmployeeName,
            EmployeeId = group.First().EmployeeId,
            Date = group.First().Date ?? DateTime.MinValue,
            DepartmentName = group.First().DepartmentName,
            CountAtt = group.First().AttendentLines?.Count / attLinesPerDay ?? 0,
            DayCheckDtos = Enumerable.Range(1, daysInMonth)
                .Select(day => new DayCheckDto
                {
                    DayInMonth = day,
                    TotalInDay = group.SelectMany(x => x.AttendentLines)
                        .Where(line => line.?.Day == day)
                        .Count() / (decimal)attLinesPerDay
                }).ToList(),
            AttendentLines = ObjectMapper.Map<List<AttendentLine>, List<AttendentLineDto>>(group.SelectMany(x => x.AttendentLines).ToList())
        }).ToList();

        return AttendentForMonthDetailDtos;
    }
    catch (Exception ex)
    {
        // Handle exceptions
        Console.WriteLine($"An error occurred: {ex.Message}");
        throw;
    }
}
*/

    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"attendentForMonth.{nameof(AttendentForMonth.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
        {
            return "employeeName";
        }
        return $"attendentForMonth.{sorting}";
    }
    
    [Authorize(HRMappPermissions.Employee.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync()
    {
        var obj = await _employeeRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    } 
    public async Task<ListResultDto<SelectResultDto>> GetListDepartmentAsync()
    {
        var obj = await _departmentRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Department>, List<SelectResultDto>>(obj));
    }

}
