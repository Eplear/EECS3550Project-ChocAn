using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocAn
{
    public class Client
    {
        protected string  LocationCode { get; set; }
        protected Provider CurrentProvider;

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

        bool validateCredential()
        {
            bool isValid = 1;

            Console.WriteLine("Enter location Code: ");
            LocationCode = Console.ReadLine();
            // use Provider class to validate location code

            /*
             * provider = grab location code
             */

            CurrentProvider = new Provider("name", 1, "add", "cit", "state", 1);

            if(isValid)
            {
                Console.WriteLine();
                Console.Write("Location: ");
                string temp = CurrentProvider.Name;
                Console.WriteLine(temp);

            } 

            else
            {
                Console.Write("> Invalid location code.");
            }
        }




    }
}
