using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChocAn
{
    class BankRecord
    {
        public const string BankRecordsPath = "C:/Users/adamb/Documents/GitHub/EECS3550Project-ChocAn/ChocAn/BankRecords";
        public BankRecord()
        {
            //setup directory
            Directory.CreateDirectory(BankRecordsPath);
        }
        public int Record(Provider p)
        {
            string currentDate = DateTime.Now.Date.ToString();
            try
            {
                string toWrite = "Name: " + p.Name + "\nNumber" + p.Number + "\nFees to be paid" + p.TotalFee() + "\n";
                File.WriteAllText(BankRecordsPath + p.Name + currentDate + ".txt", toWrite);
                //Maybe set fees to zero after recording?
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
