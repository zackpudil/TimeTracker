using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ilm.CodeAudition.Data.Models;
using Ilm.CodeAudition.Service.ViewModels;
using AutoMapperAssist;

namespace Ilm.CodeAudition.Service.Mappers
{
    public interface IProjectToViewModelMapper : IMapper<Project, ProjectViewModel> { }

    public class ProjectToViewModelMapper : Mapper<Project, ProjectViewModel>, IProjectToViewModelMapper
    {
    }
}