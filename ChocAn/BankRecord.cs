using System;
using System.IO;

namespace ChocAn
{
    /*
     * Class BankRecord
     * Simple boundary class 
     * Used for writing EFT information to disk
     */
    public class BankRecord
    {
        //Global Variable
        public const string BankRecordsPath = "BankRecords";
               
        public BankRecord()
        {
            //setup directory
            Directory.CreateDirectory(BankRecordsPath);
        }

        /*
         * Record()
         * Writes banking records into text files
         * @param: Provider to record EFT info for
         * @returns: 1 if success, 0 if failure
         */
        public int Record(Provider p)
        {
            string currentDate = DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + DateTime.Now.Date.Year;
            string toWrite = "Name: " + p.Name + "\nNumber: " + p.Number + "\nFees to be paid: " + p.TotalFee() + "\n";
            File.WriteAllText(BankRecordsPath + "/" + p.Name + "-" + currentDate + ".txt", toWrite);
            //Maybe set fees to zero after recording?
            return 1;
            
        }
        /* CleanupDirectories()
         * Deletes the directories for member and provider reports
         * Also delets any subdirectories
         * @params: none
         * @return: none
         */
        public void CleanupDirectories()
        {
            Directory.Delete(BankRecordsPath, true);
            Directory.CreateDirectory(BankRecordsPath);
        }
    }
}
