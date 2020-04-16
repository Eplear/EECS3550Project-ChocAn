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
        public ProviderClient()
        {
            printHeader();

            ///provide user with finite attempts to provide valid loaction
            int numTries = 0;
            while (!isValidLocation())
            {
                numTries++;
                if (numTries >= maxLoginAttempts)
                {
                    Console.WriteLine("> Maximum number of attempts exceeded. Please Exit.");
                    while (true) ;
                }
            }

            printLegend();
            
            //continueally ask for command until 'Exit'
            while (!executeCommands()) ;

            //include other operations
            
            printFooter();

            while (true) ;
        }

        void printHeader()
        {
            Console.WriteLine("---------------------- Chocoholics Anonymous ----------------------");
        }

        //get location code and test if its valid
        bool isValidLocation()
        {
            bool isValid = false;

            Console.WriteLine();
            Console.Write("Enter location Code: ");
            string LocationCode = Console.ReadLine();
            // use Provider class to validate location code

            /*
             * provider = grab location code
             */

            //DEBUG
            provider = new Provider("Promedica", 92, "1945 Heiss Road", "Monore", "MI", 48162);
            
            if(LocationCode == provider.Number.ToString()) isValid = true;

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
        void printLegend()
        {
            Console.WriteLine("----------------------------- Legend ------------------------------");
            Console.WriteLine(" 1 - Check member status");
            Console.WriteLine(" 2 - Log Service");
            Console.WriteLine(" e - Exit");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        //get command and execute
        //return true if cmd was 'Exit'
        bool executeCommands()
        {
            bool done = false;
            

            Console.WriteLine();
            Console.Write("Enter a command: ");
            char cmd;
            
            if (!char.TryParse(Console.ReadLine(), out cmd)) cmd = ' '; //send to default case

            switch (cmd)
            {
                case '1':
                    Console.WriteLine("> Check member status");
                    checkMemberStatus();
                    break;
                case '2':
                    Console.WriteLine("> Log service");
                    logService();
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

        void printFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }

        //command functions follow:
        bool checkMemberStatus()
        {
            bool isValid = false;
            
            Console.Write("> Slide member card (type #): ");
            string MemberNumber = Console.ReadLine();

            /*
             * member = grab member number
             * isValid = true;
             */

            //Debug
            member = new Member("Josh", 123, "1945 Heiss Road", "Monroe", "MI", 48162);
            if(MemberNumber == member.Number.ToString()) isValid = true;

            if (isValid)
            {
                Console.WriteLine();
                Console.WriteLine("Account #: " + MemberNumber);

                if (member.Suspended)
                {
                    Console.WriteLine("Status:    Suspended");
                    isValid = false;
                }
                else
                {
                    Console.WriteLine("Status:    Active");
                }
            } 
            else
            {
                Console.WriteLine("> Account not found");
            }

            return isValid;
        }

        void logService()
        {

            if (!checkMemberStatus()) return;

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

            //validate service code

            Console.Write("> Enter service fee:                  ");
            double serviceFee;
            while(true)
            {
                if (double.TryParse(Console.ReadLine(), out serviceFee)) break;
                Console.Write("> Enter a valid fee:                  ");
            }

            Service service = new Service(dateOfService, dateOfService, "comp", provider.Name, nameOfService, member.Name, member.Number, serviceCode, serviceFee);

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
