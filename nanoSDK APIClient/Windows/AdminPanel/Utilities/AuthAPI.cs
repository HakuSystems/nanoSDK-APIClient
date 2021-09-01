using Newtonsoft.Json;
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
        //Request
        public static void Loginauth(string username, string password, string aid, string apik, string secret)
        {
            var HWID = WindowsIdentity.GetCurrent().User.Value.ToString();
            try
            {
                var request = new HttpRequest();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                RequestParams appval = new RequestParams
                {
                    ["type"] = "login",
                    ["aid"] = aid,
                    ["apikey"] = apik,
                    ["secret"] = secret,
                    ["username"] = username,
                    ["password"] = password,
                    ["hwid"] = HWID,
                };
                string resp = request.Post(String.Format("https://api.auth.gg/v1/?type=login&aid={0}&apikey={1}&secret={2}&username={3}&password={4}&hwid={5}", aid, apik, secret, username, password, HWID), appval).ToString();
                object desjson = JsonConvert.DeserializeObject(resp);
                Constants.userinfoobj = desjson.ToString();
                if (desjson.ToString().Contains("\"result\": \"failed\""))
                {
                    if (new CustomMessageBox("Invalid Credentiasls", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (desjson.ToString().Contains("\"result\": \"invalid_hwid\""))
                {
                    if (new CustomMessageBox("Need to reset HWID", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (desjson.ToString().Contains("\"result\": \"hwid_updated\""))
                {
                    try
                    {
                        var rqshwid = new HttpRequest();
                        rqshwid.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                        RequestParams rqshwidval = new RequestParams
                        {
                            ["type"] = "login",
                            ["aid"] = aid,
                            ["apikey"] = apik,
                            ["secret"] = secret,
                            ["username"] = username,
                            ["password"] = password,
                            ["hwid"] = HWID,
                        };
                        string resphwid = request.Post(String.Format("https://api.auth.gg/v1/?type=login&aid={0}&apikey={1}&secret={2}&username={3}&password={4}&hwid={5}", aid, apik, secret, username, password, HWID), appval).ToString();
                        object hwidresponse = JsonConvert.DeserializeObject(resphwid);
                        if (hwidresponse.ToString().Contains("\"result\": \"success\""))
                        {
                            Constants.userinfoobj = hwidresponse.ToString();
                        }
                        else
                        {
                            if (new CustomMessageBox("HWID Problems", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                            {
                            }
                        }
                    }
                    catch { }
                }
                else if (desjson.ToString().Contains("\"result\": \"time_expired\""))
                {
                    if (new CustomMessageBox("Licence Problems", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (desjson.ToString().Contains("\"result\": \"success\""))
                {
                    if (new CustomMessageBox("Logged in successfully", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                if (new CustomMessageBox(ex.Message, CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }

            }
        }

        private static string ASDJKLSAN { get; set; }
        public static string Loginstring(string username, string password, string aid, string apik, string secret)
        {
            Loginauth(username, password, aid, apik, secret);
            var desjson = JsonConvert.DeserializeObject(Constants.userinfoobj);
            if (!(desjson.ToString().Contains("\"result\": \"failed\"") || desjson.ToString().Contains("\"result\": \"invalid_details\"") || desjson.ToString().Contains("\"result\": \"invalid_hwid\"") || desjson.ToString().Contains("\"result\": \"hwid_updated\"") || desjson.ToString().Contains("\"result\": \"time_expired\"")))
            {
                ASDJKLSAN = String.Format("ID: {0}\n" +
                  "Username: {1}\n" +
                  "HWID: {2}\n" +
                  "Email: {3}\n" +
                  "User Variable: {4}\n" +
                  "Rank: {5}\n" +
                  "Ip: {6}\n" +
                  "Expiry: {7}", desjson["id"],
                  desjson["username"],
                  desjson["hwid"],
                  desjson["email"],
                  desjson["uservar"],
                  desjson["rank"],
                  desjson["ip"],
                  desjson["expiry"]);
            }
            else
            {
                ASDJKLSAN = "Error";
            }
            return ASDJKLSAN;
        }

        public static void Senlog(string action, string pcuser, string username, string aid, string secret, string apik)
        {
            try
            {
                var request = new HttpRequest();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                RequestParams appval = new RequestParams
                {
                    ["type"] = "log",
                    ["aid"] = aid,
                    ["apikey"] = apik,
                    ["secret"] = secret,
                    ["username"] = username,
                    ["pcuser"] = pcuser,
                    ["action"] = action,
                };
                string resp = request.Post(String.Format("https://api.auth.gg/v1/?type=log&aid={0}&apikey={1}&secret={2}&username={3}&pcuser={4}&action={5}", aid, apik, secret, username, pcuser, action), appval).ToString();
                object desjson = JsonConvert.DeserializeObject(resp);
                if (desjson.ToString().Contains("\"result\": \"success\""))
                {
                    if (new CustomMessageBox("Log sent successfully", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else
                {
                    if (new CustomMessageBox("Unable to send", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
            }
            catch (Exception er)
            {
                if (new CustomMessageBox(er.Message, CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

        public static void Registerauth(string username, string password, string email, string license, string aid, string apik, string secret)
        {
            try
            {
                var HWID = WindowsIdentity.GetCurrent().User.Value.ToString();
                var request = new HttpRequest();
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                RequestParams valuePairs = new RequestParams
                {
                    ["type"] = "register",
                    ["aid"] = aid,
                    ["apikey"] = apik,
                    ["secret"] = secret,
                    ["username"] = username,
                    ["password"] = password,
                    ["hwid"] = HWID,
                    ["license"] = license,
                    ["email"] = email,
                };
                string url = String.Format("https://api.auth.gg/v1/?type=register&aid={0}&apikey={1}&secret={2}&username={3}&password={4}&hwid={5}&license={6}&email={7}", aid, apik, secret, username, password, HWID, license, email);
                string response = request.Post(url, valuePairs).ToString();
                object respjson = JsonConvert.DeserializeObject(response);
                if (respjson.ToString().Contains("\"result\": \"success\""))
                {
                    if (new CustomMessageBox("Registered correctly", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (respjson.ToString().Contains("\"result\": \"invalid_license\""))
                {
                    if (new CustomMessageBox("Invalid License", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (respjson.ToString().Contains("\"result\": \"email_used\""))
                {
                    if (new CustomMessageBox("Email already used", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else if (respjson.ToString().Contains("\"result\": \"invalid_username\""))
                {
                    if (new CustomMessageBox("Invalid or taken username", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else
                {
                    if (new CustomMessageBox("Unknown error", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                if (new CustomMessageBox(ex.Message, CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

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

        public static void Changerank(string authkey, string user, string rank)
        {
            try
            {
                WebClient webClient = new WebClient();
                string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/USERS/?type=editrank&authorization={authkey}&user={user}&rank={rank}");
                dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
                if (jsonconvert["status"] == "success")
                {
                    if (new CustomMessageBox("User rank has been updated.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                    {
                    }
                }
                else
                {
                    if (new CustomMessageBox("The rank has not been changed.", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
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

        public static void Deletelicense(string Authkey, string license)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=delete&license={license}&authorization={Authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            if (jsonconvert["status"] == "success")
            {
                if (new CustomMessageBox($"The license has been deleted: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                if (new CustomMessageBox($"The license could not be deleted: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

        public static void Uselicense(string Authkey, string license)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=use&license={license}&authorization={Authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            if (jsonconvert["status"] == "success")
            {
                if (new CustomMessageBox($"The action was successfully completed: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                if (new CustomMessageBox($"The action was not completed: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

        public static void Unuselicense(string Authkey, string license)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=unuse&license={license}&authorization={Authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            if (jsonconvert["status"] == "success")
            {
                if (new CustomMessageBox($"The action was successfully completed: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
            else
            {
                if (new CustomMessageBox($"The action was not completed: {license}", CustomMessageBox.MessageType.API, CustomMessageBox.MessageButtons.Ok).ShowDialog().Value)
                {
                }
            }
        }

        public static dynamic Licenseinfo(string license, string authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=fetch&authorization={authkey}&license={license}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert;
        }

        public static dynamic Licenseinfostring(string license, string authkey)
        {
            dynamic getobject = Licenseinfo(license, authkey);
            string result = string.Format("License: {0}\n" +
                "Rank: {1}\n" +
                "Used: {2}\n" +
                "Used by: {3}\n" +
                "Created: {4}",
                getobject["license"],
                getobject["rank"],
                getobject["used"],
                getobject["used_by"],
                getobject["created"]);
            return result;
        }

        public static dynamic Generatelicense(string ammount, string length, string prefix, string level, string days, string format, string authkey)
        {
            WebClient webClient = new WebClient();
            string jsonresponse = webClient.DownloadString($"https://developers.auth.gg/LICENSES/?type=generate&amount={ammount}&length={length}&prefix={prefix}&level={level}&days={days}&format={format}&authorization={authkey}");
            dynamic jsonconvert = JsonConvert.DeserializeObject(jsonresponse);
            return jsonconvert;
        }
    }
}
