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
        
        CreateMap<MemeberUpdateDto,AppUser>();
    
        CreateMap<RegisterDto,AppUser>();
        // the next one is to configure the change from the string to date only 

        CreateMap<string ,DateOnly>().ConstructUsing(x=>DateOnly.Parse(x));
        CreateMap <Messages,MessagesDto>().
        ForMember(x=>x.SenderPhotoUrl,o=>o.MapFrom(x=>x.Sender.Photos.FirstOrDefault(x=>x.IsMain)!.Url))
        .   ForMember(x=>x.RecipientPhotoUrl,o=>o.MapFrom(x=>x.Recipient.Photos.FirstOrDefault(x=>x.IsMain)!.Url));

    }

}
