
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EzilineApp.Api.CoreApplication.DataTransferObjs.admindtos;
using EzilineApp.Api.CoreApplication.DataTransferObjs.ReviewDetails;
using EzilineApp.Api.CoreApplication.DataTransferObjs.users_dtos;
using EzilineApp.Api.CoreApplication.Models;

public class automapperprofile : Profile
{
    public automapperprofile()
    {
         CreateMap<User,usertoreturn>()
                        .ForMember(dest=>dest.mainphoto,opt=>opt.MapFrom(src=>src.profile.imgurl));
         
         CreateMap<registerdto,User>();
         CreateMap<Role,List<string>>();

         CreateMap<User,UserRole>().ForMember(dest=>dest.User,opt=>opt.MapFrom(src=>src));
         CreateMap<Role,UserRole>().ForMember(dest=>dest.Role,opt=>opt.MapFrom(src=>src));

         CreateMap<websiteentity,website>()
                                .ForMember(dest=>dest.description,opt=>opt.MapFrom(src=>src.description))
                                .ForMember(dest=>dest.intro,opt=>opt.MapFrom(src=>src.title))
                                .ForMember(dest=>dest.layoutimgurl,opt=>opt.MapFrom(src=>src.coverimage))
                                .ForMember(dest=>dest.refrallink,opt=>opt.MapFrom(src=>src.link));

         CreateMap<website,websiteentity>()
                                .ForMember(dest=>dest.link,opt=>opt.MapFrom(src=>src.refrallink))
                                .ForMember(dest=>dest.coverimage,opt=>opt.MapFrom(src=>src.layoutimgurl))
                                .ForMember(dest=>dest.title,opt=>opt.MapFrom(src=>src.intro));

         CreateMap<reviewentity,comments>();    

         
         CreateMap<usertoreturn,WebsiteReview>()
                               .ForMember(dest=>dest.user,opt=>opt.MapFrom(src=>src));
                               
         CreateMap<comments,WebsiteReview>();
         
         CreateMap<List<ratings>,List<ratings>>();
                                      
         CreateMap<summary,summary>()
                                   .ForMember(dest=>dest.id,opt=>opt.Ignore());               
         
    }
}