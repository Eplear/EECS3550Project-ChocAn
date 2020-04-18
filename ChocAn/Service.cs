using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    public class Service
    {
        public DateTime DateOfService { get; set; }
        public DateTime DateReceived { get; set; }
        public string Computer { get; set; }
        public string ProviderName { get; set; }
        public int ProviderNumber { get; set; }
        public string ServiceName { get; set; }
        public string MemberName { get; set; }
        public int MemberNumber { get; set; }
        public int ServiceCode { get; set; }
        public string Comments { get; set; }
        public double Fee { get; set; }

        public Service()
        {

        }

        public Service(DateTime dateOfService, DateTime dateReceived, string computer, string providerName,
            int providerNumber, string serviceName, string memberName, int memberNumber, int serviceCode, string comments, double fee)
        {
            this.DateOfService = dateOfService;
            this.DateReceived = dateReceived;
            this.Computer = computer;
            this.ProviderName = providerName;
            this.ProviderNumber = providerNumber;
            this.ServiceName = serviceName;
            this.MemberName = memberName;
            this.MemberNumber = memberNumber;
            this.ServiceCode = serviceCode;
            this.Comments = comments;
            this.Fee = fee;
        }
    }
}
