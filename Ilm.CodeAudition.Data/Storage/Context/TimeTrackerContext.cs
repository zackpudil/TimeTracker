using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Ilm.CodeAudition.Data.Models;

namespace Ilm.CodeAudition.Data.Storage.Context
{
    public class TimeTrackerContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
