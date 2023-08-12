using AutoMapper;
using JeniesStory.Application.Dtos.Requests;
using JeniesStory.Application.Dtos.Responses;
using JeniesStory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeniesStory.Application.Mapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserResponseDto, User>().ReverseMap();
            CreateMap<RegistrationRequestDto, User>().ReverseMap();
            CreateMap<StoryRequestDto, Story>().ReverseMap();
            CreateMap<StoryResponseDto, Story>().ReverseMap();
            CreateMap<AuthorRequestDto, Author>().ReverseMap();
            CreateMap<AuthorResponseDto, Author>().ReverseMap();
            CreateMap<AdminResponseDto, Admin>().ReverseMap();
            CreateMap<AdminRequestDto, Admin>().ReverseMap();
            CreateMap<AuthorResponseDto, Author>().ReverseMap();
            CreateMap<AuthorResponseDto, Author>().ReverseMap();
            CreateMap<CommentRequestDto, Comment>().ReverseMap();
            CreateMap<CommentResponseDto, Comment>().ReverseMap();
            CreateMap<ApproveByAdminDto, Comment>().ReverseMap();
            CreateMap<ApproveByAuthorDto, Comment>().ReverseMap();
            CreateMap<NewsResponse, News>().ReverseMap();
            CreateMap<NewsArticles, News>().ReverseMap()
                .ForPath(dest => dest.source.id, opt => opt.MapFrom(src => src.Source.newsId))
                .ForPath(dest => dest.source.name, opt => opt.MapFrom(src => src.Source.Name)); ;
        }
    }
}
