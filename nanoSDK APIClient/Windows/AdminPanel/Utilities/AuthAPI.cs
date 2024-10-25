﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Security.Principal;
using nanoSDK_APIClient.Theme;

namespace nanoSDK_APIClient.Windows.AdminPanel
{
    class AuthAPI
    {
        //General
        public static bool Informationrequest { get; set; }
        public static dynamic AppInformation(string aid, string apikey, string secret)
        {
            Informationrequest = false;
            string url = String.Format("https://api.auth.gg/v1/?type=info&aid={0}&apikey={1}&secret={2}&username=demo&password=demo&hwid=demo", aid, apikey, secret);
            HttpRequest httpRequest = new HttpRequest();
            httpRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            RequestParams keyValues = new RequestParams
            {
                ["type"] = "info",
                ["aid"] = aid,
                ["apikey"] = apikey,
                ["secret"] = secret,
                ["username"] = "demo",
                ["password"] = "demo",
                ["hwid"] = "demo"
            };
            string request = httpRequest.Post(url, keyValues).ToString();
            dynamic jsonconvert = JsonConvert.DeserializeObject(request);
            Informationrequest = true;
            return jsonconvert;
        }

        public static dynamic AppInfoString(string aid, string apikey, string secret)
        {
            dynamic getobject = AppInformation(aid, apikey, secret);
            string result = String.Format("Status: {0}\n" +
                "Developer Mode: {1}\n" +
                "Hash: {2}\n" +
                "Version: {3}\n" +
                "Download Link: {4}\n" +
                "Free Mode: {5}\n" +
                "Login: {6}\n" +
                "Register: {7}\n" +
                "Total Users: {8}\n" +
                "Api Name: {9}",
                getobject["status"],
                getobject["developermode"],
                getobject["hash"],
                getobject["version"],
                getobject["downloadlink"],
                getobject["freemode"],
                getobject["login"],
                getobject["register"],
                getobject["users"],
                getobject["name"]);
            return result;
        }
        //Users
        public static dynamic Userinfo(string user, string authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/USERS/?type=fetch&authorization={authkey}&user={user}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert;
        }

        public static dynamic Userinfostring(string user, string authkey)
        {
            dynamic getobject = Userinfo(user, authkey);
            string result = string.Format("Email: {0}\n" +
                "Rank: {1}\n" +
                "Variable: {2}\n" +
                "Last Login: {3}\n" +
                "Last IP: {4}\n" +
                "Expiry: {5}",
                getobject["email"],
                getobject["rank"],
                getobject["variable"],
                getobject["lastlogin"],
                getobject["lastip"],
                getobject["expiry"]);
            return result;
        }

        public static void Changepassword(string authkey, string user, string password)
        {
            try
            {
                WebClient webClient = new WebClient();
                string jsonresponse = webClient.DownloadString($"hhttps://developers.auth.gg/USERS/?type=changepw&authorization={authkey}&user={user}&password={password}");
                dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
                if (jsonconvert["status"] == "success")
                {
                    if (new CustomMessageBox("Password has been updated.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else
                {
                    if (new CustomMessageBox("Password has not been changed.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
            }
            catch { }
        }

        public static void Changevariable(string authkey, string user, string variable)
        {
            try
            {
                WebClient webClient = new WebClient();
                string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/USERS/?type=editvar&authorization={authkey}&user={user}&value={variable}");
                dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
                if (jsonconvert["status"] == "success")
                {
                    if (new CustomMessageBox("User variable has been updated.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else
                {
                    if (new CustomMessageBox("The variable has not been changed.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
            }
            catch { }
        }

        public static dynamic Usercount(string Authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/USERS/?type=count&authorization={Authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert["value"];
        }

        public static void ResetHwid(string Authkey, string user)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/HWID/?type=reset&authorization={Authkey}&user={user}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            if (jsonconvert["status"] == "success")
            {
                if (new CustomMessageBox($"The HWID was reset for { user }", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                if (new CustomMessageBox($"Unable to reset HWID for { user }", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

        public static void Deleteuser(string Authkey, string user)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/USERS/?type=delete&authorization={Authkey}&user={user}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            if (jsonconvert["status"] == "success")
            {
                if (new CustomMessageBox($"The user has been deleted: { user }", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                if (new CustomMessageBox($"The user could not be deleted: { user }", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }
        //Licenses
        public static dynamic Licensecount(string Authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=count&authorization={Authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert["value"];
        }

        public static dynamic Generatelicense(string days, string ammount, string level, string length, string format, string prefix, string authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=generate&days={days}&amount={ammount}&level={level}&length={length}&format={format}&prefix={prefix}&authorization={authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert;
        }
    }
}
