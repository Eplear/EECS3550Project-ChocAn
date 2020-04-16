using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class ProviderClient
    {
        const int maxNumTries = 5;
        protected string  LocationCode { get; set; }

        protected Provider provider;
        
        //main constructor and process
        public ProviderClient()
        {
            printHeader();

            ///provide user with finite attempts to provide valid loaction
            int numTries = 0;
            while (!isValidLocation())
            {
                numTries++;
                if (numTries == maxNumTries)
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
            LocationCode = Console.ReadLine();
            // use Provider class to validate location code

            /*
             * provider = grab location code
             * 
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
                Console.WriteLine("> Invalid location code '" + LocationCode + "'. Acess Denied.");
            }

            return isValid;
        }
        void printLegend()
        {
            Console.WriteLine("----------------------------- Legend ------------------------------");
            Console.WriteLine("\ta - Log Service");
            Console.WriteLine("\tb - Add User");
            Console.WriteLine("\tc - Delete User");
            Console.WriteLine("\td - Exit");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        //get command and execute
        //return true if cmd was 'Exit'
        bool executeCommands()
        {
            bool done = false;
            char cmd;
            Console.Write("Enter a command: ");
            cmd = char.Parse(Console.ReadLine());

            switch (cmd)
            {
                case 'a':
                    Console.WriteLine("> Log Service");
                    Console.WriteLine();
                    break;
                case 'b':
                    Console.WriteLine("> Add user");
                    Console.WriteLine();
                    break;
                case 'c':
                    Console.WriteLine("> Delete user");
                    Console.WriteLine();
                    break;
                case 'd': //done
                    Console.WriteLine("> Exiting.");
                    done = true;
                    break;
                default:
                    Console.WriteLine("> Invalid command");
                    Console.WriteLine();
                    break;

            }
            return done;
        }

        void printFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }
    }
}
