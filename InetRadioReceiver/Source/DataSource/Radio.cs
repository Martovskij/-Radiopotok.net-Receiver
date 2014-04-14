using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InetRadioReceiver.Source.DataSource
{
    public class Radio
    {
        public Dictionary<String,String> DataStream { get; set; }
        public String ImageUrl { get; set; }
        public String RadioName { get; set; }

        public Radio()
        {
            DataStream = new Dictionary<String, String>();
        }

    }
}
