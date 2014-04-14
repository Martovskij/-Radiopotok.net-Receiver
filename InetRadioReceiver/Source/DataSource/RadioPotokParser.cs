using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using InetRadioReceiver.Source.AppCore;
using log4net;
using log4net.Repository.Hierarchy;

namespace InetRadioReceiver.Source.DataSource
{
    public class RadioPotokParser  
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(RadioPotokParser).ToString());
       
        private const int RADIO_SIZE = 8;
        public List<GenreContainer> GetRadioPages(String radioPages)
        {
            var genreContainerList = new List<GenreContainer>();
            var str = radioPages.Split(new[] {"\n", "'\r\n"}, StringSplitOptions.None);
            var filteredList = new List<String>(); 
            int AddCounter = 0;
            foreach (var item in str)
            { 
                if (AddCounter > 0)
                {
                    filteredList.Add(item);
                    AddCounter--;
                }

                if (item.Contains("nav sub-nav nav-stacked small"))
                {
                    AddCounter = RADIO_SIZE;
                }  
            }

            foreach (var item in filteredList)
            {
                var splittedItem = item.Split('"');
                var genreContainer = new GenreContainer();
                genreContainer.PageLink = splittedItem[1];
                genreContainer.Name = splittedItem[splittedItem.Length - 1]
                                      .Replace("</a></li>","")
                                      .Replace("<","")
                                      .Replace(">","");
                genreContainerList.Add(genreContainer);
            }
             
            return genreContainerList; 
        }


 


        public List<Radio> GetRadiosByGenre(string radioPage)
        {
            try
            {
                var radios = new List<Radio>();
                //Console.WriteLine(radioPage); 
                //1. Split all page to many elements by newLine
                var splitted = radioPage.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None).ToList();
                //2. Get radios HTML Container
                var result = splitted.Single(x => x.Contains("col-sm-3 col-lg-3 col-xs-3"));
                //3. Replace spec symbol for light parse
                var splittedContainer = result.Replace("<", "|")
                                        .Replace(">", "|")
                                        .Replace("span", "")
                                        .Split('|')
                                        .ToList();
                //4. Search string, contained data stream (data stream, data title and other)
                // One string = one radio with metadata
                var filteredResult = splittedContainer.FindAll(x => x.Contains("data-stream"));
                //5. Split everyone radio string and extract all information
                foreach (var item in filteredResult)
                {
                    var splittedConcreteRadio = item.Split(new[] { '"' }, StringSplitOptions.None);
                    var radioForAdd = new Radio();
                    for (int i = 0; i < splittedConcreteRadio.Length; i++)
                    {

                        //5.1 getting stream list
                        if (splittedConcreteRadio.ElementAt(i).Contains("data-stream1"))
                        {
                            radioForAdd.DataStream.Add("data-stream1",
                                                      splittedConcreteRadio.ElementAt(++i)
                                                      .Split(';')[0]);
                            Console.WriteLine(radioForAdd.DataStream["data-stream1"]);
                            continue;
                        }
                        if (splittedConcreteRadio.ElementAt(i).Contains("data-stream2"))
                        {
                            radioForAdd.DataStream.Add("data-stream2",
                                                      splittedConcreteRadio.ElementAt(++i)
                                                      .Split(';')[0]);
                            Console.WriteLine(radioForAdd.DataStream["data-stream2"]);
                            continue;
                        }
                        if (splittedConcreteRadio.ElementAt(i).Contains("data-stream3"))
                        {
                            radioForAdd.DataStream.Add("data-stream3",
                                                      splittedConcreteRadio.ElementAt(++i)
                                                      .Split(';')[0]); 
                            Console.WriteLine(radioForAdd.DataStream["data-stream3"]);
                            continue; 
                        }
                        if (splittedConcreteRadio.ElementAt(i).Contains("data-title"))
                        {
                            radioForAdd.RadioName = splittedConcreteRadio.ElementAt(++i);
                            // 5.2 Get radio image
                            var FindedImgString = splitted.Find((x) =>
                            {
                               return (x.Contains("img class") &
                                      x.Contains(radioForAdd.RadioName));
                            });
                            if (FindedImgString != null)
                            {
                                    radioForAdd.ImageUrl = FindedImgString.Split('"')
                                    .ToList().Find(x => x.Contains("http"));
                            }
                            else
                            {
                               // implement another pic
                            }
                            continue;
                        }
                       




                    }
                    radios.Add(radioForAdd);
                }
                return radios;
            }
            catch (Exception ex)
            {
                
               Console.WriteLine(ex);
            }
          


            return null;
        }

        public string GetRadioImage(String radioLink)
        {
            return "";
        }

        public string RadioName(String radioLink)
        {
            return "";
        }
    }

}
