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
using Volo.Abp.Uow;

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
    private readonly IAttendentLineAppService _attendentLineAppService;
    
    private readonly IAttendentRepository _repository;
    private readonly IShiftRepository _shiftRepository;

    public AttendentAppService(IAttendentRepository repository
        , IEmployeeRepository employeeRepository,
        IAttendentLineRepository attendentLineRepository,
        IShiftRepository shiftRepository,
        IAttendentLineAppService attendentLineAppService
    ) : base(repository)
    {
        _attendentLineAppService = attendentLineAppService;
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


    [UnitOfWork]
    public override async Task<AttendentDto> CreateAsync(CreateUpdateAttendentDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        var queryableShifts = await _shiftRepository.GetQueryableAsync();
        var queryableAttendentLine = await _attendentLineRepository.GetQueryableAsync();

        // gán ca làm viec vào lượt châm công (attendent line)
        var shift = queryableShifts.Where(x => x.TimeStartCheckin <= input.Date.TimeOfDay)
            .Where(x => x.TimeStopCheckout >= input.Date.TimeOfDay).ToList().FirstOrDefault();

        //lấy attendent của Emoloyee ngày hôm đó
        var attendent = queryable
            .Where(x => x.Date.Date == input.Date.Date)
            .Where(x => x.EmployeeId == input.EmployeeId).ToList().FirstOrDefault();
        // var attendentId = attendent.Id;
        // kiểm tra xem ca làm việc ngày hôm đó có attline chưa
        var attendentLine = queryableAttendentLine
            .WhereIf(attendent != null, x => x.AttendentId == attendent.Id)
            .WhereIf(shift != null, x => x.ShiftId == shift.Id)
            .Where(x => x.Type == input.Type)
            .FirstOrDefault();
        // var attendentLineId = attendentLine.Id;
        if (shift != null)
        {
            //kiểm tra thời gian check, nếu nằm trong khoảng có thể chấm công  thì mới chấm đc

            if (shift.TimeStartCheckin <= input.Date.TimeOfDay && input.Date.TimeOfDay <= shift.TimeStopCheckout)
            {
                var timeMissingIn = 0;
                var timeMissingOut = 0;
                //tính thời gian đi muộn ve sớm
                if (input.Type == TypeLine.CheckIn)
                {
                    timeMissingIn = input.Date.TimeOfDay >= shift.Start ?(int)(input.Date.TimeOfDay - shift.Start).TotalMinutes : 0;
                }
                else if (input.Type == TypeLine.CheckOut)
                {
                    timeMissingOut = input.Date.TimeOfDay <= shift.End ? (int)(shift.End - input.Date.TimeOfDay).TotalMinutes : 0;
                }

                //nếu ngày hôm đó chưa có attendent nào thì tạo attendent và tạo attendentLine
                if (attendent == null)
                {
                    var attendentSuccess = await _repository.InsertAsync(new Attendent(GuidGenerator.Create(),
                        CurrentTenant.Id, input.Date, input.EmployeeId, 0, 0));


                    await _attendentLineRepository.InsertAsync(new AttendentLine(
                        GuidGenerator.Create(),
                        CurrentTenant.Id,
                        attendentSuccess.Id,
                        input.Date, input.Type,
                        shift.Id, timeMissingIn, timeMissingOut));

                    return ObjectMapper.Map<Attendent, AttendentDto>(attendentSuccess);
                }

                //nếu có attendent rồi thì kiểm tra xem Ca làm việc hôm đó đã có attendentLine(check in/out) hay chưa
                if (attendentLine != null) // nếu có attline rồi thì update
                {
                    if (attendentLine.Type == TypeLine.CheckOut) //nếu attline alf check out
                    {
                        //kiểm tra thời gian check out có nhỏ hơn thời gian chấm k. nếu có thì update theo input
                        if (attendentLine.TimeCheck <= input.Date)
                        {
                            attendentLine.TimeCheck = input.Date;
                            attendentLine.TimeMissingIn = timeMissingIn;
                            attendentLine.TimeMissingOut = timeMissingOut;
                            await _attendentLineRepository.UpdateAsync(attendentLine);
                        }
                    }

                    if (attendentLine.Type == TypeLine.CheckIn) //nếu attline alf check in
                    {
                        //kiểm tra thời gian check in có lớn hơn thời gian chấm k. nếu có thì update theo input
                        if (attendentLine.TimeCheck >= input.Date)
                        {
                            attendentLine.TimeCheck = input.Date;
                            attendentLine.TimeMissingIn = timeMissingIn;
                            attendentLine.TimeMissingOut = timeMissingOut;
                            await _attendentLineRepository.UpdateAsync(attendentLine);
                        }
                    }
                }
                else //chưa có thì tạo
                {
                    await _attendentLineRepository.InsertAsync(new AttendentLine(
                        GuidGenerator.Create(),
                        CurrentTenant.Id,
                        attendent.Id,
                        input.Date, input.Type,
                        shift.Id, timeMissingIn, timeMissingOut));
                }
            }

            return new AttendentDto();
        }

        return new AttendentDto();
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
