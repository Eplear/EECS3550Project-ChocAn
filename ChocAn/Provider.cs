using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    public class Provider
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

        public Provider(string name, int number, string address, string city, string state, int zip)
        {
            this.Name = name;
            this.Number = number;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
        }

        public int getHelp()
        {
            return 0;
        }
        public string ServiceReport()
        {
            string result = Name + "\n" + Number + "\n" + Address + "\n" + City
                + "\n" + State + "\n" + Zip + "\n";
            /*For each service:
             *  Date Of Service
             *  Date Received by Computer 
             *  Member Name
             *  Memeber Number
             *  Service Code
             *  Fee to be paid
             */
            result += NumberOfConsultations() + "\n" + TotalFee();
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
