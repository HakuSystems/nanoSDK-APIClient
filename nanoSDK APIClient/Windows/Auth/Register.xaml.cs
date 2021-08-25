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
            if (new nanoSDK_APIClient.Theme.CustomMessageBox("do you really want to Join our Discord?", Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.YesNo).ShowDialog().Value)
            {
                string discordUrl = "https://nanosdk.net/discord";
                Process.Start(discordUrl);
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (API.Register(userInput.Text, PassInput.Password, MailInput.Text, KeyInput.Password))
            {
                if (new nanoSDK_APIClient.Theme.CustomMessageBox("Register Valid, Loggin in now.", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                    if (API.Login(userInput.Text, PassInput.Password))
                    {
                        Main.DashBoardWindow update = new Main.DashBoardWindow();
                        update.InitializeComponent();
                        update.Show();
                        Close();
                    }
                }
            }
        }

        private void ThemeBtn_Checked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);
            paletteHelper.SetTheme(theme);
        }

        private void ThemeBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
           theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void AboutBtn_Click(object sender, RoutedEventArgs e)
        {
            if (new nanoSDK_APIClient.Theme.CustomMessageBox($"nanoSDK is a modded SDK that bypasses and adds new futures, with nanoSDK you can upload avatars with a lot of polygons, make global audio sources without any limitations. {Environment.NewLine} nanoSDK is being constently updated and worked on and also have new futures that normal VRChat dosen't have such as remove missing scripts form a object automatecly, hwid spoofer etc. {Environment.NewLine} If you like nanoSDK and have new ideas or you just need help with our tool or help with custom content in general make sure you join our discord server.", Theme.CustomMessageBox.MessageType.Info, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
            {

            }
        }
    }
}
