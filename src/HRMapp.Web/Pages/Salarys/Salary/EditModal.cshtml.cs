using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRMapp.Salarys;
using HRMapp.Salarys.Dtos;
using HRMapp.Web.Pages.Salarys.Salary.ViewModels;

namespace HRMapp.Web.Pages.Salarys.Salary;

public class EditModalModel : CreateOrEditModalModel
{

    private readonly ISalaryAppService _service;

    public EditModalModel(ISalaryAppService service): base(service)
    {
        _service = service;
    }
}