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
         string pathOfFireFox;

        [DataMember]
        string pathOfChrome;

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
