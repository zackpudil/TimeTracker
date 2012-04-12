using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Service.ViewModels;
using Ilm.CodeAudition.Data.Storage.Context;
using Ilm.CodeAudition.Service.Mappers;
using Ilm.CodeAudition.Data.Models;
using Ilm.CodeAudition.Service.Validators;
using Ilm.CodeAudition.Service.Infrastructure;

namespace Ilm.CodeAudition.Service.Services
{
    public interface ITimeTrackerService 
    {
        TimeTrackerViewModel GetTimesheets(int employeeId);
        void SaveTimesheets(IEnumerable<TimesheetViewModel> timesheets, int employeeId);
    }

    public class TimeTrackerService : ITimeTrackerService
    {
        private ITimeTrackerSession _timeTrackerSession;
        private ITimesheetToViewModelMapper _timesheetMapper;
        private IProjectToViewModelMapper _projectMapper;
        private IViewModelToTimesheetMapper _viewModelMapper;
        private ITimesheetValidator _timesheetValidator;
        private IEmployeeValidator _employeeValidator;

        public TimeTrackerService
            (ITimeTrackerSession timeTrackerSession, 
            ITimesheetToViewModelMapper timesheetMapper, 
            IProjectToViewModelMapper projectMapper,
            IViewModelToTimesheetMapper viewModelMapper,
            ITimesheetValidator timesheetValidator,
            IEmployeeValidator employeeValidator)
        {
            _timeTrackerSession = timeTrackerSession;
            _timesheetMapper = timesheetMapper;
            _projectMapper = projectMapper;
            _viewModelMapper = viewModelMapper;
            _timesheetValidator = timesheetValidator;
            _employeeValidator = employeeValidator;
        }

        public TimeTrackerViewModel GetTimesheets(int employeeId)
        {
            var employee = _timeTrackerSession.Single<Employee>(x => x.Id == employeeId);
            return new TimeTrackerViewModel
            {
                AvaliableProjects = _projectMapper.CreateSet(_timeTrackerSession.All<Project>()),
                Timesheets = _timesheetMapper.CreateSet(employee.Timesheets)
            };
        }

        public void SaveTimesheets(IEnumerable<TimesheetViewModel> timesheets, int employeeId)
        {
            var savedTimesheets = _viewModelMapper.CreateSet(timesheets).ToList();
            //assign the employerid to the timesheet                      validate each timesheet
            savedTimesheets.ForEach(x => { x.EmployeeId = employeeId; _timesheetValidator.ValidateAndThrow(x); });

            //validate employeer object
            _employeeValidator.ValidateAndThrow(new Employee { Timesheets = savedTimesheets });

            _timeTrackerSession.Delete<Timesheet>(x => x.EmployeeId == employeeId);
            _timeTrackerSession.Add<Timesheet>(savedTimesheets);

            _timeTrackerSession.CommitChanges();
        }
    }
}
