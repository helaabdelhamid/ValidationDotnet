﻿using AutoMapper;
using Chat.Web.Models;
using Chat.Web.Models.ViewModels;
using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Web.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, RoomViewModel>();

            CreateMap<RoomViewModel, Room>();
        }
    }
}