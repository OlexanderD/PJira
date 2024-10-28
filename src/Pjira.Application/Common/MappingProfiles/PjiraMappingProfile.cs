using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pjira.Application.DtoModels;
using Pjira.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pjira.Application.Common.MappingProfiles
{
    public class PjiraMappingProfile:Profile
    {
        public PjiraMappingProfile()
        {
           CreateMap<Assignment,AssigmentDto>().ReverseMap();
           
           CreateMap<Project,ProjectDto>().ReverseMap();

           CreateMap<IdentityUser, User>()
                .ReverseMap()
                .ForMember(m => m.PasswordHash, n => n.MapFrom(u => u.Password)); ;
        }
    }
}
