using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Data.Models;
using FluentValidation;

namespace Ilm.CodeAudition.Service.Validators
{
    public interface ITimesheetValidator : IValidator<Timesheet> { }
        
    public class TimesheetValidator : AbstractValidator<Timesheet>, ITimesheetValidator
    {
        public TimesheetValidator()
        {
            RuleFor(x => x.ProjectId).NotEmpty();
            RuleFor(x => x.Total).NotEqual(0);
        }
    }
}
