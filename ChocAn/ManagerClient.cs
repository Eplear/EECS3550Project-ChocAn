﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* To Do
 * - generate member code
 * - modify methods
 * - generate reports method
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

            // generate member num
            int number = 123456789; //for testing, DELETE later

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            int zip = int.Parse(Console.ReadLine());
            
            //initialize a member object with entered details
            Member NewMember = new Member(name, number, address, city, state, zip);

            //add new member to the databse
            Program.database.AddMember(NewMember);

            //print confirmation of addition to database
            Console.WriteLine();
            Console.WriteLine("Member " + number + " added.");
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
            
        }


        /*
         * Promts manager to enter new provider details
         * adds new provider to database
         */
        void AddProvider()
        {
            Console.WriteLine("Provider creation");

            //get new provider details
            Console.Write("\tEnter name: ");
            string name = Console.ReadLine();

            // generate provider num
            int number = 123456789; //for testing, DELETE later

            Console.Write("\tEnter address: ");
            string address = Console.ReadLine();

            Console.Write("\tEnter city:    ");
            string city = Console.ReadLine();

            Console.Write("\tEnter state:   ");
            string state = Console.ReadLine();

            Console.Write("\tEnter ZIP:     ");
            int zip = int.Parse(Console.ReadLine());

            //initialize a provider object with entered details 
            Provider NewProvider = new Provider(name, number, address, city, state, zip);

            //add new provider to databse
            Program.database.AddProvider(NewProvider);

            Console.WriteLine();
            Console.WriteLine("Provider " + number + " added.");
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

            if (Program.database.ValidateProvider(number))
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
            double serviceFee;

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out serviceFee)) break;

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
                        Console.WriteLine("> Writing EFT...");
                        Program.database.WriteEFT();
                        Console.WriteLine("  EFT written to disk.");
                        break;
                    case '4':
                        Console.WriteLine("> Getting provider directory...");
                        Program.database.GetProviderDirectory();
                        // wait to see what function does
                        break;
                    case '5':
                        Console.WriteLine("> Generating all reports...");
                        Program.database.GeneratePayableSummary();
                        Program.database.SendMemReport();
                        Program.database.WriteEFT();
                        //Program.database.GetProviderDirectory();
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
    }
}
