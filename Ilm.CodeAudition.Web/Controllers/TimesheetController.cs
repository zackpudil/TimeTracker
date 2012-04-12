using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using System.Data.Entity;
using Ilm.CodeAudition.Data.Models;
using System;
using System.Linq;
using Mvc.RespondTo;
using Newtonsoft.Json;
using Ilm.CodeAudition.Data.Storage.Context;
using Ilm.CodeAudition.Service.Services;
using Ilm.CodeAudition.Service.Attributes;
using Ilm.CodeAudition.Service.ViewModels;
using FluentValidation;
using Ilm.CodeAudition.Service.Mappers;

namespace Ilm.CodeAudition.Service.Controllers
{
    public class TimesheetController : Controller
    {
        private ITimeTrackerService _timeTrackerService;

        public TimesheetController(ITimeTrackerService timeTrackerService) 
        {
            _timeTrackerService = timeTrackerService;
        }

        //
        // Get /Timesheet
        public ActionResult Index(int employeeId)
        {
            var timeTrackerModel = _timeTrackerService.GetTimesheets(employeeId);
            return this.RespondTo(format =>
            {
                format.Html(() => View(timeTrackerModel));
                format.Json(() => Json(timeTrackerModel, JsonRequestBehavior.AllowGet));
            });
        }

        //
        // Post /Timesheet
        [HttpPost]
        public ActionResult Index(int employeeId, [FromJson]IEnumerable<TimesheetViewModel> savedTimesheets)
        {
            TimeTrackerViewModel timeTrackerModel = default(TimeTrackerViewModel);
            try
            {
                _timeTrackerService.SaveTimesheets(savedTimesheets, employeeId);

                TempData["FlashClass"] = "success";
                TempData["FlashMessage"] = "Your timesheets have been saved!";

                timeTrackerModel = _timeTrackerService.GetTimesheets(employeeId);
            }
            catch (ValidationException ex)
            {
                TempData["FlashClass"] = "error";
                TempData["FlashMessage"] = String.Join(",", ex.Errors.Select(x => x.ErrorMessage));
            }

            return this.RespondTo(format =>
            {
                format.Html(() => View(timeTrackerModel));
                format.Json(() => Json(timeTrackerModel));
            });
        }

        public ActionResult New()
        {
            var newTimesheet = new TimesheetViewModel();
            newTimesheet.Project = new ProjectViewModel { Id = 0, Name = "" };

            return this.RespondTo(format =>
            {
                format.Html(() => View(newTimesheet));
                format.Json(() => Json(newTimesheet, JsonRequestBehavior.AllowGet));
            });
        }
    }
}