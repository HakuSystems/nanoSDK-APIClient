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
    /// Interaction logic for DashBoardWindow.xaml
    /// </summary>
    public partial class DashBoardWindow : Window
    {

        public static string assetName = "LatestUnitypackage.unitypackage";

        public DashBoardWindow()
        {
            InitializeComponent();
        }

        private void DragMoveStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Getdata();
            
        }

        private void Getdata()
        {
            string currentStatus = User.UserVariable;
            if (currentStatus == null || User.Username == null || User.ID == null || User.Email == null || User.HWID == null)
            {
                dataInformation.Text = $"LoggedIn as: Null {Environment.NewLine} with ID: Null {Environment.NewLine} E-mail: Null {Environment.NewLine} Status: InValid";
            }
            else if (currentStatus == null)
            {
                dataInformation.Text = $"LoggedIn as: {User.Username} {Environment.NewLine} with ID: {User.ID} {Environment.NewLine} E-mail: {User.Email} {Environment.NewLine} Status: Valid";

            }
            else
            {
                dataInformation.Text = $"LoggedIn as: {User.Username} {Environment.NewLine} with ID: {User.ID} {Environment.NewLine} E-mail: {User.Email} {Environment.NewLine} Status: {User.UserVariable}";

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
            string website = "https://nanosdk.net/download/newest/";
            WebClient client = new WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChangedAsync;
            client.DownloadFileCompleted += client_DownloadProgressCompletedAsync;
            client.DownloadFileAsync(new Uri(website), assetName);
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
