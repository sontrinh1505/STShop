namespace TeduShop.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TeduShop.Data.TeduShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TeduShop.Data.TeduShopDbContext context)
        {
            CreateProductCategorySample(context);
            CreateSlide(context);
            CreatePage(context);
            CreateContactDetail(context);
            CreateConfigTitle(context);
            CreateFooter(context);
            CreateUser(context);
            CreateGroup(context);
            CreatePermission(context);
            CreateMenu(context);
            CreateModelBrand(context);
        }

        private void CreateConfigTitle(TeduShop.Data.TeduShopDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "ST Shop"
                });
            }

            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "ST Shop"
                });
            }

            if (!context.SystemConfigs.Any(x => x.Code == "HomeMataDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMataDescription",
                    ValueString = "ST Shop"
                });
            }

            if (!context.SystemConfigs.Any(x => x.Code == "Email"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "Email",
                    ValueString = "STShopSupport@gmail.com"
                });
            }

            if (!context.SystemConfigs.Any(x => x.Code == "Phone"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "Phone",
                    ValueString = "01648484969"
                });
            }
        }

        private void CreateProductCategorySample(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name = "Laptop",Alias = "Laptop", Status = true },
                new ProductCategory() { Name = "Phone",Alias = "Phone", Status = true },
            };

                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }
        }


        private void CreateUser(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));

                //var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TeduShopDbContext()));

                var rolemanager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(new TeduShopDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "Administrator@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Administrator"

                };
                if (manager.Users.Count(x => x.UserName == "admin") == 0)
                {
                    manager.Create(user, "123456");

                    if (!rolemanager.Roles.Any())
                    {
                        rolemanager.Create(new ApplicationRole { Name = "Read", Description = "Read" });
                        rolemanager.Create(new ApplicationRole { Name = "Create", Description = "Create" });
                        rolemanager.Create(new ApplicationRole { Name = "Update", Description = "Update" });
                        rolemanager.Create(new ApplicationRole { Name = "Delete", Description = "Delete" });
                    }

                    var adminuser = manager.FindByEmail("Administrator@gmail.com");

                    manager.AddToRoles(adminuser.Id, new string[] { "Read", "Create", "Update", "Delete" });
                }

            }

        }

        private void CreateFooter(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Footers.Count(x => x.ID == ComomConstants.defaultFooterId) == 0)
            {
                string content = "Footer";
                context.Footers.Add(new Footer()
                {
                    ID = ComomConstants.defaultFooterId,
                    Content = content

                });
                context.SaveChanges();
            }
        }

        private void CreateSlide(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Slides.Count() == 0)
            {
                List<Slide> listSlide = new List<Slide>()
                {
                    new Slide() {
                        Name ="Slide 1",
                        DisplayOrder =1,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag.jpg",
                        Content =@"	<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>
                                <p>Lorem ipsum dolor sit amet, consectetur 
                            adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >
                        <span class=""on-get"">GET NOW</span>" },
                    new Slide() {
                        Name ="Slide 2",
                        DisplayOrder =2,
                        Status =true,
                        Url ="#",
                        Image ="/Assets/client/images/bag1.jpg",
                        Content=@"<h2>FLAT 50% 0FF</h2>
                                <label>FOR ALL PURCHASE <b>VALUE</b></label>

                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et </ p >

                                <span class=""on-get"">GET NOW</span>"},
                };
                context.Slides.AddRange(listSlide);
                context.SaveChanges();
            }
        }

        private void CreatePage(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                var page = new Page()
                {
                    Alias = "about",
                    Name = "about",
                    Content = @"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam
                    eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit,
                    sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt.Neque porro quisquam est,
                    qui dolorem ipsum quia dolor sit amet,
                    consectetur,
                    adipisci velit,
                    sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem.Ut enim ad minima veniam,
                    quis nostrum exercitationem ullam corporis suscipit laboriosam,
                    nisi ut aliquid ex ea commodi consequatur ? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur,
                    vel illum qui dolorem eum fugiat quo voluptas nulla pariatur ? ",
                    Status = true
                };
                context.Pages.Add(page);
                context.SaveChanges();
            }
        }


        private void CreateContactDetail(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                var contactDetail = new TeduShop.Model.Models.ContactDetail()
                {
                    Name = "ST Shop",
                    Address = @"69 quang trung",
                    Email = "st@gmail.com",
                    Lat = 10.8418171,
                    Lng = 106.6415437,
                    Phone = "09069692111",
                    Website = "http://st.com",
                    Others = "",
                    Status = true
                };
                context.ContactDetails.Add(contactDetail);
                context.SaveChanges();
            }
        }


        private void CreatePermission(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.ApplicationPermissions.Count() == 0)
            {
                var permissionList = new List<ApplicationPermission>()
                {
                    new ApplicationPermission
                    {
                        Name = "User",
                        Description = "User",
                        ControllerName="ApplicationUser",
                    },
                    new ApplicationPermission
                    {
                        Name = "Group",
                        Description = "Group",
                        ControllerName="ApplicationGroup",
                    },
                    new ApplicationPermission
                    {
                        Name = "Role",
                        Description = "Role",
                        ControllerName="ApplicationRole",
                    },
                    new ApplicationPermission
                    {
                        Name = "Product",
                        Description = "Product",
                        ControllerName="Product",
                    },
                    new ApplicationPermission
                    {
                        Name = "Catagory",
                        Description = "Catagory",
                        ControllerName="ProductCategory",
                    },
                    new ApplicationPermission
                    {
                        Name = "Post",
                        Description = "Post",
                        ControllerName="PostCategory",
                    }
                };

                context.ApplicationPermissions.AddRange(permissionList);
                context.SaveChanges();

                var group = context.ApplicationGroups.FirstOrDefault(x => x.Name == "Super Admin");

                var permissionListAdd = context.ApplicationPermissions.ToList();

                var roles = context.ApplicationRoles.ToList();

                var listPermisionGroup = new List<ApplicationPermissionGroup>();

                var listRolePermission = new List<ApplicationRolePermission>();

                foreach (var permision in permissionListAdd)
                {
                    listPermisionGroup.Add(new ApplicationPermissionGroup()
                    {
                        GroupId = group.ID,
                        PermissionId = permision.ID
                    });
                    foreach (var role in roles)
                    {
                        listRolePermission.Add(new ApplicationRolePermission()
                        {

                            RoleId = role.Id,
                            PermissonId = permision.ID,
                            GroupId = group.ID
                        });
                    }
                }

                context.ApplicationPermissionGroups.AddRange(listPermisionGroup);
                context.ApplicationRolePermissions.AddRange(listRolePermission);
                context.SaveChanges();
            }

        }


        private void CreateGroup(TeduShop.Data.TeduShopDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TeduShopDbContext()));
            if (context.ApplicationGroups.Count() == 0)
            {
                var groupList = new List<ApplicationGroup>()
                {
                    new ApplicationGroup
                    {
                        Name = "Super Admin",
                        Description = "Super Admin"
                    },
                    new ApplicationGroup
                    {
                        Name = "Admin",
                        Description = "Admin"
                    },
                    new ApplicationGroup
                    {
                        Name = "User",
                        Description = "User"
                    }

                };



                context.ApplicationGroups.AddRange(groupList);
                context.SaveChanges();

                var group = context.ApplicationGroups.FirstOrDefault(x => x.Name == "Super Admin");
                var adminuser = manager.FindByEmail("Administrator@gmail.com");
                var userGroup = new ApplicationUserGroup()
                {
                    UserId = adminuser.Id,
                    GroupId = group.ID,
                };

                context.ApplicationUserGroups.Add(userGroup);
                context.SaveChanges();
            }
        }

        private void CreateMenu(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.MenuGroups.Count() == 0)
            {
                var groupList = new List<MenuGroup>()
                {
                    new MenuGroup
                    {
                        Name = "Laptops"
                    },
                    new MenuGroup
                    {
                        Name = "Phones"
                    },
                    new MenuGroup
                    {
                        Name = "Tablets"
                    },
                    new MenuGroup
                    {
                        Name = "TV & Audio"
                    }

                };

                context.MenuGroups.AddRange(groupList);
                context.SaveChanges();

                var laptopGroup = context.MenuGroups.Where(x => x.Name == "Laptops").SingleOrDefault();
                var phoneGroup = context.MenuGroups.Where(x => x.Name == "Phones").SingleOrDefault();
                var tabletGroup = context.MenuGroups.Where(x => x.Name == "Tablets").SingleOrDefault();
                var tvAudioGroup = context.MenuGroups.Where(x => x.Name == "TV & Audio").SingleOrDefault();

                var MenuList = new List<Menu>()
                {
                    new Menu
                    {
                        URL="#",
                        Name = "Dell",
                        GroupID =laptopGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Asus",
                        GroupID =laptopGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "MSI",
                        GroupID =laptopGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Aser",
                        GroupID =laptopGroup.ID
                    },
                     new Menu
                    {
                         URL="#",
                        Name = "Apple",
                        GroupID =phoneGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Samsung",
                        GroupID =phoneGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "One Plus",
                        GroupID =phoneGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Xiaomi",
                        GroupID =phoneGroup.ID
                    },
                     new Menu
                    {
                        URL="#",
                        Name = "Apple",
                        GroupID = tabletGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Samsung",
                        GroupID = tabletGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Apple",
                        GroupID = tvAudioGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Samsung",
                        GroupID = tvAudioGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Sony",
                        GroupID = tvAudioGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "LG",
                        GroupID = tvAudioGroup.ID
                    },
                    new Menu
                    {
                        URL="#",
                        Name = "Asanzo",
                        GroupID = tvAudioGroup.ID
                    }


                };

                context.Menus.AddRange(MenuList);
                context.SaveChanges();
            }
        }

        private void CreateModelBrand(TeduShop.Data.TeduShopDbContext context)
        {
            if (context.Brands.Count() == 0)
            {
                var groupList = new List<Brand>()
                {
                    new Brand
                    {
                        Name = "Apple",
                        Country = "USA",
                        Status = true
                    },
                    new Brand
                    {
                        Name = "Sansung",
                        Country = "Korea",
                        Status = true

                    },
                    new Brand
                    {
                        Name = "One Plus",
                        Country = "China",
                        Status = true
                    },
                    new Brand
                    {
                        Name = "XiaoMi",
                        Country = "China",
                        Status = true
                    }

                };

                context.Brands.AddRange(groupList);
                context.SaveChanges();

                var appleBrand = context.Brands.Where(x => x.Name == "Apple").SingleOrDefault();
                var samsungBrand = context.Brands.Where(x => x.Name == "Sansung").SingleOrDefault();
                var oneplusBrand = context.Brands.Where(x => x.Name == "One Plus").SingleOrDefault();
                var xiaomiBrand = context.Brands.Where(x => x.Name == "XiaoMi").SingleOrDefault();

                var modelList = new List<ModelBrand>()
                {
                    new ModelBrand
                    {
                        Name="Iphone 6",
                        Mode = "iphone6",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone 6 Plus",
                        Mode = "iphone6plus",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone 6s",
                        Mode = "iphone6s",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone 6s Plus",
                        Mode = "iphone6splus",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone 7",
                        Mode = "iphone7",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone 7 Plus",
                        Mode = "iphone7plus",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Iphone 8",
                        Mode = "iphone8",
                        BrandID = appleBrand.ID,
                        Status = true
                    },

                     new ModelBrand
                    {
                        Name="Iphone 8",
                        Mode = "iphone8plus",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                   new ModelBrand
                    {
                        Name="Iphone X",
                        Mode = "iphonex",
                        BrandID = appleBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Galaxy S7",
                        Mode = "galaxys7",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Galaxy S7 Edge",
                        Mode = "galaxys7edge",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Galaxy S8",
                        Mode = "galaxys78",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Galaxy S8 Edge",
                        Mode = "galaxys8edge",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Galaxy Note7",
                        Mode = "galaxynote7",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Galaxy Note 8",
                        Mode = "galaxynote8",
                        BrandID = samsungBrand.ID,
                        Status = true
                    },

                    new ModelBrand
                    {
                        Name="One Plus 2",
                        Mode = "oneplus2",
                        BrandID = oneplusBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="One Plus 3",
                        Mode = "oneplus3",
                        BrandID = oneplusBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="One Plus 3T",
                        Mode = "oneplus3",
                        BrandID = oneplusBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="One Plus 5",
                        Mode = "oneplus5",
                        BrandID = oneplusBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Redmi 4 ",
                        Mode = "Redmi4",
                        BrandID = xiaomiBrand.ID,
                        Status = true
                    },
                    new ModelBrand
                    {
                        Name="Redmi Note 4 ",
                        Mode = "Redminote4",
                        BrandID = xiaomiBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Redmi Note 4X ",
                        Mode = "Redminote4x",
                        BrandID = xiaomiBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Mi 5",
                        Mode = "mi5",
                        BrandID = xiaomiBrand.ID,
                        Status = true
                    },
                     new ModelBrand
                    {
                        Name="Mi 6",
                        Mode = "mi6",
                        BrandID = xiaomiBrand.ID,
                        Status = true
                    },

                };

                context.ModelBrands.AddRange(modelList);
                context.SaveChanges();
            }
        }


    }
}
