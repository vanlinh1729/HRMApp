using System;
using System.Linq;
using System.Threading.Tasks;
using HRMapp.Permissions;
using HRMapp.Contacts.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HRMapp.Contacts;


public class ContactAppService : CrudAppService<Contact, ContactDto, Guid, ContactGetListInput, CreateUpdateContactDto, CreateUpdateContactDto>,
    IContactAppService
{
    protected override string GetPolicyName { get; set; } = HRMappPermissions.Contact.Default;
    protected override string GetListPolicyName { get; set; } = HRMappPermissions.Contact.Default;
    protected override string CreatePolicyName { get; set; } = HRMappPermissions.Contact.Create;
    protected override string UpdatePolicyName { get; set; } = HRMappPermissions.Contact.Update;
    protected override string DeletePolicyName { get; set; } = HRMappPermissions.Contact.Delete;

    private readonly IContactRepository _repository;

    public ContactAppService(IContactRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override async Task<IQueryable<Contact>> CreateFilteredQueryAsync(ContactGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.Gender != null, x => x.Gender == input.Gender)
            .WhereIf(input.BirthDay != null, x => x.BirthDay == input.BirthDay)
            .WhereIf(input.Active != null, x => x.Active == input.Active)
            .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Email))
            .WhereIf(!input.PhoneNumber.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(input.PhoneNumber))
            .WhereIf(!input.Address.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Address))
            .WhereIf(!input.Education.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Education))
            .WhereIf(!input.Language.IsNullOrWhiteSpace(), x => x.Address.Contains(input.Language))
            ;
    }
    [Authorize(HRMappPermissions.Contact.Default)]
    public async Task<int> ContactCountAsync()
    {
        return await _repository.CountAsync();
    }
}
