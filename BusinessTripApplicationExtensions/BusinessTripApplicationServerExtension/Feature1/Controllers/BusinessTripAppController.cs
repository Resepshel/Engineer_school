using System;
using BusinessTripApplicationServerExtension.Feature1.Models;
using BusinessTripApplicationServerExtension.Feature1.Services;
using DocsVision.Platform.WebClient;
using DocsVision.Platform.WebClient.Models;
using DocsVision.Platform.WebClient.Models.Generic;
using Microsoft.AspNetCore.Mvc;

namespace BusinessTripApplicationServerExtension.Feature1.Controllers
{
    public class BusinessTripAppController : ControllerBase
    {
        private readonly ICurrentObjectContextProvider contextProvider;
        private readonly IBusinessTripAppService BusinessTripAppService;

        public BusinessTripAppController(
            ICurrentObjectContextProvider contextProvider,
            IBusinessTripAppService BusinessTripAppService)
        {
            this.contextProvider = contextProvider;
            this.BusinessTripAppService = BusinessTripAppService;
        }

        [HttpPost]
        public CommonResponse<BusinessTripAppNameModel> GetComPersInfo([FromBody] BusinessTripAppNameRequestModel model)
        {
            var sessionContext = contextProvider.GetOrCreateCurrentSessionContext();
            var result = BusinessTripAppService.GetComPersInfo(sessionContext, model);
            return CommonResponse.CreateSuccess(result);
        }

        [HttpPost]
        public CommonResponse<BusinessTripAppNameModel> GetCityInfo([FromBody] BusinessTripAppNameRequestModel model)
        {
            var sessionContext = contextProvider.GetOrCreateCurrentSessionContext();
            var result = BusinessTripAppService.GetCityInfo(sessionContext, model);
            return CommonResponse.CreateSuccess(result);
        }
    }
}
