using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents.Dtos;
using HRMapp.Employees;
using HRMapp.Shifts;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public class AttendentAppService : CrudAppService<Attendent, AttendentDto, Guid, AttendentGetListInput, CreateUpdateAttendentDto, CreateUpdateAttendentDto>,
    IAttendentAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Attendent.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Attendent.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Attendent.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Attendent.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Attendent.Delete;

    private readonly IAttendentLineRepository _attendentLineRepository;
    private readonly IEmployeeRepository _employeeRepository;

    private readonly IAttendentRepository _repository;
    private readonly IShiftRepository _shiftRepository;

    public AttendentAppService(IAttendentRepository repository
        , IEmployeeRepository employeeRepository,
        IAttendentLineRepository attendentLineRepository,
        IShiftRepository shiftRepository
    ) : base(repository)
    {
        _shiftRepository = shiftRepository;
        _attendentLineRepository = attendentLineRepository;
        _repository = repository;
        _employeeRepository = employeeRepository;
    }

    // protected override async Task<IQueryable<Attendent>> CreateFilteredQueryAsync(AttendentGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         /*
    //         .WhereIf(input.Date != null, x => x.Date == input.Date)
    //         */
    //         .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
    //         .WhereIf(input.MissingIn != null, x => x.MissingIn == input.MissingIn)
    //         .WhereIf(input.MissingOut != null, x => x.MissingOut == input.MissingOut)
    //         /*
    //         .WhereIf(input.AttendentLines != null, x => x.AttendentLines == input.AttendentLines)
    //         */
    //         ;
    // }
    
    
     [Authorize(HRMappPermissions.Attendent.Default)]
    public override async Task<PagedResultDto<AttendentDto>> GetListAsync(AttendentGetListInput input)
    {
        var query = from attendent in await Repository.WithDetailsAsync(x => x.AttendentLines)
            join employee in await _employeeRepository.GetQueryableAsync() on attendent.EmployeeId equals employee.Id
                into attendentemployee
            from employees in attendentemployee.DefaultIfEmpty()
            select new
            {
                attendent.Id,
                attendent.Date,
                attendent.MissingIn,
                attendent.MissingOut,
                attendent.AttendentLines,
                EmployeeName = employees.Name ?? string.Empty
            };
            query = query
                .WhereIf(!input.Datetime.IsNullOrEmpty()
                    , x => x.Date >= DateTimeformatCustom.DateRangeToDateTime(input.Datetime)[0]
                           && x.Date <= DateTimeformatCustom.DateRangeToDateTime(input.Datetime)[1])
                /*
                .WhereIf(input.Start != null && input.End != null, x=>x.attendent.Date >= input.Start && x.attendent.Date <= input.End )
                */
                .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
                .WhereIf(input.MissingIn != null, x => x.MissingIn == input.MissingIn)
                .WhereIf(input.MissingOut != null, x => x.MissingOut == input.MissingOut)
                .OrderBy(x=>NormalizeSorting(input.Sorting))
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(query);
        var AttendentDtos = queryResult.Select(x => new AttendentDto
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            Date = x.Date,
            MissingIn = x.MissingIn,
            MissingOut = x.MissingOut,
            AttendentLines = ObjectMapper.Map<List<AttendentLine>, List<AttendentLineDto>>(x.AttendentLines.ToList())
        }).ToList();
        var totalCount = await AsyncExecuter.CountAsync(query);
        return new PagedResultDto<AttendentDto>(
            totalCount,
            AttendentDtos
        );
    }
    
    [Authorize(HRMappPermissions.Employee.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListEmployeeAsync()
    {
        var obj = await _employeeRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Employee>, List<SelectResultDto>>(obj));
    }
    
    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"attendentLine.{nameof(AttendentLine.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("employeeName", StringComparison.OrdinalIgnoreCase))
        {
            return "employeeName";
        }
        return $"attendent.{sorting}";
    }
    
}
