using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{ 
    class Program
    {
        const bool PROVIDER_TERMINAL = true;
        const bool MANAGER_TERMINAL  = false;

        static public DataCenter database = new DataCenter();

        public static void Main(string[] args)
        {
            //generate 'database' from files
            //DataCenter database = new DataCenter();

            //Initialize report class
            Report report = new Report();

            //enter if terminal should be provider
            if (PROVIDER_TERMINAL)
            {
                ProviderClient client = new ProviderClient();
            }

            //enter if terminal should be provider
            if (MANAGER_TERMINAL)
            {
                ManagerClient client = new ManagerClient();
            }
        }        
    }
}
