using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Data.Repositories;
using TeduShop.Service;
using TeduShop.Web.Customs;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/menugroup")]
   // [CustomAuthorize]
    public class MenuGroupController : ApiControllerBase
    {
        #region Initialize
        IMenuGroupService _menuGroupService;

        public MenuGroupController(IErrorService errorService, IMenuGroupService menuGroupService)
            : base(errorService)
        {
            this._menuGroupService = menuGroupService;
        }
        #endregion

        [Route("getall")]
       // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _menuGroupService.GetAll();
                totalRow = model.Count();
                var viewModel = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize).ToListViewModel();
                //var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<MenuGroupViewModel>>(query);
                var paginationSet = new PaginationSet<MenuGroupViewModel>()
                {
                    Items = viewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("getallparents")]
        // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetParent(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _menuGroupService.GetAll().ToListViewModel();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        //[Route("getall")]
        ////[CustomAuthorize(Roles = "Read")]
        //[HttpGet]
        //public HttpResponseMessage GetAll(HttpRequestMessage request)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        var viewModel = _menuGroupService.GetAll().ToListViewModel();
        //        //var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<MenuGroupViewModel>>(model);
        //        var response = request.CreateResponse(HttpStatusCode.OK, viewModel);
        //        return response;
        //    });
        //}

        [Route("getbyid/{id:int}")]
       // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetbyId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var viewModel = _menuGroupService.GetById(id).ToViewModel();
                //var responseData = Mapper.Map<Product, MenuGroupViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, viewModel);
                return response;
            });
        }

        [Route("create")]
       // [CustomAuthorize(Roles = "Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, MenuGroupViewModel menuGrouptVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;

                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var model = menuGrouptVm.ToModel();
                    
                    _menuGroupService.Add(model);
                    _menuGroupService.Save();

                    //var responeData = Mapper.Map<Product, MenuGroupViewModel>(newProduct);
                    respone = request.CreateResponse(HttpStatusCode.Created, model.ToViewModel());
                }

                return respone;
            });
        }

        [Route("update")]
       // [CustomAuthorize(Roles = "Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, MenuGroupViewModel menuGrouptVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;

                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var model = menuGrouptVm.ToModel();
                    _menuGroupService.Update(model);
                    _menuGroupService.Save();

                    respone = request.CreateResponse(HttpStatusCode.OK, model.ToViewModel());
                }

                return respone;
            });
        }

        [Route("delete")]
      //  [CustomAuthorize(Roles = "Delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;

                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldProduct = _menuGroupService.Delete(id).ToViewModel();
                    _menuGroupService.Save();
                   // var responeData = Mapper.Map<Product, MenuGroupViewModel>(oldProduct);
                    respone = request.CreateResponse(HttpStatusCode.OK, oldProduct);
                }

                return respone;
            });
        }

        [Route("deletemulti")]
       // [CustomAuthorize(Roles = "Delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedProducts)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage respone = null;

                if (!ModelState.IsValid)
                {
                    respone = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProduct = new JavaScriptSerializer().Deserialize<List<int>>(checkedProducts);
                    foreach (var item in listProduct)
                    {
                        _menuGroupService.Delete(item);
                    }
                    _menuGroupService.Save();
                    respone = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return respone;
            });
        }
    }
}
