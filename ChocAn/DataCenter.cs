using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Security.Policy;
using System.Windows;

namespace ChocAn
{
    public class DataCenter
    {
        private readonly SQLiteConnection sqliteConn = CreateConnection();
        private bool isTesting = false;
        public Hashtable ProviderDirectory;
        const string PROVIDER_DIRECTORY = "ProviderDirectory.pdf";

        public DataCenter()
        {
            CreateTable(sqliteConn);
            ReadData(sqliteConn);
            InitializeDirectory();
        }

        public DataCenter(bool isTesting)
        {
            CreateTable(sqliteConn);
            ReadData(sqliteConn);
            InitializeDirectory();
        }
        public void InitializeDirectory()
        {
            ProviderDirectory = new Hashtable
            {
                { 246894, new ServiceInfo(246894, "Therapy Session", 150) },
                { 634378, new ServiceInfo(634378, "Swimming Lessons", 45) },
                { 448238, new ServiceInfo(448238, "Physical Checkup", 30) },
                { 893209, new ServiceInfo(893209, "Dental Cleaning", 120) },
                { 435248, new ServiceInfo(435248, "Rahab", 750) },
                { 234546, new ServiceInfo(234546, "Prescription Filling", 100) },
                { 567467, new ServiceInfo(567467, "Prescription Refill", 50) },
                { 213579, new ServiceInfo(213579, "STD Testing", 15) },
                { 980343, new ServiceInfo(980343, "Kidney Transplant", 9000) },
                { 108375, new ServiceInfo(108375, "Physical Therapy", 220) }
            };
        }
        public string FetchService(int key)
        {
            try
            {
                return (string)ProviderDirectory[key];
            }
            catch
            {
                return null;
            }
        }

        public bool? ValidateMember(string memNum)
        {
            bool? isValid = null;
            string status = "Member does not exist.";
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT isSuspended EXISTS(SELECT 1 FROM member WHERE mNum = " + memNum + "); ";
            

            try
            {
                reader = sqliteCmd.ExecuteReader();
                isValid = reader.GetBoolean(10);
                if (isValid == true)
                {
                    status = "Valid Member.";
                }
                if (isValid == false)
                {
                    status = "Suspended.";
                }
                Console.WriteLine("Validation Status: " + );
            }
            catch
            {
                Console.WriteLine("ERROR: Member number not found.");
            }
            
            
            return isValid;
        }

        public bool ValidateProvider(string provNum)
        {
            bool isValid = false;
            int found = 0;
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "SELECT EXISTS(SELECT 1 FROM provider WHERE pNum = " + provNum + "); ";

            try
            {
                found = sqliteCmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("ERROR: Member number not found.");
            }

            if (found == 1) isValid = true;

            return isValid;
        }

        public void AddMember(Member member)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO member(mNum, mName, mStreet, mCity, mState, mZip) VALUES(" +
                "'" + member.Number + "', " +
                "'" + member.Name + "', " +
                "'" + member.Address + "', " +
                "'" + member.City + "', " +
                "'" + member.State + "', " +
                "'" + member.Zip.ToString() + "'); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void AddProvider(Provider provider)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO provider(pNum, pName, pStreet, pCity, pState, pZip) VALUES(" +
                "'" + provider.Number + "', " +
                "'" + provider.Name + "', " +
                "'" + provider.Address + "', " +
                "'" + provider.City + "', " +
                "'" + provider.State + "', " +
                "'" + provider.Zip.ToString() + "'); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void AddService(Service service)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO service(dServ, dRec, pNum, mNum, sCode, com) VALUES(" +
                "'" + service.DateOfService + "', " +
                "'" + service.DateReceived + "', " +
                "'" + service.ProviderNumber + "', " +
                "'" + service.MemberNumber + "', " +
                "'" + service.ServiceCode + "', " +
                "'" + service.Comments + "'); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteMember(string memberID)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "DELETE FROM member WHERE mNum='"+ memberID + "';";
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteProvider(string providerID)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "DELETE FROM provider WHERE pNum='" + providerID + "';";
            sqliteCmd.ExecuteNonQuery();
        }

        public void ModifyMember(Member oldMember, Member newMember)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "UPDATE member " +
                                    "SET mName = " + newMember.Name +
                                    "mNum = " + newMember.Number +
                                    "mStreet = " + newMember.Address +
                                    "mCity = " + newMember.City +
                                    "mState = " + newMember.State +
                                    "mZip = " + newMember.Zip +
                                    "isSuspended = " + newMember.Suspended +
                                    ";" +
                                    "WHERE mNum = '" + oldMember.Number + "'" +
                                    "LIMIT 1;";
            sqliteCmd.ExecuteNonQuery();
        }

