using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terakoya.Models;
using Terakoya.ViewModels;

namespace Terakoya.Data
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ProjectModel, ProjectIndexViewModel>();
            CreateMap<ProjectModel, ProjectDetailViewModel>();
        }
    }
}
