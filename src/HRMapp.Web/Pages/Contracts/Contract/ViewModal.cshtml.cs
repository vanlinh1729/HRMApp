using System;
using System.Threading.Tasks;
using HRMapp.Contracts;
using HRMapp.Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.Employee.ViewModels;

namespace HRMapp.Web.Pages.Contracts.Contract;

public class ViewModalModel : HRMappPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }
    
    [BindProperty]
    public ContractDto ViewModel { get; set; }

    private readonly IContractAppService _service;

    public ViewModalModel(IContractAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = dto;
    }
}
