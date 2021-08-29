using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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

namespace nanoSDK_APIClient.Windows.Main
{
    /// <summary>
    /// Interaction logic for DownloadLatestUnitypackgeUserControl.xaml
    /// </summary>
    public partial class DownloadLatestUnitypackgeUserControl : UserControl
    {
        public static string assetName = "LatestUnitypackage.unitypackage";

        public DownloadLatestUnitypackgeUserControl()
        {
            InitializeComponent();
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

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ((MainTransitioner)Window.GetWindow(this)).Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            API.Log(User.Username, "Login");
            Getdata();
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            paletteHelper.SetTheme(theme);
        }

        private void Getdata()
        {
            string currentStatus = User.UserVariable;
            if (currentStatus == "Developer" || currentStatus == "Moderator")
            {
                AdminPanelBtn.Visibility = Visibility.Visible;
            }
            if (currentStatus == null || User.Username == null || User.ID == null || User.Email == null || User.HWID == null)
            {
                API.Log(User.Username, "Invalid UserInformation");
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                API.Log(User.Username, "Valid User"); 
            }
            TitleText.Text = $"Welcome, {User.Username}";
            summaryText.Text = $"This tool is used to download/update nanoSDK.";
        }

        private void downloadBtn_Click(object sender, RoutedEventArgs e)
        {
            Process[] unityProgram = Process.GetProcessesByName("unity");
            if (unityProgram.Length > 0)
            {
                DownloadAndRunLatestAsync();
            }
            else
            {
                if (new Theme.CustomMessageBox("cant find unity please Open Unity First.", Theme.CustomMessageBox.MessageType.Error, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {

                }
            }
        }

        private void DownloadAndRunLatestAsync()
        {
            downloadBtn.IsEnabled = false;
            string website = "https://nanosdk.net/download/apinewest/";
            WebClient client = new WebClient();
            client.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
            client.DownloadFileCompleted += client_DownloadProgressCompletedAsync;
            client.DownloadProgressChanged += client_DownloadProgressChangedAsync;
            try
            {
                client.DownloadFileAsync(new Uri(website), assetName);
            }
            catch (Exception ex)
            {
                if (new Theme.CustomMessageBox(ex.Message, Theme.CustomMessageBox.MessageType.Error, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {

                }
            }
            
            API.Log(User.Username, "Started UnityDownload");
        }

        private void client_DownloadProgressCompletedAsync(object sender, AsyncCompletedEventArgs e)
        {
            Getdata();
            downloadBtn.IsEnabled = true;
            CloseBtn.IsEnabled = true;
            if (new Theme.CustomMessageBox("Do you want to Open the Latest Version?", Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.YesNo).ShowDialog().Value)
            {
                Process.Start(assetName);
            }

        }

        private void client_DownloadProgressChangedAsync(object sender, DownloadProgressChangedEventArgs e)
        {
            TitleText.Text = $"Downloading.. ({e.BytesReceived / 1046576}MB / {e.TotalBytesToReceive / 1046576}MB) ";
            CloseBtn.IsEnabled = false;

        }
    }
}