using System;
using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperHelper:Profile
{

    public AutoMapperHelper()
    {

        CreateMap<AppUser,MemberDto>()
        .ForMember(x=>x.PhotoUrl
        , o=>o.MapFrom(x=>x.Photos.FirstOrDefault(f=>f.IsMain)!.Url))
        ;
        CreateMap<Photo,PhotoDto>();
        
    }

}
