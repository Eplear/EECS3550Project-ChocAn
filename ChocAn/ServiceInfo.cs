using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class ServiceInfo
    {
        protected int Code{ get; set; }
        protected string Name { get; set; }
        protected int Fee { get; set; }

        public ServiceInfo(int code, string name, int fee)
        {
            Code = code;
            Name = name;
            Fee = fee;
        }
    }
}
