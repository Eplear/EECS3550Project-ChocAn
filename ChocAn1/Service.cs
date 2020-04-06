using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class Service
    {
        protected DateTime DateOfService { get; set; }
        protected DateTime DateReceived { get; set; }
        protected string Computer { get; set; }
        protected string ProviderName { get; set; }
        protected string ServiceName { get; set; }
        protected string MemberName { get; set; }
        protected int MemberNumber { get; set; }
        protected int ServiceCode { get; set; }
        protected double Fee { get; set; }

        public Service(DateTime dateOfService, DateTime dateReceived, string computer, string providerName, 
            string serviceName, string memberName, int memberNumber, int serviceCode, double fee)
        {
            DateOfService = dateOfService;
            DateReceived = dateReceived;
            Computer = computer;
            ProviderName = providerName;
            ServiceName = serviceName;
            MemberName = memberName;
            MemberNumber = memberNumber;
            ServiceCode = serviceCode;
            Fee = fee;
        }
    }
}
