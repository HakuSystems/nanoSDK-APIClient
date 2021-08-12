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
            if (API.Login(userInput.Text, PassInput.Text))
            {
                //correct
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            Register registerwin = new Register();
            registerwin.InitializeComponent();
            registerwin.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoginBtn.IsChecked = true;
        }

        private void DiscordBtn_Click(object sender, RoutedEventArgs e)
        {
            string discordUrl = "https://nanosdk.net/discord";
            Process.Start(discordUrl);
        }
    }
}
