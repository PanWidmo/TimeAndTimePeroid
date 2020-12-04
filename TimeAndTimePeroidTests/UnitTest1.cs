using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeAndTimePeroidLib;

namespace TimeAndTimePeroidLib
{
    [TestClass]
    public class UnitTests
    {
        private const byte defaultValue = 0;
        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }


        [TestMethod, TestCategory("Time Constructor")]
        public void TimeConstructorDefault()
        {
            var time = new Time();

            Assert.AreEqual(defaultValue, time.Hours);
            Assert.AreEqual(defaultValue, time.Minutes);
            Assert.AreEqual(defaultValue, time.Seconds);
        }

        [TestMethod, TestCategory("Time Constructor")]
        [DataRow((byte)00, (byte)00, (byte)00, (byte)00, (byte)00, (byte)00)]
        [DataRow((byte)15, (byte)22, (byte)59, (byte)15, (byte)22, (byte)59)]
        [DataRow((byte)22, (byte)44, (byte)17, (byte)22, (byte)44, (byte)17)]
        [DataRow((byte)23, (byte)59, (byte)59, (byte)23, (byte)59, (byte)59)]
        public void TimeConstructorThreeParameters(byte h, byte m, byte s, byte expectedH, byte expectedM, byte exptectedS)
        {
            var time = new Time(h, m, s);

            AssertTime(time, expectedH, expectedM, exptectedS);
        }

        [TestMethod, TestCategory("Time Constructor")]
        [DataRow((byte)00, (byte)00, (byte)00, (byte)00)]
        [DataRow((byte)14, (byte)11, (byte)14, (byte)11)]
        [DataRow((byte)17, (byte)51, (byte)17, (byte)51)]
        [DataRow((byte)23, (byte)59, (byte)23, (byte)59)]
        public void TimeConstructorTwoParameters(byte h, byte m, byte expectedH, byte expectedM)
        {
            var time = new Time(h, m);

            AssertTime(time, expectedH, expectedM, 0);
        }

        [TestMethod, TestCategory("Time Constructor")]
        [DataRow((byte)00, (byte)00)]
        [DataRow((byte)21, (byte)21)]
        [DataRow((byte)15, (byte)15)]
        [DataRow((byte)23, (byte)23)]
        public void TimeConstructorOneParameter(byte h, byte expectedH)
        {
            var time = new Time(h);

            AssertTime(time, expectedH, 0, 0);
        }

        [TestMethod, TestCategory("Time Constructor")]
        [DataRow("00:00:00", (byte)00, (byte)00, (byte)00)]
        [DataRow("15:14:21", (byte)15, (byte)14, (byte)21)]
        [DataRow("22:14:51", (byte)22, (byte)14, (byte)51)]
        [DataRow("23:59:59", (byte)23, (byte)59, (byte)59)]
        public void TimeConstructorStringParameter(string t, byte h, byte m, byte s)
        {
            var stringTime = new Time(t);

            AssertTime(stringTime, h, m, s);
        }



        [TestMethod, TestCategory("Time String representation")]
        public void TimeToStringDefault()
        {
            var time1 = new Time(11, 59);
            string expectedTime1 = "11:59:00";

            var time2 = new Time(23, 59, 59);
            string expectedTime2 = "23:59:59";

            Assert.AreEqual(expectedTime1, time1.ToString());
            Assert.AreEqual(expectedTime2, time2.ToString());
        }


        [TestMethod, TestCategory("Time Equal")]
        [DataRow((byte)11, (byte)29, (byte)59, (byte)11, (byte)29, (byte)59)]
        public void CheckIfTimesAreEqual(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var firstTime = new Time(h1, m1, s1);
            var secondTime = new Time(h2, m2, s2);

            Assert.AreEqual(true, firstTime == secondTime);
        }




    }
}