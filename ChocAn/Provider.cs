using System;

namespace ChocAn
{
    /*
     * Class Provider
     * Entity class for holding provider information
     */
    public class Provider
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Provider(string name, string number, string address, string city, string state, string zip)
        {
            this.Name = name;
            this.Number = number;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public Provider()
        {
        }

        /*
         * ServiceReport()
         * Compiles information about the provider and
         * the services they have provided
         * @params: none
         * @returns: string containing compiled information
         */
        public string ServiceReport()
        {
            string result = "Name: " + Name + "\nProvider Number:" + Number + "\nStreet Address: " + Address + "\nCity: " + City
                + "\nState: " + State + "\nZipcode: " + Zip + "\n";
            var list = Program.database.ProviderServiceList(Number);
            foreach (Service l in list)
            {
                result += "\n\tDate of Service: " + l.DateOfService + "\n\tDate Received by Computer: " 
                    + l.DateReceived + "\n\tMember Name: " + l.MemberName + "\n\tMember Number: " + l.MemberNumber
                    + "\n\tService Code: " + l.ServiceCode + "\n\tFee: " + l.Fee + "\n";
            }
            result += "Total Number of Consultations: " + list.Count + "\nTotal Fees: " + TotalFee();
            return result;
        }
        //Completed - functional
        public int TotalFee()
        {
            return Program.database.TotalProviderFee(Number);
        }
    }
}
