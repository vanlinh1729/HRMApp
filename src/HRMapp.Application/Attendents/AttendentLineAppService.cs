using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Attendents.Dtos;
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

    public AttendentLineAppService(IAttendentLineRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<AttendentLine>> CreateFilteredQueryAsync(AttendentLineGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.AttendentId != null, x => x.AttendentId == input.AttendentId)
            .WhereIf(input.TimeCheck != null, x => x.TimeCheck == input.TimeCheck)
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(input.ShiftId != null, x => x.ShiftId == input.ShiftId)
            /*.WhereIf(input.TimeMissingIn != null, x => x.TimeMissingIn == input.TimeMissingIn)
            .WhereIf(input.TimeMissingOut != null, x => x.TimeMissingOut == input.TimeMissingOut)*/
            ;
    }
}
