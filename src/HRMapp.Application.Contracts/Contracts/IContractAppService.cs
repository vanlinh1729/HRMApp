using System;
using System.Threading.Tasks;
using HRMapp.Contracts.Dtos;
using Volo.Abp.Application.Dtos;
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
    Task<ListResultDto<SelectResultDto>> GetListEmployees();
    Task<ContractDto> GetContractDetail(Guid ContractId);
    Task<int> ContractCountAsync();

}