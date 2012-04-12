using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilm.CodeAudition.Service.ViewModels
{
    public class TimeTrackerViewModel
    {
        public int EmployeeId { get; set; }

        public IEnumerable<ProjectViewModel> AvaliableProjects { get; set; }

        public IEnumerable<TimesheetViewModel> Timesheets { get; set; }
    }
}
