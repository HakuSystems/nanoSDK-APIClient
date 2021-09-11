using nanoSDK_APIClient.Theme;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CreateLicneseUserControl.xaml
    /// </summary>
    public partial class CreateLicneseUserControl : UserControl
    {
        public CreateLicneseUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            doBtnCheckings();
        }


        private void doBtnCheckings()
        {
            ((AdminPanel)Window.GetWindow(this)).adminInfoBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).adminUserBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).createLicenseBtn.IsChecked = true;
            ((AdminPanel)Window.GetWindow(this)).OthersBtn.IsChecked = false;
        }

        private string days { get; set; }
        private static readonly Regex regex = new Regex(@"^\d+$");
        private void licCreateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (regex.IsMatch(PrefixTxtBox.Text))
                {
                    if (new CustomMessageBox("The prefix does not accept numbers", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                    return;
                }
                dataGridLicense.Items.Clear();
                string ammount = "1"; // how much licenses
                string length = "8"; // xxxx-xxx-xxxx
                string level = "1"; // useless but we have to.
                string prefix = PrefixTxtBox.Text; // discordName
                string dayscount = "9998"; // lifetime bc api is dumb and we cant create 99999 days
                string format = "3"; //{prefix}-length

                var obje = AuthAPI.Generatelicense(dayscount, ammount, level, length, format.ToString(), prefix, Constants.ApplicationAuthKey);
                string status = obje.ToString();
                var convertData = JsonConvert.DeserializeObject<dynamic>(status);
                DISPLAYTEXT.Content = status;
                List<int> licenses = new List<int>();

                for (int num = 0; num < (Convert.ToInt32(ammount) + 0); num++)
                    licenses.Add(num);

                foreach (var id in licenses)
                {
                    string[] gridData = new string[] { id.ToString(), convertData[id.ToString()] };
                    dataGridLicense.Items.Add(gridData[1]);
                }
            }
            catch { }
        }

        private void dataGridLicense_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dataGridLicense.SelectedItem == null)
                {
                    CopyPrefabCompletedBtn.IsEnabled = false;
                    SelcetedLicense.Text = "Currently No License Selected.";
                }
                else
                {
                    CopyPrefabCompletedBtn.IsEnabled = true;
                    SelcetedLicense.Text = dataGridLicense.SelectedItem.ToString();
                    Clipboard.SetText(SelcetedLicense.Text);

                }
            }
            catch (NullReferenceException)
            {

            }
        }

        private void CopyPrefabCompletedBtn_Click(object sender, RoutedEventArgs e)
        {
            string prefabContent = $@"```                == License ==
          nanoSDK License Completed

 Congratulation getting your nanoSDK license 
  License key:[{SelcetedLicense.Text}]
            License key duration:[Lifetime]
 
Note: The license key is only for 1 account

[nanoSDK development team]
            ```";

            Clipboard.SetText(prefabContent);
        }
    }
}
