using AutoMapper;
using API_Bloodborne.Models;
using API_Bloodborne.Data.DTOs.Itens;

namespace API_Bloodborne.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<CreateItemDto, Item>();
            CreateMap<UpdateItemDto, Item>();
            CreateMap<Item, UpdateItemDto>();
            CreateMap<Item, ReadItemDto>();
        }
    }
}
