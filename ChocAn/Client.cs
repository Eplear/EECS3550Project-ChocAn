using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class Client
    {
        protected string  LocationCode { get; set; }

        public Client()
        {

        }

        void printHeader()
        {
            Console.WriteLine("---------------------- Chocoholics Anonymous ----------------------");
        }

        void printFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }

        void validateCredential()
        {
            bool isValid = true;

            Console.WriteLine("Enter location Code: ");
            LocationCode = Console.ReadLine();
            // use Provider class to validate location code

            /*
             * provider = grab location code
             */


            if(isValid)
            {
                Console.WriteLine();
                Console.Write("Location: ");

            } 

            else
            {
                Console.Write("> Invalid location code.");
            }
        }
    }
}
