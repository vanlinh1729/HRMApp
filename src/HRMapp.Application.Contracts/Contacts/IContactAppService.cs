using System;
using System.Threading.Tasks;
using HRMapp.Contacts.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Contacts;


public interface IContactAppService :
    ICrudAppService< 
        ContactDto, 
        Guid, 
        ContactGetListInput,
        CreateUpdateContactDto,
        CreateUpdateContactDto>
{
    Task<int> ContactCountAsync();
}