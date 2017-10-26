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
    [RoutePrefix("api/brand")]
   // [CustomAuthorize]
    public class BrandController : ApiControllerBase
    {
        #region Initialize
        IBrandService _brandService;

        public BrandController(IErrorService errorService, IBrandService brandService)
            : base(errorService)
        {
            this._brandService = brandService;
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
                var model = _brandService.GetAll(keyWord);
                totalRow = model.Count();
                var viewModel = model.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize).ToListViewModel();
                var paginationSet = new PaginationSet<BrandViewModel>()
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

        [Route("getall")]
        // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _brandService.GetAll().ToListViewModel();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getmodelbybrandid")]
        // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetModelByBrandId(HttpRequestMessage request, int brandId)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _brandService.GetModelByBrandId(brandId).ToListViewModel();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
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
                var model = _brandService.GetAll().ToListViewModel();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
       // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetbyId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var viewModel = _brandService.GetById(id).ToViewModel();
                //var responseData = Mapper.Map<Product, BrandViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, viewModel);
                return response;
            });
        }

        [Route("create")]
       // [CustomAuthorize(Roles = "Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, BrandViewModel menuGrouptVm)
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
                    
                    _brandService.Add(model);
                    _brandService.Save();

                    //var responeData = Mapper.Map<Product, BrandViewModel>(newProduct);
                    respone = request.CreateResponse(HttpStatusCode.Created, model.ToViewModel());
                }

                return respone;
            });
        }

        [Route("update")]
       // [CustomAuthorize(Roles = "Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, BrandViewModel menuGrouptVm)
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
                    _brandService.Update(model);
                    _brandService.Save();

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
                    var oldProduct = _brandService.Delete(id).ToViewModel();
                    _brandService.Save();
                   // var responeData = Mapper.Map<Product, BrandViewModel>(oldProduct);
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
                        _brandService.Delete(item);
                    }
                    _brandService.Save();
                    respone = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return respone;
            });
        }
    }
}
