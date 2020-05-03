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
            /*For each service:
             *  Date Of Service
             *  Date Received by Computer 
             *  Member Name
             *  Memeber Number
             *  Service Code
             *  Fee to be paid
             */
            result += "Total Number of Consultations: " + NumberOfConsultations() + "\nTotal Fees: " + TotalFee();
            return result;
        }
        //WIP
        public int NumberOfConsultations()
        {
            return 0;
        }
        //WIP
        public int TotalFee()
        {
            return 0;
        }
    }
}
