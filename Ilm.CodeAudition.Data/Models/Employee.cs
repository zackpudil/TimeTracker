using System.Collections.Generic;
namespace Ilm.CodeAudition.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Timesheet> Timesheets {get; set;}
    }
}