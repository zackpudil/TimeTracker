using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Ilm.CodeAudition.Data.Models;

namespace Ilm.CodeAudition.Data.Storage.Context
{
    public class TimeTrackerInitializer : DropCreateDatabaseAlways<TimeTrackerContext>
    {
        protected override void Seed(TimeTrackerContext context)
        {
            base.Seed(context);

            var ProjectAwesome = new Project { Name = "Project Awesome" };
            var PTO = new Project { Name = "PTO" };
            var ProjectNotAwesome = new Project { Name = "Project Not So Awesome "};
            var TightBudget = new Project { Name = "Tight Buget" };
            var SomethingElse = new Project { Name = "Something Else" };

            context.Projects.Add(ProjectAwesome);
            context.Projects.Add(PTO);
            context.Projects.Add(ProjectNotAwesome);
            context.Projects.Add(TightBudget);
            context.Projects.Add(SomethingElse);

            context.Employees.Add(
            new Employee
            {
                Name = "Zack",
                Timesheets = new List<Timesheet>
                {
                    new Timesheet() { Project = ProjectAwesome, Monday = 8, Tuesday = 0, Wednesday = 8, Thursday = 8, Friday = 8, Total = 32 },
                    new Timesheet() { Project = PTO, Monday = 0, Tuesday = 8, Wednesday = 0, Thursday = 0, Friday = 0, Total = 8 }
                }
            });

            context.Employees.Add(
            new Employee
            {
                Name = "John",
                Timesheets = new List<Timesheet>
                {
                    new Timesheet() { Project = PTO, Monday = 8, Tuesday = 8, Wednesday = 0, Thursday = 0, Friday = 0, Total = 16},
                    new Timesheet() { Project = ProjectNotAwesome, Monday = 0, Tuesday = 0, Wednesday = 8, Thursday = 8, Friday = 8, Total = 24 }
                }
            });

            context.Employees.Add(
            new Employee
            {
                Name = "Paul",
                Timesheets = new List<Timesheet>
                {
                    new Timesheet() { Project = SomethingElse, Monday = 8, Tuesday = 8, Wednesday = 0, Thursday = 0, Friday = 0, Total = 16},
                    new Timesheet() { Project = TightBudget, Monday = 0, Tuesday = 0, Wednesday = 8, Thursday = 0, Friday = 0, Total = 8},
                    new Timesheet() { Project = ProjectNotAwesome, Monday = 0, Tuesday = 0, Wednesday = 0, Thursday = 8, Friday = 8, Total = 16}
                }
            });


            context.SaveChanges();
        }
    }
}
