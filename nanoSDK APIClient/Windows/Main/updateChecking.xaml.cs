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
                if (serverVersion != thisVersion)
                {
                    WebClient client = new WebClient();
                    client.Headers.Set(HttpRequestHeader.UserAgent, "Webkit Gecko wHTTPS (Keep Alive 55)");
                    client.DownloadFileCompleted += client_DownloadProgressCompletedAsync;
                    client.DownloadProgressChanged += client_DownloadProgressChanged;
                    try
                    {
                        client.DownloadFileAsync(new Uri(AppExecutableURL), System.IO.Path.GetTempPath() + "\\" + downloadedapplicationName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please Notify lyze on Discord. - " + ex.Message, "Error While Updating");
                    }
                }
                else
                {
                    AuthTransitoner login = new AuthTransitoner();
                    login.InitializeComponent();
                    login.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Notify lyze on Discord. - " + ex.Message, "Error While Updating");
                await Task.Run(() =>
                {
                    Process.GetCurrentProcess().Kill();
                });
            }
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
                UpdateText.Text = $"Updating Client Please Wait.. ({e.BytesReceived / 1046576}MB / {e.TotalBytesToReceive / 1046576}MB) ";
        }

        private void client_DownloadProgressCompletedAsync(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error == null)
                {
                    string temp = System.IO.Path.GetTempPath();
                    Process.Start(temp + "\\" + downloadedapplicationName);
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Notify lyze on Discord. - " + ex.Message, "Error While Updating");
                Process.GetCurrentProcess().Kill();
            }
            ((WebClient)sender).Dispose();
        }
    }
}
