using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class ProviderClient
    {
        const int maxLoginAttempts = 5;

        //protected string LocationCode;

        protected Provider provider;
        protected Member member;
        
        //main constructor and process
        public ProviderClient(DataCenter.LoginToken token)
        {
            PrintHeader();
            
            ///provide user with finite attempts to provide valid loaction
            int numTries = 0;
            while (!IsValidLocation())
            {
                numTries++;
                if (numTries >= maxLoginAttempts)
                {
                    Console.WriteLine("> Maximum number of attempts exceeded. Please Exit.");
                    while (true) ;
                }
            }

            PrintLegend();
            
            //continueally ask for command until 'Exit'
            while (!ExecuteCommands()) ;

            //include other operations
            
            PrintFooter();

            while (true) ;
        }

        void PrintHeader()
        {
            Console.WriteLine("---------------------- Chocoholics Anonymous ----------------------");
        }

        //get location code and test if its valid
        bool IsValidLocation()
        {
            bool isValid;

            Console.WriteLine();
            Console.Write("Enter location Code: ");
            string LocationCode = Console.ReadLine();

            isValid = Program.database.validateProvider(LocationCode);
           
            if (isValid)
            {
                Console.WriteLine("> Access granted.");
                Console.WriteLine();
                Console.WriteLine("-------------------------- Information ----------------------------");
                Console.WriteLine("Provider:\t" + provider.Name);
                Console.WriteLine("Location:\t" + provider.Address);
                Console.WriteLine("\t\t" + provider.City + ", " + provider.State + " " + provider.Zip);
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
            } 

            else
            {
                Console.WriteLine("> Invalid location code '" + LocationCode);
                Console.WriteLine("> Access Denied.");
            }

            return isValid;
        }
        void PrintLegend()
        {
            Console.WriteLine("----------------------------- Legend ------------------------------");
            Console.WriteLine(" 1 - Check member status");
            Console.WriteLine(" 2 - Log Service");
            Console.WriteLine(" e - Exit");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        /*
         * get command and execute
         * return true if cmd was 'Exit'
        */
        bool ExecuteCommands()
        {
            bool done = false;
            
            Console.WriteLine();
            Console.Write("Enter a command: ");

            if (!char.TryParse(Console.ReadLine(), out char cmd)) cmd = ' '; //send to default case

            switch (cmd)
            {
                case '1':
                    Console.WriteLine("> Check member status");
                    CheckMemberStatus();
                    break;
                case '2':
                    Console.WriteLine("> Log service");
                    LogService();
                    break;
                case 'e': //done
                    Console.WriteLine("> Exiting");
                    done = true;
                    break;
                default:
                    Console.WriteLine("> Invalid command");
                    break;
            }
            return done;
        }

        void PrintFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }

        //command functions follow:
        bool? CheckMemberStatus()
        {
            Console.Write("> Slide member card (type #): ");
            string MemberNumber = Console.ReadLine();

            //isValid = database.ValidateMember(123);
            bool? memStatus = Program.database.ValidateMember(MemberNumber);

            switch (memStatus)
            {
                case true:
                    Console.WriteLine();
                    Console.WriteLine("Account #: " + MemberNumber);
                    Console.WriteLine("Status:    Active");
                    break;
                case false:
                    Console.WriteLine();
                    Console.WriteLine("Account #: " + MemberNumber);
                    Console.WriteLine("Status:    Susepended");
                    break;
                default:
                    Console.WriteLine("> Account not found");
                    break;
            }

            return memStatus;
        }

        void LogService()
        {
            if (CheckMemberStatus() == true) return;

            Console.WriteLine();
            Console.Write("> Enter date of service (MM-DD-YYYY): ");
            DateTime dateOfService;
            
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out dateOfService)) break;
                
                Console.Write("> Enter a valid date MM-DD-YYYY:      ");
            }

            Console.Write("> Enter name of service:              ");
            string nameOfService = Console.ReadLine();

            Console.Write("> Enter service code:                 ");
            int serviceCode;// = Int32.Parse(Console.ReadLine());
            
            while (true)
            {
                if (Int32.TryParse(Console.ReadLine(), out serviceCode)) break;
                Console.Write("> Enter a numeric service code:       ");
            }

            //TODO validate service code

            Console.Write("> Enter service fee:                  ");
            double serviceFee;
            
            while(true)
            {
                if (double.TryParse(Console.ReadLine(), out serviceFee)) break;
                
                Console.Write("> Enter a valid fee:                  ");
            }

            Console.WriteLine("> Enter any comments:");
            string comment = Console.ReadLine();

            Service service = new Service(dateOfService, dateOfService, "comp", provider.Name, provider.Number, nameOfService, member.Name, member.Number, serviceCode, comment, serviceFee);

            Console.Write("> Would you like to log service? y/n: ");
            char cmd;
            
            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out cmd)) break;
                
                Console.Write("Enter a y or n: ");
            }

            if (char.ToUpperInvariant(cmd) == 'Y')
            {
                //log service
                //if properly logged
                Console.WriteLine("> Service logged");
            }
            else
            {
                Console.WriteLine("> Service not logged");
            }
        }
    }
}
