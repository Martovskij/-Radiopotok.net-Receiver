using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InetRadioReceiver.Source.Sound;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InetRadioReceiverTest
{
    [TestClass]
    public class StreamDataProviderTest
    {

        [TestMethod]
        public void StartReceiveTest()
        {
           // StreamDataProvider provider = new StreamDataProvider("http://174.36.206.197/");
            StreamDataProvider provider = new StreamDataProvider("http://radiopotok.ru/");
            provider.DataAviable += ProviderOnDataAviable;
            provider.StartReceive();


        }

        private void ProviderOnDataAviable(byte[] bytes)
        {
            int i = 0;
        }
    }
}
