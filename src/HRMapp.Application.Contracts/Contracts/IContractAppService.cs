using System;
using HRMapp.Contracts.Dtos;
using Volo.Abp.Application.Services;

namespace HRMapp.Contracts;


public interface IContractAppService :
    ICrudAppService< 
        ContractDto, 
        Guid, 
        ContractGetListInput,
        CreateUpdateContractDto,
        CreateUpdateContractDto>
{

}