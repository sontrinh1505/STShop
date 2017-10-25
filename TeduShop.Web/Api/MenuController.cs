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
    [RoutePrefix("api/menu")]
   // [CustomAuthorize]
    public class MenuController : ApiControllerBase
    {
        #region Initialize
        IMenuService _menuService;

        public MenuController(IErrorService errorService, IMenuService menuService)
            : base(errorService)
        {
            this._menuService = menuService;
        }
        #endregion

        [Route("getall")]
       // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _menuService.GetAll(keyWord);
                totalRow = model.Count();
                var viewModel = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize).ToListViewModel();
                //var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<MenuViewModel>>(query);
                var paginationSet = new PaginationSet<MenuViewModel>()
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
                var model = _menuService.GetAll().ToListViewModel();
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
        //        var viewModel = _menuService.GetAll().ToListViewModel();
        //        //var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<MenuViewModel>>(model);
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
                var viewModel = _menuService.GetById(id).ToViewModel();
                //var responseData = Mapper.Map<Product, MenuViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, viewModel);
                return response;
            });
        }

        [Route("create")]
       // [CustomAuthorize(Roles = "Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, MenuViewModel menuGrouptVm)
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
                    
                    _menuService.Add(model);
                    _menuService.Save();

                    //var responeData = Mapper.Map<Product, MenuViewModel>(newProduct);
                    respone = request.CreateResponse(HttpStatusCode.Created, model.ToViewModel());
                }

                return respone;
            });
        }

        [Route("update")]
       // [CustomAuthorize(Roles = "Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, MenuViewModel menuGrouptVm)
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
                    _menuService.Update(model);
                    _menuService.Save();

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
                    var oldProduct = _menuService.Delete(id).ToViewModel();
                    _menuService.Save();
                   // var responeData = Mapper.Map<Product, MenuViewModel>(oldProduct);
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
                        _menuService.Delete(item);
                    }
                    _menuService.Save();
                    respone = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return respone;
            });
        }
    }
}
