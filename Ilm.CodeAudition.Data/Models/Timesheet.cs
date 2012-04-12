using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Ilm.CodeAudition.Data.Models
{
    public class Timesheet
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string ProjectId { get; set; }

        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }

        public int Total { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Project Project { get; set; }
    }

}