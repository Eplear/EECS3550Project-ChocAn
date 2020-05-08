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

            //removed Nuke
            dataCenter.AddProvider(p1);
            isValidProvider = dataCenter.ValidateProvider("123456789");
            //removed Nuke
            dataCenter.Close();

            Assert.IsTrue(isValidProvider);
        }

        [TestMethod]
        public void ValidateNonExistingProvider()
        {
            DataCenter dataCenter = new DataCenter();
            bool isValidProvider;

            //removed Nuke
            isValidProvider = dataCenter.ValidateProvider("000000001");
            //removed Nuke
            dataCenter.Close();

            Assert.IsFalse(isValidProvider);
        }

        [TestMethod]
        public void ValidateSuspendedMember()
        {
            DataCenter dataCenter = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", true);
            bool? isValidMember;

            dataCenter.AddMember(m1);
            isValidMember = dataCenter.ValidateMember("123456789");
            bool isValid = isValidMember == false;
            //removed Nuke
            dataCenter.Close();

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void ValidateGoodMember()
        {
            DataCenter dataCenter = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", false);
            bool? isValidMember;

            dataCenter.AddMember(m1);
            isValidMember = dataCenter.ValidateMember("123456789");
            bool isValid = isValidMember == true;
            //removed Nuke
            dataCenter.Close();

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void ValidateNonExistingMember()
        {
            DataCenter dataCenter = new DataCenter();
            bool? isValidMember;

            //removed Nuke
            isValidMember = dataCenter.ValidateMember("000000001");
            dataCenter.Close();

            Assert.IsNull(isValidMember);
        }

        [TestMethod]
        public void ModifyMember()
        {
            DataCenter dataCenter = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607", false);
            Member m2 = new Member("Adam Lebowski", "123456789", "Bancroft", "Toledo", "Ohio", "43606", false);

            //removed Nuke
            dataCenter.AddMember(m1);
            dataCenter.ModifyMember(m1, m2);
            Member testingMem = dataCenter.ParseMember(m2.Number);
            //removed Nuke
            dataCenter.Close();

            Assert.AreEqual("Adam Lebowski", testingMem.Name);
        }

        [TestMethod]
        public void ModifyProvider()
        {
            DataCenter dataCenter = new DataCenter();
            Provider p1 = new Provider("Big Pharma", "123456789", "1 Main St.", "New York", "New York", "55432");
            Provider p2 = new Provider("Big Pharma Inc", "123456789", "123 Sesame St.", "Hell's Kitchen", "New York", "55667");

            //removed Nuke
            dataCenter.AddProvider(p1);
            dataCenter.ModifyProvider(p1, p2);
            Provider testingProv = dataCenter.ParseProvider(p2.Number);
            //removed Nuke
            dataCenter.Close();

            Assert.AreEqual("Big Pharma Inc", testingProv.Name);
        }

        [TestMethod]
        public void WriteEFT()
        {
            DataCenter dataCenter = new DataCenter();

            //removed Nuke
            dataCenter.Populate();
            dataCenter.WriteEFT();
            //removed Nuke
            dataCenter.Close();
        }

        [TestMethod]
        public void AddNewServices()
        {
            DataCenter dataCenter = new DataCenter();
            Service s1 = new Service(new DateTime(), new DateTime(), "George", "111222333", "Fayes", "222333444", "246894");

            //removed Nuke
            dataCenter.AddService(s1);
            //removed Nuke
            dataCenter.Close();
        }

        [TestMethod]
        public void ReportMember()
        {
            DataCenter dataCenter = new DataCenter();
            Member m = new Member("Katy", "112233445", "21552 Drive St", "LA", "California", "31244");
            int reportReturn;

            //removed Nuke
            dataCenter.Populate();
            Report report = new Report();
            reportReturn = report.MemberReport(m);
            //removed Nuke
            dataCenter.Close();

            Assert.AreEqual(1, reportReturn);
        }

        [TestMethod]
        public void ReportProvider()
        {
            DataCenter dataCenter = new DataCenter();
            
            int reportReturn;
            Provider p = new Provider("Adam", "123456789", "111 Elm St", "Toledo", "Ohio", "43606");

            //removed Nuke
            dataCenter.Populate();
            Report report = new Report();
            reportReturn = report.ProviderReport(p);
            //removed Nuke
            dataCenter.Close();

            Assert.AreEqual(1, reportReturn);
        }

        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException),
    "Member does not exist.")]
        public void DeleteTables()
        {
            DataCenter dataCenter = new DataCenter();

            
            dataCenter.Populate();
            dataCenter.NukeTables();
            dataCenter.ParseMember("123456789");
            //removed Nuke
            dataCenter.Close();
            
        }

    }
}
 