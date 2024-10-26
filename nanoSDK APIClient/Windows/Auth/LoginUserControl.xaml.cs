﻿using MaterialDesignThemes.Wpf;
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

namespace nanoSDK_APIClient.Windows.Auth
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ((AuthTransitoner)Window.GetWindow(this)).Close();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userInput.IsEnabled)
            {
                if (API.Login(userInput.Text, PassInput.Password))
                {
                    Main.MainTransitioner update = new Main.MainTransitioner();
                    update.InitializeComponent();
                    update.Show();
                    ((AuthTransitoner)Window.GetWindow(this)).Close();
                }
            }
            else
            {
                if (API.AIORegister(PassInput.Password))
                {
                    if (API.AIOLogin(PassInput.Password))
                    {
                        if (new nanoSDK_APIClient.Theme.CustomMessageBox("Registered and Logged In!", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                            Main.MainTransitioner update = new Main.MainTransitioner();
                            update.InitializeComponent();
                            update.Show();
                            ((AuthTransitoner)Window.GetWindow(this)).Close();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackToNormalData();
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
                ChangeNormalData();
            }
            else
            {
                BackToNormalData();
            }
        }

        private void ChangeNormalData()
        {
            AIOLoginBtn.Visibility = Visibility.Visible;
            loginBtnContent.Text = "Register";
            loginBtnIcon.Kind = PackIconKind.RegisterOutline;
            RegisterBtn.Visibility = Visibility.Collapsed;

            InformationBtn.Visibility = Visibility.Collapsed;

            authChangerBtn.Visibility = Visibility.Visible;
            UserTitle.Text = "Disabled";
            userInput.IsEnabled = false;
            passTitle.Text = "License";
            PassInput.ToolTip = "Enter License";
            RegisterBtn.IsEnabled = false;
        }

        private void BackToNormalData()
        {
            AIOLoginBtn.Visibility = Visibility.Collapsed;
            loginBtnContent.Text = "Login";
            loginBtnIcon.Kind = PackIconKind.LoginVariant;
            RegisterBtn.Visibility = Visibility.Visible;

            InformationBtn.Visibility = Visibility.Visible;

            RegisterBtn.IsEnabled = true;
            authChangerBtn.Visibility = Visibility.Collapsed;
            passTitle.Text = "Password";
            UserTitle.Text = "Username";
            userInput.IsEnabled = true;
            PassInput.ToolTip = "Enter Password";
        }

        private void authChangerBtn_Click(object sender, RoutedEventArgs e)
        {
            BackToNormalData();
        }

        private void AIOLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (API.AIOLogin(PassInput.Password))
            {
                if (new nanoSDK_APIClient.Theme.CustomMessageBox("Logged In!", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                    Main.MainTransitioner update = new Main.MainTransitioner(); // add transitioner window
                    update.InitializeComponent();
                    update.Show();
                    ((AuthTransitoner)Window.GetWindow(this)).Close();
                }
            }
            else
            {
                if (new nanoSDK_APIClient.Theme.CustomMessageBox("Wrong License / Or not Registered", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
    }
}
