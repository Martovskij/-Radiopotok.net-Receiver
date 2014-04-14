using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using log4net;

namespace InetRadioReceiver.Source.Sound
{
    public class StreamDataProvider : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(StreamDataProvider).ToString());
       
        public event Action<byte[]> DataAviable = delegate {  };
        private Thread dataReceiverThread = null;
        private String targetHttpSource = null;

        public  StreamDataProvider(String httpSource)
        {
            targetHttpSource = httpSource;
        }

        public void StartReceive()
        {
            var request = HttpWebRequest.Create(targetHttpSource);
            var dataStream = request.GetResponse().GetResponseStream();
            ThreadPool.QueueUserWorkItem(ReceiveRunnable); 
        }
         
        private void ReceiveRunnable(object s)
        {
            
            dataReceiverThread = Thread.CurrentThread;
            Stream dataStream = null;
            try
            {
                var request = HttpWebRequest.Create(targetHttpSource);
                dataStream = request.GetResponse().GetResponseStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                int i = 0; 
            }
           
            while (true)
            {
                try
                { 
                    var dataBuffer = new byte[8000];
                    var bytesRead = dataStream.Read(dataBuffer, 0, 0);
                    var readedMp3Data = new byte[bytesRead];
                    Array.Copy(dataBuffer, readedMp3Data, bytesRead);
                    DataAviable(readedMp3Data);
                }
                catch (ThreadAbortException e)
                {
                    Dispose();
                    return;
                }
                catch (Exception ex)
                {
                     
                } 
            }
        }

        public void Dispose()
        {
           //  if(dataReceiverThread!=null)
             //    if(!dataReceiverThread.IsAlive)
                  // dataReceiverThread.Abort();
        }
    }
}
