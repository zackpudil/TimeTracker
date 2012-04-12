using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Service.ViewModels;
using Ilm.CodeAudition.Data.Models;
using AutoMapperAssist;

namespace Ilm.CodeAudition.Service.Mappers
{
    public interface IViewModelToTimesheetMapper : IMapper<TimesheetViewModel, Timesheet> { }

    class ViewModelToTimesheetMapper : Mapper<TimesheetViewModel, Timesheet>, IViewModelToTimesheetMapper
    {
        public override void DefineMap(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<TimesheetViewModel, Timesheet>()
                .ForMember(dest => dest.Project, opt => opt.MapFrom(
                    src => new Project { Id = src.Project.Id, Name = src.Project.Name }));
        }
    }
}
