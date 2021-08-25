using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using nanoSDK_APIClient.Windows.Auth;

namespace nanoSDK_APIClient.Windows.Main
{
    /// <summary>
    /// Interaction logic for updateChecking.xaml
    /// </summary>
    public partial class updateChecking : Window
    {
        #region strings
        public static string downloadedapplicationName = "nanoSDKClient.exe";
        public static string versionURL = "https://nanosdk.net/download/LoginClient/version.txt";
        public static string AppExecutableURL = "https://nanosdk.net/download/LoginClient/nanoSDK%20APIClient.exe";

        public static string AppExecutablePath = "\\nanoSDK\\Security\\";
        public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        #endregion
        public updateChecking()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetAsync(versionURL);
            var strServerVersion = await result.Content.ReadAsStringAsync();
            var serverVersion = Version.Parse(strServerVersion);
            var thisVersion = Application.ResourceAssembly.ManifestModule.Assembly.GetName().Version;
            try
            {
                if (serverVersion > thisVersion)
                {
                    WebClient client = new WebClient();
                    Directory.CreateDirectory(desktopPath + AppExecutablePath);
                    client.DownloadFileCompleted += client_DownloadFileCompleted;
                    client.DownloadFileAsync(new Uri(AppExecutableURL), desktopPath + AppExecutablePath + downloadedapplicationName);
                }
                else
                {
                    Login dash = new Login();
                    dash.InitializeComponent();
                    dash.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                if (new nanoSDK_APIClient.Theme.CustomMessageBox(ex.Message, Theme.CustomMessageBox.MessageType.Error, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                    await Task.Run(() =>
                    {
                        Process.GetCurrentProcess().Kill();
                    });
                }
            }
        }

        private async void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (new Theme.CustomMessageBox("Updating Complete", Theme.CustomMessageBox.MessageType.Info, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
            {
                Process.Start(desktopPath + AppExecutablePath + downloadedapplicationName);
                await Task.Run(() =>
                {
                    Process.GetCurrentProcess().Kill();
                });
            }
        }
    }
}
