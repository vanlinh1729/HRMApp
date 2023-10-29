using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents;
using HRMapp.Attendents.Dtos;
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

    private readonly IAttendentForMonthRepository _repository;

    public AttendentForMonthAppService(IAttendentRepository attendentRepository,
    IAttendentLineRepository attendentLineRepository,
    IAttendentForMonthRepository repository,
        IEmployeeRepository employeeRepository) : base(repository)
    {
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
                EmployeeName = employeeinAttendentForMonths.Name,
                EmployeeId = AttendentForMonth.EmployeeId,
                Month = AttendentForMonth.Month,
                Count = AttendentForMonth.Count
            };
    
        query = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(input.Month != null, x => x.Month.ToString("MM-yyyy") == input.Month.GetValueOrDefault().ToString("MM-yyyy"))
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
        var totalCount = await AsyncExecuter.CountAsync(query);
        return new PagedResultDto<AttendentForMonthDto>(
            totalCount,
            AttendentForMonthDtos
        );
       
    }

    [UnitOfWork]
    [Authorize(HRMappPermissions.AttendentForMonth.Create)]
    public async override Task<AttendentForMonthDto> CreateAsync(CreateUpdateAttendentForMonthDto input)
    {
        var attendentLinesCount = (from a in await _attendentRepository.GetQueryableAsync()
            join al in await _attendentLineRepository.GetQueryableAsync() on a.Id equals al.AttendentId
            where a.EmployeeId == input.EmployeeId &&
                  a.Date.Year == input.Month.Year &&
                  a.Date.Month == input.Month.Month 
            select al).Count();
        var attendentformonth = (await _repository.GetQueryableAsync()).Where(x => x.EmployeeId == input.EmployeeId)
            .Where(x => x.Month.Month == input.Month.Month).Where(x=>x.Month.Year==input.Month.Year).ToList().FirstOrDefault();
        if (attendentformonth != null)
        {
            attendentformonth.Count = attendentLinesCount;
            await _repository.UpdateAsync(attendentformonth);
        }
        else
        {
            await _repository.InsertAsync(new AttendentForMonth(GuidGenerator.Create(), CurrentTenant.Id, input.EmployeeId,
                input.Month, attendentLinesCount));
        }
       
        return new AttendentForMonthDto();

    }

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

}
