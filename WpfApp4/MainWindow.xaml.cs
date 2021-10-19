using Syroot.Windows.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        UserInfo olduser;
        UserInfo newuser;

        public MainWindow()
        {
            InitializeComponent();

            

            try
            {
                olduser = SaveToFile.LoadViaDataContractSerialization<UserInfo>("oldUser.xml");
                DPOldUser.DataContext = olduser;
                newuser = SaveToFile.LoadViaDataContractSerialization<UserInfo>("newUser.xml");
                DPNewUser.DataContext = newuser;
                // oldUserName.DataContext = prof;
            }
            catch (Exception)
            {

                Console.WriteLine("no file found");
            }

            

        }



        private void clickOldAccount(object sender, RoutedEventArgs e)
        {

            //System.Environment.GetEnvironmentVariable("USER")

            olduser = new UserInfo("old");
            olduser.ProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string nameoffile = Reader.NameOfFirefoxProfile();
            if (nameoffile == "")
            {
                MessageBox.Show("can not found ini file, please copy file of firefox manullay", "error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            olduser.PathOfFireFox = olduser.ProfilePath + @"\AppData\Roaming\Mozilla\Firefox\Profiles\" + nameoffile;

            string his = Environment.GetFolderPath(Environment.SpecialFolder.History);

            // for download folder, it may change location by user
            // https://stackoverflow.com/questions/10667012/getting-downloads-folder-in-c
            // PM> Install-Package Syroot.Windows.IO.KnownFolders

            olduser.DownloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;

            olduser.Favorites = new KnownFolder(KnownFolderType.Favorites).Path;
            //string dl = Environment.GetFolderPath(Environment.SpecialFolder.)


            //MessageBox.Show(olduser.PathOfFireFox);

            olduser.PathOfChrome = olduser.ProfilePath + @"\AppData\Local\Google\Chrome\User Data";

           // olduser.PathOfFireFox = olduser.ProfilePath + "\\AppData\\Roaming\\Mozilla\\Firefox\\Profiles";

            // oldUserName.DataContext = oldUser;
            DPOldUser.DataContext = olduser;
            SaveToFile.SaveViaDataContractSerialization(olduser, "oldUser.xml");



        }

        private void clickNewAccount(object sender, RoutedEventArgs e)
        {
            newuser = new UserInfo("new");
            newuser.ProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string nameoffile = Reader.NameOfFirefoxProfile();
            if (nameoffile == "")
            {
                MessageBox.Show("can not found ini file, please click Run Browser button below at first","error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           // Console.WriteLine(nameoffile);

            newuser.PathOfFireFox = newuser.ProfilePath + @"\AppData\Roaming\Mozilla\Firefox\Profiles\" + nameoffile;

            newuser.DownloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;

            newuser.Favorites = new KnownFolder(KnownFolderType.Favorites).Path;

            //MessageBox.Show(newuser.PathOfFireFox);

            newuser.PathOfChrome = newuser.ProfilePath + @"\AppData\Local\Google\Chrome\User Data";

            // oldUserName.DataContext = oldUser;
            DPNewUser.DataContext = newuser;
            SaveToFile.SaveViaDataContractSerialization(newuser, "newUser.xml");

        }

        private async void clickCopy(object sender, RoutedEventArgs e)
        {
            // Process.Start(olduser.PathOfFireFox);
            //
            //olduser.PathOfFireFox
            textBoxOfLogs.DataContext = olduser;

            try
            {
                
                    FAspinning.Spin = true;
                    FAspinning.Visibility = Visibility.Visible;
               
               

                string from = "from: " + olduser.PathOfFireFox + "\n";
                string to =  "to  : " + newuser.PathOfFireFox + "\n";

                string log = from;

                olduser.Logs = log;

                olduser.Logs = from + to;

                await copyDirectoryAsync(olduser, newuser, "");

               
                 
            }
            catch (Exception ex )
            {
                  
               foreach (DictionaryEntry de in ex.Data)
                {
                    string s = " Key: " + de.Key.ToString();
                    string k = s + de.Value.ToString();
                    MessageBox.Show(k, s,MessageBoxButton.OK, MessageBoxImage.Error);

                }
                               
                throw;
            }
            finally
            {
                FAspinning.Spin = false;
                FAspinning.Visibility = Visibility.Hidden;

            }
            MessageBox.Show("copy done");


        }

        private void beforeClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("before exit, saving...");
            SaveToFile.SaveViaDataContractSerialization(olduser, "oldUser.xml");
            SaveToFile.SaveViaDataContractSerialization(newuser, "newUser.xml");

        }

        private void clickRunBrowser(object sender, RoutedEventArgs e)
        {
            Process.Start("firefox.exe");
            string f = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";

            if (System.IO.File.Exists(f))
            {
                var url = "chrome://flags";
                //var url2 = " --load-extension=flags";
                Process.Start(f );
            }
            
        }

        private async Task copyDirectoryAsync(UserInfo old, UserInfo newuser, string dir)
        {
            var c = new Microsoft.VisualBasic.Devices.Computer();

            olduser.Logs = olduser.Logs + "copying fireFox's profile \n";
            olduser.Logs = olduser.Logs + "copying chrome's profile\n";
           

           // List<string> logs = new List<string>();
            List<Task> tasks = new List<Task>();


            tasks.Add(Task.Run(() => c.FileSystem.CopyDirectory(old.PathOfFireFox, newuser.PathOfFireFox, true)));
            tasks.Add(Task.Run(() => c.FileSystem.CopyDirectory(old.PathOfChrome, newuser.PathOfChrome, true)));

            if (_isDownload.IsChecked == true)
            {
                olduser.Logs = olduser.Logs + "copying Downloads \n";
                await Task.Run(() => {
                    new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(old.DownloadsPath, newuser.DownloadsPath, true);

                });

                olduser.Logs = olduser.Logs + "copy done of Downloads \n";
            }

            if (_isFavorites.IsChecked == true)
            {
                olduser.Logs = olduser.Logs + "copying Favorites\n";
                await Task.Run(() => {
                    new Microsoft.VisualBasic.Devices.Computer().FileSystem.CopyDirectory(old.Favorites, newuser.Favorites, true);

                });

                olduser.Logs = olduser.Logs + "copy done of Favorites \n";
            }

            // tasks.Add( Task.Run(() => { c.FileSystem.CopyDirectory(olduser.DownloadsPath, newuser.DownloadsPath, true); }));

            await Task.WhenAll(tasks);


        }
    }
}
