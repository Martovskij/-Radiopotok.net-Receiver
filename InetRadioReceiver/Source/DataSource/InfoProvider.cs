using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using NAudio.Mixer;

namespace InetRadioReceiver.Source.DataSource
{
    public class InfoProvider
    { 
        private static readonly ILog logger = LogManager.GetLogger(typeof(App).ToString());
        private HttpConnector connector = null;
        private RadioPotokParser parser = null;
        private String targetUrl = null;
         
        public InfoProvider(String targetSite)
        {
            targetUrl = targetSite;
            connector = new HttpConnector(targetSite);
            parser = new RadioPotokParser(); 
        }
         
        public List<Radio> GetRadios()
        {
            try
            {
                var Genres = parser.GetRadioPages(connector.GetMainPage()); 
                foreach (var genrePage in Genres)
                {
                    String radioPageRaw = connector.GetChildPage(targetUrl + "//" + genrePage.PageLink);
                    genrePage.Radios = parser.GetRadiosByGenre(radioPageRaw);
                } 
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
            return null;
        }








    }
}
