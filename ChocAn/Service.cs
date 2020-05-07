using System;

namespace ChocAn
{
    /*
     * Class Service
     * Entity class for holding service information
     */
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

        public Service(DateTime dateOfService, DateTime dateReceived, string providerName,
            string providerNumber, string memberName, string memberNumber, string serviceCode, string comments = null)
        {
            this.DateOfService = dateOfService;
            this.DateReceived = dateReceived;
            this.ProviderName = providerName;
            this.ProviderNumber = providerNumber;
            this.ServiceName = Program.database.FetchService(serviceCode).Name;
            this.MemberName = memberName;
            this.MemberNumber = memberNumber;
            this.ServiceCode = serviceCode;
            this.Comments = comments;
            this.Fee = Program.database.FetchService(serviceCode).Fee;
        }
        /*
         * MemberReport()
         * Compiles information regarding the member to report on
         * @params: none
         * @returns: string containing compiled information
         */
        public string MemberReport()
        {
            return DateOfService + "\n" + ProviderName + "\n" + ServiceName + "\n"; 
        }
        /*
         * ProviderReport()
         * Compiles information regarding the provider to report on
         * @params: none
         * @returns: string containing compiled information
         */
        public string ProviderReport()
        {
            return DateOfService + "\n" + DateReceived + "\n" + MemberName + "\n" + 
                MemberNumber + "\n" + ServiceCode + "\n" + Fee;
        }
    }
}
