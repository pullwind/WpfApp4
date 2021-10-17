using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{
    class Reader
    {
        public static string NameOfFirefoxProfile()
        {
            string apppath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string mozilla = System.IO.Path.Combine(apppath, "Mozilla");
            bool exist = System.IO.Directory.Exists(mozilla);

            if (exist)
            {
                string firefox = System.IO.Path.Combine(mozilla, "firefox");
                if (System.IO.Directory.Exists(firefox))
                {
                    string prof_file = System.IO.Path.Combine(firefox, "profiles.ini");
                    bool file_exist = System.IO.File.Exists(prof_file);

                    if (file_exist)
                    {
                        StreamReader reader = new StreamReader(prof_file);
                        string readed = reader.ReadToEnd();

                        string[] lines = readed.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        string location = lines.First(x => x.Contains("release")).Split(new string[] { "/" }, StringSplitOptions.None)[1];
                        //string prof_dir = System.IO.Path.Combine(firefox, location);

                        //return prof_dir;
                        return location;
                    }
                }
            }

            return "";
        }
    }
}
