using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using NAudio.Gui;
using NAudio.Wave;

namespace InetRadioReceiver.Source.Sound
{
    public class StreamPlayer : IDisposable
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(StreamPlayer).ToString());
       
        private Thread playThread;
        private StreamBuffer mp3Buffer = null;
        private String streamUrl = "";
        private IWavePlayer waveOut = null;
        private BufferedWaveProvider waveProvider = null;
        private AcmMp3FrameDecompressor decompressor = null;
        public StreamPlayer()
        {
            
        }

        public void PlayRadioStream(String newstreamUrl)
        {
            decompressor = new AcmMp3FrameDecompressor(new WaveFormat());
            waveOut = new WaveOut();   
            waveProvider = new BufferedWaveProvider(new WaveFormat());
            waveOut.Init(waveProvider);
            ThreadPool.QueueUserWorkItem(new WaitCallback(PlayAsync));
            this.streamUrl = newstreamUrl;
        }


        public void StopPlay()
        {
            waveOut.Dispose();
            playThread.Abort();
        }


        private void PlayAsync(object arg)
        {
            playThread = Thread.CurrentThread; 
            waveOut.Play();
            try
            {
                while (true)
                {
                    var frame = Mp3Frame.LoadFromStream(mp3Buffer.GetStream());
                    var dataBuffer = new byte[8000];
                    int bytesCount = decompressor.DecompressFrame(frame,dataBuffer,0);
                    var decompressedData = new byte[bytesCount];
                    Array.Copy(dataBuffer,decompressedData,bytesCount);
                    waveProvider.AddSamples(decompressedData,0,decompressedData.Length);
                }
            }
            catch (ThreadAbortException ex)
            {
                waveOut.Stop();
                Dispose();
                return;
            }
            catch (Exception)
            {
                 
            }
        }


        public void Dispose()
        {
            if(waveOut!=null)
                 waveOut.Dispose();
            if(playThread!=null)
                if(playThread.IsAlive)
                    playThread.Abort();
        }

         ~StreamPlayer()
        {
            this.Dispose();
        }

    }
}
