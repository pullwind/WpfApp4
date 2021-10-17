using System;
using System.Collections.Generic;
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
           // oldUserName.DataContext = oldUser;
            DPOldUser.DataContext = olduser;
            SaveToFile.SaveViaDataContractSerialization(olduser, "oldUser.xml");


        }

        private void clickNewAccount(object sender, RoutedEventArgs e)
        {
            newuser = new UserInfo("new");
            newuser.ProfilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            // oldUserName.DataContext = oldUser;
            DPNewUser.DataContext = newuser;
            SaveToFile.SaveViaDataContractSerialization(newuser, "newUser.xml");

        }

        private void clickCopy(object sender, RoutedEventArgs e)
        {

        }

        private void beforeClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Console.WriteLine("before exit, saving...");
            SaveToFile.SaveViaDataContractSerialization(olduser, "oldUser.xml");
            SaveToFile.SaveViaDataContractSerialization(newuser, "newUser.xml");

        }
    }
}
