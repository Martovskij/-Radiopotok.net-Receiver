using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InetRadioReceiver.Source.DataSource;

namespace InetRadioReceiver.Source.AppCore
{
    public class GenreContainer
    { 
        public List<Radio> Radios { get; set; }
        public String Name { get; set; }
        public String PageLink { get; set; }

        public GenreContainer()
        {
            Radios = new List<Radio>();
            Name = "unknown";
            PageLink = "";
        }

    }
}
