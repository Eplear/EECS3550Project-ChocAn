using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAn;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InitializeDataCenter()
        {
            DataCenter dataCenter = new DataCenter();
            
            

        }

        [TestMethod]
        public void ValidateExistingProvider()
        {
            DataCenter dataCenter = new DataCenter();
            bool isValidProvider;
            Provider p1 = new Provider("Big Pharma", "123456789", "1 Main St.", "New York", "New York", "55432");
            dataCenter.AddProvider(p1);
            isValidProvider = dataCenter.ValidateProvider("123456789");

            Assert.IsTrue(isValidProvider);
        }

        [TestMethod]
        public void ValidateNonExistingProvider()
        {
            DataCenter dataCenter = new DataCenter();
            bool isValidProvider;

            isValidProvider = dataCenter.ValidateProvider("000000001");

            Assert.IsFalse(isValidProvider);
        }

        [TestMethod]
        public void ValidateSuspendedMember()
        {
            DataCenter database = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", true);
            bool? isValidMember;

            database.AddMember(m1);
            isValidMember = database.ValidateMember("123456789");
            bool isValid = isValidMember == false;

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateGoodMember()
        {
            DataCenter database = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", false);
            bool? isValidMember;

            database.AddMember(m1);
            isValidMember = database.ValidateMember("123456789");
            bool isValid = isValidMember == true;

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateNonExistingMember()
        {
            DataCenter database = new DataCenter();
            bool? isValidMember;
            
            isValidMember = database.ValidateMember("000000001");

            Assert.IsNull(isValidMember);
        }

        [TestMethod]
        public void WrtieEFT()
        {
            Provider p1 = new Provider("name1", "123", "add", "city", "st", "4000");
            Provider p2 = new Provider("name2", "135", "dress", "city2", "st2", "5000");
            DataCenter database = new DataCenter();
            BankRecord bank = new BankRecord();
            bank.CleanupDirectories();
            database.AddProvider(p1);
            database.AddProvider(p2);
            database.WriteEFT();
        }

        [TestMethod]
        public void AddNewServices()
        {
            DataCenter dataCenter = new DataCenter();
            Service s1 = new Service(new DateTime(), new DateTime(), "George", "111222333", "Fayes", "222333444", "246894");
            dataCenter.AddService(s1);
        }

        [TestMethod]
        public void ReportMember()
        {
            DataCenter database = new DataCenter();
            Service s1 = new Service(DateTime.Now, DateTime.Now, "George", "111222333", "Fayes", "222333444", "246894");
            Member m = new Member("Faye", "222333444", "Address", "City", "State", "zip");
            Provider p = new Provider("George", "111222333", "x", "xz", "asd", "czx");
            int reportReturn;

            database.AddService(s1);
            database.AddMember(m);
            database.AddProvider(p);
            Report report = new Report();
            report.CleanupDirectories();
            reportReturn = report.MemberReport(m);

            Assert.Equals(1, reportReturn);
        }

        [TestMethod]
        public void ReportProvider()
        {
            DataCenter database = new DataCenter();
            Service s1 = new Service(DateTime.Now, DateTime.Now, "George", "111222333", "Fayes", "222333444", "246894");
            Member m = new Member("Faye", "222333444", "Address", "City", "State", "zip");
            Provider p = new Provider("George", "111222333", "x", "xz", "asd", "czx");
            int reportReturn;

            database.AddService(s1);
            database.AddMember(m);
            database.AddProvider(p);
            Report report = new Report();
            report.CleanupDirectories();
            reportReturn = report.ProviderReport(p);

            Assert.Equals(1, reportReturn);
        }
    }
}
 