using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Data.Models;
using FluentValidation;

namespace Ilm.CodeAudition.Service.Validators
{
    public interface IEmployeeValidator : IValidator<Employee> { }

    public class EmployeeValidator : AbstractValidator<Employee>, IEmployeeValidator
    {
        public EmployeeValidator()
        {
            RuleFor(employee => employee.Timesheets.Sum(x => x.Total)).GreaterThanOrEqualTo(40).WithName("GrandTotal");
        }
    }
}
