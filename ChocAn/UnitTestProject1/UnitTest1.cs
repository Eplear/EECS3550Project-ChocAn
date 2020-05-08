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
            dataCenter.NukeTables();

            Assert.IsTrue(isValidProvider);
        }

        [TestMethod]
        public void ValidateNonExistingProvider()
        {
            DataCenter dataCenter = new DataCenter();
            bool isValidProvider;

            isValidProvider = dataCenter.ValidateProvider("000000001");
            dataCenter.NukeTables();

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
            database.NukeTables();

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
            database.NukeTables();

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateNonExistingMember()
        {
            DataCenter database = new DataCenter();
            bool? isValidMember;
            
            isValidMember = database.ValidateMember("000000001");
            database.NukeTables();

            Assert.IsNull(isValidMember);
        }

        [TestMethod]
        public void ModifyMember()
        {
            DataCenter database = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", false);
            Member m2 = new Member("Adam Lebowski", "123456789", "Bancroft", "Toledo", "Ohio", "43606", false);

            database.AddMember(m1);
            database.ModifyMember(m1, m2);
            Member testingMem = database.ParseMember(m2.Number);
            database.NukeTables();

            Assert.AreEqual("Adam Lebowski", testingMem.Name);
        }

        [TestMethod]
        public void ModifyProvider()
        {
            DataCenter database = new DataCenter();
            Provider p1 = new Provider("Big Pharma", "123456789", "1 Main St.", "New York", "New York", "55432");
            Provider p2 = new Provider("Big Pharma Inc", "123456789", "123 Sesame St.", "Hell's Kitchen", "New York", "55667");

            database.AddProvider(p1);
            database.ModifyProvider(p1, p2);
            Provider testingProv = database.ParseProvider(p2.Number);
            database.NukeTables();

            Assert.AreEqual("Big Pharma Inc", testingProv.Name);
        }

        [TestMethod]
        public void WriteEFT()
        {
            DataCenter database = new DataCenter();
            database.Populate();
            database.WriteEFT();
        }

        [TestMethod]
        public void AddNewServices()
        {
            DataCenter dataCenter = new DataCenter();
            Service s1 = new Service(new DateTime(), new DateTime(), "George", "111222333", "Fayes", "222333444", "246894");
            dataCenter.AddService(s1);
            dataCenter.NukeTables();
        }

        [TestMethod]
        public void ReportMember()
        {
            DataCenter database = new DataCenter();
            database.Populate();
            Member m = new Member("Katy", "112233445", "21552 Drive St", "LA", "California", "31244");
            int reportReturn;
            Report report = new Report();
            reportReturn = report.MemberReport(m);
            database.NukeTables();

            Assert.AreEqual(1, reportReturn);
        }

        [TestMethod]
        public void ReportProvider()
        {
            DataCenter database = new DataCenter();
            database.Populate();
            int reportReturn;
            Provider p = new Provider("Adam", "123456789", "111 Elm St", "Toledo", "Ohio", "43606");
            Report report = new Report();
            reportReturn = report.ProviderReport(p);
            database.NukeTables();

            Assert.AreEqual(1, reportReturn);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException),
    "Member does not exist.")]
        public void DeleteTables()
        {
            DataCenter database = new DataCenter();

            database.Populate();
            database.NukeTables();
            database.ParseMember("123456789");
            database.NukeTables();
        }

    }
}
 