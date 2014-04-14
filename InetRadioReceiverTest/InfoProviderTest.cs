using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InetRadioReceiver.Source.DataSource;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InetRadioReceiverTest
{

    [TestClass]
    public class InfoProviderTest
    {

        [TestMethod]
        public void InitTest()
        {
            
        }


        [TestMethod]
        public void GetDataTest()
        {
            var provider = new InfoProvider("http://radiopotok.ru");
            var radios = provider.GetRadios();


        }


    }
}
