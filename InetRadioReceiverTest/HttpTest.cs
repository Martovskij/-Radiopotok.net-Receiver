using System;
using System.Collections.Generic;
using InetRadioReceiver.Source.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InetRadioReceiverTest
{
    [TestClass]
    public class HttpTest
    {
        [TestMethod]
        public void InitTest()
        {
            // correct arg
            try {
                var connector = new HttpConnector("yandex.ru");

            }
            catch (Exception){throw;}

            // incorrect arg
            try{
                var connector = new HttpConnector("trololo");
                throw new Exception();
            }
            catch (Exception) {}


            // incorrect arg
            try
            {
                var connector = new HttpConnector("yandex.ru");
                throw new Exception();
            }
            catch (Exception) { } 
        }

        [TestMethod]
        public void LoadTest()
        {
            bool AllOk = false;
            var connector = new HttpConnector("http://yandex.ru");
            if (connector.GetMainPage() != null)
                AllOk = true; 
            Assert.IsTrue(AllOk); 
        }




       



    }
}
