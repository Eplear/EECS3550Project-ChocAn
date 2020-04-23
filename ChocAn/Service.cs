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
        public string ProviderName { get; set; }
        public string ProviderNumber { get; set; }
        public string ServiceName { get; set; }
        public string MemberName { get; set; }
        public string MemberNumber { get; set; }
        public string ServiceCode { get; set; }
        public string Comments { get; set; }
        public double Fee { get; set; }

        public Service()
        {

        }

        public Service(DateTime dateOfService, DateTime dateReceived, string computer, string providerName,
            string providerNumber, string serviceName, string memberName, string memberNumber, string serviceCode, string comments, double fee)
        {
            this.DateOfService = dateOfService;
            this.DateReceived = dateReceived;
            this.ProviderName = providerName;
            this.ProviderNumber = providerNumber;
            this.ServiceName = serviceName;
            this.MemberName = memberName;
            this.MemberNumber = memberNumber;
            this.ServiceCode = serviceCode;
            this.Comments = comments;
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
