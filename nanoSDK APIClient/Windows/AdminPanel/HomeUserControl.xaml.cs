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

namespace nanoSDK_APIClient.Windows.AdminPanel
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        public HomeUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            doBtnCheckings();
            string authkey = Constants.ApplicationAuthKey;
            string licensecount = AuthAPI.Licensecount(authkey);
            string accID = "859404"; // i like hardcoding
            string appSecret = "ZrGWTpiQV8WGqC6zIPozy1zyC0LVyOlryUx"; // i like hardcoding
            string apiKey = "342312532692143235"; // i like hardcoding
            try
            {
                licenseCount.Content = $"Total Licenses: {licensecount}";
                InforTextBlog.Text = AuthAPI.AppInfoString(accID, apiKey, appSecret);
            }
            catch { }

        }

        private void doBtnCheckings()
        {
            ((AdminPanel)Window.GetWindow(this)).adminInfoBtn.IsChecked = true;
            ((AdminPanel)Window.GetWindow(this)).adminUserBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).createLicenseBtn.IsChecked = false;
            ((AdminPanel)Window.GetWindow(this)).OthersBtn.IsChecked = false;
        }
    }
}
