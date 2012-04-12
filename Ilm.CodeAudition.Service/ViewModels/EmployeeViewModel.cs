using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilm.CodeAudition.Service.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<EmployeeTimesheetViewModel> Timesheets { get; set; }
        public int GrandTotal
        { get { return Timesheets.Sum(x => x.TotalHours); }}
    }
}
