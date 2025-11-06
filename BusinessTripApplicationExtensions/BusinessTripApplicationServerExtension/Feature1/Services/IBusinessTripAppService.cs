using System;
using BusinessTripApplicationServerExtension.Feature1.Models;
using DocsVision.Platform.WebClient;

namespace BusinessTripApplicationServerExtension.Feature1.Services
{

    public interface IBusinessTripAppService
    {
        BusinessTripAppNameModel GetComPersInfo(SessionContext sessionContext, BusinessTripAppNameRequestModel model);
        BusinessTripAppNameModel GetCityInfo(SessionContext sessionContext, BusinessTripAppNameRequestModel model);
        void InitBusinessTrip(SessionContext sessionContext, Guid cardId);
    }
}
