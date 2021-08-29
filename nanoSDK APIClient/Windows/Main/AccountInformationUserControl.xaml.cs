using MaterialDesignThemes.Wpf;
using System;
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

namespace nanoSDK_APIClient.Windows.Main
{
    /// <summary>
    /// Interaction logic for AccountInformationUserControl.xaml
    /// </summary>
    public partial class AccountInformationUserControl : UserControl
    {
        public AccountInformationUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Getdata();
            checkUserVariable();
            GetCurrentTheme();
        }

        private void GetCurrentTheme()
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            paletteHelper.SetTheme(theme);
        }

        private void checkUserVariable()
        {
            string currentStatus = User.UserVariable;
            if (currentStatus == null || User.Username == null || User.ID == null || User.Email == null || User.HWID == null)
            {
                API.Log(User.Username, "Invalid UserInformation");
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                API.Log(User.Username, "Valid User");
            }
        }

        private void Getdata()
        {
            ID.Content = $"User ID -> {User.ID}";
            Username.Content = $"Username -> {User.Username}";
            Email.Content = $"Email -> {User.Email}";
            HWID.Content = $"HWID -> {User.HWID}";
            Status.Content = $"Status -> {User.UserVariable}";
            Expiry.Content = $"Expiry -> {User.Expiry}";
            LastLogin.Content = $"Last Login -> {User.LastLogin}";
            RegisterDate.Content = $"Register Date -> {User.RegisterDate}";
        }

        private void ThemeBtn_Checked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);
            paletteHelper.SetTheme(theme);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainTransitioner)Window.GetWindow(this)).Close();
        }

        private void ThemeBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
