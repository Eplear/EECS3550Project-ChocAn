﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
/* BROKEN
 * validate member
 * modify provider
 */
namespace ChocAn
{
    public class DataCenter
    {
        private readonly SQLiteConnection sqliteConn = CreateConnection();
        public Hashtable ProviderDirectory;
        const string PROVIDER_DIRECTORY = "ProviderDirectory.pdf";

        public DataCenter()
        {
            CreateTable(sqliteConn);
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
        public ServiceInfo FetchService(string key)
        {
            try
            {
                return (ServiceInfo)ProviderDirectory[int.Parse(key)];
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
            int found = 0;
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT EXISTS(SELECT isSuspended FROM member WHERE mNum = @memNum); ";
            sqliteCmd.Parameters.AddWithValue("@memNum", memNum);
                        
            try
            {
                found = Convert.ToInt32(sqliteCmd.ExecuteScalar());
                // Checks if any rows are returned.
                if (found != 0)
                {
                    // Checks suspension status of member.
                    reader = sqliteCmd.ExecuteReader();
                    reader.Read();
                    isValid = reader.GetBoolean(0);
                }
            }
            catch
            {
                Console.WriteLine("ERROR: Member number not found.");
            }


            if (isValid == true)
            {
                status = "Valid Member.";
            }
            if (isValid == false)
            {
                status = "Suspended.";
            }

            // Returns 'null' if member doesn't exist, 
            //    'false' if the member is suspended, 
            //    and 'true' if the member exists with no suspension

            return isValid;
        }

        public bool ValidateProvider(string provNum)
        {
            bool isValid = false;
            int found = 0;
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "SELECT EXISTS(SELECT 1 FROM provider WHERE pNum = @provNum)";
            sqliteCmd.Parameters.AddWithValue("@provNum", provNum);
            
            try
            {
                // 'found' will equal 0 if no matching provider is found.
                found = Convert.ToInt32(sqliteCmd.ExecuteScalar());
            }
            catch
            {
                Console.WriteLine("ERROR: Provider number not found.");
            }

            isValid = (found == 1);

            return isValid;
        }

        public void AddMember(Member member)
        {
            int isSuspended = 0;
            if (member.Suspended) isSuspended = 1;
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "INSERT INTO member(mNum, mName, mStreet, mCity, mState, mZip, isSuspended) " +
                    "VALUES(@num, @name, @street, @city, @state, @zip, @suspended)";
            sqliteCmd.Parameters.AddWithValue("@num", member.Number);
            sqliteCmd.Parameters.AddWithValue("@name", member.Name);
            sqliteCmd.Parameters.AddWithValue("@street", member.Address);
            sqliteCmd.Parameters.AddWithValue("@city", member.City);
            sqliteCmd.Parameters.AddWithValue("@state", member.State);
            sqliteCmd.Parameters.AddWithValue("@zip", member.Zip);
            sqliteCmd.Parameters.AddWithValue("@suspended", isSuspended);

            try
            {
                sqliteCmd.ExecuteNonQuery();
            }
            catch { }
        }

        public void AddProvider(Provider provider)
        {
            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "INSERT INTO provider(pNum, pName, pStreet, pCity, pState, pZip) " +
                "VALUES(@num, @name, @street, @city, @state, @zip)";
            sqliteCmd.Parameters.AddWithValue("@num", provider.Number);
            sqliteCmd.Parameters.AddWithValue("@name", provider.Name);
            sqliteCmd.Parameters.AddWithValue("@street", provider.Address);
            sqliteCmd.Parameters.AddWithValue("@city", provider.City);
            sqliteCmd.Parameters.AddWithValue("@state", provider.State);
            sqliteCmd.Parameters.AddWithValue("@zip", provider.Zip);

            try
            {
                sqliteCmd.ExecuteNonQuery();
            }
            catch { }
        }

        public void AddService(Service service)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText =
                "INSERT INTO service(currDate, servDate, sProviderNum, sMemberNum, comment, sCode) " +
                "VALUES(@dateServ, @dateRec, @pNum, @mNum, @sCode, @com); ";
            sqliteCmd.Parameters.AddWithValue("@dateServ", service.DateOfService);
            sqliteCmd.Parameters.AddWithValue("@dateRec", service.DateReceived);
            sqliteCmd.Parameters.AddWithValue("@pNum", service.ProviderNumber);
            sqliteCmd.Parameters.AddWithValue("@mNum", service.MemberNumber);
            sqliteCmd.Parameters.AddWithValue("@sCode", service.ServiceCode);
            sqliteCmd.Parameters.AddWithValue("@com", service.Comments);

            try
            {
                sqliteCmd.ExecuteNonQuery();
            }
            catch { }
        }
            

        public void DeleteMember(string memberID)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "DELETE FROM member WHERE mNum= @num;";
            sqliteCmd.Parameters.AddWithValue("@num", memberID);
            sqliteCmd.ExecuteNonQuery();
        }

