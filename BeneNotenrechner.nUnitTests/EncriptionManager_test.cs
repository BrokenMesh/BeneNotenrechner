using BeneNotenrechner.Backend;

namespace BeneNotenrechner.nUnitTests
{
    public class EncriptionManager_test
    {
        [SetUp]
        public void Setup() {
        }

        [TestCase("This is a Test String")]
        [TestCase("ausdhiuuvuzcxvuzbdsbfzuebwu")]
        [TestCase("5.5")]
        [TestCase(";'^a@asc7776666sd<<hasuidoddddddhsaudhsiuhds")]
        public void EncripDecripString_EqualTest(string _value) {
            User _user = new User(0, "DemoUser", 
                "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31", 
                "688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6");

            string _testString = _value;

            string _encripted;

            if(!EncriptionManager.EncripString(_testString, _user, out _encripted)) {
                Assert.Fail();
            }

            string _resultString;

            if (!EncriptionManager.DecripString(_encripted, _user, out _resultString)) {
                Assert.Fail();
            }

            Assert.That(_resultString, Is.EqualTo(_testString));
        }

        [TestCase(0f)]
        [TestCase(23746872346f)]
        [TestCase(0.27364726347f)]
        [TestCase(23849283.23429384f)]
        public void EncripDecripFloat_EqualTest(float _value) {
            User _user = new User(0, "DemoUser",
                "7214f780d3d36bab4b03bdf3ed67b0df7b7b707a506566f765772a242ffefe31",
                "688787d8ff144c502c7f5cffaafe2cc588d86079f9de88304c26b0cb99ce91c6");

            float _testValue = _value;

            string _encripted;

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