using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    class ProviderClient
    {
        protected string  LocationCode { get; set; }

        protected Provider provider;
        
        public ProviderClient()
        {

        }

        public void printHeader()
        {
            Console.WriteLine("---------------------- Chocoholics Anonymous ----------------------");
        }

        public void printFooter()
        {
            Console.WriteLine("----------------------------- Goodbye -----------------------------");
            Console.WriteLine("");
        }

        public bool isValidLocation()
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
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine("Provider: " + provider.Name);
                Console.WriteLine("Location: " + provider.Address);
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
            } 

            else
            {
                Console.WriteLine("> Invalid location code '" + LocationCode + "'. Acess Denied.");
            }

            return isValid;
        }
    }
}
