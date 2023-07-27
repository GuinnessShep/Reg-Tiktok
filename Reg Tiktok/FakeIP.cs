using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Reg_Tiktok
{
    public class FakeIP
    {
        public static string RandomCountry()
        {
            try
            {
                Random random = new Random();
                string[] array = new string[]
				{
						"Albania",
						"Argentina",
						"Australia",
						"Austria",
						"Belgium",
						"Bosnia and Herzegovina",
						"Brazil",
						"Bulgaria",
						"Canada",
						"Chile",
						"Costa Rica",
						"Croatia",
						"Cyprus",
						"Czech Republic",
						"Denmark",
						"Estonia",
						"Finland",
						"France",
						"Georgia",
						"Germany",
						"Greece",
						"Hong Kong",
						"Hungary",
						"Iceland",
						"India",
						"Indonesia",
						"Ireland",
						"Israel",
						"Italy",
						"Japan",
						"Latvia",
						"Luxembourg",
						"Mexico",
						"Moldova",
						"Netherlands",
						"New Zealand",
						"North Macedonia",
						"Norway",
						"Poland",
						"Portugal",
						"Romania",
						"Serbia",
						"Singapore",
						"Slovakia",
						"Slovenia",
						"South Africa",
						"South Korea",
						"Spain",
						"Sweden",
						"Switzedland",
						"Taiwan",
						"Thailand",
						"Turkey",
						"Ukraine",
						"United Kingdom",
						"United States",
						"Vietnam"
				};
                return array[random.Next(0, array.Length - 1)];
            }
            catch (Exception ex)
            {
                throw new ArithmeticException(ex.Message);
            }
        }
        public static class Hma
        {
            public static void Reset()
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C netsh interface show interface";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.Verb = "runas";
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                if (text.Contains("HMA! Pro VPN OpenVPN"))
                {
                    Process process1 = new Process();
                    process1.StartInfo.FileName = "cmd.exe";
                    process1.StartInfo.Arguments = "/C netsh interface set interface \"HMA! Pro VPN OpenVPN\" disable";
                    process1.StartInfo.RedirectStandardOutput = true;
                    process1.StartInfo.UseShellExecute = false;
                    process1.StartInfo.CreateNoWindow = true;
                    process1.StartInfo.Verb = "runas";
                    process1.Start();
                    process1.WaitForExit();
                }
                else
                {
                    Process process1 = new Process();
                    process1.StartInfo.FileName = "cmd.exe";
                    process1.StartInfo.Arguments = "/C netsh interface set interface \"HMA! Pro VPN\" disable";
                    process1.StartInfo.RedirectStandardOutput = true;
                    process1.StartInfo.UseShellExecute = false;
                    process1.StartInfo.CreateNoWindow = true;
                    process1.StartInfo.Verb = "runas";
                    process1.Start();
                    process1.WaitForExit();
                }
            }
        }
        public static class Dcom
        {
            public static void Reset(string ProfileName)
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "rasdial.exe";
                    process.StartInfo.Arguments = "\"" + ProfileName + "\" /disconnect";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();
                    process.WaitForExit();
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            public static void Start(string ProfileName)
            {
                try
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "rasdial.exe";
                    process.StartInfo.Arguments = "\"" + ProfileName + "\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();
                    process.WaitForExit();
                    Thread.Sleep(1500);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public class NordVPN
        {
            public NordVPN(string FilePath)
            {
                filePath = FilePath;
            }
            public bool ConnectServerByName(string Name, int DelaySeconds)
            {
                bool result;
                try
                {
                    RunCMD("\"" + filePath + "\\NordVPN.exe\" -d");
                    Thread.Sleep(10000);
                    RunCMD("\"" + filePath + "\\NordVPN.exe\" -c -n" + Name);
                    Thread.Sleep(TimeSpan.FromSeconds(DelaySeconds));
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
            public bool ConnectServerByGroup(string Group, int DelaySeconds)
            {
                RunCMD("\"" + filePath + "\\NordVPN.exe\" -d");
                Thread.Sleep(10000);
                RunCMD("\"" + filePath + "\\NordVPN.exe\" -c -g" + Group);
                Thread.Sleep(TimeSpan.FromSeconds(DelaySeconds));
                return true;
            }
            public bool Disconnect()
            {
                bool result;
                try
                {
                    RunCMD("\"" + filePath + "\\NordVPN.exe\" -d");
                    Thread.Sleep(10000);
                    result = true;
                }
                catch (Exception)
                {
                    result = false;
                }
                return result;
            }
            private string filePath;
            private string RunCMD(string cmd)
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + cmd;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string text = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                bool flag = string.IsNullOrEmpty(text);
                bool flag2 = flag;
                string result;
                if (flag2)
                {
                    result = "";
                }
                else
                {
                    result = text;
                }
                return result;
            }
        }
        public static class Tinsoft
        {
            public static string GetProxy(string key)
            {
                string result;
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        result = Regex.Match(client.DownloadString("http://proxy.tinsoftsv.com/api/changeProxy.php?key=" + key), "\"proxy\":\"(.*?)\"").Groups[1].Value;
                    }
                }
                catch
                {
                    result = "";
                }
                return result;
            }
            public static List<string> GetListKeyFromApiUser(string key)
			{
				List<string> list = new List<string>();
				try
				{
					using (WebClient client = new WebClient())
					{
						client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
						string result = client.DownloadString("http://proxy.tinsoftsv.com/api/getUserKeys.php?key=" + key);
						JObject jobject = JObject.Parse(result);
						if (jobject["data"] != null)
						{
							using (IEnumerator<JToken> enumerator = ((IEnumerable<JToken>)jobject["data"]).GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									JToken jtoken = enumerator.Current;
									if (Convert.ToBoolean(jtoken["success"].ToString()))
									{
										list.Add(jtoken["key"].ToString());
									}
								}
								return list;
							}
						}
						else
						{
							return list;
						}
					}
				}
				catch
				{
					return list;
				}
			}
            public static string CheckApi(string key)
			{
				try
				{
					using (WebClient client = new WebClient())
					{
						client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
						string result = client.DownloadString("http://proxy.tinsoftsv.com/api/getKeyInfo.php?key=" + key);
						if (result != "")
						{
							JObject jobject = JObject.Parse(result);
							if (bool.Parse(jobject["success"].ToString()))
							{
								return "OK";
							}
						}
						return "NOT_OK";
					}
				}
				catch
				{
					return "ERROR";
				}
			}
        }
        public class TMProxy
        {
            private string api { get; set; }
            public TMProxy(string api)
            {
                this.api = api;
            }
            public string CheckAPI()
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://tmproxy.com/api/proxy/stats");
                    var data = Encoding.UTF8.GetBytes("{\"api_key\":\"" + this.api + "\"}");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (responseString != "")
                    {
                        try
                        {
                            JObject jobject = JObject.Parse(responseString);
                            string datetime = jobject["data"]["expired_at"].ToString();
                            DateTime t = DateTime.ParseExact(datetime, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture);
                            if (DateTime.Compare(t, DateTime.Now) > 0)
                            {
                                return "OK";
                            }
                        }
                        catch { }
                    }
                    return "NOT_OK";
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
            private string Md5Encode(string text)
            {
                MD5 md = MD5.Create();
                byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
            public string GetProxy()
            {
                try
                {
                    var request = (HttpWebRequest)WebRequest.Create("https://tmproxy.com/api/proxy/get-new-proxy");
                    string arg = "abccd9f3bf38f38414cb87e36f76c8e4";
                    string text = string.Format("{0}{1}{2}", arg, this.api, (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds() / 60 + 8989);
                    text = Md5Encode(text);
                    var data = Encoding.UTF8.GetBytes("{\"api_key\":\"" + this.api + "\",\"sign\":\"" + text + "\"}");
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    request.ContentLength = data.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (responseString != "")
                    {
                        try
                        {
                            JObject jobject = JObject.Parse(responseString);
                            string value = Regex.Match(JObject.Parse(responseString)["message"].ToString(), "\\d+").Value;
                            int next_change = ((value == "") ? 0 : int.Parse(value));
                            if (next_change == 0)
                            {
                                string proxy = jobject["data"]["https"].ToString();
                                return proxy;
                            }
                        }
                        catch { }
                    }
                    return "";
                }
                catch (Exception)
                {
                    return "ERROR";
                }
            }
        }
        public class XProxy
        {
            private string uri { get; set; }
            private string proxy { get; set; }
            public XProxy(string uri, string proxy)
            {
                this.uri = uri;
                this.proxy = proxy;
            }
            public bool CheckLiveProxy()
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        string json = client.DownloadString(uri.TrimEnd('/') + "/status?proxy=" + proxy);
                        try
                        {
                            return Convert.ToBoolean(JObject.Parse(json)["status"].ToString());
                        }
                        catch
                        {
                            return (JObject.Parse(json)["error_code"].ToString() == "0");
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
            public bool ChangeProxy()
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        string json = client.DownloadString(uri.TrimEnd('/') + "/reset?proxy=" + proxy);
                        JObject jobject = JObject.Parse(json);
                        if ((json.Contains("\"msg\"") && (JObject.Parse(json)["msg"].ToString() == "command_sent" || JObject.Parse(json)["msg"].ToString() == "OK" || JObject.Parse(json)["msg"].ToString().ToLower() == "ok")) || (json.Contains("\"error_code\"") && JObject.Parse(json)["error_code"].ToString().ToLower() == "0"))
                        {
                            for (int i = 0; i < 120; i++)
                            {
                                if (CheckLiveProxy())
                                {
                                    Thread.Sleep(1000);
                                    return true;
                                }
                                Thread.Sleep(1000);
                            }
                        }
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
