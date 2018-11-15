using AutoMapper;
using Chat.Web.Models;
using Chat.Web.Models.ViewModels;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Web.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dst => dst.Username, opt => opt.MapFrom(x => x.UserName));
        }
    }
}