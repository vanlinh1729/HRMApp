using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using HRMapp.Web.Pages.Contracts.Contract.ViewModels;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class EditModalModel : CreateOrEditModalModel
{

    private readonly IContractAppService _service;

    public EditModalModel(IContractAppService service): base(service)
    {
        _service = service;
    }
}