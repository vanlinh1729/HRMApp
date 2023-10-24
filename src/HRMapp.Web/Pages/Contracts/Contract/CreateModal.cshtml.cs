using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IContractAppService _service;

    public CreateModalModel(IContractAppService service): base(service)
    {
        _service = service;
    }
}