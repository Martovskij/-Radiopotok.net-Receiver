using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace InetRadioReceiver.Source.Sound
{
    public class StreamBuffer
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(StreamBuffer).ToString());
       
        private readonly object mStreamMutex = new object();
        private readonly MemoryStream mStream = new MemoryStream();

        private MemoryStream BufferedMp3Stream
        {
            get
            {
                lock (mStreamMutex)
                {
                    return mStream;
                }
            } 
        }

        public void ProcessData()
        {
            
        }

        public void WriteToBuffer(byte[] data)
        {
            BufferedMp3Stream.Write(data, 0, data.Length);
            BufferedMp3Stream.Flush();
        }


        public Stream GetStream()
        {
            while (true)
            {
                if (BufferedMp3Stream.Capacity > 1000)
                {
                    break;
                }
                Thread.Sleep(10);
            } 
            return BufferedMp3Stream; 
        }

    }
}
