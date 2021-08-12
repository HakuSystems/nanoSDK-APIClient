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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
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
            Login loginwin = new Login();
            loginwin.InitializeComponent();
            loginwin.Show();
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterBtn.IsChecked = true;
        }

        private void DiscordBtn_Click(object sender, RoutedEventArgs e)
        {
            string discordUrl = "https://nanosdk.net/discord";
            Process.Start(discordUrl);
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (API.Register(userInput.Text, PassInput.Text, MailInput.Text, KeyInput.Text))
            {
                //Finish
                Login loginwin = new Login();
                loginwin.InitializeComponent();
                loginwin.Show();
                Close();
            }
        }
    }
}
