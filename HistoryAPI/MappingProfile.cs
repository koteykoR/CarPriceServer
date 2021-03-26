﻿using AutoMapper;
using HistoryAPI.Domain.Entities;
using HistoryAPI.Models;

namespace HistoryAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CarHistoryModel, CarHistory>();
            CreateMap<CarHistory, CarHistoryModel>();
        }
    }
}
