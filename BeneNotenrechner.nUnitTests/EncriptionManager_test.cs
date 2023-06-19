using BeneNotenrechner.Backend;

namespace BeneNotenrechner.nUnitTests
{
    public class EncriptionManager_test
    {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void EncripDecripString_EqualTest() {
            User _user = new User(0, "DemoUser", 
                "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31", 
                "688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6");

            string _testString = "This is some Test Data";

            byte[] _encripted;

            if(!EncriptionManager.EncripString(_testString, _user, out _encripted)) {
                Assert.Fail();
            }

            string _resultString;

            if (!EncriptionManager.DecripString(_encripted, _user, out _resultString)) {
                Assert.Fail();
            }

            Assert.That(_resultString, Is.EqualTo(_testString));
        }

        [Test]
        public void EncripDecripFloat_EqualTest() {
            User _user = new User(0, "DemoUser",
                "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31",
                "688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6");

            float _testValue = 12637123.903485f;

            byte[] _encripted;

            if (!EncriptionManager.EncripFloat(_testValue, _user, out _encripted)) {
                Assert.Fail();
            }

            float _resultValue;

            if (!EncriptionManager.DecripFloat(_encripted, _user, out _resultValue)) {
                Assert.Fail();
            }

            Assert.That(_resultValue, Is.EqualTo(_testValue));
        }
    }
}