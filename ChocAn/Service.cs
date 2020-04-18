using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class Service
    {
        public DateTime DateOfService { get; set; }
        public DateTime DateReceived { get; set; }
        public string Computer { get; set; }
        public string ProviderName { get; set; }
        public string ServiceName { get; set; }
        public string MemberName { get; set; }
        public int MemberNumber { get; set; }
        public int ServiceCode { get; set; }
        public double Fee { get; set; }

        public Service()
        {

        }

        public Service(DateTime dateOfService, DateTime dateReceived, string computer, string providerName,
            string serviceName, string memberName, int memberNumber, int serviceCode, double fee)
        {
            this.DateOfService = dateOfService;
            this.DateReceived = dateReceived;
            this.Computer = computer;
            this.ProviderName = providerName;
            this.ServiceName = serviceName;
            this.MemberName = memberName;
            this.MemberNumber = memberNumber;
            this.ServiceCode = serviceCode;
            this.Fee = fee;
        }
        public string MemberReport()
        {
            return DateOfService + "\n" + ProviderName + "\n" + ServiceName + "\n"; 
        }
        public string ProviderReport()
        {
            return DateOfService + "\n" + DateReceived + "\n" + MemberName + "\n" + 
                MemberNumber + "\n" + ServiceCode + "\n" + Fee;
        }
    }
}
