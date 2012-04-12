using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilm.CodeAudition.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Timesheet> Timesheets { get; set; }
    }
}
