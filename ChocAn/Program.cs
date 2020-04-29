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

            //Testing Member Reports - Adam
            Member membtest = new Member("Jim Bob", "93201409", "908 Elm St", "Detroit", "Michigan", "12948");
            report.MemberReport(membtest);
            //Cleanup test reports
            Console.WriteLine("Press any key to clean up report directories");
            Console.ReadKey();
            report.CleanupDirectories();

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
