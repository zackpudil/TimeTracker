using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ilm.CodeAudition.Service.Services;

namespace Ilm.CodeAudition.Service.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            return View(_employeeService.GetEmployees());
        }

    }
}
