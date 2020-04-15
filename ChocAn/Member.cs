using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class Member
    {
        protected string Name { get; set; }
        protected int Number { get; set; }
        protected string Address { get; set; }
        protected string City { get; set; }
        protected string State { get; set; }
        protected int Zip { get; set; }
        protected bool Suspended { get; set; }

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

        void Suspend()
        {
            Suspended = true;
        }
    }
}
