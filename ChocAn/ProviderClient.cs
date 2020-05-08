using System;

/* To do
 * - handle login 
 * - initialize a provider from code
 * - check that service was properly logged
 */

namespace ChocAn
{
    class ProviderClient
    {
        const int maxLoginAttempts = 5;
        const string PROVIDER_DIRECTORY = "ProviderDirectory.pdf";

        Provider provider;

        /*
         * Main constructor
         * implements functions and creates the terminal interface
         */
        public ProviderClient()
        {
            //beginning
            PrintHeader();

            ///provide Provider with finite attempts to provide valid loaction
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
            
            PrintFooter();

            Environment.Exit(0);
        }


        /*
         *  Type that appears at the beginning top of terminal
         */
        void PrintHeader()
        {
            Console.WriteLine("---------------------- Chocoholics Anonymous ----------------------");
        }


        /*
         * Prompt Provider for provider number
         * return true if location exists in database
         */
        bool IsValidLocation()
        {
            bool isValid;

            Console.WriteLine();
            Console.Write("Enter provider number: ");
            string number = Console.ReadLine();

            isValid = Program.database.ValidateProvider(number);
           
            if (isValid)
            {
                provider = Program.database.ParseProvider(number);
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
                Console.WriteLine("> Invalid provider number '" + number);
                Console.WriteLine("> Access Denied.");
            }

            return isValid;
        }


        /*
         * print list of executable commands
         */
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
            bool exit = false;
            
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
                case 'e':
                    Console.WriteLine("> Exiting");
                    exit = true;
                    break;
                case 's':
                    Console.WriteLine("Switching to Manager Terminal...");
                    Console.Clear();
                    ManagerClient client = new ManagerClient();
                    break;
                default:
                    Console.WriteLine("> Invalid command");
                    break;
            }
            return exit;
        }


        /*
         * type that appears at the bottom of the terminal
         */
        void PrintFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }


        /*
         * Prompt Provider to slide member card
         * check member status
         * return false if member does not exist
         * return true if member is valid
         * return null if member is suspended
         */
        bool? CheckMemberStatus()
        {
            Console.Write("> Slide member card (type #): ");
            string MemberNumber = Console.ReadLine();

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


        /*
         * Prompt Provider for service information
         * log service rendered to database
         */
        void LogService()
        {
            //if member is invalid, don't log a service
            if (CheckMemberStatus() != true) return;

            Console.WriteLine();
            Console.Write("> Enter date of service (MM-DD-YYYY): ");
            DateTime dateOfService;
            
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out dateOfService)) break;
                
                Console.Write("> Enter a valid date MM-DD-YYYY:      ");
            }

            DateTime dateRecieved = DateTime.Now;

            Console.Write("> Enter service code or ls for list:  ");
            string serviceCode;
            while (true)
            {
                string s = Console.ReadLine();

                if (s.Equals("ls"))
                {
                    Program.database.GetProviderDirectory();
                    Console.Write("> Enter service code or ls for list:  ");
                }

                else 
                {
                    serviceCode = s;
                    //is numeric code valid
                    if (Program.database.FetchService(serviceCode).Name.Equals(null))
                    {
                        break;
                    }
                    Console.Write("> Enter a valid service code: ");
                }
            }

            Console.WriteLine("> Enter any comments:");
            string comment = Console.ReadLine();

            Service service = new Service(dateOfService, dateRecieved, provider.Name, provider.Number, "Member name", "Member number", serviceCode, comment);

            Console.Write("> Would you like to log service? y/n: ");
            char cmd;
            
            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out cmd)) break;
                
                Console.Write("Enter a y or n: ");
            }

            if (char.ToUpperInvariant(cmd) == 'Y')
            {
                Program.database.AddService(service);
                // check if properly logged?
                Console.WriteLine("> Service logged");
            }
            else
            {
                Console.WriteLine("> Service not logged");
            }
        }
    }
}