        public void ModifyProvider(Provider oldProvider, Provider newProvider)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "UPDATE provider " +
                                    "SET mName = " + newProvider.Name +
                                    "pNum = " + newProvider.Number +
                                    "pStreet = " + newProvider.Address +
                                    "pCity = " + newProvider.City +
                                    "pState = " + newProvider.State +
                                    "pZip = " + newProvider.Zip +
                                    "WHERE pNum = '" + oldProvider.Number + "'" +
                                    "LIMIT 1;";
            sqliteCmd.ExecuteNonQuery();
        }

        public Provider ParseProvider(string pNum)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM provider WHERE pNum = '" + pNum + "';";
            reader = sqliteCmd.ExecuteReader();
            return new Provider(reader.GetString(0), 
                                reader.GetString(1), 
                                reader.GetString(2), 
                                reader.GetString(3), 
                                reader.GetString(4), 
                                reader.GetString(5));

        }

        public Member ParseMember(string mNum)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM member WHERE mNum = '" + mNum + "';";
            reader = sqliteCmd.ExecuteReader();
            return new Member(reader.GetString(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetBoolean(10));
        }

        private Service ParseService(string sCode)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sCode = '" + sCode  +"';";
            reader = sqliteCmd.ExecuteReader();
            return new Service( reader.GetDateTime(0),
                                reader.GetDateTime(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6),
                                reader.GetString(7),
                                reader.GetString(8),
                                reader.GetString(9),
                                reader.GetDouble(10));
        }
        /*
         * Created by Adam (don't know what I'm doing tho)
         * INCOMPLETE
         * need one similar for provider service list
         */
        public ArrayList MemberServiceList(string mNum)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sMemberNum = '" + mNum + "';";
            reader = sqliteCmd.ExecuteReader();
            ArrayList result = new ArrayList();
            while (reader.Read())
            {
                result.Add(new Service(reader.GetDateTime(0),
                                reader.GetDateTime(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6),
                                reader.GetString(7),
                                reader.GetString(8),
                                reader.GetString(9),
                                reader.GetDouble(10)));
                reader.NextResult();
            }
            return result;
        }
        /*
         * Created by Adam (don't know what I'm doing tho)
         * INCOMPLETE
         */
        public int TotalProviderFee(string pNum)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sProviderNum = '" + pNum + "';";
            reader = sqliteCmd.ExecuteReader();
            int result = 0;
            while (reader.Read())
            {
                //Add together the fees
                result += 0;
                reader.NextResult();
            }
            return result;
        }

        public void WriteEFT()
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM member;";
            reader = sqliteCmd.ExecuteReader();
            while (reader.Read())
            {
                Provider temp = new Provider(
                    reader.GetString(0),
                    reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5));
                Program.bankrecord.Record(temp);
                reader.NextResult();
            }
        }

        public void GetProviderDirectory()
        {
            System.Diagnostics.Process.Start(PROVIDER_DIRECTORY);
        }

        public void GeneratePayableSummary()
        {
            
        }

        public void SendMemReport()
        {
        }

        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                // thank you come again 
            }

            return sqlite_conn;
        }

        private static void CreateTable(SQLiteConnection conn)
        {
            SQLiteCommand sqliteCmd;
            var createProvTable = "CREATE TABLE IF NOT EXISTS provider(" +
                                  "pNum TEXT PRIMARY KEY, " +
                                  "pName TEXT, " +
                                  "pStreet TEXT, " +
                                  "pCity TEXT, " +
                                  "pState TEXT, " +
                                  "pZip TEXT)";

            var createMemTable = "CREATE TABLE IF NOT EXISTS member(" +
                                 "mNum TEXT PRIMARY KEY, " +
                                 "mName TEXT, " +
                                 "mStreet TEXT, " +
                                 "mCity TEXT, " +
                                 "mState TEXT, " +
                                 "mZip TEXT, " +
                                 "isSuspended BOOLEAN)";

            var createServTable = "CREATE TABLE IF NOT EXISTS service(" +
                                  "currDate TEXT, " +
                                  "servDate TEXT," +
                                  "sProviderNum TEXT, " +
                                  "sMemberNum TEXT, " +
                                  "comment TEXT, " +
                                  "sCode TEXT, " +
                                  "PRIMARY KEY(servDate, sMemberNum, sCode)," +
                                  "FOREIGN KEY(sProviderNum) REFERENCES provider(pNum), " +
                                  "FOREIGN KEY(sMemberNum) REFERENCES member(mNum))";

            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = createProvTable;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = createMemTable;
            sqliteCmd.ExecuteNonQuery();
            sqliteCmd.CommandText = createServTable;
            sqliteCmd.ExecuteNonQuery();
        }


        private static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqliteDatareader;
            SQLiteCommand sqliteCmd;
            sqliteCmd = conn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM service";

            sqliteDatareader = sqliteCmd.ExecuteReader();
            while (sqliteDatareader.Read())
            {
                var tempReader = sqliteDatareader.GetString(0);
                Console.WriteLine(tempReader);
            }
        }

    }
}