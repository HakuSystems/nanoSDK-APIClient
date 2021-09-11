using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace nanoSDK_APIClient.Windows.AdminPanel
{
    /// <summary>
    /// Interaction logic for UsersUserControl.xaml
    /// </summary>
    public partial class UsersUserControl : UserControl
    {

        public UsersUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            doBtnCheckings();
            setEverythingToZero();
            Getallusersdataview();
        }
        private void doBtnCheckings()
        {
            ((AdminPanel)Window.GetWindow(this)).adminInfoBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).adminUserBtn.IsChecked = true;
            ((AdminPanel)Window.GetWindow(this)).createLicenseBtn.IsChecked = false;
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Getallusersdataview();
        }

        public class Root
        {
            public string status { get; set; }
            public string value { get; set; }
        }


        private async void Getallusersdataview()
        {
            try
            {

                UsersDataGrid.Items.Clear();
                var _client = new HttpClient();
                string userCount = await _client.GetStringAsync($"https://developers.auth.gg/USERS/?type=count&authorization={Constants.ApplicationAuthKey}");
                var objs = JsonConvert.DeserializeObject<Root>(userCount);
                _client.Dispose();
                userCount = objs.value.ToString();

                WebClient wc = new WebClient();
                string data = wc.DownloadString($"https://developers.auth.gg/USERS/?type=fetchall&authorization={Constants.ApplicationAuthKey}");
                var obj = JsonConvert.DeserializeObject<dynamic>(data);

                List<int> usersIds = new List<int>();

                for (int num = 0; num < (Convert.ToInt32(userCount) + 0); num++)
                {
                    usersIds.Add(num);
                }

                foreach (var id in usersIds)
                {
                    string userVar = obj[id.ToString()]["variable"];
                    if (userVar == "")
                    {
                        userVar = "N/A";
                    }
                    string[] gridData = new string[]
                    {
                        id.ToString(), //count (0)
                        obj[id.ToString()]["username"], // (1)
                        obj[id.ToString()]["email"], // (2)
                        obj[id.ToString()]["rank"], // (3)
                        userVar, //dev or noo (4)
                        obj[id.ToString()]["lastlogin"], // (5)
                        obj[id.ToString()]["lastip"], // (6)
                        obj[id.ToString()]["expiry_date"] // (7)
                    };
                    
                    UsersDataGrid.Items.Add(gridData[1]);

                }


            }
            catch { }

        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (UsersDataGrid.SelectedItem == null)
                {
                    setEverythingToZero();
                }
                else
                {
                    ActionCard.IsEnabled = true;
                    UsernameOutput.Text = UsersDataGrid.SelectedItem.ToString();
                }
            }
            catch (NullReferenceException)
            {
                setEverythingToZero();
            }

        }

        private void setEverythingToZero()
        {
            ActionCard.IsEnabled = false;
            UsernameOutput.Text = "null";
            PassBoxChangePassBox.Password = null;
            VariableChangerTxtBox.Text = null;
        }


        private void changeVarbtn_Click(object sender, RoutedEventArgs e)
        {
            AuthAPI.Changevariable(Constants.ApplicationAuthKey, UsernameOutput.Text, VariableChangerTxtBox.Text);
        }

        private void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthAPI.Changepassword(Constants.ApplicationAuthKey, UsernameOutput.Text, PassBoxChangePassBox.Password);
        }

        private void resetHWIDBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthAPI.ResetHwid(Constants.ApplicationAuthKey, UsernameOutput.Text);
        }

        private void deleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            AuthAPI.Deleteuser(Constants.ApplicationAuthKey, UsernameOutput.Text);
        }
    }
}