using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Shifts.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Shifts;


public class ShiftAppService : CrudAppService<Shift, ShiftDto, Guid, ShiftGetListInput, CreateUpdateShiftDto, CreateUpdateShiftDto>,
    IShiftAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Shift.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Shift.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Shift.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Shift.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Shift.Delete;

    private readonly IShiftRepository _repository;

    public ShiftAppService(IShiftRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<Shift>> CreateFilteredQueryAsync(ShiftGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.Start != null, x => x.Start == input.Start)
            .WhereIf(input.End != null, x => x.End == input.End)
            .WhereIf(input.TimeStartCheckin != null, x => x.TimeStartCheckin == input.TimeStartCheckin)
            .WhereIf(input.TimeStopCheckout != null, x => x.TimeStopCheckout == input.TimeStopCheckout)
            ;
    }
}
