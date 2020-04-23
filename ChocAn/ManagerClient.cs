using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* To Do
 * - generate member code
 */

namespace ChocAn
{
    class ManagerClient
    {

        public ManagerClient()
        {
            PrintHeader();

            //validate manager

            //continueally ask for command until 'Exit'
            while (!ExecuteCommands()) ;

            //end of console
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
         * 
         */
        void PrintLegend()
        {
            Console.WriteLine("----------------------------- Legend ------------------------------");
            Console.WriteLine(" 1 - Add new member");
            Console.WriteLine(" 2 - Delete member");
            Console.WriteLine(" 3 - Modify member");
            Console.WriteLine(" 4 - Add new provider");
            Console.WriteLine(" 5 - Delete Provider");
            Console.WriteLine(" 6 - Modify provider");
            Console.WriteLine(" 7 - Add new service");
            Console.WriteLine(" 8 - Generate reports");
            Console.WriteLine(" e - Exit");
            Console.WriteLine("-------------------------------------------------------------------");
        }


        /*
         * 
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
                    Console.WriteLine("> Add new member");
                    AddMember();
                    break;
                case '2':
                    Console.WriteLine("> Delete member");
                    DeleteMember();
                    break;
                case '3':
                    Console.WriteLine("> Modify Member");
                    ModifyMember();
                    break;
                case '4':
                    Console.WriteLine("> Add new provider");
                    AddProvider();
                    break;
                case '5':
                    Console.WriteLine("> Delete provider");
                    DeleteProvider();
                    break;
                case '6':
                    Console.WriteLine("> Modify provider");
                    ModifyProvider();
                    break;
                case '7':
                    Console.WriteLine("> Add service");
                    AddService();
                    break;
                case '8':
                    Console.WriteLine("> Generate reports");
                    GenerateReports();
                    break;
                case 'e':
                    Console.WriteLine("> Exiting");
                    exit = true;
                    break;
                default:
                    break;
            }

            return exit;
        }


        /*
         * 
         */
        void PrintFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }


        /*
         * Prompt user manager
         */
        void AddMember()
        {
            Console.WriteLine("New member creation");
            Console.Write("\tEnter name:    ");
            string name = Console.ReadLine();

            // generate member num
            int number = 123456789; 

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            int zip = int.Parse(Console.ReadLine());
            
            Member NewMember = new Member(name, number, address, city, state, zip);

            Program.database.AddMember(NewMember);

            Console.WriteLine();
            Console.WriteLine("Member " + number + " added.");
        }


        /*
         * 
         */
        void DeleteMember()
        {
            Console.Write("Enter a member number: ");
            string number = Console.ReadLine();

            if(Program.database.ValidateMember(number) == null)
            {
                // member does not exist
                Console.WriteLine("Member " + number + " does not exist.");
            }
            else
            {
                // maybe show member details before deletion
                Console.WriteLine("Are you sure you want to delte member" + number + " (Y/N)?");

                //confirm choice to delete
                Console.Write("> Would you like to delete member? y/n: ");
                char cmd;

                while (true)
                {
                    if (char.TryParse(Console.ReadLine(), out cmd)) break;

                    Console.Write("Enter a y or n: ");
                }

                if (char.ToUpperInvariant(cmd) == 'Y')
                {
                    //continue with deletion
                    Program.database.DeleteMember(number);
                    Console.WriteLine("Member deleted.");
                }
            }
        }

        void ModifyMember()
        {
            
        }


        /*
         * 
         */
        void AddProvider()
        {
            Console.WriteLine("Provider creation");
            Console.Write("\tEnter name: ");
            string name = Console.ReadLine();

            // generate provider num
            int number = 123456789;

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            int zip = int.Parse(Console.ReadLine());

            Provider NewProvider = new Provider(name, number, address, city, state, zip);

            Program.database.AddProvider(NewProvider);

            Console.WriteLine();
            Console.WriteLine("Provider " + number + " added.");
        }


        /*
         * 
         */
        void DeleteProvider()
        {
            Console.Write("Enter a provider number: ");
            string number = Console.ReadLine();

            if (Program.database.ValidateProvider(number))
            {
                // member does not exist
                Console.WriteLine("Provider " + number + " does not exist.");
            }
            else
            {
                // maybe show member details before deletion
                Console.WriteLine("Are you sure you want to delte provider" + number + " (Y/N)?");

                //confirm choice to delete
                Console.Write("> Would you like to delete provider? y/n: ");
                char cmd;

                while (true)
                {
                    if (char.TryParse(Console.ReadLine(), out cmd)) break;

                    Console.Write("Enter a y or n: ");
                }

                if (char.ToUpperInvariant(cmd) == 'Y')
                {
                    //continue with deletion
                    Program.database.DeleteMember(number);
                    Console.WriteLine("Provider deleted.");
                }
            }
        }

        /*
         * 
         */
        void ModifyProvider()
        {

        }


        /*
         * 
         */
        void AddService()
        {
            Console.WriteLine("Service creation");
            Console.Write("\tEnter name: ");
            string name = Console.ReadLine();

            //generate code

            Console.Write("\tEnter fee:  ");
            double serviceFee;

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out serviceFee)) break;

                Console.Write("> Enter a valid fee:                  ");
            }

            //still need add service function

        }

        /*
         * 
         */
        void GenerateReports()
        {

        }
    }
}
