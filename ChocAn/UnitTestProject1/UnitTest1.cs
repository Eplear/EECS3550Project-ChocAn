using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChocAn;

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
            DataCenter dataCenter = new DataCenter();
            dataCenter.ValidateMember("123456789");

        }

        [TestMethod]
        public void ValidateNonExistingMember()
        {
            DataCenter dataCenter = new DataCenter();
            dataCenter.ValidateMember("000000001");

        }

        [TestMethod]
        public void AddNewMember()
        {
            DataCenter dataCenter = new DataCenter();
            dataCenter.ValidateMember("000000001");

        }
    }
}
 