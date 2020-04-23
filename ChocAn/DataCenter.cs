using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;

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
            ProviderDirectory = new Hashtable();
            ProviderDirectory.Add(246894, "Therapy Session");
            ProviderDirectory.Add(634378, "Swimming Lessons");
            ProviderDirectory.Add(448238, "Physical Checkup");
            ProviderDirectory.Add(893209, "Dental Cleaning");
            ProviderDirectory.Add(435248, "Rahab");
            ProviderDirectory.Add(234546, "Prescription Filling");
            ProviderDirectory.Add(567467, "Prescription Refill");
            ProviderDirectory.Add(213579, "STD Testing");
            ProviderDirectory.Add(980343, "Kidney Transplant");
            ProviderDirectory.Add(108375, "Physical Therapy");
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
            string status = null;
            Member member;
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "SELECT isSuspended EXISTS(SELECT 1 FROM member WHERE mNum = " + memNum + "); ";

            try
            {
                status = sqliteCmd.ExecuteScalar().ToString();
                Console.WriteLine("Validation Status: " + status);
            }
            catch
            {
                Console.WriteLine("ERROR: Member number not found.");
            }

            if (!status.Equals(null))
            {
                if (status.Equals('1'))
                {
                    isValid = true;
                }else if (status.Equals('0'))
                {
                    isValid = false;
                }
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
                "INSERT INTO service(pNum, pName, pStreet, pCity, pState, pZip) VALUES(" +
                "'" + service.DateOfService + "', " +
                "'" + service.DateReceived + "', " +
                "'" + service.ProviderNumber.ToString() + "', " +
                "'" + service.MemberNumber.ToString() + "', " +
                "'" + service.ServiceCode + "', " +
                "'" + service.Comments + "); ";
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteMember(String memberID)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "DELETE FROM member WHERE mNum='"+ memberID + "';";
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteProvider(String providerID)
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

        private Provider ParseProvider(String pNum)
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

        private Member ParseMember(String mNum)
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

        public void WriteEFT()
        {
        }

        public Hashtable GetProviderDirectory()
        {
            return ProviderDirectory;
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