using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Customs;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Api
{
    [RoutePrefix("api/systemconfig")]
   // [CustomAuthorize]
    public class SystemConfigController : ApiControllerBase
    {
        #region Initialize
        ISystemConfigService _systemConfigService;

        public SystemConfigController(IErrorService errorService, ISystemConfigService systemConfigService)
            : base(errorService)
        {
            this._systemConfigService = systemConfigService;
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
                var model = _systemConfigService.GetAll(keyWord);
                totalRow = model.Count();
                var viewModel = model.OrderByDescending(x => x.Code).Skip(page * pageSize).Take(pageSize).ToListViewModel();
                //var responseData = Mapper.Map<IEnumerable<Product>, IEnumerable<SystemConfigViewModel>>(query);
                var paginationSet = new PaginationSet<SystemConfigViewModel>()
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


        [Route("getbyid/{id:int}")]
       // [CustomAuthorize(Roles = "Read")]
        [HttpGet]
        public HttpResponseMessage GetbyId(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var viewModel = _systemConfigService.GetById(id).ToViewModel();
                //var responseData = Mapper.Map<Product, SystemConfigViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, viewModel);
                return response;
            });
        }

        [Route("create")]
       // [CustomAuthorize(Roles = "Create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SystemConfigViewModel menuGrouptVm)
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
                    
                    _systemConfigService.Add(model);
                    _systemConfigService.Save();
                    respone = request.CreateResponse(HttpStatusCode.Created, model.ToViewModel());
                }

                return respone;
            });
        }

        [Route("update")]
       // [CustomAuthorize(Roles = "Update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SystemConfigViewModel menuGrouptVm)
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
                    _systemConfigService.Update(model);
                    _systemConfigService.Save();

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
                    var oldProduct = _systemConfigService.Delete(id).ToViewModel();
                    _systemConfigService.Save();
                   // var responeData = Mapper.Map<Product, SystemConfigViewModel>(oldProduct);
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
                        _systemConfigService.Delete(item);
                    }
                    _systemConfigService.Save();
                    respone = request.CreateResponse(HttpStatusCode.OK, listProduct.Count);
                }

                return respone;
            });
        }
    }
}
