using BusinessTripApplicationServerExtension.Feature1.Models;
using DocsVision.BackOffice.CardLib.CardDefs;
using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectModel;
using DocsVision.Platform.WebClient;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Linq;

namespace BusinessTripApplicationServerExtension.Feature1.Services
{
    public class BusinessTripAppService : IBusinessTripAppService
    {
        public BusinessTripAppNameModel GetComPersInfo(SessionContext sessionContext, BusinessTripAppNameRequestModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var ctx = sessionContext.ObjectContext ?? throw new ArgumentException(nameof(sessionContext));

            if (!model.EmployeeId.HasValue || model.EmployeeId == Guid.Empty)
            {
                return new BusinessTripAppNameModel
                {
                    Phone = string.Empty,
                    ManagerName = string.Empty
                };
            }

            //var employee = ctx.GetObject<StaffEmployee>(model.EmployeeId);
            var staffSvc = ctx.GetService<IStaffService>();
            var employee = staffSvc.Get(model.EmployeeId.Value);
            if (employee == null)
            {
                return new BusinessTripAppNameModel
                {
                    Phone = string.Empty,
                    ManagerName = string.Empty
                };
            }

            string phone = Convert.ToString(employee.Phone) ?? string.Empty;

            var manager = staffSvc.GetEmployeeManager(employee);
            Guid managerId = Guid.Empty;
            string managerName = string.Empty;
                
            if(manager != null)
            {
                managerId = manager.GetObjectId();
                managerName = manager.DisplayString ?? string.Empty;
            }
            
            return new BusinessTripAppNameModel
            {
                Phone = phone,
                ManagerName = managerName
            };
        }

        public BusinessTripAppNameModel GetCityInfo(SessionContext sessionContext, BusinessTripAppNameRequestModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var ctx = sessionContext.ObjectContext ?? throw new ArgumentException(nameof(sessionContext));
            var baseUniversalSvc = ctx.GetService<IBaseUniversalService>();
            var cityItem = ctx.GetObject<BaseUniversalItem>(new Guid(model.CityId));
            var card = cityItem.ItemCard;
            var dayPrice = Convert.ToDecimal(card.MainInfo["DayPrice"]);
            var comDays = (model.DateTo - model.DateFrom).Days;
            decimal salary = dayPrice * comDays;

            return new BusinessTripAppNameModel
            {
               Salary = salary,
               TripDays = comDays
            };
        }

        public void InitBusinessTrip(SessionContext sessionContext, Guid cardId)
        {
            var card = sessionContext.ObjectContext.GetObject<Document>(cardId);
            var ctx = sessionContext.ObjectContext ?? throw new ArgumentException(nameof(sessionContext));
            var staffSvc = ctx.GetService<IStaffService>();
            var currentEmpl = staffSvc.GetCurrentEmployee();
            string phone = string.IsNullOrWhiteSpace(currentEmpl?.Phone) ? null : currentEmpl.Phone;

            var manager = staffSvc.GetEmployeeManager(currentEmpl);

            card.MainInfo["ComPers"] = currentEmpl?.GetObjectId();
            card.MainInfo["Phone"] = phone;
            card.MainInfo[CardDocument.MainInfo.Registrar] = manager;
            sessionContext.ObjectContext.SaveObject(card);
        }
    }
}
