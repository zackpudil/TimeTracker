using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Data.Models;
using Ilm.CodeAudition.Service.ViewModels;
using AutoMapperAssist;

namespace Ilm.CodeAudition.Service.Mappers
{
    public interface IEmployeeToViewModelMapper : IMapper<Employee, EmployeeViewModel> { }

    public class EmployeeToViewModelMapper : Mapper<Employee, EmployeeViewModel>, IEmployeeToViewModelMapper
    {
        public override void DefineMap(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Employee, EmployeeViewModel>()
                .ForMember(src => src.Timesheets,
                    opt => opt.MapFrom(
                        dest => dest.Timesheets.Select(
                            x => new EmployeeTimesheetViewModel { ProjectName = x.Project.Name, TotalHours = x.Total })));
        }
    }
}
