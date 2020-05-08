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

            Assert.AreEqual(1, reportReturn);
        }

        [TestMethod]
        public void DeleteTables()
        {

        }

    }
}
 