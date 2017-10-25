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
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<PostCategory, PostCategoryViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Tag, TagViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Product, ProductViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<ProductTag, ProductTagViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Footer, FooterViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Slide, SlideViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Page, PageViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<ContactDetail, ContactDetailViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<MenuGroup, MenuGroupViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Menu, MenuViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<Slide, SlideViewModel>().ReverseMap().MaxDepth(4);
                cfg.CreateMap<ApplicationGroup, ApplicationGroupViewModel>().ReverseMap().MaxDepth(4);
              
                cfg.CreateMap<ApplicationRole, ApplicationRoleViewModel>().ReverseMap().MaxDepth(4);

                cfg.CreateMap<ApplicationUser, ApplicationUserViewModel>().ReverseMap().MaxDepth(4);

                cfg.CreateMap<ApplicationPermission, ApplicationPermissionViewModel>().ReverseMap().MaxDepth(4);

            });
        }
    }
}