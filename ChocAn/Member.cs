using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
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
        

        public void Suspend()
        {
            Suspended = true;
        }
        public string ServiceReport()
        {
            string result = Name + "\n" + Number + "\n" + Address + "\n"
                + City + "\n" + State + "\n" + Zip + "\n";
            string path = "MemberReports/" + Name + ".txt";
            /* For each service:
             *  Date Of Service
             *  Proider Name
             *  Service Name
             */
            return result;
        }
    }
}
