using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Attendents;


public class AttendentForMonthAppService : CrudAppService<AttendentForMonth, AttendentForMonthDto, Guid, AttendentForMonthGetListInput, CreateUpdateAttendentForMonthDto, CreateUpdateAttendentForMonthDto>,
    IAttendentForMonthAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.AttendentForMonth.Delete;

    private readonly IAttendentForMonthRepository _repository;

    public AttendentForMonthAppService(IAttendentForMonthRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<AttendentForMonth>> CreateFilteredQueryAsync(AttendentForMonthGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.EmployeeId != null, x => x.EmployeeId == input.EmployeeId)
            .WhereIf(input.Month != null, x => x.Month == input.Month)
            ;
    }
}
