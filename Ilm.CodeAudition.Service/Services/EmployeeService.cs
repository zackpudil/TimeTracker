using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Service.ViewModels;
using Ilm.CodeAudition.Service.Mappers;
using Ilm.CodeAudition.Data.Models;
using Ilm.CodeAudition.Data.Storage.Context;

namespace Ilm.CodeAudition.Service.Services
{
    public interface IEmployeeService 
    {
        IEnumerable<EmployeeViewModel> GetEmployees();
    }

    public class EmployeeService : IEmployeeService
    {
        private ITimeTrackerSession _timeTrackerSession;
        private IEmployeeToViewModelMapper _employeeMapper;

        public EmployeeService(ITimeTrackerSession timeTrackerSession, IEmployeeToViewModelMapper employeeMapper)
        {
            _timeTrackerSession = timeTrackerSession;
            _employeeMapper = employeeMapper;
        }

        public IEnumerable<EmployeeViewModel> GetEmployees()
        {
            return _employeeMapper.CreateSet(_timeTrackerSession.All<Employee>());
        }
    }
}
