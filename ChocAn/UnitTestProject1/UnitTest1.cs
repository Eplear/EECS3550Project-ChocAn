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
            dataCenter.ValidateProvider("123456789");
        }

        [TestMethod]
        public void ValidateNonExistingProvider()
        {
            DataCenter dataCenter = new DataCenter();
            dataCenter.ValidateProvider("000000001");

        }

        [TestMethod]
        public void ValidateExistingMember()
        {
            DataCenter database = new DataCenter();
            Member m1 = new Member("Adam", "123456789", "Perth St", "Toledo", "Ohio", "43607");
            database.AddMember(m1);
            database.ValidateMember("123456789");

        }

        [TestMethod]
        public void ValidateNonExistingMember()
        {
            DataCenter database = new DataCenter();
            database.ValidateMember("000000001");

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
    }
}
 