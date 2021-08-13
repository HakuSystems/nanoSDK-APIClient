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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;

namespace nanoSDK_APIClient.Windows.Auth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DragMoveStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            logLabel.Text = "Checking..";
            if (API.Login(userInput.Text, PassInput.Password))
            {
                //correct
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            logLabel.Text = "Opening RegisterWindow";
            Register registerwin = new Register();
            registerwin.InitializeComponent();
            registerwin.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginBtn.IsChecked = true;
            logLabel.Text = "Please Login";
        }

        private void DiscordBtn_Click(object sender, RoutedEventArgs e)
        {
            logLabel.Text = "Opening URL..";
            string discordUrl = "https://nanosdk.net/discord";
            Process.Start(discordUrl);
        }

        private void RegisterBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            RegisterBtn.IsChecked = true;
            logLabel.Text = "Register?";
        }

        private void DiscordBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            DiscordBtn.IsChecked = true;
            logLabel.Text = "join?";
        }

        private void AboutBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            AboutBtn.IsChecked = true;
            logLabel.Text = "who are we?";
        }

        private void InformationBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            InformationBtn.IsChecked = true;
            logLabel.Text = "This is a new Login method where u only need our License Key. means that u dont have to specify a username or password but this also means u wont get so much support when u have login problems bc we cant see wich wich username u  are logged in.?";
        }

        private void ThemeBtn_Checked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Dark);
            paletteHelper.SetTheme(theme);
        }

        private void ThemeBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(Theme.Light);
            paletteHelper.SetTheme(theme);
        }
    }
}
