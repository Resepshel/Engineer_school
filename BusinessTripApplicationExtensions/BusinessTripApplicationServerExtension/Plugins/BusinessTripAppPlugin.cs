using AngleSharp.Io;
using BusinessTripApplicationSQL;
using DocsVision.BackOffice.ObjectModel;
using DocsVision.Layout.WebClient.Models;
using DocsVision.Layout.WebClient.Models.TableData;
using DocsVision.Layout.WebClient.Services;
using DocsVision.Platform.ObjectManager;
using DocsVision.Platform.WebClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTripApplicationServerExtension.Plugins
{
    public class BusinessTripAppPlugin : IDataGridControlPlugin
    {
        public string Name => "BusinessTripAppPlugin";
        const string CurrentCardIdParameterName = "CurrentCardId";

        public TableModel GetTableData(SessionContext sessionContext, List<ParamModel> parameters)
        {
            Guid cardId = new Guid(parameters.Find(x => x.Key == CurrentCardIdParameterName)?.Value);
            Document doc = sessionContext.ObjectContext.GetObject<Document>(cardId);
            TableModel tableModel = new TableModel();
            var employeeIdObj = doc.MainInfo["ComPers"];
            if (employeeIdObj == null || !(employeeIdObj is Guid employeeId))
                return tableModel;

            // Создаем столбцы
            string numberColumn = "Number";
            string dateFromColumn = "DateFrom";
            string cityColumn = "City";
            string reasonColumn = "Reason";
            string stateColumn = "State";

            tableModel.Columns.Add(new ColumnModel { Id = numberColumn, Name = "№", Type = DocsVision.WebClient.Models.Grid.ColumnType.String });
            tableModel.Columns.Add(new ColumnModel { Id = dateFromColumn, Name = "Дата", Type = DocsVision.WebClient.Models.Grid.ColumnType.String });
            tableModel.Columns.Add(new ColumnModel { Id = cityColumn, Name = "Город", Type = DocsVision.WebClient.Models.Grid.ColumnType.String });
            tableModel.Columns.Add(new ColumnModel { Id = reasonColumn, Name = "Цель", Type = DocsVision.WebClient.Models.Grid.ColumnType.String });
            tableModel.Columns.Add(new ColumnModel { Id = stateColumn, Name = "Состояние", Type = DocsVision.WebClient.Models.Grid.ColumnType.String });

            // Вызываем серверное расширение (SQL)
            //var sqlExtension = new BusinessTripAppSQL();
            //var results = sqlExtension.GetRequestsInfo(employeeId);

            ExtensionManager extensionManager = sessionContext.Session.ExtensionManager;
            ExtensionMethod getReqInfo = extensionManager.GetExtensionMethod("BTA_Extension", "GetRequestsInfo");
            getReqInfo.Parameters.AddNew("employeeId", ParameterValueType.Guid, employeeId);
            var results = getReqInfo.Execute();
            List<BusinessTripApplicationSQL.BusinessTripAppSQL.RequestInfo> requests = null;
            if (results is JArray jsonArray)
            {
                requests = jsonArray.ToObject<List<BusinessTripApplicationSQL.BusinessTripAppSQL.RequestInfo>>();
            }
            if (requests == null || requests.Count == 0)
                return tableModel;

            // Заполняем строки таблицы
            foreach (var r in requests)
            {
                tableModel.Rows.Add(new RowModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    EntityId = Guid.NewGuid().ToString(),
                    Cells = new List<CellModel>()
                    {
                        new CellModel { ColumnId = numberColumn, Value = r.Number.ToString() },
                        new CellModel { ColumnId = dateFromColumn, Value = r.DateFrom?.ToString("dd.MM.yyyy") ?? "" },
                        new CellModel { ColumnId = cityColumn, Value = r.City ?? "" },
                        new CellModel { ColumnId = reasonColumn, Value = r.Reason ?? "" },
                        new CellModel { ColumnId = stateColumn, Value = r.State ?? "" }
                    }
                });
            }

            tableModel.Id = Guid.NewGuid().ToString();
            return tableModel;
        }
    }
}
