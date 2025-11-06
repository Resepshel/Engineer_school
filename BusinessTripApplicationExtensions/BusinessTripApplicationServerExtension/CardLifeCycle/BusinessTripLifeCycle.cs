using BusinessTripApplicationServerExtension.Feature1.Services;
using DocsVision.Platform.Data.Metadata.CardModel;
using DocsVision.Platform.WebClient;
using DocsVision.WebClientLibrary.ObjectModel.Services.EntityLifeCycle;
using DocsVision.WebClientLibrary.ObjectModel.Services.EntityLifeCycle.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripApplicationServerExtension.CardLifeCycle
{
    public class BusinessTripLifeCycle : ICardLifeCycleEx
    {
        public BusinessTripLifeCycle(ICardLifeCycleEx baseLifeCycle, IBusinessTripAppService service)
        {
            this.BaseLifeCycle = baseLifeCycle;
            this.BusinessTripAppService = service;
        }
        protected ICardLifeCycleEx BaseLifeCycle { get; }
        protected IBusinessTripAppService BusinessTripAppService { get; }
        public Guid CardTypeId => BaseLifeCycle.CardTypeId;
        public Guid Create(SessionContext sessionContext, CardCreateLifeCycleOptions options)
        {
            var cardId = BaseLifeCycle.Create(sessionContext, options);
            if (options.CardKindId == new Guid("{B9C8EC14-5CAD-43EF-BC66-4E5171FA0EED}"))
            {
                BusinessTripAppService.InitBusinessTrip(sessionContext, cardId);
            }
            return cardId;
        }

        public bool CanDelete(SessionContext sessionContext, CardDeleteLifeCycleOptions options, out string message)
        => BaseLifeCycle.CanDelete(sessionContext, options, out message);

        public string GetDigest(SessionContext sessionContext, CardDigestLifeCycleOptions options)
        => BaseLifeCycle.GetDigest(sessionContext, options);

        public void OnDelete(SessionContext sessionContext, CardDeleteLifeCycleOptions options)
        => BaseLifeCycle.OnDelete(sessionContext, options);

        public void OnSave(SessionContext sessionContext, CardSaveLifeCycleOptions options)
        =>  BaseLifeCycle.OnSave(sessionContext, options);

        public bool Validate(SessionContext sessionContext, CardValidateLifeCycleOptions options, out List<ValidationResult> validationResults)
        => BaseLifeCycle.Validate(sessionContext,options,out validationResults);
    }
}
