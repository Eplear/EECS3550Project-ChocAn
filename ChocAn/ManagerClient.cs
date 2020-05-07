using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/* To Do
 * - generate member code, should we actually? or just take string
 * - add service method, function missing from database
 * - 
 */

namespace ChocAn
{
    public class ManagerClient
    {

        public ManagerClient()
        {
            PrintHeader();
            PrintLegend();

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
         * print list of executable commands
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
         * get command and execute
         * return true if cmd was 'Exit'
         */
        bool ExecuteCommands()
        {
            bool exit = false;

            //get command from console, store in cmd
            Console.WriteLine();
            Console.Write("Enter a command: ");

            if (!char.TryParse(Console.ReadLine(), out char cmd)) cmd = ' '; //send to default case

            Console.WriteLine();

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
                    Console.WriteLine("Invalid command.");
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
         * Promts manager to enter new member details
         * adds new member to database
         */
        void AddMember()
        {
            Console.WriteLine("New member creation");
            
            //get new member details
            Console.Write("\tEnter name:    ");
            string name = Console.ReadLine();

            Console.Write("\tEnter number:  ");
            string number = Console.ReadLine();
            // generate member num
            //string number = "123456789"; //for testing, DELETE later

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            string zip = Console.ReadLine();

            //initialize a member object with entered details
            try
            {
                Member NewMember = new Member(name, number, address, city, state, zip, true);
                Program.database.AddMember(NewMember);
                //print confirmation of addition to database
                Console.WriteLine();
                Console.WriteLine("Member " + number + " added.");
            }
            catch
            {
                Console.WriteLine("Member " + number + " already exists.");
            }
        }


        /*
         * Promts manager to enter a member code
         * Checks if code exists
         * deletes member from database
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
                //TODO maybe show member details before deletion

                //Confirm member deletion
                Console.WriteLine("Are you sure you want to delte member" + number + " (Y/N)?");

                char cmd;

                while (true)
                {
                    if (char.TryParse(Console.ReadLine(), out cmd)) break;
                    
                    //invalid command, prompt again
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


        /*
         * Prompts manager to enter a member code
         * allow modifiactions to member information
         * saves changes to database
         */
        void ModifyMember()
        {
            Console.Write("Enter a member number: ");
            string number = Console.ReadLine();

            if (Program.database.ValidateMember(number) != null)
            {
                Member OldMemember = Program.database.ParseMember(number);

                string Name     = OldMemember.Name;
                string Address  = OldMemember.Address;
                string City     = OldMemember.City;
                string State    = OldMemember.State;
                string Zip      = OldMemember.Zip;
                bool Suspended  = OldMemember.Suspended;

                //show current details of member
                Console.WriteLine("Member details:");
                Console.WriteLine("Name:      " + Name);
                Console.WriteLine("Address:   " + Address);
                Console.WriteLine("City:      " + City);
                Console.WriteLine("State      " + State);
                Console.WriteLine("Zip:       " + Zip);
                Console.WriteLine("Suspended: " + Suspended);
                Console.WriteLine();

                Console.WriteLine();
                Console.Write("Modify 'name' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new name:      ");
                    Name = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'address' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new address:   ");
                    Address = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'city' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new city:      ");
                    City = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'state' field? (y/n) ");
                if (AnsweredYes())
                { 
                    Console.Write("Enter new state:     ");
                    State = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'zip' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new ZIP:       ");
                    Zip = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'suspended' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter TRUE or FALSE: ");
                    Suspended = bool.Parse(Console.ReadLine());
                }

                Member NewMember = new Member(Name, number, Address, City, State, Zip, Suspended);

                try
                {
                    Program.database.ModifyMember(OldMemember, NewMember);

                    Console.WriteLine();
                    Console.WriteLine("Record updated.");
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Record not updated.");
                }
            }
        }


        /*
         * Returns true id user typed Y
         * return false otherwise
         */
        bool AnsweredYes()
        {
            char cmd;

            while (true)
            {
                if (char.TryParse(Console.ReadLine(), out cmd)) break;

                Console.Write("Enter a y or n: ");
            }

            return (char.ToUpperInvariant(cmd) == 'Y');
        }

        /*
         * Promts manager to enter new provider details
         * adds new provider to database
         */
        void AddProvider()
        {
            Console.WriteLine("Provider creation");

            //get new provider details
            Console.Write("\tEnter name:    ");
            string name = Console.ReadLine();

            Console.Write("\tEnter number:  ");
            string number = Console.ReadLine();
            // generate provider num
            //string number = "123456789"; //for testing, DELETE later

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            string zip = Console.ReadLine();

            try
            {
                //initialize a provider object with entered details 
                Provider NewProvider = new Provider(name, number, address, city, state, zip);

                //add new provider to databse
                Program.database.AddProvider(NewProvider);

                Console.WriteLine();
                Console.WriteLine("Provider " + number + " added.");
            }
            catch
            {
                Console.WriteLine("Provider " + number + " already exists.");
            }
        }


        /*
         * Promts manager to enter a provider code
         * checks if code exists
         * deletes provider from database
         */
        void DeleteProvider()
        {
            Console.Write("Enter a provider number: ");
            string number = Console.ReadLine();

            if (!Program.database.ValidateProvider(number))
            {
                //provider does not exist
                Console.WriteLine("Provider " + number + " does not exist.");
            }
            else
            {
                //TODO maybe show provider details before deletion

                //confirm provider deletion
                Console.WriteLine("Are you sure you want to delte provider" + number + " (Y/N)?");

                char cmd;

                while (true)
                {
                    if (char.TryParse(Console.ReadLine(), out cmd)) break;

                    //invalid command, prompt again
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
         * Prompts manager to enter a provider code
         * allow modifiactions to provider information
         * saves changes to database
         */
        void ModifyProvider()
        {
            Console.Write("Enter a Provider number: ");
            string number = Console.ReadLine();

            if (Program.database.ValidateProvider(number))
            {
                Provider OldProvider = Program.database.ParseProvider(number);

                string Name = OldProvider.Name;
                string Address = OldProvider.Address;
                string City = OldProvider.City;
                string State = OldProvider.State;
                string Zip = OldProvider.Zip;

                //show current details of member
                Console.WriteLine();
                Console.WriteLine("Provider details:");
                Console.WriteLine("Name:      " + Name);
                Console.WriteLine("Address:   " + Address);
                Console.WriteLine("City:      " + City);
                Console.WriteLine("State      " + State);
                Console.WriteLine("Zip:       " + Zip);
                Console.WriteLine();

                Console.WriteLine();
                Console.Write("Modify 'name' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new name:    ");
                    Name = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'address' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new address: ");
                    Address = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'city' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new city:    ");
                    City = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'state' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new state:   ");
                    State = Console.ReadLine();
                }

                Console.WriteLine();
                Console.Write("Modify 'zip' field? (y/n) ");
                if (AnsweredYes())
                {
                    Console.Write("Enter new ZIP:     ");
                    Zip = Console.ReadLine();
                }

                Provider NewProvider = new Provider(Name, number, Address, City, State, Zip);

                try
                {
                    Program.database.ModifyProvider(OldProvider, NewProvider);

                    Console.WriteLine();
                    Console.WriteLine("Record updated.");
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Record not updated.");
                }
            }
        }


        /*
         * Adds a new service offering to database
         */
        void AddService()
        {
            Console.WriteLine("Service creation");

            //get new service details
            Console.Write("\tEnter name: ");
            string name = Console.ReadLine();

            //generate code

            Console.Write("\tEnter fee:  ");

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double serviceFee)) break;

                Console.Write("> Enter a valid fee:                  ");
            }

            //initialize new service offering from entered details

            //add new service offering to directory
            //TODO still need add service function

        }


        /*
         * prints a legend of commands
         * allows the manager to choos which report to generate
         * chosen report is created
         */
        void GenerateReports()
        {
            //print legend of executable commands for report generation
            Console.WriteLine("---------------------------- Reports ------------------------------");
            Console.WriteLine(" 1 - Generate payable summary");
            Console.WriteLine(" 2 - Send member report");
            Console.WriteLine(" 3 - Send provider report");
            Console.WriteLine(" 3 - Write EFT");
            Console.WriteLine(" 4 - Get provider directory");
            Console.WriteLine(" 5 - Generate all reports");
            Console.WriteLine(" e - Exit");
            Console.WriteLine("-------------------------------------------------------------------");

            bool exit = false; ;

            //continueally ask for command until 'Exit'
            do
            {
                Console.WriteLine();
                Console.Write("Enter a command: ");

                if (!char.TryParse(Console.ReadLine(), out char cmd)) cmd = ' '; //send to default case

                switch (cmd)
                {
                    case '1':
                        Console.WriteLine("> Generating payable summary...");
                        Program.database.GeneratePayableSummary();
                        Console.WriteLine("  Payable summary has been created.");
                        break;
                    case '2':
                        Console.WriteLine("> Sending member reports...");
                        Program.database.SendMemReport();
                        Console.WriteLine("  member reports have been sent.");
                        break;
                    case '3':
                        Console.WriteLine("> Sending provider reports...");
                        Program.database.SendProvReport();
                        Console.WriteLine("  provider reports have been sent.");
                        break;
                    case '4':
                        Console.WriteLine("> Writing EFT...");
                        Program.database.WriteEFT();
                        Console.WriteLine("  EFT written to disk.");
                        break;
                    case '5':
                        Console.WriteLine("> Getting provider directory...");
                        Program.database.GetProviderDirectory();
                        break;
                    case '6':
                        Console.WriteLine("> Generating all reports...");
                        Program.database.GeneratePayableSummary();
                        Program.database.SendMemReport();
                        Program.database.WriteEFT();
                        Console.WriteLine("  Reports have been created. ");
                        break;
                    case 'e':
                        Console.WriteLine("> Exiting");
                        exit = true;
                        break;
                    default:
                        break;
                }

            } while (!exit);
        }


        void PrintProviderDirectory(Hashtable dir)
        {
            Console.Write("Printing directory [code, name, fee].");

            foreach (string key in dir.Keys)
            {
                ServiceInfo si = (ServiceInfo)dir[key];
                Console.Write(si.Code);
                Console.Write(si.Name);
                Console.WriteLine(si.Fee);
            }
        }
    }
}
