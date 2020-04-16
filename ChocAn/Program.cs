using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChocAn
{
    class Program
    {
        const int maxNumTries = 5;

        public static void Main(string[] args)
        {
            ProviderClient provider_client = new ProviderClient();

            provider_client.printHeader();

            int numTries = 0;
            while(!provider_client.isValidLocation())
            {
                numTries++;
                if (numTries == maxNumTries)
                {
                    Console.WriteLine("> Maximum number of attempts exceeded. Please Exit.");
                    while (true) ;
                }
            }
            provider_client.printFooter();

            while (true) ;
        }
        
    }
}
