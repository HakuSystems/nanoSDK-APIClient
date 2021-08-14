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
            if (userInput.IsEnabled)
            {
                if (API.Login(userInput.Text, PassInput.Password))
                {
                    //open actual program
                }
            }
            else
            {
                if (API.AIORegister(PassInput.Password))
                {
                    if (API.AIOLogin(PassInput.Password))
                    {
                        if (new nanoSDK_APIClient.Theme.CustomMessageBox("Worked", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                            //open actual program
                        }
                    }
                    else
                    {
                        if (new nanoSDK_APIClient.Theme.CustomMessageBox("Wrong License", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                }
                else
                {
                    if (new nanoSDK_APIClient.Theme.CustomMessageBox("Wrong License", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
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
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            paletteHelper.SetTheme(theme);
            LoginBtn.IsChecked = true;
        }

        private void DiscordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (new nanoSDK_APIClient.Theme.CustomMessageBox("do you really want to Join our Discord?", Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.YesNo).ShowDialog().Value)
            {
                string discordUrl = "https://nanosdk.net/discord";
                Process.Start(discordUrl);
            }
        }

        private void InformationBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            InformationBtn.IsChecked = true;
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

        private void InformationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (new nanoSDK_APIClient.Theme.CustomMessageBox("thats Okay, just put your License then without Username!", Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.OkCancel).ShowDialog().Value)
            {

                authChangerBtn.Visibility = Visibility.Visible;
                UserTitle.Text = "Disabled";
                userInput.IsEnabled = false;
                passTitle.Text = "License";
                PassInput.ToolTip = "Enter License";
                RegisterBtn.IsEnabled = false;
            }
            else
            {
                RegisterBtn.IsEnabled = true;
                authChangerBtn.Visibility = Visibility.Collapsed;
                passTitle.Text = "Password";
                UserTitle.Text = "Username";
                userInput.IsEnabled = true;
                PassInput.ToolTip = "Enter Password";
            }
        }

        private void authChangerBtn_Click(object sender, RoutedEventArgs e)
        {
            RegisterBtn.IsEnabled = true;
            passTitle.Text = "Password";
            UserTitle.Text = "Username";
            userInput.IsEnabled = true;
            PassInput.ToolTip = "Enter Password";
            authChangerBtn.Visibility = Visibility.Collapsed;
        }
    }
}
