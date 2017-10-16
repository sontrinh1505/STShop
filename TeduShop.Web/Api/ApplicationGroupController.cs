using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Common.Exceptions;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.App_Start;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/applicationGroup")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        private IApplicationGroupService _appGroupService;
        private IApplicationRoleService _appRoleService;
        private ApplicationUserManager _userManager;

        public ApplicationGroupController(IErrorService errorService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IApplicationGroupService appGroupService) : base(errorService)
        {
            _appGroupService = appGroupService;
            _appRoleService = appRoleService;
            _userManager = userManager;
        }
        [Route("getlistpaging")]
        [HttpGet]
        [Authorize(Roles = "Read")]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var listViewModel = _appGroupService.GetAll(page, pageSize, out totalRow, filter).ToListViewModel();
                //IEnumerable<ApplicationGroupViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                PaginationSet<ApplicationGroupViewModel> pagedSet = new PaginationSet<ApplicationGroupViewModel>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = listViewModel
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }
        [Route("getlistall")]
        [HttpGet]
        [Authorize(Roles = "Read")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var listViewModel = _appGroupService.GetAll().ToListViewModel();
                //IEnumerable<ApplicationGroupViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, listViewModel);

                return response;
            });
        }
        [Route("detail/{id:int}")]
        [HttpGet]
        [Authorize(Roles = "Read")]
        public HttpResponseMessage Details(HttpRequestMessage request, int id)
        {
            if (id == 0)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(id) + " is required.");
            }
            ApplicationGroup appGroup = _appGroupService.GetDetail(id);
            //var appGroupViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroup);
            var appGroupViewModel = appGroup.ToViewModel();
            if (appGroup == null)
            {
                return request.CreateErrorResponse(HttpStatusCode.NoContent, "No group");
            }

            var listRole = _appRoleService.GetListRoleByGroupId(appGroupViewModel.ID);
            //appGroupViewModel.Roles = Mapper.Map<IEnumerable<ApplicationRole>,IEnumerable<ApplicationRoleViewModel>>(listRole);
            appGroupViewModel.Roles = listRole.ToListViewModel();

            var listPermission = _appGroupService.GetListPermissionByGroupId(appGroupViewModel.ID);
            appGroupViewModel.Permissions = listPermission.ToListViewModel();
            return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Create")]
        public HttpResponseMessage Create(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppGroup = new ApplicationGroup();
                newAppGroup.Name = appGroupViewModel.Name;
                try
                {
                    var appGroup = _appGroupService.Add(newAppGroup);
                    _appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.ID,
                            RoleId = role.Id
                        });
                    }

                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    _appRoleService.Save();

                    //save Permission
                    var listPermisionGroup = new List<ApplicationPermissionGroup>();
                    foreach (var permission in appGroupViewModel.Permissions)
                    {
                        listPermisionGroup.Add(new ApplicationPermissionGroup()
                        {
                            GroupId = appGroup.ID,
                            PermissionId = permission.ID
                        });
                    }
                    
                   
                    _appGroupService.AddPermissionsToGroup(listPermisionGroup, appGroup.ID);

                    _appRoleService.Save();


                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);


                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }

            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "Update")]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationGroupViewModel appGroupViewModel)
        {
            if (ModelState.IsValid)
            {
                //var appGroup = _appGroupService.GetDetail(appGroupViewModel.ID);
                try
                {
                    //appGroup.UpdateApplicationGroup(appGroupViewModel);
                    var appGroup = appGroupViewModel.ToModel();
                    _appGroupService.Update(appGroup);
                    //_appGroupService.Save();

                    //save group
                    var listRoleGroup = new List<ApplicationRoleGroup>();
                    foreach (var role in appGroupViewModel.Roles)
                    {
                        listRoleGroup.Add(new ApplicationRoleGroup()
                        {
                            GroupId = appGroup.ID,
                            RoleId = role.Id
                        });
                    }

                    _appRoleService.AddRolesToGroup(listRoleGroup, appGroup.ID);
                    _appRoleService.Save();

                    //save Permission
                    var listPermisionGroup = new List<ApplicationPermissionGroup>();
                    foreach (var permission in appGroupViewModel.Permissions)
                    {
                        listPermisionGroup.Add(new ApplicationPermissionGroup()
                        {
                            GroupId = appGroup.ID,
                            PermissionId = permission.ID
                        });
                    }

                    _appGroupService.AddPermissionsToGroup(listPermisionGroup, appGroup.ID);                 
                    _appRoleService.Save();

                    //add role to user
                    var listRole = _appRoleService.GetListRoleByGroupId(appGroup.ID);
                    var listUserInGroup = _appGroupService.GetListUserByGroupId(appGroup.ID);
                    foreach (var user in listUserInGroup)
                    {
                        var listRoleName = listRole.Select(x => x.Name).ToList();
                        foreach (var roleName in listRoleName)
                        {
                            await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                            await _userManager.AddToRoleAsync(user.Id, roleName);
                        }
                    }

                    
                   // //add permission to group
                   // var listPermision = _appGroupService.GetListPermissionByGroupId(appGroup.ID);
                   //// var listPermisionInGroup = _appGroupService.GetListUserByGroupId(appGroup.ID);
                   // foreach (var permission in listPermision)
                   // {
                   //     var listPermissionName = listPermision.Select(x => x.Name).ToList();
                   //     foreach (var permissionName in listPermissionName)
                   //     {
                   //         await _userManager.RemoveFromRoleAsync(user.Id, roleName);
                   //         await _userManager.AddToRoleAsync(user.Id, roleName);
                   //     }
                   // }


                   // var listPermisionGroup = new List<ApplicationPermissionGroup>();

                   // foreach (var permision in permissionListAdd)
                   // {
                   //     listPermisionGroup.Add(new ApplicationPermissionGroup()
                   //     {
                   //         GroupId = group.ID,
                   //         PermissionId = permision.ID
                   //     });
                   // }


                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }

            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        [Authorize(Roles = "Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var appGroup = _appGroupService.Delete(id);
            _appGroupService.Save();
            return request.CreateResponse(HttpStatusCode.OK, appGroup);
        }

        [Route("deletemulti")]
        [Authorize(Roles = "Delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedList)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listItem = new JavaScriptSerializer().Deserialize<List<int>>(checkedList);
                    foreach (var item in listItem)
                    {
                        _appGroupService.Delete(item);
                    }

                    _appGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listItem.Count);
                }

                return response;
            });
        }


        [Route("getlistpermission")]
        [Authorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetListPermission(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _appGroupService.GetAllPermissions();
                var modelVm = model.ToListViewModel();
                //IEnumerable<ApplicationRoleViewModel> modelVm = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }
    }
}
