using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.Apis.Models;
using Portal.Apis.Core.DAL.Entities;
using Portal.Apis.Core.Extensions;
using Portal.Apis.Core.Helpers;
using NpgsqlTypes;
using System.IO;
using Microsoft.AspNetCore.Http;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Core.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User Management

            CreateMap<RegistrationViewModel, User>()
                .ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));

            CreateMap<User, UserViewModel>()
                .ForMember(d => d.Photo, map => map.MapFrom(s => s.Photo.GetFullPath("Storage:Avatars")))
                .ForMember(d => d.Roles, map => map.Ignore());
            CreateMap<UserViewModel, User>()
                .ForMember(d => d.Roles, map => map.Ignore());

            CreateMap<RoleViewModel, Role>();
            CreateMap<Role, RoleViewModel>()
                .ForMember(d => d.Permissions, map => map.MapFrom(s => s.Claims.Select(ss => ss.ClaimValue)))
                .ForMember(d => d.UsersCount, map => map.ResolveUsing(s => s.Users?.Count ?? 0));

            #endregion

            #region Manuals

            CreateMap<CountryViewModel, Country>();
            CreateMap<Country, CountryViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()));

            CreateMap<RegionViewModel, Region>();
            CreateMap<Region, RegionViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Translate()))
                .ForMember(dest => dest.Preview, opt => opt.MapFrom(src => src.Preview.Translate()));

            CreateMap<CityViewModel, City>();
            CreateMap<City, CityViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Translate()))
                .ForMember(dest => dest.Preview, opt => opt.MapFrom(src => src.Preview.Translate()));

            CreateMap<LanguageViewModel, Language>();
            CreateMap<Language, LanguageViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()));

            CreateMap<CultureViewModel, Culture>();
            CreateMap<Culture, CultureViewModel>()
                .ForMember(dest => dest.Icon, map => map.MapFrom(src => src.Icon.GetFullPath("Storage:Images")))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name.Translate()));

            #endregion

            #region Announcement Dashboard

            CreateMap<ArticleCategoryViewModel, ArticleCategory>();
            CreateMap<ArticleCategory, ArticleCategoryViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()));

            CreateMap<ArticleViewModel, Article>()
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => Path.GetFileName(src.PhotoPath)));
            CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Translate()))
                .ForMember(dest => dest.Preview, opt => opt.MapFrom(src => src.Preview.Translate()))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.Translate()))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name.Translate()))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name.Translate()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ArticleCategory.Name.Translate()))
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.PhotoPath.GetFullPath("Storage:PhotoFiles:PhotoFiles")));

            CreateMap<NewsCategoryViewModel, NewsCategory>();
            CreateMap<NewsCategory, NewsCategoryViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()));

            CreateMap<NewsViewModel, News>()
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => Path.GetFileName(src.PhotoPath)));
            CreateMap<News, NewsViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Translate()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Translate()))
                .ForMember(dest => dest.Preview, opt => opt.MapFrom(src => src.Preview.Translate()))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.Translate()))
                .ForMember(dest => dest.RegionName, opt => opt.MapFrom(src => src.Region.Name.Translate()))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name.Translate()))
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.Translate()))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.NewsCategory.Name.Translate()))
                .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.PhotoPath.GetFullPath("Storage:PhotoFiles:PhotoFiles")));

            #endregion
        }
    }
}
