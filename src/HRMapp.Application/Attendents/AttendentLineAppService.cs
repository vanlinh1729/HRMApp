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


public class AttendentLineAppService : CrudAppService<AttendentLine, AttendentLineDto, Guid, AttendentLineGetListInput, CreateUpdateAttendentLineDto, CreateUpdateAttendentLineDto>,
    IAttendentLineAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.AttendentLine.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.AttendentLine.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.AttendentLine.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.AttendentLine.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.AttendentLine.Delete;

    private readonly IAttendentLineRepository _repository;
    private readonly IShiftRepository _shiftRepository;
    private readonly IAttendentRepository _attendentRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AttendentLineAppService(IAttendentLineRepository repository,IShiftRepository shiftRepository
        ,IAttendentRepository attendentRepository,
        IEmployeeRepository employeeRepository) : base(repository)
    {
        _employeeRepository = employeeRepository;
        _shiftRepository = shiftRepository;
        _attendentRepository = attendentRepository;
        _repository = repository;
    }

    // protected override async Task<IQueryable<AttendentLine>> CreateFilteredQueryAsync(AttendentLineGetListInput input)
    // {
    //     // TODO: AbpHelper generated
    //     return (await base.CreateFilteredQueryAsync(input))
    //         .WhereIf(input.AttendentId != null, x => x.AttendentId == input.AttendentId)
    //         .WhereIf(input.TimeCheck != null, x => x.TimeCheck == input.TimeCheck)
    //         .WhereIf(input.Type != null, x => x.Type == input.Type)
    //         .WhereIf(input.ShiftId != null, x => x.ShiftId == input.ShiftId)
    //         /*.WhereIf(input.TimeMissingIn != null, x => x.TimeMissingIn == input.TimeMissingIn)
    //         .WhereIf(input.TimeMissingOut != null, x => x.TimeMissingOut == input.TimeMissingOut)*/
    //         ;
    // }
    
    
    [Authorize(HRMappPermissions.AttendentLine.Default)]
    public override async Task<PagedResultDto<AttendentLineDto>> GetListAsync(AttendentLineGetListInput input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from AttendentLine in queryable
            join shift in await _shiftRepository.GetQueryableAsync() on AttendentLine.ShiftId equals
                shift.Id into shiftAttendentLine
            from shiftAttendentLines in shiftAttendentLine.DefaultIfEmpty()
            join attendent in await _attendentRepository.GetQueryableAsync() on AttendentLine.AttendentId equals 
                attendent.Id into attendentAttendentLine
            from attendentAttendentLines in attendentAttendentLine.DefaultIfEmpty()
            join employee in await _employeeRepository.GetQueryableAsync() on attendentAttendentLines.EmployeeId equals employee.Id
            into employeeattAttendentLine
            from employeeattAttendentLines in employeeattAttendentLine.DefaultIfEmpty()
            select new AttendentLineDto()
            {
                Id = AttendentLine.Id,
                EmployeeName = employeeattAttendentLines.Name,
                TimeCheck = AttendentLine.TimeCheck,
                Type = AttendentLine.Type,
                ShiftId = AttendentLine.ShiftId,
                ShiftName = shiftAttendentLines.Name,
                TimeMissingIn = AttendentLine.TimeMissingIn,
                TimeMissingOut = AttendentLine.TimeMissingOut
            };
        var listAttendentLine = query
            .WhereIf(!input.EmployeeName.IsNullOrWhiteSpace(), x => x.EmployeeName.ToLower().Contains(input.EmployeeName.ToLower()))
            .WhereIf(!input.ShiftName.IsNullOrWhiteSpace(), x => x.ShiftName.ToLower().Contains(input.ShiftName.ToLower()))
            .WhereIf(input.TimeMissingIn != null, x => x.TimeMissingIn == input.TimeMissingIn)
            .WhereIf(input.Type!=null,
                x => x.Type == input.Type)
            .WhereIf(input.TimeMissingOut!=null,
                x => x.TimeMissingOut == input.TimeMissingOut)
            .OrderBy(x=>NormalizeSorting(input.Sorting))
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
        var queryResult = await AsyncExecuter.ToListAsync(listAttendentLine);

        var totalCount = await Repository.GetCountAsync();
        return new PagedResultDto<AttendentLineDto>(
            totalCount,
            queryResult
        );
    }
    
    [Authorize(HRMappPermissions.AttendentLine.Default)]
    public async Task<ListResultDto<SelectResultDto>> GetListShifts()
    {
        var obj = await _shiftRepository.GetListAsync();
        return new ListResultDto<SelectResultDto>(ObjectMapper.Map<List<Shift>, List<SelectResultDto>>(obj));
    }
    
    public async Task<AttendentLineDto> GetAttendentLineDetail(Guid attendentLineId)
    {
        var queryable = await _repository.GetQueryableAsync();
        var query = from AttendentLine in queryable
            where AttendentLine.Id == attendentLineId
            join shift in await _shiftRepository.GetQueryableAsync() on AttendentLine.ShiftId equals
                shift.Id into shiftAttendentLine
            from shiftAttendentLines in shiftAttendentLine.DefaultIfEmpty()
            join attendent in await _attendentRepository.GetQueryableAsync() on AttendentLine.AttendentId equals 
                attendent.Id into attendentAttendentLine
            from attendentAttendentLines in attendentAttendentLine.DefaultIfEmpty()
            select new AttendentLineDto()
            {
                Id = AttendentLine.Id,
                TimeCheck = AttendentLine.TimeCheck,
                Type = AttendentLine.Type,
                ShiftId = AttendentLine.ShiftId,
                ShiftName = shiftAttendentLines.Name,
                TimeMissingIn = AttendentLine.TimeMissingIn,
                TimeMissingOut = AttendentLine.TimeMissingOut
            };
        
        var queryResult = await AsyncExecuter.FirstAsync(query);

        return queryResult;
    }

    public async override Task<AttendentLineDto> CreateAsync(CreateUpdateAttendentLineDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var queryableShifts = await _shiftRepository.GetQueryableAsync();
        // gán ca làm viec vào lượt châm công (attendent line)
        var shift = await _shiftRepository.GetAsync(input.ShiftId);
        if (shift != null)
        {
            var timeMissingIn = 0;
            var timeMissingOut = 0;
            //tính thời gian đi muộn ve sớm
            if (input.Type == TypeLine.CheckIn)
            {
                timeMissingIn =(int)(input.TimeCheck.TimeOfDay - shift.Start).TotalMinutes;
                
            }
            else if(input.Type == TypeLine.CheckOut)
            {
                timeMissingOut= (int)( shift.End - input.TimeCheck.TimeOfDay).TotalMinutes;
            }
            await _repository.InsertAsync(new AttendentLine(
                GuidGenerator.Create(),
                CurrentTenant.Id,
                input.AttendentId,
                input.TimeCheck,
                input.Type,
                input.ShiftId,
                timeMissingIn,
                timeMissingOut));
        }
        return new AttendentLineDto();

    }


    private static string NormalizeSorting(string sorting)
    {
        if (sorting.IsNullOrEmpty())
        {
            return $"attendentLine.{nameof(AttendentLine.Id)}";
        }

        // custom contain sorting 
        if (sorting.Contains("shiftName", StringComparison.OrdinalIgnoreCase))
        {
            return "shiftName";
        }
        return $"attendentLine.{sorting}";
    }
    
}
