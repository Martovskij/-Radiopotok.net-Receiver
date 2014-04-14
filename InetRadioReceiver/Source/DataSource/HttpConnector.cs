using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace InetRadioReceiver.Source.DataSource
{

    public class HttpConnector 
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(HttpConnector).ToString());
        private String requestHost = "";
        private HttpWebRequest request = null;
        private HttpWebResponse responce = null;
        
        public HttpConnector(String targetSite)
        {
            if(targetSite==null) throw new ArgumentNullException("targetSite is null");
            if(targetSite == "") throw new ArgumentException("targetSite is empty");  
            requestHost = targetSite;
        }


        public String GetMainPage()
        { 
            request = (HttpWebRequest) HttpWebRequest.Create(requestHost);
            responce = (HttpWebResponse)request.GetResponse(); 
            String readed = null;
            using (var stream = new StreamReader(responce.GetResponseStream(),Encoding.UTF8))
            {
                readed = stream.ReadToEnd();
            } 
            return readed;
        }

         
        public String GetChildPage(String targetUrl)
        {
            request = (HttpWebRequest)HttpWebRequest.Create(targetUrl);
            responce = (HttpWebResponse)request.GetResponse(); 
            String readed = null;
            using (var stream = new StreamReader(responce.GetResponseStream(), Encoding.UTF8))
            {
                readed = stream.ReadToEnd();
            }
            return readed;
        } 
    }

}
