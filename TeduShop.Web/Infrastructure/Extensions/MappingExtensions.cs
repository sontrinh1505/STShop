using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeduShop.Model.Models;
using TeduShop.Web.App_Start;
using TeduShop.Web.Models;

namespace TeduShop.Web.Infrastructure.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        //PostCategory <-> PostCategoryViewModel
        public static PostCategory ToModel(this PostCategoryViewModel viewmodel)
        {
            return Mapper.Map<PostCategoryViewModel, PostCategory>(viewmodel);
        }

        public static PostCategoryViewModel ToViewModel(this PostCategory model)
        {
            return Mapper.Map<PostCategory, PostCategoryViewModel>(model);
        }

        public static IEnumerable<PostCategory> ToListModel(this IEnumerable<PostCategoryViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<PostCategoryViewModel>, IEnumerable<PostCategory>>(viewmodel);
        }

        public static IEnumerable<PostCategoryViewModel> ToListViewModel(this IEnumerable<PostCategory> model)
        {
            return Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(model);
        }


        //ProductCategory <-> ProductCategoryViewModel
        public static ProductCategory ToModel(this ProductCategoryViewModel viewmodel)
        {
            return Mapper.Map<ProductCategoryViewModel, ProductCategory>(viewmodel);
        }

        public static ProductCategoryViewModel ToViewModel(this ProductCategory model)
        {
            return Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
        }

        public static IEnumerable<ProductCategory> ToListModel(this IEnumerable<ProductCategoryViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ProductCategoryViewModel>, IEnumerable<ProductCategory>>(viewmodel);
        }

        public static IEnumerable<ProductCategoryViewModel> ToListViewModel(this IEnumerable<ProductCategory> model)
        {
            return Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
        }



        //Post <-> PostViewModel
        public static Post ToModel(this PostViewModel viewmodel)
        {
            return Mapper.Map<PostViewModel, Post>(viewmodel);
        }

        public static PostViewModel ToViewModel(this Post model)
        {
            return Mapper.Map<Post, PostViewModel>(model);
        }

        public static IEnumerable<Post> ToListModel(this IEnumerable<PostViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<PostViewModel>, IEnumerable<Post>>(viewmodel);
        }

        public static IEnumerable<PostViewModel> ToListViewModel(this IEnumerable<Post> model)
        {
            return Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(model);
        }


        //Product <-> ProductViewModel
        public static Product ToModel(this ProductViewModel viewmodel)
        {
            return Mapper.Map<ProductViewModel, Product>(viewmodel);
        }

        public static ProductViewModel ToViewModel(this Product model)
        {
            return Mapper.Map<Product, ProductViewModel>(model);
        }

        public static IEnumerable<Product> ToListModel(this IEnumerable<ProductViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ProductViewModel>, IEnumerable<Product>>(viewmodel);
        }

        public static IEnumerable<ProductViewModel> ToListViewModel(this IEnumerable<Product> model)
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(model);
        }


        //Feedback <-> FeedbackViewModel
        public static Feedback ToModel(this FeedbackViewModel viewmodel)
        {
            return Mapper.Map<FeedbackViewModel, Feedback>(viewmodel);
        }

        public static FeedbackViewModel ToViewModel(this Feedback model)
        {
            return Mapper.Map<Feedback, FeedbackViewModel>(model);
        }

        public static IEnumerable<Feedback> ToListModel(this IEnumerable<FeedbackViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<FeedbackViewModel>, IEnumerable<Feedback>>(viewmodel);
        }

        public static IEnumerable<FeedbackViewModel> ToListViewModel(this IEnumerable<Feedback> model)
        {
            return Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(model);
        }


        //Order <-> OrderViewModel
        public static Order ToModel(this OrderViewModel viewmodel)
        {
            return Mapper.Map<OrderViewModel, Order>(viewmodel);
        }

        public static OrderViewModel ToViewModel(this Order model)
        {
            return Mapper.Map<Order, OrderViewModel>(model);
        }

        public static IEnumerable<Order> ToListModel(this IEnumerable<OrderViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<OrderViewModel>, IEnumerable<Order>>(viewmodel);
        }

        public static IEnumerable<OrderViewModel> ToListViewModel(this IEnumerable<Order> model)
        {
            return Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(model);
        }

        //ApplicationGroup <-> ApplicationGroupViewModel
        public static ApplicationGroup ToModel(this ApplicationGroupViewModel viewmodel)
        {
            return Mapper.Map<ApplicationGroupViewModel, ApplicationGroup>(viewmodel);
        }

        public static ApplicationGroupViewModel ToViewModel(this ApplicationGroup model)
        {
            return Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(model);
        }

        public static IEnumerable<ApplicationGroup> ToListModel(this IEnumerable<ApplicationGroupViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ApplicationGroupViewModel>, IEnumerable<ApplicationGroup>>(viewmodel);
        }

        public static IEnumerable<ApplicationGroupViewModel> ToListViewModel(this IEnumerable<ApplicationGroup> model)
        {
            return Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);
        }

        //ApplicationRole <-> ApplicationRoleViewModel
        public static ApplicationRole ToModel(this ApplicationRoleViewModel viewmodel)
        {
            return Mapper.Map<ApplicationRoleViewModel, ApplicationRole>(viewmodel);
        }

        public static ApplicationRoleViewModel ToViewModel(this ApplicationRole model)
        {
            return Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(model);
        }

        public static IEnumerable<ApplicationRole> ToListModel(this IEnumerable<ApplicationRoleViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ApplicationRoleViewModel>, IEnumerable<ApplicationRole>>(viewmodel);
        }

        public static IEnumerable<ApplicationRoleViewModel> ToListViewModel(this IEnumerable<ApplicationRole> model)
        {
            return Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);
        }


        //ApplicationUser <-> ApplicationUserViewModel
        public static ApplicationUser ToModel(this ApplicationUserViewModel viewmodel)
        {
            var a =  Mapper.Map<ApplicationUserViewModel, ApplicationUser>(viewmodel);
            return a;
        }

        public static ApplicationUserViewModel ToViewModel(this ApplicationUser model)
        {
            return Mapper.Map<ApplicationUser, ApplicationUserViewModel>(model);
        }

        public static IEnumerable<ApplicationUser> ToListModel(this IEnumerable<ApplicationUserViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ApplicationUserViewModel>, IEnumerable<ApplicationUser>>(viewmodel);
        }

        public static IEnumerable<ApplicationUserViewModel> ToListViewModel(this IEnumerable<ApplicationUser> model)
        {
            return Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(model);
        }




        //ApplicationPermission <-> ApplicationPermissionViewModel
        public static ApplicationPermission ToModel(this ApplicationPermissionViewModel viewmodel)
        {
            return Mapper.Map<ApplicationPermissionViewModel, ApplicationPermission>(viewmodel);
        }

        public static ApplicationPermissionViewModel ToViewModel(this ApplicationPermission model)
        {
            return Mapper.Map<ApplicationPermission, ApplicationPermissionViewModel>(model);
        }

        public static IEnumerable<ApplicationPermission> ToListModel(this IEnumerable<ApplicationPermissionViewModel> viewmodel)
        {
            return Mapper.Map<IEnumerable<ApplicationPermissionViewModel>, IEnumerable<ApplicationPermission>>(viewmodel);
        }

        public static IEnumerable<ApplicationPermissionViewModel> ToListViewModel(this IEnumerable<ApplicationPermission> model)
        {
            return Mapper.Map<IEnumerable<ApplicationPermission>, IEnumerable<ApplicationPermissionViewModel>>(model);
        }
    }
}