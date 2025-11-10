using DocsVision.Platform.StorageServer.Extensibility;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessTripApplicationSQL
{
    public class  BusinessTripAppSQL : StorageServerExtension
    {
        public BusinessTripAppSQL() { }

        public class RequestInfo
        {
            public int Number { get; set; }
            public DateTime? DateFrom { get; set; }
            public string City { get; set; }
            public string Reason { get; set; }
            public string State { get; set; }
        }

        [ExtensionMethod]
        public List<RequestInfo> GetRequestsInfo(Guid employeeId)
        {
            var results = new List<RequestInfo> ();
            using (var cmd = DbRequest.DataLayer.Connection.CreateCommand("getRequestInfo", System.Data.CommandType.StoredProcedure))
            {
                cmd.AddParameter("EmployeeId", System.Data.DbType.Guid, System.Data.ParameterDirection.Input, 0, employeeId);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        results.Add(new RequestInfo
                        {
                            Number = (int)reader.GetInt32(0),
                            DateFrom = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                            City = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Reason = reader.IsDBNull(3) ? null : reader.GetString(3),
                            State = reader.IsDBNull(4) ? null : reader.GetString(4)
                        });
                    }
                }
            }
            return results;
        }
    }
}