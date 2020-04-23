using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChocAn
{
    /* Report Class
     * Writes reports into text files
     */
    class Report
    {
        public const string ReportsPath = "./Reports";
        public Report()
        {
            //setup directories for member and provider reports
            Directory.CreateDirectory(ReportsPath + "/MemberReports");
            Directory.CreateDirectory(ReportsPath + "/ProviderReports");
        }
        /* CleanupDirectories()
         * Deletes the directories for member and provider reports
         * Also delets any subdirectories
         * @params: none
         * @return: none
         */
        public void CleanupDirectories()
        {
            Directory.Delete(ReportsPath + "/MemberReports", true);
            Directory.Delete(ReportsPath + "/ProviderReports", true);
        }
        /* MemberReport()
         * writes a report into a text file for a member
         * @params: Member to be reported on
         * @return: 1 if successful, 0 else
         */
        public int MemberReport(Member m)
        {
            string currentDate = DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "-" + DateTime.Now.Date.Year;
            string path = ReportsPath + "/MemberReports/" + m.Name + " " + currentDate + ".txt";
            try
            {
                File.WriteAllText(path, m.ServiceReport());
                return 1;
            }
            catch
            {
                Console.WriteLine(path + "\nPath Not Valid");
                return 0;
            }
        }
        /* ProviderReport()
         * writes a report into a text file for a provider
         * @params: Provider to be reported on
         * @return: 1 if successful, 0 else
         */
        public int ProviderReport(Provider p)
        {
            string currentDate = DateTime.Now.Date.ToString();
            try
            {
                File.WriteAllText(ReportsPath + "/ProviderReports/" + p.Name + " - " + currentDate + ".txt", p.ServiceReport());
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}
