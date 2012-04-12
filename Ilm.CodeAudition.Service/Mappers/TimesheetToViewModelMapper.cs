using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilm.CodeAudition.Data.Models;
using Ilm.CodeAudition.Service.ViewModels;
using AutoMapperAssist;
using AutoMapper;

namespace Ilm.CodeAudition.Service.Mappers
{
    public interface ITimesheetToViewModelMapper : IMapper<Timesheet, TimesheetViewModel> { }

    public class TimesheetToViewModelMapper : Mapper<Timesheet, TimesheetViewModel>, ITimesheetToViewModelMapper
    {
        public override void DefineMap(IConfiguration configuration)
        {
            configuration.CreateMap<Timesheet, TimesheetViewModel>()
                .ForMember(
                    src => src.Project, 
                    opt => opt.MapFrom(
                        dest => new ProjectViewModel { Id = dest.Project.Id, Name = dest.Project.Name }
                    )
                );
        }
    }
}