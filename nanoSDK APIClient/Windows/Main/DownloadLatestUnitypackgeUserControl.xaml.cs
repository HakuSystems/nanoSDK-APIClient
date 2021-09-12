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
using Newtonsoft.Json;
using System.IO;
using Microsoft.Win32;

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
            if (e.Error == null)
            {
                Getdata();
                downloadBtn.IsEnabled = true;
                CloseBtn.IsEnabled = true;
                if (new Theme.CustomMessageBox("Do you want to Open the Latest Version?", Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.YesNo).ShowDialog().Value)
                {
                    Process.Start(assetName);
                }
            }
            else
            {
                if (new Theme.CustomMessageBox(e.Error.Message, Theme.CustomMessageBox.MessageType.Confirmation, Theme.CustomMessageBox.MessageButtons.YesNo).ShowDialog().Value)
                {

                }
            }
            ((WebClient)sender).Dispose();

        }

        private void client_DownloadProgressChangedAsync(object sender, DownloadProgressChangedEventArgs e)
        {
            TitleText.Text = $"Downloading.. ({e.BytesReceived / 1046576}MB / {e.TotalBytesToReceive / 1046576}MB) ";
            CloseBtn.IsEnabled = false;
        }

        private Jsondata GetJsondata(string appname, string authkey)
        {
            var jsondata = new Jsondata
            {
                Appname = appname,
                Authkey = authkey,
            };
            return jsondata;
        }

        public class Jsondata
        {
            public string Appname { get; set; }
            public string Authkey { get; set; }
        }

        private void Writejsonapp(string Aplicationame, string authentikey)
        {
            WebClient client = new WebClient();
            if (string.IsNullOrWhiteSpace(Aplicationame) || string.IsNullOrWhiteSpace(authentikey))
            {
                if (new Theme.CustomMessageBox("Invalid Authorization Key", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                string path = Directory.GetCurrentDirectory();
                string apname = Aplicationame;
                string autkey = Base64Encode(authentikey);
                string finalpath = String.Format("{0}\\{1}.json", path, apname);
                var jsonf = GetJsondata(Base64Encode(apname), autkey);
                var jsonToWrite = JsonConvert.SerializeObject(jsonf, Formatting.Indented);

                if (File.Exists(finalpath))
                {
                    if (new Theme.CustomMessageBox("json data Found, Start admin panel by clicking Ok", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                        Readjson(Localizepath("nanoSDK Api")); //hardcoded
                    }
                }
                else
                {
                    if (client.DownloadString($"https://developers.auth.gg/USERS/?type=count&authorization={authentikey}").Contains("success"))
                    {
                        try
                        {
                            File.WriteAllText(finalpath, jsonToWrite);
                            if (new Theme.CustomMessageBox("json data Saved.", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                            {
                                Readjson(Localizepath("nanoSDK Api")); //hardcoded
                            }
                        }
                        catch (Exception ex)
                        {
                            if (new Theme.CustomMessageBox(ex.Message, Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                            {
                            }
                        }
                    }
                    else
                    {
                        if (new Theme.CustomMessageBox("Invalid Authorization Key", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                        }
                    }
                }
            }
        }



        private void AdminPanelBtn_Click(object sender, RoutedEventArgs e)
        {
            string appname = "nanoSDK Api"; //hardcoded
            string appauth = "OQWVBQYUPUNT"; //hardcoded
            Writejsonapp(appname, appauth);
        }
        private string appnameread { get; set; }

        private string Localizepath(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                    fileDialog.Filter = "json files (*.json)|*.json";
                    fileDialog.RestoreDirectory = true;
                    var fldlg = fileDialog.ShowDialog();
                    string fnalpath = fileDialog.FileName;
                    if (fldlg.ToString() == "OK")
                    {
                        return fnalpath;
                    }
                    else if (fldlg.ToString() == "Cancel")
                    {
                        return "Invalid";
                    }
                    else
                    {
                        return "Invalid";
                    }
                }
                else
                {
                    string path = Directory.GetCurrentDirectory();
                    string apname = name;
                    string finalpath = String.Format("{0}\\{1}.json", path, apname);
                    if (File.Exists(finalpath))
                    {
                        return finalpath;
                    }
                    else
                    {
                        if (new Theme.CustomMessageBox("File could not be found.", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                        }
                        return "Invalid";
                    }
                }
            }
            catch { return "Invalid"; }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private void Readjson(string path)
        {
            if (path != "Invalid")
            {
                try
                {
                    string comppath = path;
                    string json = File.ReadAllText(comppath);
                    dynamic jsonobj = JsonConvert.DeserializeObject<Jsondata>(json);
                    string applicationname = Base64Decode(jsonobj.Appname);
                    string applicationauth = Base64Decode(jsonobj.Authkey);
                    WebClient wb = new WebClient();
                    if (wb.DownloadString($"https://developers.auth.gg/USERS/?type=count&authorization={applicationauth}").Contains("success"))
                    {
                        AdminPanel.Constants.ApplicationName = applicationname;
                        AdminPanel.Constants.ApplicationAuthKey = applicationauth;
                        AdminPanel.AdminPanel adminPanel = new AdminPanel.AdminPanel();
                        adminPanel.Show();
                    }
                    else
                    {
                        if (new Theme.CustomMessageBox("Invalid Authorization Key", Theme.CustomMessageBox.MessageType.API, Theme.CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                        {
                        }
                    }
                }
                catch { }
            }
        }

    }
}