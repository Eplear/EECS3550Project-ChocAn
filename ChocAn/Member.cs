namespace ChocAn
{
    /*
     * Class Member
     * Entity class for holding member information
     */
    public class Member
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool Suspended { get; set; }

        public Member(string name, string number, string address, string city, string state, string zip)
        {
            this.Name = name;
            this.Number = number;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            Suspended = false;
        }

        public Member(string name, string number, string address, string city, string state, string zip, bool isSuspended)
        {
            this.Name = name;
            this.Number = number;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Suspended = isSuspended;
        }
        
        /*
         * Suspend()
         * Suspends a member
         * @params: none
         * @returns: none
         */
        public void Suspend()
        {
            Suspended = true;
        }
        /*
         * ServiceReport()
         * Compiles information about the member and
         * the services they have been provided
         * @params: none
         * @returns: string containing compiled information
         */
        public string ServiceReport()
        {
            string result = "Name: " + Name + "\nMember Number: " + Number + "\nStreet Address: " + Address + "\nCity: "
                + City + "\nState: " + State + "\nZipcode: " + Zip + "\n";
            /* For each service:
             *  Date Of Service
             *  Proider Name
             *  Service Name
             */
            return result;
        }
    }
}
