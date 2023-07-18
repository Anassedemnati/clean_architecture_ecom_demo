﻿using AutoMapper;
using Basket.Application.Contracts.Handlers.Dtos;
using Basket.Domain.Entities;

namespace Basket.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>(MemberList.Source).ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>(MemberList.Source).ReverseMap();
    }
}
