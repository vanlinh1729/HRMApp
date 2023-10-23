using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Employees;
using HRMapp.Employees.Dtos;
using HRMapp.Web.Pages.Employees.EmployeeHistory.ViewModels;

namespace HRMapp.Web.Pages.Employees.EmployeeHistory;

public class CreateModalModel : CreateOrEditModalModel
{

    private readonly IEmployeeHistoryAppService _service;

    public CreateModalModel(IEmployeeHistoryAppService service): base(service)
    {
        _service = service;
    }
}