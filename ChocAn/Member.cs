using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    public class Member
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public bool Suspended { get; set; }

        public Member(string name, int number, string address, string city, string state, int zip)
        {
            this.Name = name;
            this.Number = number;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            Suspended = false;
        }

        public void Suspend()
        {
            Suspended = true;
        }
        public string ServiceReport()
        {
            string result = Name + "\n" + Number + "\n" + Address + "\n"
                + City + "\n" + State + "\n" + Zip + "\n";
            /* For each service:
             *  Date Of Service
             *  Proider Name
             *  Service Name
             */
            return result;
        }
    }
}