        public void DeleteProvider(string providerID)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "DELETE FROM provider WHERE pNum= @num;";
            sqliteCmd.Parameters.AddWithValue("@num", providerID);
            sqliteCmd.ExecuteNonQuery();
        }

        public void ModifyMember(Member oldMember, Member newMember)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            int isSuspended = newMember.Suspended ? 1 : 0;
            sqliteCmd.CommandText = "UPDATE member " +
                "SET mName = @name, mNum = @num, mStreet = @street, mCity = @city, mState = @state, mZip = @zip, isSuspended = @suspended " +
                "WHERE mNum = @oldNum;";
            sqliteCmd.Parameters.AddWithValue("@name", newMember.Name);
            sqliteCmd.Parameters.AddWithValue("@num", newMember.Number);
            sqliteCmd.Parameters.AddWithValue("@street", newMember.Address);
            sqliteCmd.Parameters.AddWithValue("@city", newMember.City);
            sqliteCmd.Parameters.AddWithValue("@state", newMember.State);
            sqliteCmd.Parameters.AddWithValue("@zip", newMember.Zip);
            sqliteCmd.Parameters.AddWithValue("@suspended", isSuspended);
            sqliteCmd.Parameters.AddWithValue("@oldNum", oldMember.Number);

            sqliteCmd.ExecuteNonQuery();
        }

        public void ModifyProvider(Provider oldProvider, Provider newProvider)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            
            sqliteCmd.CommandText = "UPDATE provider " +
                                    "SET pName = @name, pNum = @num, pStreet = @street, pCity = @city, pState = @state, pZip = @zip " +
                                    "WHERE pNum = @oldNum;";
            sqliteCmd.Parameters.AddWithValue("@name", newProvider.Name);
            sqliteCmd.Parameters.AddWithValue("@num", newProvider.Number);
            sqliteCmd.Parameters.AddWithValue("@street", newProvider.Address);
            sqliteCmd.Parameters.AddWithValue("@city", newProvider.City);
            sqliteCmd.Parameters.AddWithValue("@state", newProvider.State);
            sqliteCmd.Parameters.AddWithValue("@zip", newProvider.Zip);
            sqliteCmd.Parameters.AddWithValue("@oldNum", oldProvider.Number);

            sqliteCmd.ExecuteNonQuery();
        }

        public Provider ParseProvider(string pNum)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM provider WHERE pNum = @pNum;";
            sqliteCmd.Parameters.AddWithValue("@pNum", pNum);
            reader = sqliteCmd.ExecuteReader();
            reader.Read();
            return new Provider(reader.GetString(1),
                                reader.GetString(0),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5));
        }

        public Member ParseMember(string mNum)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM member WHERE mNum = @mNum;";
            sqliteCmd.Parameters.AddWithValue("@mNum", mNum);
            reader = sqliteCmd.ExecuteReader();
            reader.Read();
        
            return new Member(reader.GetString(1),
                reader.GetString(0),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5));
        }
        private Service ParseService(string sCode)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sCode = @sCode;";
            sqliteCmd.Parameters.AddWithValue("@sCode", sCode);
            reader = sqliteCmd.ExecuteReader();
            reader.Read();
            return new Service( reader.GetDateTime(0),
                                reader.GetDateTime(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                reader.GetString(6),
                                reader.GetString(7));
        }

        public List<Service> MemberServiceList(string mNum)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sMemberNum = @mNum;";
            sqliteCmd.Parameters.AddWithValue("@mNum", mNum);
            reader = sqliteCmd.ExecuteReader();
            List<Service> result = new List<Service>();
            while (reader.Read())
            {
                Service temp = new Service(
                        DateTime.Parse(reader.GetString(0)),
                        DateTime.Parse(reader.GetString(0)),
                        ParseProvider(reader.GetString(2)).Name,
                        reader.GetString(2),
                        ParseMember(reader.GetString(3)).Name,
                        reader.GetString(3),
                        reader.GetString(4));
                result.Add(temp);
            }
            return result;
        }

        public List<Service> ProviderServiceList(string pNum)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sProviderNum = @pNum;";
            sqliteCmd.Parameters.AddWithValue("@pNum", pNum);
            reader = sqliteCmd.ExecuteReader();
            List<Service> result = new List<Service>();
            while (reader.Read())
            {
                Service temp = new Service(
                        DateTime.Parse(reader.GetString(0)),
                        DateTime.Parse(reader.GetString(0)),
                        ParseProvider(reader.GetString(2)).Name,
                        reader.GetString(2),
                        ParseMember(reader.GetString(3)).Name,
                        reader.GetString(3),
                        reader.GetString(4));
                result.Add(temp);
            }
            return result;
        }

        public int TotalProviderFee(string pNum)
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM service WHERE sProviderNum = @pNum;";
            sqliteCmd.Parameters.AddWithValue("@pNum", pNum);
            reader = sqliteCmd.ExecuteReader();
            int result = 0;
            while (reader.Read())
            {
                //Add together the fees
                result += FetchService(reader.GetString(4)).Fee;
            }
            return result;
        }

        public void WriteEFT()
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM provider;";
            reader = sqliteCmd.ExecuteReader();
            while (reader.Read())
            {
                Provider temp = new Provider(
                    reader.GetString(1),
                    reader.GetString(0),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    reader.GetString(5));
                Program.bankrecord.Record(temp);
            }
        }

        public void GetProviderDirectory()
        {
            System.Diagnostics.Process.Start(PROVIDER_DIRECTORY);
        }

        public void GeneratePayableSummary()
        {
            SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
            SQLiteDataReader reader;
            sqliteCmd.CommandText = "SELECT * FROM provider;";
            reader = sqliteCmd.ExecuteReader();
            Console.WriteLine("\n---Payable Summary---\nProviders to be paid:\n");
            int fees = 0, count = 0;
            while (reader.Read())
            {
                Provider temp = ParseProvider(reader.GetString(0));
                Console.WriteLine("\tName: " + temp.Name + "\n\tTotal Fee: " + temp.TotalFee() +
                    "\n\tNumber of Consultations: " + temp.NumberOfConsultations() + "\n");
                fees += temp.TotalFee();
                count += temp.NumberOfConsultations();
            }
            Console.WriteLine("Overall Total Fees: " + fees + "\nOverall Number of Consultations: " + count);
        }

        public void SendMemReport()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the member number (or 'list' or 'all'): ");
                string cmd = Console.ReadLine();
                if (cmd.Equals("list"))
                {
                    //print list of names - numbers
                    SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
                    SQLiteDataReader reader;
                    sqliteCmd.CommandText = "SELECT * FROM member;";
                    reader = sqliteCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(1) + " - " + reader.GetString(0));
                    }
                }
                else if (cmd.Equals("all"))
                {
                    SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
                    SQLiteDataReader reader;
                    sqliteCmd.CommandText = "SELECT * FROM member;";
                    reader = sqliteCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Program.report.MemberReport(ParseMember(reader.GetString(0)));
                    }
                    return;
                }
                else
                {
                    try
                    {
                        Program.report.MemberReport(ParseMember(cmd));
                        return;
                    }
                    catch { Console.WriteLine("Not a valid member number!"); }
                }
            }
        }

        public void SendProvReport()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Enter the provider number (or 'list' or 'all'): ");
                string cmd = Console.ReadLine();
                if (cmd.Equals("list"))
                {
                    //print list of names - numbers
                    SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
                    SQLiteDataReader reader;
                    sqliteCmd.CommandText = "SELECT * FROM provider;";
                    reader = sqliteCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(1) + " - " + reader.GetString(0));
                    }
                }
                else if (cmd.Equals("all"))
                {
                    SQLiteCommand sqliteCmd = sqliteConn.CreateCommand();
                    SQLiteDataReader reader;
                    sqliteCmd.CommandText = "SELECT * FROM provider;";
                    reader = sqliteCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Program.report.ProviderReport(ParseProvider(reader.GetString(0)));
                    }
                    return;
                }
                else
                {
                    try
                    {
                        Program.report.ProviderReport(ParseProvider(cmd));
                        return;
                    }
                    catch { Console.WriteLine("Not a valid provider number!"); }
                }
            }
        }
        private static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Creates a new database connection.
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Opens the connection.
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                // Doesn't create new connection.
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
                                  "PRIMARY KEY (currDate, sMemberNum)" +
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

        private void ReadData(string table)
        {
            SQLiteDataReader sqliteDatareader;

            var sqliteCmd = sqliteConn.CreateCommand();
            sqliteCmd.CommandText = "SELECT * FROM @table";
            sqliteCmd.Parameters.AddWithValue("@table", table);

            sqliteDatareader = sqliteCmd.ExecuteReader();
            
            while (sqliteDatareader.Read())
            {
                var tempReader = sqliteDatareader.GetString(0);
                Console.WriteLine(tempReader);
            }
        }

        public void NukeTables()
        {
            SQLiteCommand sqliteCmd;
            sqliteCmd = sqliteConn.CreateCommand();
            // Delete member table
            sqliteCmd.CommandText = "DELETE FROM member;";
            sqliteCmd.ExecuteNonQuery();
            // Delete provider table
            sqliteCmd.CommandText = "DELETE FROM provider;";
            sqliteCmd.ExecuteNonQuery();
            // Delete service table
            sqliteCmd.CommandText = "DELETE FROM service;";
            sqliteCmd.ExecuteNonQuery();
        }

        public void Close()
        {
            sqliteConn.Close();
        }

        //For Testing
        public void Populate()
        {
            AddProvider(new Provider("Adam", "123456789", "111 Elm St", "Toledo", "Ohio", "43606"));
            AddProvider(new Provider("Trey", "111222333", "456 Park Rd", "Miami", "Florida", "12345"));
            AddProvider(new Provider("Josh", "987654321", "000 Zero Dr", "Madrid", "Spain", "21345"));
            AddMember(new Member("Katy", "112233445", "21552 Drive St", "LA", "California", "31244"));
            AddMember(new Member("Sherry", "998877665", "544 Street Dr", "Portland", "Oregon", "31224"));
            AddMember(new Member("Martha", "000000000", "899 Hello Bl", "Dallas", "Texas", "31244"));
            AddService(new Service(DateTime.Parse("01-09-2000"), DateTime.Now, "Adam", "123456789", "Katy", "112233445", "634378"));
            AddService(new Service(DateTime.Parse("04-20-2019"), DateTime.Now, "Adam", "123456789", "Sherry", "998877665", "980343"));
            AddService(new Service(DateTime.Parse("10-08-2019"), DateTime.Now, "Josh", "987654321", "Sherry", "998877665", "108375"));
            AddService(new Service(DateTime.Parse("12-20-2012"), DateTime.Now, "Josh", "987654321", "Martha", "000000000", "234546"));
            AddService(new Service(DateTime.Parse("09-20-2012"), DateTime.Now, "Trey", "111222333", "Katy", "112233445", "893209"));
        }
        
    }
}