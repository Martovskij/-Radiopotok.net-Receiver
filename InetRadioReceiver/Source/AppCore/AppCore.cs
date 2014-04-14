using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InetRadioReceiver.Source.DataSource;
using log4net;

namespace InetRadioReceiver.Source.AppCore
{
    public class AppCore : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AppCore).ToString());
       
        private HttpConnector httpConnector = null;

        public void Dispose()
        {
             
        }
    }
}
