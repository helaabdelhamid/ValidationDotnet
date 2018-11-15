﻿using AutoMapper;
using Chat.Web.Helpers;
using Chat.Web.Models;
using Chat.Web.Models.ViewModels;
using Domaine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat.Web.Mappings
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageViewModel>()
                .ForMember(dst => dst.From, opt => opt.MapFrom(x => x.FromUser.UserName))
                .ForMember(dst => dst.To, opt => opt.MapFrom(x => x.ToRoom.Name))
                .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => "data:image/false;base64,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"))
                .ForMember(dst => dst.Content, opt => opt.MapFrom(x => BasicEmojis.ParseEmojis(x.Content)))
                .ForMember(dst => dst.Timestamp, opt => opt.MapFrom(x => new DateTime(long.Parse(x.Timestamp)).ToLongTimeString()));

            CreateMap<MessageViewModel, Message>();
        }
    }
}