using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAn
{
    class Program
    {
        const bool OPERATOR_PROVIDER = true;
        const bool OPERATOR_MANAGER  = false;

        public static void Main(string[] args)
        {
            //generate 'database' from files
            DataCenter database = new DataCenter();

            //enter if terminal should be provider
            if (OPERATOR_PROVIDER)
            {
                ProviderClient client = new ProviderClient(database.GenerateLoginToken());
            }

            //enter if terminal should be provider
            if (OPERATOR_MANAGER)
            {
                ManagerClient client = new ManagerClient();
            }
            
        }
        
    }
}
