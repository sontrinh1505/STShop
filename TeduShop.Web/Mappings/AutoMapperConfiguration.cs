using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeduShop.Model.Models;
using TeduShop.Web.Models;

namespace TeduShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Post, PostViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<PostCategory, PostCategoryViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<Tag, TagViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<Product, ProductViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ProductTag, ProductTagViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<Footer, FooterViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<Slide, SlideViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<Page, PageViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ContactDetail, ContactDetailViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap().MaxDepth(2);
                cfg.CreateMap<ApplicationPermission, ApplicationPermissionViewModel>().ReverseMap().MaxDepth(2);
            });
        }
    }
}