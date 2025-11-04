using DocsVision.BackOffice.CardLib.CardDefs;
using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.ObjectModel.Services;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.ObjectModel;
using DocsVision.Platform.ObjectModel.Search;
using DocsVision.Platform.Utils.Maybe;
using System.Collections;
using System;
using System.ComponentModel.Design;
using DocsVision.Platform.StorageServer;

namespace IntroductionToSDK
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var serverURL = System.Configuration.ConfigurationManager.AppSettings["DVUrl"];
            var username = System.Configuration.ConfigurationManager.AppSettings["Username"];
            var password = System.Configuration.ConfigurationManager.AppSettings["Password"];

            var sessionManager = SessionManager.CreateInstance();
            sessionManager.Connect(serverURL, String.Empty, username, password);

            UserSession? session = null;
            try
            {
                session = sessionManager.CreateSession();
                var context = CreateContext(session);
                SomeLogic(session, context);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            finally
            {
                session?.Close();
            }
        }

        public static ObjectContext CreateContext(UserSession session)
        {
            return DocsVision.BackOffice.ObjectModel.ContextFactory.CreateContext(session);
        }

        static void ChangeCardState(ObjectContext context, Document card, string targetState)
        {
            IStateService stateSvc = context.GetService<IStateService>();
            var branch = stateSvc.FindLineBranchesByStartState(card.SystemInfo.State)
                .FirstOrDefault(s => s.EndState.DefaultName == targetState);
            stateSvc.ChangeState(card, branch);
        }

        public static void SomeLogic(UserSession session, ObjectContext context)
        {
            Console.WriteLine($"Session: {session.Id}");

            var businessTripKind = context.FindObject<KindsCardKind>(
                new QueryObject(
                    KindsCardKind.NameProperty.Name, "Заявка на командировку"));

            var docSvc = context.GetService<IDocumentService>();
            var staffSvc = context.GetService<IStaffService>();
            var businessTrip = docSvc.CreateDocument(null, businessTripKind);
            
            businessTrip.MainInfo.Name = "hthrtaa";
            businessTrip.MainInfo[CardDocument.MainInfo.RegDate] = DateTime.Now;
            businessTrip.MainInfo.Author = staffSvc.GetCurrentEmployee();

            var dateFrom = new DateTime(2025, 11, 5, 0, 0, 0);
            var dateTo = new DateTime(2025, 11, 6, 0, 0, 0);
            if (dateTo < dateFrom)
            {
                dateTo = dateFrom;
            }
            businessTrip.MainInfo["DateComFrom"] = dateFrom;
            businessTrip.MainInfo["DateComTo"] = dateTo;

            int comDays = (dateTo - dateFrom).Days;
            businessTrip.MainInfo["BusinessTripDays"] = comDays;


            businessTrip.MainInfo["Transport"] = 1;

            var baseUniversalSvc = context.GetService<IBaseUniversalService>();
            BaseUniversalItemType cityType = baseUniversalSvc.FindItemTypeWithSameName("Города", null);
            BaseUniversalItem cityItem = baseUniversalSvc.FindItemWithSameName("Москва", cityType);
            businessTrip.MainInfo["City"] = cityItem?.GetObjectId();

            businessTrip.MainInfo["ComSum"] = 10;

            var partnersService = context.GetService<IPartnersService>();
            var company = partnersService.FindCompanyByNameOnServer(null, "MyCompany").Name;
            businessTrip.MainInfo["ReceiverPartnerCo"] = company;

            businessTrip.MainInfo["ComCause"] = "Test Test Test";

            var approvers = (IList<BaseCardSectionRow>)businessTrip.GetSection(CardDocument.Approvers.ID);
            var approverRow = new BaseCardSectionRow();
            approverRow[CardDocument.Approvers.Approver] = staffSvc.FindEmpoyeeByAccountName("ENGINEER\\DVAdmin")?.GetObjectId();
            approvers.Add(approverRow);

            var whoForm = staffSvc.FindEmpoyeeByAccountName("ENGINEER\\s.n.kolesnikova");
            businessTrip.MainInfo["WhoForm"] = whoForm?.GetObjectId();

            var comPers = staffSvc.FindEmpoyeeByAccountName("ENGINEER\\asivanov");
            businessTrip.MainInfo["ComPers"] = comPers?.GetObjectId();
            businessTrip.MainInfo["Phone"] = comPers.Phone;
            var manager = comPers.Manager;
            if (manager != null)
            {
                businessTrip.MainInfo[CardDocument.MainInfo.Registrar] = manager;
            }
            else
            {
                businessTrip.MainInfo[CardDocument.MainInfo.Registrar] = comPers;
            }

            context.AcceptChanges();

            var filePath = @"C:\Users\Sabin\Desktop\guid.txt";
            docSvc.AddMainFile(businessTrip, filePath);

            context.AcceptChanges();

            ChangeCardState(context, businessTrip, "OnApproval");

            context.AcceptChanges();

            Console.WriteLine($"New card id: {businessTrip.GetObjectId()}");
        }

    }
}
