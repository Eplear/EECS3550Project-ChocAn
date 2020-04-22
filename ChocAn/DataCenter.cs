using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace ChocAn
{
    public class DataCenter
    {
        private readonly SQLiteConnection sqliteConn = CreateConnection();
        public Hashtable ProviderDirectory;
        public DataCenter()
        {
            CreateTable(sqliteConn);
            ReadData(sqliteConn);
            sqliteConn.Close();
            InitializeDirectory();
        }
        public void InitializeDirectory()
        {
            ProviderDirectory = new Hashtable
            {
                { 246894, new ServiceInfo(246894, "Therapy Session", 100) },
                { 634378, new ServiceInfo(634378, "Swimming Lessons", 100) },
                { 448238, new ServiceInfo(448238, "Physical Checkup", 100) },
                { 893209, new ServiceInfo(893209, "Dental Cleaning", 100) },
                { 435248, new ServiceInfo(435248, "Rahab", 100) },
                { 234546, new ServiceInfo(234546, "Prescription Filling", 100) },
                { 567467, new ServiceInfo(567467, "Prescription Refill", 100) },
                { 213579, new ServiceInfo(213579, "STD Testing", 100) },
                { 980343, new ServiceInfo(980343, "Kidney Transplant", 100) },
                { 108375, new ServiceInfo(108375, "Physical Therapy", 100) }
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
        public bool? ValidateMember(int memNum)
        {
            bool? isValid = false;
            int found = 0;
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "SELECT EXISTS(SELECT 1 FROM member WHERE mNum = " + memNum.ToString() + "); ";
            try
            {
                found = sqliteCmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("ERROR: Member number not found.");
            }

            if (found >= 1)
            {
                //TODO Check suspended status of member.

                isValid = true;
            }

            

            return isValid;
        }

        public LoginToken GenerateLoginToken()
        {
            var token = new LoginToken();
            return token;
        }

        public void AddMember(Member member)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO member(mNum, mName, mStreet, mCity, mState, mZip) VALUES(" +
                "'" + member.Number.ToString() + "', " +
                "'" + member.Name + "', " +
                "'" + member.Address + "', " +
                "'" + member.City + "', " +
                "'" + member.State + "', " +
                "'" + member.Zip.ToString() + "); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void AddProvider(Provider provider)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO provider(pNum, pName, pStreet, pCity, pState, pZip) VALUES(" +
                "'" + provider.Number.ToString() + "', " +
                "'" + provider.Name + "', " +
                "'" + provider.Address + "', " +
                "'" + provider.City + "', " +
                "'" + provider.State + "', " +
                "'" + provider.Zip.ToString() + "); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void AddService(Service service)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO provider(pNum, pName, pStreet, pCity, pState, pZip) VALUES(" +
                "'" + service.DateOfService + "', " +
                "'" + service.DateReceived + "', " +
                "'" + service.ProviderNumber.ToString() + "', " +
                "'" + service.MemberNumber.ToString() + "', " +
                "'" + service.ServiceCode + "', " +
                "'" + service.Comments + "); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteMember()
        {
        }

        public void DeleteProvider()
        {
        }

        public void ModifyMember()
        {
        }

        public void ModifyProvider()
        {
        }

        public void WriteEFT()
        {
        }

        public void GetProviderDirectory()
        {
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
                                 "mZip TEXT)";

            var createServTable = "CREATE TABLE IF NOT EXISTS service(" +
                                  "currDate TEXT, " +
                                  "servDate TEXT," +
                                  "sProviderNum TEXT, " +
                                  "sMemberNum TEXT, " +
                                  "comment TEXT, " +
                                  "sCode TEXT PRIMARY KEY, " +
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

        public class LoginToken
        {
            private static Hashtable validProviders;
            private bool isValid = false;

            public void login(int provNum)
            {
            }
        }
    }
}