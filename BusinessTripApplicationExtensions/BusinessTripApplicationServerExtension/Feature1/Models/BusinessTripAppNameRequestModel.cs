using System;

namespace BusinessTripApplicationServerExtension.Feature1.Models
{
    public class BusinessTripAppNameRequestModel {

        public Guid? EmployeeId { get; set; }

        public string CityId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
