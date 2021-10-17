using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4
{


    [DataContract]
    class UserInfo : INotifyPropertyChanged
    {
        [DataMember]
        public string name;

        [DataMember]
        private string _profilePath;
        public string ProfilePath
        {
            get { return _profilePath; }
            set
            {
                if (value != _profilePath)
                {
                    _profilePath = value;
                    Console.WriteLine("writing value: {0} to account: {1}", ProfilePath, name);
                    OnPropertyChanged("ProfilePath");
                  //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }

        [DataMember]
        string _pathOfFireFox;
        //private string _profilePath;
        public string PathOfFireFox
        {
            get { return _pathOfFireFox; }
            set
            {
                if (value != _pathOfFireFox)
                {
                    _pathOfFireFox = value;
                    Console.WriteLine("writing value: {0} to account: {1}", PathOfFireFox, name);
                    OnPropertyChanged("PathOfFireFox");
                    //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }

        
        [DataMember]
        string _pathOfChrome;
        //private string _profilePath;
        public string PathOfChrome
        {
            get { return _pathOfChrome; }
            set
            {
                if (value != _pathOfChrome)
                {
                    _pathOfChrome = value;
                    Console.WriteLine("writing value: {0} to account: {1}", PathOfChrome, name);
                    OnPropertyChanged("PathOfChrome");
                    //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }

        [DataMember]
        string _downloadsPath;
        //private string _profilePath;
        public string DownloadsPath
        {
            get { return _downloadsPath; }
            set
            {
                if (value != _downloadsPath)
                {
                    _downloadsPath = value;
                   // Console.WriteLine("writing value: {0} to account: {1}", DownloadsPath, name);
                    OnPropertyChanged("DownloadsPath");
                    //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }

        [DataMember]
        string _favorites;
        //private string _profilePath;
        public string Favorites
        {
            get { return _favorites; }
            set
            {
                if (value != _favorites)
                {
                    _favorites = value;
                    // Console.WriteLine("writing value: {0} to account: {1}", DownloadsPath, name);
                    OnPropertyChanged("Favorites");
                    //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }

        //[DataMember]
        string _logs;
        //private string _profilePath;
        public string Logs
        {
            get { return _logs; }
            set
            {
                if (value != _logs)
                {
                    _logs = value;
                    // Console.WriteLine("writing value: {0} to account: {1}", DownloadsPath, name);
                    OnPropertyChanged("Logs");
                    //  SaveToFile.SaveViaDataContractSerialization(oldUser, "oldUser.xml");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public UserInfo(string name)
        {
            this.name = name;

        }



    }
}
