using AE.Net.Mail;
using Common;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace Reg_Tiktok
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
            Panel.CheckForIllegalCrossThreadCalls = false;
            TabControl.CheckForIllegalCrossThreadCalls = false;
            Form.CheckForIllegalCrossThreadCalls = false;
            Button.CheckForIllegalCrossThreadCalls = false;
            NumericUpDown.CheckForIllegalCrossThreadCalls = false;
            CheckBox.CheckForIllegalCrossThreadCalls = false;
            RadioButton.CheckForIllegalCrossThreadCalls = false;
            TextBox.CheckForIllegalCrossThreadCalls = false;
            DataGridView.CheckForIllegalCrossThreadCalls = false;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            timer1.Start();
            LoadAll();
        }
        public fMemu fmu = new fMemu();
        int success = 0;
        int error = 0;
        public void LoadAll(bool isAddEvent = true)
        {
            LoadCheckBox(isAddEvent);
            LoadRadioButton(isAddEvent);
            LoadTextBox(isAddEvent);
            LoadComboBox(isAddEvent);
            LoadNumericUpDown(isAddEvent);
        }
        public void SaveNumericUpDown(NumericUpDown num)
        {
            string regex = Regex.Match(Properties.Settings.Default.NumericUpDown, num.Name + "=(.*?)&").Groups[1].Value.Replace("=", "");
            Properties.Settings.Default.NumericUpDown = Properties.Settings.Default.NumericUpDown.Replace(num.Name + "=" + regex + "&", "");
            Properties.Settings.Default.Save();
            Properties.Settings.Default.NumericUpDown += num.Name + "=" + num.Value + "&";
            Properties.Settings.Default.Save();
        }
        public void LoadNumericUpDown(bool isAddEvent = true)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<NumericUpDown>())
                {
                    string text = Regex.Match(Properties.Settings.Default.NumericUpDown, control.Name + "=(.*?)&").Groups[1].Value.Replace("=", "");
                    try
                    {
                        if (Properties.Settings.Default.NumericUpDown.Contains(control.Name + "=")) control.Value = int.Parse(text);
                    }
                    catch { }
                    if (isAddEvent) control.ValueChanged += Num_ValueChanged;
                }
                foreach (var control in tab.Controls.OfType<GroupBox>())
                {
                    foreach (var controlCon in control.Controls.OfType<NumericUpDown>())
                    {
                        string text = Regex.Match(Properties.Settings.Default.NumericUpDown, controlCon.Name + "=(.*?)&").Groups[1].Value.Replace("=", "");
                        try
                        {
                            if (Properties.Settings.Default.NumericUpDown.Contains(controlCon.Name + "=")) controlCon.Value = int.Parse(text);
                        }
                        catch { }
                        if (isAddEvent) controlCon.ValueChanged += Num_ValueChanged;
                    }
                }
            }
        }
        private void Num_ValueChanged(object sender, EventArgs e)
        {
            SaveNumericUpDown((NumericUpDown)sender);
        }
        public void SaveComboBox(ComboBox cbb)
        {
            string regex = Regex.Match(Properties.Settings.Default.ComboBox, cbb.Name + "(.*?)&").Groups[1].Value.Replace("=", "");
            Properties.Settings.Default.ComboBox = Properties.Settings.Default.ComboBox.Replace(cbb.Name + "=" + regex + "&", "");
            Properties.Settings.Default.Save();
            Properties.Settings.Default.ComboBox += cbb.Name + "=" + HttpUtility.UrlEncode(cbb.Text) + "&";
            Properties.Settings.Default.Save();
        }
        public void LoadComboBox(bool isAddEvent = true)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<ComboBox>())
                {
                    string text = HttpUtility.UrlDecode(Regex.Match(Properties.Settings.Default.ComboBox, control.Name + "=(.*?)&").Groups[1].Value.Replace("=", ""));
                    if (Properties.Settings.Default.ComboBox.Contains(control.Name)) control.Text = text;
                    if (string.IsNullOrEmpty(control.Text)) control.SelectedIndex = 0;
                    if (!isAddEvent) control.SelectedIndex = 0;
                    if (isAddEvent) control.SelectedIndexChanged += Cbb_TextChanged;
                }
                foreach (var control in tab.Controls.OfType<GroupBox>())
                {
                    foreach (var controlCon in control.Controls.OfType<ComboBox>())
                    {
                        string text = HttpUtility.UrlDecode(Regex.Match(Properties.Settings.Default.ComboBox, controlCon.Name + "=(.*?)&").Groups[1].Value.Replace("=", ""));
                        if (Properties.Settings.Default.ComboBox.Contains(controlCon.Name)) controlCon.Text = text;
                        if (string.IsNullOrEmpty(controlCon.Text)) controlCon.SelectedIndex = 0;
                        if (!isAddEvent) controlCon.SelectedIndex = 0;
                        if (isAddEvent) controlCon.TextChanged += Cbb_TextChanged;
                    }
                }
            }
        }
        private void Cbb_TextChanged(object sender, EventArgs e)
        {
            SaveComboBox((ComboBox)sender);
        }
        public void SaveTextBox(TextBox tb)
        {
            string regex = Regex.Match(Properties.Settings.Default.TextBox, tb.Name + "(.*?)&").Groups[1].Value.Replace("=", "");
            Properties.Settings.Default.TextBox = Properties.Settings.Default.TextBox.Replace(tb.Name + "=" + regex + "&", "");
            Properties.Settings.Default.Save();
            Properties.Settings.Default.TextBox += tb.Name + "=" + HttpUtility.UrlEncode(tb.Text) + "&";
            Properties.Settings.Default.Save();
        }
        public void LoadTextBox(bool isAddEvent = true)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<TextBox>())
                {
                    string text = HttpUtility.UrlDecode(Regex.Match(Properties.Settings.Default.TextBox, control.Name + "=(.*?)&").Groups[1].Value.Replace("=", ""));
                    if (isAddEvent && Properties.Settings.Default.TextBox.Contains(control.Name)) control.Text = text;
                    if (isAddEvent) control.TextChanged += Tb_TextChanged;
                }
                foreach (var control in tab.Controls.OfType<GroupBox>())
                {
                    foreach (var controlCon in control.Controls.OfType<TextBox>())
                    {
                        string text = HttpUtility.UrlDecode(Regex.Match(Properties.Settings.Default.TextBox, controlCon.Name + "=(.*?)&").Groups[1].Value.Replace("=", ""));
                        if (isAddEvent && Properties.Settings.Default.TextBox.Contains(controlCon.Name)) controlCon.Text = text;
                        if (isAddEvent) controlCon.TextChanged += Tb_TextChanged;
                    }
                }
            }
        }
        private void Tb_TextChanged(object sender, EventArgs e)
        {
            SaveTextBox((TextBox)sender);
        }
        public void SaveCheckBox(CheckBox cb)
        {
            if (!Properties.Settings.Default.CheckBox.Contains(cb.Name + "|") && cb.Checked) Properties.Settings.Default.CheckBox += cb.Name + "|";
            else if (!cb.Checked) Properties.Settings.Default.CheckBox = Properties.Settings.Default.CheckBox.Replace(cb.Name + "|", "");
            Properties.Settings.Default.Save();
        }
        public void LoadCheckBox(bool isAddEvent = true)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<CheckBox>())
                {
                    control.Checked = Properties.Settings.Default.CheckBox.Contains(control.Name + "|");
                    if (isAddEvent) control.CheckedChanged += Cb_CheckedChanged;
                }
                foreach (var control in tab.Controls.OfType<GroupBox>())
                {
                    foreach (var controlCon in control.Controls.OfType<CheckBox>())
                    {
                        controlCon.Checked = Properties.Settings.Default.CheckBox.Contains(controlCon.Name + "|");
                        if (isAddEvent) controlCon.CheckedChanged += Cb_CheckedChanged;
                    }
                }
            }
        }
        private void Cb_CheckedChanged(object sender, EventArgs e)
        {
            SaveCheckBox((CheckBox)sender);
        }
        public void SaveRadioButton(RadioButton rb)
        {
            if (!Properties.Settings.Default.RadioButton.Contains(rb.Name + "|") && rb.Checked) Properties.Settings.Default.RadioButton += rb.Name + "|";
            else if (!rb.Checked) Properties.Settings.Default.RadioButton = Properties.Settings.Default.RadioButton.Replace(rb.Name + "|", "");
            Properties.Settings.Default.Save();
        }
        public void LoadRadioButton(bool isAddEvent = true)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                foreach (var control in tab.Controls.OfType<RadioButton>())
                {
                    if (Properties.Settings.Default.RadioButton.Contains(control.Name + "|")) control.Checked = Properties.Settings.Default.RadioButton.Contains(control.Name + "|");
                    if (isAddEvent) control.CheckedChanged += Rb_CheckedChanged;
                }
                foreach (var control in tab.Controls.OfType<GroupBox>())
                {
                    foreach (var controlCon in control.Controls.OfType<RadioButton>())
                    {
                        if (Properties.Settings.Default.RadioButton.Contains(controlCon.Name + "|")) controlCon.Checked = Properties.Settings.Default.RadioButton.Contains(controlCon.Name + "|");
                        if (isAddEvent) controlCon.CheckedChanged += Rb_CheckedChanged;
                    }
                }
            }
        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            SaveRadioButton((RadioButton)sender);
        }
        public static bool IsNumeric(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.') return false;
            }
            return true;
        }
        static Random rand = new Random();
        string pathMu { get { return textBox2.Text + @"\memuc.exe"; } }
        public void ChangeProxy(string param, string NameOrId, string ipProxy, string portProxy)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell settings put global http_proxy {2}:{3}\"", param, NameOrId, ipProxy, portProxy));
        }
        public void RemoveProxy(string param, string NameOrId)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell settings put global http_proxy :0\"", param, NameOrId));
        }
        public void ExecuteMu(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = pathMu;
            p.StartInfo.Arguments = cmd;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.EnableRaisingEvents = true;
            p.Start();
            p.WaitForExit();
            p.Close();
        }
        public int CountMemu()
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = pathMu,
                    Arguments = "listvms",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            if (string.IsNullOrEmpty(result) || !result.Contains("\n")) return 0;
            result = result.Replace("\r", "");
            return result.Split('\n').Length - 1;
        }
        public string GetIdFromNameMemu(string name)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = pathMu,
                    Arguments = "listvms",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            if (string.IsNullOrEmpty(result) || !result.Contains("\n")) return "";
            result = result.Replace("\r", "");
            foreach (var item in result.Split('\n'))
            {
                if (item.ToLower().Contains("," + name.ToLower() + ",")) return item.Split(',')[0].Trim();
            }
            return "";
        }
        public string GetNameFromIdMemu(string id)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = pathMu,
                    Arguments = "listvms",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            if (string.IsNullOrEmpty(result) || !result.Contains("\n")) return "";
            result = result.Replace("\r", "");
            foreach (var item in result.Split('\n'))
            {
                if (item.ToLower().Contains(id.ToLower() + ",")) return item.Split(',')[1].Trim();
            }
            return "";
        }
        public static string GetRandomMacAddress()
        {
            var buffer = new byte[6];
            rand.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }
        public static string RandomImei()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 15).Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        public static string RandomSimserial()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 20).Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        public void ChangeInfoMemu(string param, string NameOrId)
        {
            Close(param, NameOrId);
            ExecuteMu(string.Format("setconfigex -{0} {1} macaddress auto", param, NameOrId));
            ExecuteMu(string.Format("setconfigex -{0} {1} country_code \"vn\"", param, NameOrId));
            ExecuteMu(string.Format("setconfigex -{0} {1} operator_countrycode \"84\"", param, NameOrId));
            ExecuteMu(string.Format("setgps -{0} {1} {2}.{3} {4}.{5}", param, NameOrId, rand.Next(1, 200), rand.Next(100000, 999999), rand.Next(1, 200), rand.Next(100000, 999999)));
            ExecuteMu(string.Format("setconfigex -{0} {1} linenum \"+8435{2}\"", param, NameOrId, rand.Next(1000000, 9999999)));
            ExecuteMu(string.Format("setconfigex -{0} {1} bssid \"{2}\"", param, NameOrId, GetRandomMacAddress()));
            ExecuteMu(string.Format("setconfigex -{0} {1} hmac \"{2}\"", param, NameOrId, GetRandomMacAddress()));
            ExecuteMu(string.Format("setconfigex -{0} {1} operator_iso \"vn\"", param, NameOrId));
            ExecuteMu(string.Format("setconfigex -{0} {1} imsi {2}", param, NameOrId, RandomImei()));
            ExecuteMu(string.Format("setconfigex -{0} {1} imei {2}", param, NameOrId, RandomImei()));
            ExecuteMu(string.Format("setconfigex -{0} {1} simserial {2}", param, NameOrId, RandomSimserial()));
            ExecuteMu(string.Format("setconfigex -{0} {1} ssid auto", param, NameOrId));
            ExecuteMu(string.Format("setconfigex -{0} {1} custom_resolution 360 600 160", param, NameOrId));
            string path = Application.StartupPath + "\\pictures\\pictures_" + RandomString(10);
            Directory.CreateDirectory(path);
            ExecuteMu(string.Format("setconfigex -{0} {1} picturepath \"{2}\"", param, NameOrId, path));
            Thread.Sleep(5000);
        }
        public void Open(string param, string NameOrId)
        {
            ExecuteMu(string.Format("start -{0} {1}", param, NameOrId));
        }
        public void Open_App(string param, string NameOrId, string Package_Name)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell monkey -p {2} -c android.intent.category.LAUNCHER 1\"", param, NameOrId, Package_Name));
        }
        public void Stop_App(string param, string NameOrId, string Package_Name)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell am force-stop {2}\"", param, NameOrId, Package_Name));
        }
        public void Clear_Data_App(string param, string NameOrId, string Package_Name)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell pm clear {2}\"", param, NameOrId, Package_Name));
        }
        public void InstallApp_File(string param, string NameOrId, string FileName)
        {
            ExecuteMu(string.Format("installapp -{0} {1} \"{2}\"", param, NameOrId, FileName));
        }
        public void Grant(string param, string NameOrId, string Package_Name)
        {
            string[] pres = new string[] { "READ_CONTACTS", "CAMERA", "WRITE_EXTERNAL_STORAGE", "RECORD_AUDIO" };
            foreach (var pre in pres) ExecuteMu(string.Format("-{0} {1} adb \"shell pm grant {2} android.permission.{3}\"", param, NameOrId, Package_Name, pre));
        }
        public void Close(string param, string NameOrId)
        {
            ExecuteMu(string.Format("stop -{0} {1}", param, NameOrId));
        }
        public string ExecuteMu_Result(string cmdCommand)
        {
            string result;
            try
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = pathMu,
                    Arguments = cmdCommand,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true
                };
                process.Start();
                process.WaitForExit();
                string text = process.StandardOutput.ReadToEnd();
                result = text;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        public void InputUniCode(string param, string NameOrId, string Text, string Type = "text")
        {
            //foreach (char c in Text)
            //{
            //    ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a ADB_INPUT_TEXT -es msg '{2}'\"", param, NameOrId, c.ToString()));
            //    Thread.Sleep(20);
            //}
            if (Type == "Text") ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a  ADB_INPUT_TEXT --es msg '{2}'\"", param, NameOrId, Text));
            else ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a  ADB_INPUT_B64 --es msg '{2}'\"", param, NameOrId, Base64.Base64Encode(Text)));
        }
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rand.Next(s.Length)]).ToArray()).ToLower();
        }
        public string DumpXML(string param, string NameOrId)
        {
            string index = RandomString(10);
            ExecuteMu(string.Format("-{0} {1} adb \"shell uiautomator dump /sdcard/window_dump_{2}.xml\"", param, NameOrId, index));
            ExecuteMu(string.Format("-{0} {1} adb \"pull /sdcard/window_dump_{2}.xml {3}\"", param, NameOrId, index, "\\\"" + Application.StartupPath + "\\window_dump_" + index + ".xml\\\""));
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm /sdcard/window_dump_{2}.xml\"", param, NameOrId, index));
            string text = File.ReadAllText(Application.StartupPath + "\\window_dump_" + index + ".xml");
            File.Delete(Application.StartupPath + "\\window_dump_" + index + ".xml");
            return text;
        }
        public void KeyEventBack(string param, string NameOrId)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent 4\"", param, NameOrId));
        }
        public void KeyEventHome(string param, string NameOrId)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent 3\"", param, NameOrId));
        }
        public void Tap(string param, string NameOrId, int x, int y, int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                ExecuteMu(string.Format("-{0} {1} adb \"shell input tap {2} {3}\"", param, NameOrId, x, y));
            }
        }
        public XmlNode GetElement(string param, string NameOrId, string xpath, int seconds = 5)
        {
            for (var i = 0; i < seconds; i++)
            {
                try
                {
                    string xml = DumpXML(param, NameOrId);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(xpath);
                    if (node != null) return node;
                }
                catch { }
                Thread.Sleep(500);
            }
            return null;
        }
        public XmlNode GetElements(string param, string NameOrId, string xpath, int index, int seconds = 5)
        {
            for (var i = 0; i < seconds; i++)
            {
                try
                {
                    string xml = DumpXML(param, NameOrId);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectNodes(xpath)[index];
                    if (node != null) return node;
                }
                catch { }
                Thread.Sleep(500);
            }
            return null;
        }
        public string GetBounds(string param, string NameOrId, string xpath, int seconds = 5)
        {
            for (var i = 0; i < seconds; i++)
            {
                try
                {
                    string xml = DumpXML(param, NameOrId);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectSingleNode(xpath);
                    if (node != null) return node.Attributes["bounds"].Value;
                }
                catch { }
                Thread.Sleep(500);
            }
            return null;
        }
        public string GetBounds(string param, string NameOrId, string xpath, int index, int seconds = 5)
        {
            for (var i = 0; i < seconds; i++)
            {
                try
                {
                    string xml = DumpXML(param, NameOrId);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode root = doc.DocumentElement;
                    XmlNode node = root.SelectNodes(xpath)[index];
                    if (node != null) return node.Attributes["bounds"].Value;
                }
                catch { }
                Thread.Sleep(500);
            }
            return null;
        }
        public int CountXpathXML(string param, string NameOrId, string xpath, int seconds = 5)
        {
            for (var i = 0; i < seconds; i++)
            {
                try
                {
                    string xml = DumpXML(param, NameOrId);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNode root = doc.DocumentElement;
                    XmlNodeList node = root.SelectNodes(xpath);
                    if (node.Count != 0) return node.Count;
                }
                catch { }
                Thread.Sleep(500);
            }
            return 0;
        }
        public System.Drawing.Point GetPointFromXpath(string bounds)
        {
            bounds = bounds.Replace("][", ",").Replace("]", "").Replace("[", "");
            return new System.Drawing.Point(int.Parse(bounds.Split(',')[0]), int.Parse(bounds.Split(',')[1]));
        }
        public static System.Drawing.Point GetPointApp(int indexPos, int Width = 400, int Height = 250)
        {
            if (indexPos < 0)
            {
                throw new Exception("Lỗi: indexPos không được nhỏ hơn 0 !!!");
            }
            else if (Width < 0)
            {
                throw new Exception("Lỗi: Width không được nhỏ hơn 0 !!!");
            }
            else if (Height < 0)
            {
                throw new Exception("Lỗi: Height không được nhỏ hơn 0 !!!");
            }
            else
            {
                int j = 0;
                for (int i = 0; i < indexPos; i++)
                {
                    if (j == ((Convert.ToInt32(Screen.PrimaryScreen.Bounds.Width / Width)) * (Convert.ToInt32(Screen.PrimaryScreen.Bounds.Height / Height))))
                    {
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
                return new System.Drawing.Point((Height) * Convert.ToInt32(j % ((Screen.PrimaryScreen.Bounds.Width * Width) / (Width * Height))), (Width) * (j / ((Screen.PrimaryScreen.Bounds.Width * Width) / (Width * Height))));
            }
        }
        public struct TempMailAPI
        {
            public string Mail { get; set; }
            public string Id { get; set; }
        }
        public TempMailAPI GetMailTempMail()
        {
            string get = new SpeedRequest.HttpRequest().Get("http://localhost/tempmail/?action=create-request-tempmail").ToString();
            return new TempMailAPI() { Id = Regex.Match(get, "\"id\":\"(.*?)\"").Groups[1].Value, Mail = Regex.Match(get, "\"mailbox\":\"(.*?)\"").Groups[1].Value };
        }
        public string SoloverCaptcha(string param, string NameOrId)
        {
            try
            {
                int count_try_again = 0;
            BACK:;
                count_try_again++;
                if (count_try_again == 1) goto SOLOVER;
                if (count_try_again > 10) return "ERROR_CAPTCHA";
                try
                {
                    string bounds_ = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and not(contains(@bounds, '[0,0]'))]", 1);
                    var point_ = GetPointFromXpath(bounds_);
                    Tap(param, NameOrId, point_.X, point_.Y);
                    Thread.Sleep(8000);
                }
                catch { Thread.Sleep(5000); }
            SOLOVER:;
                string path = ScreenShootPath(param, NameOrId);
                var screm_ = ScreenShoot(param, NameOrId);
                var lammoi_ = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm_, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                if (lammoi_ == null) return "CANNOT_FIND_CAPTCHA";
                var response = CaptchaTiktokHelper.Verify(Path.GetFileName(path));
                try
                {
                    File.Delete(path);
                }
                catch { }
                if (response.KetQua)
                {
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input swipe {2} {3} {4} {5} {6}\"", param, NameOrId, response.X1, response.Y1, response.X2, response.Y2, response.TimeSwipe));
                    Thread.Sleep(10000);
                }
                else Thread.Sleep(3000);
                screm_ = ScreenShoot(param, NameOrId);
                lammoi_ = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm_, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                if (lammoi_ != null)
                {
                    Tap(param, NameOrId, lammoi_.Value.X, lammoi_.Value.Y);
                    goto BACK;
                }
                Thread.Sleep(15000);
                try
                {
                    File.Delete(path);
                }
                catch { }
                return "SUCCESS";
            }
            catch { }
            return "ERROR";
        }
        public string GetCodeTempMail(string id)
        {
            return Regex.Match(new SpeedRequest.HttpRequest().Get("http://localhost/tempmail/?action=data-request-tempmail&id=" + id).ToString(), "\"code\":(.*?),").Groups[1].Value;
        }
        public bool NavigateMeTikTokLite(string param, string NameOrId)
        {
            try
            {
                ExecuteMu(string.Format("-{0} {1} adb \"shell am start -a android.intent.action.VIEW -d snssdk1233://notification com.zhiliaoapp.musically.go\"", param, NameOrId));
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var me = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\me.png"));
                    if (me != null)
                    {
                        Thread.Sleep(5000);
                        Tap(param, NameOrId, me.Value.X, me.Value.Y);
                        goto DONE_;
                    }
                    Thread.Sleep(1000);
                }
                return false;
            DONE_:;
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        return true;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch { }
            return false;
        }
        public string RegHotmailTiktokLite(string param, string NameOrId, string hotmail)
        {
            try
            {
                for (var i = 0; i < 15; i++)
                {
                    int count_ = CountXpathXML(param, NameOrId, "//node[@text='Số điện thoại / Email / TikTok ID' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    if (count_ > 0) goto DONE____;
                    count_ = CountXpathXML(param, NameOrId, "//node[@text='Sử dụng số điện thoại hoặc email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    if (count_ > 0) goto DONE____;

                }
            DONE____:;
                int count = CountXpathXML(param, NameOrId, "//node[@text='Số điện thoại / Email / TikTok ID' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                if (count > 0)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Bạn không có tài khoản? Đăng ký']", 1, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y);//cộng 1 mới click đc
                }
                string bounds_ = GetBounds(param, NameOrId, "//node[@text='Sử dụng số điện thoại hoặc email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                var point_ = GetPointFromXpath(bounds_);
                Tap(param, NameOrId, point_.X, point_.Y);
                GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 10);
                for (var i = 0; i < rand.Next(1, 6); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 0, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < rand.Next(1, 4); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 1, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < rand.Next(5, 10); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 2, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    for (var j = 0; j < 10; j++)
                    {
                        count = CountXpathXML(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 1);
                        if (count == 0) goto DONE;
                    }
                }
                return "CANNOT_FIND_BUTTON_NEXT";
            DONE:;
                count = CountXpathXML(param, NameOrId, "//node[@text='Email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 10);
                if (count == 0) return "CANNOT_FIND_ELEMENT";
                bounds_ = GetBounds(param, NameOrId, "//node[@text='Email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                point_ = GetPointFromXpath(bounds_);
                Tap(param, NameOrId, point_.X + 1, point_.Y);//cộng 1 mới click đc
                Thread.Sleep(3000);
                InputUniCode(param, NameOrId, hotmail.Split('|')[0].Trim());
                Thread.Sleep(1000);
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and not(contains(@bounds, '[0,0]'))]", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    for (var j = 0; j < 10; j++)
                    {
                        count = CountXpathXML(param, NameOrId, "//node[@text='Tạo mật khẩu']", 1);
                        if (count > 0) goto DONE_;
                        count = CountXpathXML(param, NameOrId, "//node[@text='Bạn đã đăng ký']", 1);
                        if (count > 0)
                        {
                            textBox4.Text = textBox4.Text.Replace("\r", "").Replace(hotmail + "\n", "").Replace("\n", "\r\n");
                            return "YOU_HAVE_SIGN_UP";
                        }    
                    }
                }
                return "CANNOT_FIND_CLICK_BUTTON_PASSWORD";
            DONE_:;
                Thread.Sleep(3000);
                string password = textBox3.Text;
                if (checkBox3.Checked) password = RandomString(10) + "@";
                InputUniCode(param, NameOrId, password);
                Thread.Sleep(1000);
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and not(contains(@bounds, '[0,0]'))]", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    Thread.Sleep(10000);
                    var screm = ScreenShoot(param, NameOrId);
                    var lammoi = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                    if (lammoi != null) SoloverCaptcha(param, NameOrId);
                    count = CountXpathXML(param, NameOrId, "//node[@text='Tạo mật khẩu']", 1);
                    if (count == 0) goto DONE__;
                }
                return "ERROR_REG_PASSWORD";
            DONE__:;
                int count_error = 0;
            BACK:;
                try
                {
                    if (count_error > 1) return "ERROR_REG_PASSWORD";
                    DumpXML(param, NameOrId);
                    if (CountXpathXML(param, NameOrId, "//node[@text='Nhập mã gồm 6 chữ số']") > 0) return "ERROR_REG_PASSWORD";
                    bounds_ = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.input.LynxInputView']", 10);
                    point_ = GetPointFromXpath(bounds_);
                    Tap(param, NameOrId, point_.X + 2, point_.Y + 2);
                    Thread.Sleep(1000);
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_MOVE_END\"", param, NameOrId));
                    Thread.Sleep(1000);
                    for (var i = 0; i < 20; i++)
                    {
                        ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_DEL\"", param, NameOrId));
                    }
                    string username = RandomString(10);
                    InputUniCode(param, NameOrId, username);
                    Thread.Sleep(5000);
                    bounds_ = GetBounds(param, NameOrId, "//node[@text='Đăng ký' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 3);
                    point_ = GetPointFromXpath(bounds_);
                    if (point_.X > point_.Y)
                    {
                        bounds_ = GetBounds(param, NameOrId, "//node[@text='Đăng ký' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                        point_ = GetPointFromXpath(bounds_);
                    }
                    Tap(param, NameOrId, point_.X + 1, point_.Y);
                    Thread.Sleep(10000);
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input swipe 200 400 200 100 100\"", param, NameOrId));
                    Thread.Sleep(3000);
                    if (!NavigateMeTikTokLite(param, NameOrId)) return "ERROR_REG_LAST_STEP";
                    textBox4.Text = textBox4.Text.Replace("\r", "").Replace(hotmail + "\n", "").Replace("\n", "\r\n");
                    using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\output\\Hotmail.txt", true, Encoding.UTF8))
                    {
                        sw.WriteLine($"{username}|{password}|{hotmail}");
                        sw.Close();
                    }
                    return $"{username}|{password}|{hotmail}";
                }
                catch
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var lammoi = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                    if (lammoi != null)
                    {
                        SoloverCaptcha(param, NameOrId);
                        goto BACK;
                    }
                    count_error++;
                    string code = GetCodeHotmailTiktokLite(hotmail.Split('|')[0].Trim(), hotmail.Split('|')[1].Trim(), 20);
                    if (string.IsNullOrEmpty(code)) return "EMPTY_CODE";
                    foreach (char c in code)
                    {
                        InputUniCode(param, NameOrId, c.ToString());
                    }
                    goto BACK;
                }
            }
            catch { }
            return "ERROR";
        }
        public string RegTempMailTiktokLite(string param, string NameOrId)
        {
            try
            {
                for (var i = 0; i < 15; i++)
                {
                    int count_ = CountXpathXML(param, NameOrId, "//node[@text='Số điện thoại / Email / TikTok ID' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    if (count_ > 0) goto DONE____;
                    count_ = CountXpathXML(param, NameOrId, "//node[@text='Sử dụng số điện thoại hoặc email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    if (count_ > 0) goto DONE____;

                }
            DONE____:;
                int count = CountXpathXML(param, NameOrId, "//node[@text='Số điện thoại / Email / TikTok ID' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                if (count > 0)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Bạn không có tài khoản? Đăng ký']", 1, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y);//cộng 1 mới click đc
                }
                string bounds_ = GetBounds(param, NameOrId, "//node[@text='Sử dụng số điện thoại hoặc email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                var point_ = GetPointFromXpath(bounds_);
                Tap(param, NameOrId, point_.X, point_.Y);
                GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 10);
                for (var i = 0; i < rand.Next(1, 6); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 0, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < rand.Next(1, 4); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 1, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < rand.Next(5, 10); i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 2, 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                }
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    for (var j = 0; j < 10; j++)
                    {
                        count = CountXpathXML(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.pickview.LynxPickerViewColumn']", 1);
                        if (count == 0) goto DONE;
                    }
                }
                return "CANNOT_FIND_BUTTON_NEXT";
            DONE:;
                count = CountXpathXML(param, NameOrId, "//node[@text='Email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 10);
                if (count == 0) return "CANNOT_FIND_ELEMENT";
                bounds_ = GetBounds(param, NameOrId, "//node[@text='Email' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                point_ = GetPointFromXpath(bounds_);
                Tap(param, NameOrId, point_.X + 1, point_.Y);//cộng 1 mới click đc
                Thread.Sleep(3000);
                TempMailAPI tempMailAPI = new TempMailAPI();
                for (var i = 0; i < 5; i++)
                {
                    tempMailAPI = GetMailTempMail();
                    if (!string.IsNullOrEmpty(tempMailAPI.Mail)) goto DONE_GET;
                }
            DONE_GET:;
                if (string.IsNullOrEmpty(tempMailAPI.Mail)) return "SERVER_TEMPMAIL_ERROR";
                InputUniCode(param, NameOrId, tempMailAPI.Mail);
                Thread.Sleep(1000);
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and not(contains(@bounds, '[0,0]'))]", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    for (var j = 0; j < 10; j++)
                    {
                        count = CountXpathXML(param, NameOrId, "//node[@text='Tạo mật khẩu']", 1);
                        if (count > 0) goto DONE_;
                        count = CountXpathXML(param, NameOrId, "//node[@text='Bạn đã đăng ký']", 1);
                        if (count > 0) return "YOU_HAVE_SIGN_UP";
                    }
                }
                return "CANNOT_FIND_CLICK_BUTTON_PASSWORD";
            DONE_:;
                Thread.Sleep(3000);
                string password = textBox3.Text;
                if (checkBox3.Checked) password = RandomString(10) + "@";
                InputUniCode(param, NameOrId, password);
                Thread.Sleep(1000);
                for (var i = 0; i < 10; i++)
                {
                    string bounds = GetBounds(param, NameOrId, "//node[@text='Tiếp' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and not(contains(@bounds, '[0,0]'))]", 1);
                    var point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    Thread.Sleep(10000);
                    var screm = ScreenShoot(param, NameOrId);
                    var lammoi = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                    if (lammoi != null)
                    {
                        SoloverCaptcha(param, NameOrId);
                    }
                    count = CountXpathXML(param, NameOrId, "//node[@text='Tạo mật khẩu']", 1);
                    if (count == 0) goto DONE__;
                }
                return "ERROR_REG_PASSWORD";
            DONE__:;
                int count_error = 0;
            BACK:;
                try
                {
                    if (count_error > 1) return "ERROR_REG_PASSWORD";
                    DumpXML(param, NameOrId);
                    if (CountXpathXML(param, NameOrId, "//node[@text='Nhập mã gồm 6 chữ số']") > 0) return "ERROR_REG_PASSWORD";
                    bounds_ = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.input.LynxInputView']", 10);
                    point_ = GetPointFromXpath(bounds_);
                    Tap(param, NameOrId, point_.X + 2, point_.Y + 2);
                    Thread.Sleep(1000);
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_MOVE_END\"", param, NameOrId));
                    Thread.Sleep(1000);
                    for (var i = 0; i < 20; i++)
                    {
                        ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_DEL\"", param, NameOrId));
                    }
                    string username = RandomString(10);
                    InputUniCode(param, NameOrId, username);
                    Thread.Sleep(3500);
                    bounds_ = GetBounds(param, NameOrId, "//node[@text='Đăng ký' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 3);
                    point_ = GetPointFromXpath(bounds_);
                    if (point_.X > point_.Y)
                    {
                        bounds_ = GetBounds(param, NameOrId, "//node[@text='Đăng ký' and @class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 1, 1);
                        point_ = GetPointFromXpath(bounds_);
                    }
                    Tap(param, NameOrId, point_.X + 1, point_.Y);
                    Thread.Sleep(10000);
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input swipe 200 400 200 100 100\"", param, NameOrId));
                    Thread.Sleep(3000);
                    if (!NavigateMeTikTokLite(param, NameOrId)) return "ERROR_REG_LAST_STEP";
                    using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\accountTempMail.txt", true, Encoding.UTF8))
                    {
                        sw.WriteLine($"{username}|{password}|{tempMailAPI.Mail}");
                        sw.Close();
                    }
                    return $"{username}|{password}|{tempMailAPI.Mail}";
                }
                catch
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var lammoi = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\lammoi.png"));
                    if (lammoi != null)
                    {
                        SoloverCaptcha(param, NameOrId);
                        goto BACK;
                    }
                    count_error++;
                    string code = "";
                    for (var i = 0; i < 5; i++)
                    {
                        code = GetCodeTempMailTikTokLite(tempMailAPI.Id, 20);
                        if (!string.IsNullOrEmpty(code)) goto DONE_GET_;
                    }
                DONE_GET_:;
                    if (string.IsNullOrEmpty(code)) return "EMPTY_CODE";
                    foreach (char c in code)
                    {
                        InputUniCode(param, NameOrId, c.ToString());
                    }
                    goto BACK;
                }
            }
            catch { }
            return "ERROR";
        }
        public string GetCodeTempMailTikTokLite(string id, int timeout = 30)
        {
            for (var i = 0; i < timeout; i++)
            {
                string code = GetCodeTempMail(id);
                if (code != "0") return code;
                Thread.Sleep(1000);
            }
            return null;
        }
        public void PushFile(string param, string NameOrId, string path)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"push \\\"{2}\\\" /sdcard/Pictures/\"", param, NameOrId, path));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.BOOT_COMPLETED -n com.android.providers.media/.MediaScannerReceiver\"", param, NameOrId));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///mnt/sdcard/Pictures/\"", param, NameOrId));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/Pictures\"", param, NameOrId));
        }
        public void ClearFile(string param, string NameOrId, string filename)
        {
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm /sdcard/Pictures/{2}\"", param, NameOrId, filename));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.BOOT_COMPLETED -n com.android.providers.media/.MediaScannerReceiver\"", param, NameOrId));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///mnt/sdcard/Pictures/\"", param, NameOrId));
            ExecuteMu(string.Format("-{0} {1} adb \"shell am broadcast -a android.intent.action.MEDIA_MOUNTED -d file:///sdcard/Pictures\"", param, NameOrId));
        }
        public Bitmap ScreenShoot(string param, string NameOrId)
        {
            string index = RandomString(10);
            string CurrentPath = Application.StartupPath + "\\screenshoot_" + index + ".png";
            ExecuteMu(string.Format("-{0} {1} adb \"shell screencap -p /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            ExecuteMu(string.Format("-{0} {1} adb \"pull /sdcard/screenshoot_{2}.png \\\"{3}\\\"\"", param, NameOrId, index, CurrentPath));
            Bitmap result = null;
            try
            {
                using (Bitmap bitmap = new Bitmap(CurrentPath))
                {
                    result = new Bitmap(bitmap);
                }
            }
            catch { }
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            try
            {
                File.Delete(CurrentPath);
            }
            catch { }
            return result;
        }
        public string ScreenShootPathError(string param, string NameOrId)
        {
            string index = RandomString(10);
            string CurrentPath = Application.StartupPath + "\\error\\screenshoot_" + index + ".png";
            ExecuteMu(string.Format("-{0} {1} adb \"shell screencap -p /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            ExecuteMu(string.Format("-{0} {1} adb \"pull /sdcard/screenshoot_{2}.png \\\"{3}\\\"\"", param, NameOrId, index, CurrentPath));
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            return CurrentPath;
        }
        public string ScreenShootPath(string param, string NameOrId)
        {
            string index = RandomString(10);
            string CurrentPath = Application.StartupPath + "\\screenshoot_" + index + ".png";
            ExecuteMu(string.Format("-{0} {1} adb \"shell screencap -p /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            ExecuteMu(string.Format("-{0} {1} adb \"pull /sdcard/screenshoot_{2}.png \\\"{3}\\\"\"", param, NameOrId, index, CurrentPath));
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm /sdcard/screenshoot_{2}.png\"", param, NameOrId, index));
            return CurrentPath;
        }
        public bool PublicLikeTiktokLite(string param, string NameOrId)
        {
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var settings = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\settings.png"));
                    if (settings != null)
                    {
                        Tap(param, NameOrId, settings.Value.X, settings.Value.Y);
                        goto DONE_;
                    }
                    Thread.Sleep(1000);
                }
                goto ERROR;
            DONE_:;
                string bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.view.UIView' and @text='Quyền riêng tư']", 10);
                var point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.view.UIView' and @text='Video đã thích']", 10);
                ExecuteMu(string.Format("-{0} {1} adb \"shell input swipe 100 100 100 -1000\"", param, NameOrId));
                bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.view.UIView' and @text='Video đã thích']", 1);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.text.FlattenUIText' and @text='Mọi người']", 10);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                for (var i = 0; i < 3; i++)
                {
                    KeyEventBack(param, NameOrId);
                }
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        return true;
                    }
                    Thread.Sleep(1000);
                }
                goto ERROR;
            }
            catch { }
        ERROR:;
            NavigateMeTikTokLite(param, NameOrId);
            return false;
        }
        public bool SetProxy(string param, string NameOrId, string proxy)
        {
            try
            {
                KeyEventHome(param, NameOrId);
                Clear_Data_App(param, NameOrId, "com.cell47.College_Proxy");
                Open_App(param, NameOrId, "com.cell47.College_Proxy");
                for (var i = 0; i < 30; i++)
                {
                    string xml = DumpXML(param, NameOrId);
                    if (xml.Contains("textView_address")) goto DONE;
                    Thread.Sleep(500);
                }
            DONE:;
                string bounds = GetBounds(param, NameOrId, "//*[@resource-id=\"com.cell47.College_Proxy:id/editText_address\"]");
                var point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                Thread.Sleep(1000);
                InputUniCode(param, NameOrId, proxy.Split(':')[0].Trim());
                bounds = GetBounds(param, NameOrId, "//*[@resource-id=\"com.cell47.College_Proxy:id/editText_port\"]");
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                Thread.Sleep(1000);
                InputUniCode(param, NameOrId, proxy.Split(':')[1].Trim());
                if (proxy.Split(':').Length > 2)
                {
                    bounds = GetBounds(param, NameOrId, "//*[@resource-id=\"com.cell47.College_Proxy:id/editText_username\"]");
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    Thread.Sleep(1000);
                    InputUniCode(param, NameOrId, proxy.Split(':')[2].Trim());
                    bounds = GetBounds(param, NameOrId, "//*[@resource-id=\"com.cell47.College_Proxy:id/editText_password\"]");
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X, point.Y);
                    Thread.Sleep(1000);
                    InputUniCode(param, NameOrId, proxy.Split(':')[3].Trim());
                }
                bounds = GetBounds(param, NameOrId, "//*[@resource-id=\"com.cell47.College_Proxy:id/proxy_start_button\"]");
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                for (var i = 0; i < 30; i++)
                {
                    string xml = DumpXML(param, NameOrId);
                    if (xml.Contains("text=\"OK\""))
                    {
                        bounds = GetBounds(param, NameOrId, "//*[@text=\"OK\"]");
                        point = GetPointFromXpath(bounds);
                        Tap(param, NameOrId, point.X, point.Y);
                        Thread.Sleep(3000);
                    }
                    if (xml.Contains("text=\"Connected\"")) return true;
                    Thread.Sleep(500);
                }  
            }
            catch { }
            return false;
        }
        public bool ChangeNickNameTiktokLite(string param, string NameOrId)
        {
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        Tap(param, NameOrId, editprofile.Value.X, editprofile.Value.Y);
                        goto DONE_;
                    }
                    Thread.Sleep(1000);
                }
                goto ERROR;
            DONE_:;
                string bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.text.FlattenUIText']", 3, 10);
                var point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                bounds = GetBounds(param, NameOrId, "//node[@class='com.bytedance.ies.xelement.input.LynxInputView']", 10);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y, 2);
                Thread.Sleep(3000);
                ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_MOVE_END\"", param, NameOrId));
                Thread.Sleep(1000);
                for (var i = 0; i < 25; i++)
                {
                    ExecuteMu(string.Format("-{0} {1} adb \"shell input keyevent KEYCODE_DEL\"", param, NameOrId));
                }
                string name = TextSupport.RandomFromString(TextSupport.GetListLastNameVN()) + " " + TextSupport.RandomFromString(TextSupport.GetListLastNameVN());
                InputUniCode(param, NameOrId, name, "base64");
                Thread.Sleep(5000);
                bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.behavior.ui.view.UIView' and @text='Lưu']", 10);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X + 1, point.Y + 1);
                GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.ui.image.UIImage']", 10);
                KeyEventBack(param, NameOrId);
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        return true;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch { }
        ERROR:;
            NavigateMeTikTokLite(param, NameOrId);
            return false;
        }
        public bool UpAvatarTiktokLite(string param, string NameOrId, string filename)
        {
            string name = Path.GetFileName(filename);
            try
            {
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        Tap(param, NameOrId, editprofile.Value.X, editprofile.Value.Y);
                        goto DONE_;
                    }
                    Thread.Sleep(1000);
                }
                goto ERROR;
            DONE_:;
                PushFile(param, NameOrId, filename);
                string bounds = GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.ui.image.UIImage']", 10);
                var point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X, point.Y);
                bounds = GetBounds(param, NameOrId, "//node[@class='android.view.ViewGroup' and @index='1']", 10);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X + 1, point.Y + 1);
                bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.ImageButton']", 10);
                int count = CountXpathXML(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 1);
                if (count > 0)
                {
                    bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 1);
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    goto DONE;
                }
                count = CountXpathXML(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='Hình ảnh']", 1);
                if (count > 0)
                {
                    bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='Hình ảnh']", 5);
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='Pictures']", 5);
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    Thread.Sleep(1000);
                    count = CountXpathXML(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 3);
                    if (count > 0)
                    {
                        bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 1);
                        point = GetPointFromXpath(bounds);
                        Tap(param, NameOrId, point.X + 1, point.Y + 1);
                        goto DONE;
                    }
                    goto ERROR;
                }
                else
                {
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='Hình ảnh']", 5);
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='Pictures']", 5);
                    point = GetPointFromXpath(bounds);
                    Tap(param, NameOrId, point.X + 1, point.Y + 1);
                    Thread.Sleep(1000);
                    count = CountXpathXML(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 3);
                    if (count > 0)
                    {
                        bounds = GetBounds(param, NameOrId, "//node[@class='android.widget.TextView' and @resource-id='android:id/title' and @text='" + name + "']", 1);
                        point = GetPointFromXpath(bounds);
                        Tap(param, NameOrId, point.X + 1, point.Y + 1);
                        goto DONE;
                    }
                    goto ERROR;
                }
            DONE:;
                int count_ = CountXpathXML(param, NameOrId, "//node[@class='android.view.ViewGroup']", 10);
                bounds = GetBounds(param, NameOrId, "//node[@class='android.view.ViewGroup']", count_ - 1, 1);
                point = GetPointFromXpath(bounds);
                Tap(param, NameOrId, point.X + 1, point.Y + 1);
                GetBounds(param, NameOrId, "//node[@class='com.lynx.tasm.ui.image.UIImage']", 10);
                KeyEventBack(param, NameOrId);
                ClearFile(param, NameOrId, name);
                for (var i = 0; i < 10; i++)
                {
                    var screm = ScreenShoot(param, NameOrId);
                    var editprofile = KAutoHelper.ImageScanOpenCV.FindOutPoint(screm, (Bitmap)Bitmap.FromFile(Application.StartupPath + "\\img\\editprofile.png"));
                    if (editprofile != null)
                    {
                        ClearFile(param, NameOrId, name);
                        return true;
                    }
                    Thread.Sleep(1000);
                }
                goto ERROR;
            }
            catch { }
        ERROR:;
            ClearFile(param, NameOrId, name);
            NavigateMeTikTokLite(param, NameOrId);
            return false;
        }
        public string GetCodeHotmailTiktokLite(string hotmail, string password, int timeout = 30)
        {
            try
            {
                ImapClient imapClient = new ImapClient("outlook.office365.com", hotmail, password, AuthMethods.Login, 993, secure: true);
                for (int i = 0; i < timeout - 5; i++)
                {
                    try
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            if (j == 0)
                            {
                                imapClient.SelectMailbox("Inbox");
                            }
                            else
                            {
                                imapClient.SelectMailbox("Spam");
                            }
                            int messageCount = imapClient.GetMessageCount();
                            if (messageCount <= 0)
                            {
                                continue;
                            }
                            Lazy<MailMessage>[] array = null;
                            array = imapClient.SearchMessages(SearchCondition.From("noreply@account.tiktok.com").And(SearchCondition.Unseen()));
                            if (array.Length == 0)
                            {
                                continue;
                            }
                            int mail = array.Count() - 1;
                            while (mail >= 0)
                            {
                                string input = array[mail].Value.Body.ToString();
                                if (string.IsNullOrEmpty(input))
                                {
                                    mail--;
                                    continue;
                                }
                                string code = Regex.Match(input.Replace(" ", ""), "<pstyle=\"font-family:arial;color:blue;font-size:20px;\">(.*?)<p>").Groups[1].Value;
                                if (string.IsNullOrEmpty(code))
                                {
                                    mail--;
                                    continue;
                                }
                                if (imapClient.IsDisposed)
                                {
                                    imapClient.Dispose();
                                }
                                if (imapClient.IsConnected)
                                {
                                    imapClient.Disconnect();
                                }
                                return code;
                            }
                        }
                    }
                    catch { }
                    Thread.Sleep(200);
                }
                if (imapClient.IsDisposed)
                {
                    imapClient.Dispose();
                }
                if (imapClient.IsConnected)
                {
                    imapClient.Disconnect();
                }
            }
            catch
            {
                try
                {
                    OpenPop.Pop3.Pop3Client pop3Client = new OpenPop.Pop3.Pop3Client();
                    pop3Client.Connect("pop3.live.com", 995, true);
                    pop3Client.Authenticate(hotmail, password, OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
                    for (int i = 0; i < timeout - 5; i++)
                    {
                        for (int k = pop3Client.GetMessageCount(); k > 0; k--)
                        {
                            try
                            {
                                var msg = pop3Client.GetMessage(i);
                                if (msg.Headers.From.Address == "noreply@account.tiktok.com")
                                {
                                    string input = msg.FindFirstHtmlVersion().GetBodyAsText();
                                    if (string.IsNullOrEmpty(input))
                                    {
                                        continue;
                                    }
                                    string code = Regex.Match(input.Replace(" ", ""), "<pstyle=\"font-family:arial;color:blue;font-size:20px;\">(.*?)<p>").Groups[1].Value;
                                    if (string.IsNullOrEmpty(code))
                                    {
                                        continue;
                                    }
                                    if (pop3Client.Connected)
                                    {
                                        pop3Client.Disconnect();
                                    }
                                    return code;
                                }
                            }
                            catch { }
                        }
                    }
                    if (pop3Client.Connected)
                    {
                        pop3Client.Disconnect();
                    }
                }
                catch { }
            }
            return null;
        }
        public void PrepareBeforeReg(string param, string NameOrId, string proxy = "")
        {
            RemoveProxy(param, NameOrId);
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm -r /sdcard/Download/apkIcon\"", param, NameOrId));
            ExecuteMu(string.Format("-{0} {1} adb \"shell rm -r /sdcard/Download/flagCache\"", param, NameOrId));
            string result = ExecuteMu_Result(string.Format("-{0} {1} adb \"shell pm list packages\"", param, NameOrId));
            if (result.Contains("com.zhiliaoapp.musically.go") == false) InstallApp_File(param, NameOrId, Application.StartupPath + "\\apk\\TiktokLite.apk");
            if (result.Contains("com.android.adbkeyboard") == false) InstallApp_File(param, NameOrId, Application.StartupPath + "\\apk\\ADBKeyboard.apk");
            if (result.Contains("com.cell47.College_Proxy") == false) InstallApp_File(param, NameOrId, Application.StartupPath + "\\apk\\College_Proxy.apk");
            if (!result.Contains("com.zhiliaoapp.musically.go") || !result.Contains("com.android.adbkeyboard") || !result.Contains("com.cell47.College_Proxy"))
            {
                for (var i = 0; i < 60; i++)
                {
                    result = ExecuteMu_Result(string.Format("-{0} {1} adb \"shell pm list packages\"", param, NameOrId));
                    if (result.Contains("com.zhiliaoapp.musically.go") && result.Contains("com.android.adbkeyboard") && result.Contains("com.cell47.College_Proxy")) goto DONE;
                    Thread.Sleep(1000);
                }
            }
        DONE:;
            ExecuteMu(string.Format("-{0} {1} adb \"shell ime set com.android.adbkeyboard/.AdbIME\"", param, NameOrId));
            if (proxy.Contains(":")) SetProxy(param, NameOrId, proxy);
        }
        bool isStop = false;
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dia = MessageBox.Show("Bạn có chắc chắn muốn khôi phục cài đặt ban đầu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dia == DialogResult.Yes && button3.Text == "Dừng")
            {
                MessageBox.Show("Bạn không thể khôi phục cài đặt ban đầu khi đang chạy!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            LoadAll(false);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) textBox2.Text = fbd.SelectedPath;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                label15.Text = "Success: " + success + "\r\nError: " + error;
            }
            catch { }
        }
        public void WriteFile(string path, string text)
        {
        BACK:;
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            catch
            {
                goto BACK;
            }
        }
        public partial class CaptchaTiktokHelper
        {
            public static (bool KetQua, int X1, int Y1, int X2, int Y2, int TimeSwipe) Verify(string filename)
            {
                try
                {
                    if (filename != null)
                    {
                        FileInfo fi = new FileInfo(filename);
                        if (fi.Length != 0)
                        {
                            Mat img = Cv2.ImRead(filename);

                            var Anh_captcha = Cat_anh_captcha(img);
                            Mat Anh_da_su_ly = new Mat();
                            if (img.Width >= 540)
                            {
                                Anh_da_su_ly = Su_ly_hinh_anh_1(Anh_captcha.imgOutput);
                            }
                            else
                            {
                                Anh_da_su_ly = Su_ly_hinh_anh_2(Anh_captcha.imgOutput);
                            }
                            var Toa_do = BoroderComparison(Anh_da_su_ly);

                            if (Toa_do.Ketqua == true)
                            {
                                int X1 = Anh_captcha.X + ((int)(Anh_da_su_ly.Width * (double)19) / 100) / 2;
                                int Y1 = Anh_captcha.Y + Toa_do.Y;
                                int X2 = Anh_captcha.X + Toa_do.X;
                                int Y2 = Anh_captcha.Y + Toa_do.Y;

                                int TimeSwipe = (X2 - X1) * 10;

                                return (true, X1, Y1, X2, Y2, TimeSwipe);
                            }
                            else
                            {
                                return (false, 0, 0, 0, 0, 0);
                            }
                        }
                        else
                        {
                            return (false, 0, 0, 0, 0, 0);
                        }

                    }
                    else
                    {
                        return (false, 0, 0, 0, 0, 0);
                    }
                }
                catch
                {
                    return (false, 0, 0, 0, 0, 0);
                }

            }
            static (int X, int Y, int Width, int Height) Xac_dinh_captcha(Mat img)
            {
                Mat src = img.Clone();
                Cv2.CvtColor(src, src, ColorConversionCodes.RGB2BGR);
                Mat mask = new Mat(src.Size(), MatType.CV_8UC1);
                var lower_blue = new OpenCvSharp.Scalar(250, 250, 255);
                var upper_blue = new OpenCvSharp.Scalar(255, 255, 255);
                Cv2.InRange(src, lower_blue, upper_blue, mask);

                OpenCvSharp.Point[][] contours;
                OpenCvSharp.HierarchyIndex[] hierarchy;
                Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxNone);

                var rect = Cv2.BoundingRect(contours[0]);

                return (rect.X, rect.Y, rect.Width, rect.Height);
            }
            static (int X, int Y, int Width, int Height) Xac_dinh_anh_captcha(Mat img)
            {
                Mat src = img.Clone();
                Cv2.CvtColor(src, src, ColorConversionCodes.RGB2BGR);
                Mat mask = new Mat(src.Size(), MatType.CV_8UC1);
                var lower_blue = new OpenCvSharp.Scalar(250, 250, 255);
                var upper_blue = new OpenCvSharp.Scalar(255, 255, 255);
                Cv2.InRange(src, lower_blue, upper_blue, mask);

                OpenCvSharp.Point[][] contours;
                OpenCvSharp.HierarchyIndex[] hierarchy;
                Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxSimple);

                var Vung_cap_captcha = Xac_dinh_captcha(img);

                int minWidth = (Vung_cap_captcha.Width * 87) / 100;
                int minHeight = (Vung_cap_captcha.Height * 54) / 100;

                List<OpenCvSharp.Point[]> newcontours = new List<OpenCvSharp.Point[]>();

                foreach (var contour in contours)
                {
                    var rect = Cv2.BoundingRect(contour);
                    if (rect.Width < Vung_cap_captcha.Width && rect.Height < Vung_cap_captcha.Height && rect.Width >= minWidth && rect.Height >= minHeight)
                    {
                        newcontours.Add(contour);
                    }
                }
                var rect2 = Cv2.BoundingRect(newcontours[0]);

                return (rect2.X, rect2.Y, rect2.Width, rect2.Height);

            }
            static (Mat imgOutput, int X, int Y, int Width, int Height) Cat_anh_captcha(Mat img)
            {
                Mat src = img.Clone();
                Mat Anh = new Mat();
                var Anh_captcha = Xac_dinh_anh_captcha(src);
                Rect rect = new Rect(Anh_captcha.X, Anh_captcha.Y, Anh_captcha.Width, Anh_captcha.Height);
                Anh = src.SubMat(rect);
                return (Anh, Anh_captcha.X, Anh_captcha.Y, Anh_captcha.Width, Anh_captcha.Height);
            }
            static Mat Su_ly_hinh_anh_1(Mat img)
            {
                Mat src = img.Clone();
                int Width = src.Width;
                int Height = src.Height;
                Cv2.PyrUp(src, src);
                Cv2.Resize(src, src, new OpenCvSharp.Size(Width, Height));

                Cv2.CvtColor(src, src, ColorConversionCodes.BGR2GRAY);

                Cv2.Threshold(src, src, 127, 255, ThresholdTypes.Otsu);
                Cv2.Dilate(src, src, new Mat(), null, 1);
                Cv2.Threshold(src, src, 127, 255, ThresholdTypes.BinaryInv);

                int minWidth = (int)(src.Width * (double)19.9) / 100;
                for (int x = 0; x < src.Width; x++)
                {
                    for (int y = 0; y < src.Height; y++)
                    {
                        if (x <= minWidth)
                        {
                            Vec3b color = new Vec3b();
                            color.Item0 = 0;
                            color.Item1 = 0;
                            color.Item2 = 0;
                            src.Set(y, x, color);
                        }

                    }
                }
                Mat str = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
                Cv2.MorphologyEx(src, src, MorphTypes.Open, str);
                Cv2.MorphologyEx(src, src, MorphTypes.Close, str);
                return src;
            }
            static Mat Su_ly_hinh_anh_2(Mat img)
            {
                Mat src = img.Clone();
                int Width = src.Width;
                int Height = src.Height;
                Cv2.CvtColor(src, src, ColorConversionCodes.BGR2GRAY);
                Mat gray = new Mat();
                Cv2.PyrUp(src, src);
                InputArray kernel = InputArray.Create<int>(new int[3, 3] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } });
                Cv2.Filter2D(src, gray, src.Depth(), kernel, new OpenCvSharp.Point(-1, -1), 0);
                Cv2.Threshold(gray, gray, 127, 255, ThresholdTypes.Otsu);
                Cv2.Dilate(gray, gray, new Mat(), null, 3);
                Cv2.Erode(gray, gray, new Mat(), null, 1);
                Cv2.Resize(gray, gray, new OpenCvSharp.Size(Width, Height));
                Cv2.Threshold(gray, gray, 127, 255, ThresholdTypes.Otsu);

                int minWidth = (int)(gray.Width * (double)19.9) / 100;
                for (int x = 0; x < gray.Width; x++)
                {
                    for (int y = 0; y < gray.Height; y++)
                    {
                        if (x <= minWidth)
                        {
                            Vec3b color = new Vec3b();
                            color.Item0 = 0;
                            color.Item1 = 0;
                            color.Item2 = 0;
                            gray.Set(y, x, color);
                        }

                    }
                }
                Cv2.Threshold(gray, gray, 127, 255, ThresholdTypes.BinaryInv);
                return gray;
            }
            static (bool Ketqua, int X, int Y) BoroderComparison(Mat imgInput)
            {
                OpenCvSharp.Point[][] ContoursInput = GetContours(imgInput);

                Mat subImg = Cv2.ImRead(Application.StartupPath + "\\img\\img.png", 0);
                Cv2.Threshold(subImg, subImg, 127, 255, ThresholdTypes.Binary);
                OpenCvSharp.Point[][] ContoursSub;
                OpenCvSharp.HierarchyIndex[] hierarchy;
                Cv2.FindContours(subImg, out ContoursSub, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxNone);

                List<OpenCvSharp.Point[]> listContours = new List<OpenCvSharp.Point[]>();
                List<double> listRatio = new List<double>();

                int minWidth = (imgInput.Width * 13) / 100;
                int maxWidth = (imgInput.Width * 17) / 100;

                int minHeight = (imgInput.Height * 20) / 100;
                int maxHeight = (imgInput.Height * 27) / 100;

                foreach (var contour1 in ContoursInput)
                {
                    OpenCvSharp.Point[] contour2 = Cv2.ApproxPolyDP(contour1, 30, true);
                    var rect = Cv2.BoundingRect(contour1);

                    if (contour2.Length <= 10 && rect.Width >= minWidth && rect.Height >= minHeight && rect.Width <= maxWidth && rect.Height <= maxHeight)
                    {
                        var Shapes = Cv2.MatchShapes(ContoursSub[1], contour2, ShapeMatchModes.I2);

                        if (Shapes != null)
                        {
                            listContours.Add(contour1);
                            listRatio.Add(Shapes);
                        }
                    }
                }
                if (listRatio.Count != 0)
                {
                    double Ratio = listRatio[0];
                    var Contours = listContours[0];
                    for (int i = 1; i < listRatio.Count; i++)
                    {
                        if (Ratio > listRatio[i])
                        {
                            Contours = listContours[i];
                        }
                    }

                    var Toa_do = Cv2.BoundingRect(Contours);
                    int X = Toa_do.X + (Toa_do.Width / 2);
                    int Y = Toa_do.Y + (Toa_do.Height / 2);
                    return (true, X, Y);
                }
                else
                {
                    return (false, 0, 0);
                }
            }
            static OpenCvSharp.Point[][] GetContours(OpenCvSharp.Mat img)
            {
                OpenCvSharp.Point[][] contours;
                OpenCvSharp.HierarchyIndex[] hierarchy;
                Cv2.FindContours(img, out contours, out hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxNone);
                return contours;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Dừng")
            {
                button3.Text = "Chạy";
                isStop = true;
            }
            else
            {
                button3.Text = "Dừng";
                isStop = false;
                if (checkBox11.Checked)
                {
                    fmu = new fMemu();
                    fmu.Show();
                }
                textBox4.Lines = TextSupport.RemoveEmptyItems(textBox4.Lines.ToList()).ToArray();
                List<string> lists = new List<string>();
                foreach (var item in textBox4.Lines)
                {
                    if (item.Contains("|")) lists.Add(item);
                }
                textBox4.Lines = lists.ToArray();
            }
            CreateThread(int.Parse(numericUpDown7.Value.ToString()));
        }
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public IntPtr WinGetHandle(string title)
        {
            return FindWindow(null, title);
        }

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public async Task RunThreadMemu(DataGridViewRow row, int index, string hotmail = "")
        {
            await Task.Run(async () =>
            {
                row.Cells[0].Value = (index + 1).ToString();
                if (!string.IsNullOrEmpty(hotmail)) row.Cells[3].Value = hotmail;
                row.Cells[4].Value = "Delay"+ numericUpDown1.Value.ToString() +"s...";
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(numericUpDown1.Value.ToString()) * index));
                string proxy = "";
                if (radioButton10.Checked)
                {
                    row.Cells[4].Value = "Đang delay get proxy...";
                    string[] proxys = textBox6.Lines;
                BACK:;
                    proxy = proxys[rand.Next(0, proxys.Length)];
                    if (string.IsNullOrEmpty(proxy)) goto BACK;
                }
                else if (radioButton13.Checked)
                {
                    row.Cells[4].Value = "Đang delay get proxy...";
                    if (textBox10.Lines.Length <= index)
                    {
                    BACK:;
                        List<string> list = new List<string>();
                        List<string> list2 = textBox10.Lines.ToList();
                        list2 = TextSupport.RemoveEmptyItems(list2);
                        foreach (string text in list2)
                        {
                            if (FakeIP.Tinsoft.CheckApi(text) == "OK") list.Add(text);
                        }
                        if (list.Count > 0)
                        {
                            proxy = FakeIP.Tinsoft.GetProxy(list[rand.Next(0, list.Count)]);
                            if (string.IsNullOrEmpty(proxy))
                            {
                                Thread.Sleep(30000);
                                goto BACK;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không có proxy khả dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                    }
                    else
                    {
                    BACK:;
                        proxy = FakeIP.Tinsoft.GetProxy(textBox10.Lines[index]);
                        if (string.IsNullOrEmpty(proxy))
                        {
                            Thread.Sleep(30000);
                            goto BACK;
                        }
                    }
                }
                else if (radioButton12.Checked)
                {
                    row.Cells[4].Value = "Đang delay get proxy...";
                    string[] tmproxys = textBox9.Lines;
                    if (tmproxys.Length <= index)
                    {
                    BACK:;
                        FakeIP.TMProxy tmproxy = new FakeIP.TMProxy(tmproxys[rand.Next(0, tmproxys.Length)]);
                        proxy = tmproxy.GetProxy();
                        if (string.IsNullOrEmpty(proxy))
                        {
                            Thread.Sleep(30000);
                            goto BACK;
                        }
                    }
                    else
                    {
                    BACK:;
                        FakeIP.TMProxy tmproxy = new FakeIP.TMProxy(tmproxys[index]);
                        proxy = tmproxy.GetProxy();
                        if (string.IsNullOrEmpty(proxy))
                        {
                            Thread.Sleep(30000);
                            goto BACK;
                        }
                    }
                }
                else if (radioButton14.Checked)
                {
                    row.Cells[4].Value = "Đang delay get proxy...";
                BACK:;
                    string key = textBox11.Text;
                    List<string> listKey = FakeIP.Tinsoft.GetListKeyFromApiUser(key);
                    if (listKey.Count == 0)
                    {
                        MessageBox.Show("Không có proxy khả dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        return;
                    }
                    proxy = FakeIP.Tinsoft.GetProxy(listKey[rand.Next(listKey.Count)]);
                    if (string.IsNullOrEmpty(proxy))
                    {
                        Thread.Sleep(30000);
                        goto BACK;
                    }
                }
                else if (radioButton11.Checked)
                {
                    row.Cells[4].Value = "Đang delay get proxy...";
                    try
                    {
                        string[] _xproxy = textBox7.Lines;
                        string url = textBox8.Text;
                        if (_xproxy.Length <= index)
                        {
                        BACK:;
                            proxy = _xproxy[rand.Next(0, _xproxy.Length)];
                            if (string.IsNullOrEmpty(proxy)) goto BACK;
                            FakeIP.XProxy xproxy = new FakeIP.XProxy(url, proxy);
                            xproxy.ChangeProxy();
                        }
                        else
                        {
                        BACK:;
                            proxy = _xproxy[index];
                            if (string.IsNullOrEmpty(proxy)) goto BACK;
                            FakeIP.XProxy xproxy = new FakeIP.XProxy(url, proxy);
                            xproxy.ChangeProxy();
                        }
                    }
                    catch { }
                }
                row.Cells[4].Value = "Đang change info...";
                ChangeInfoMemu("i", index.ToString());
                string name = RandomString(10);
                Thread.Sleep(5000);
                ExecuteMu(string.Format("rename -i {0} \"{1}\"", index.ToString(), name));
                row.Cells[4].Value = "Đang mở memu...";
                Thread.Sleep(5000);
                Open("i", index.ToString());
                if (checkBox11.Checked) fmu.Add(name, index.ToString());
                else
                {
                    int timeout = 30 * 1000;
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    while (WinGetHandle("(" + name + ")") == IntPtr.Zero)
                    {
                        System.Threading.Thread.Sleep(500);
                        if (sw.ElapsedMilliseconds > timeout)
                        {
                            sw.Stop();
                            goto ERROR_FIND_HANDLE;
                        }
                    }
                    IntPtr windowhander = WinGetHandle("(" + name + ")");
                    Thread.Sleep(1000);
                    System.Drawing.Point point = GetPointApp(index, 480, 320);
                    MoveWindow(windowhander, point.X, point.Y, 320, 480, true);
                    Thread.Sleep(1000);
                ERROR_FIND_HANDLE:;
                }    
                for (var k = 0; k < 120; k++)
                {
                    if (!ExecuteMu_Result(string.Format("-i {0} adb \"shell input tap 0 0\"", index.ToString())).ToLower().Contains("is not launched")) goto DONE;
                    Thread.Sleep(1000);
                }
                row.Cells[4].Value = "Mở memu quá lâu!";
                row.DefaultCellStyle.BackColor = Color.Red;
                error++;
                goto DONE_REG;
            DONE:;
                row.Cells[4].Value = "Đang reg...";
                PrepareBeforeReg("i", index.ToString(), proxy);
                Stop_App("i", index.ToString(), "com.zhiliaoapp.musically.go");//tắt tiktok lite
                Clear_Data_App("i", index.ToString(), "com.zhiliaoapp.musically.go");//clear data app tiktok lite
                Grant("i", index.ToString(), "com.zhiliaoapp.musically.go");//cho phép all quyền của ứng dụng tiktok lite
                Open_App("i", index.ToString(), "com.zhiliaoapp.musically.go");
                string result = "";
                if (radioButton2.Checked) result = RegTempMailTiktokLite("i", index.ToString());
                else if (radioButton1.Checked) result = RegHotmailTiktokLite("i", index.ToString(), hotmail);
                if (result.Contains("|"))
                {
                    row.Cells[1].Value = result.Split('|')[0].Trim();
                    row.Cells[2].Value = result.Split('|')[1].Trim();
                    if (result.Split('|').Length == 3)
                    {
                        row.Cells[3].Value = result.Split('|')[2].Trim();
                    }
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.Cells[4].Value = "Đã reg xong!";
                    success++;
                    //Thread.Sleep(1500);
                    if (checkBox2.Checked)
                    {
                        Thread.Sleep(1000);
                        row.Cells[4].Value = "Đang bật công khai tym";
                        if (PublicLikeTiktokLite("i", index.ToString())) row.Cells[4].Value = "Bật công khai tym thành công";
                        else row.Cells[4].Value = "Bật công khai tym thất bại";
                    }    
                    if (checkBox13.Checked)
                    {
                        Thread.Sleep(1000);
                        row.Cells[4].Value = "Đang up avatar";
                        string path = RandomFile(textBox1.Text);
                        if (UpAvatarTiktokLite("i", index.ToString(), path)) row.Cells[4].Value = "Up avatar thành công";
                        else row.Cells[4].Value = "Up avatar thất bại";
                    }
                    if (checkBox1.Checked)
                    {
                        Thread.Sleep(1000);
                        row.Cells[4].Value = "Đang đổi tên";
                        if (ChangeNickNameTiktokLite("i", index.ToString())) row.Cells[4].Value = "Đổi tên thành công";
                        else row.Cells[4].Value = "Đổi tên thất bại";
                    }
                    Thread.Sleep(3000);
                    goto DONE_REG;
                }
                else if (radioButton1.Checked)
                {
                    try
                    {
                        new ImapClient("outlook.office365.com", hotmail.Split('|')[0].Trim(), hotmail.Split('|')[1].Trim(), AuthMethods.Login, 993, secure: true);
                    }
                    catch
                    {
                        try
                        {
                            OpenPop.Pop3.Pop3Client pop3Client = new OpenPop.Pop3.Pop3Client();
                            pop3Client.Connect("pop3.live.com", 995, true);
                            pop3Client.Authenticate(hotmail.Split('|')[0].Trim(), hotmail.Split('|')[1].Trim(), OpenPop.Pop3.AuthenticationMethod.UsernameAndPassword);
                        }
                        catch
                        {
                            textBox4.Text = textBox4.Text.Replace("\r", "").Replace(hotmail + "\n", "").Replace("\n", "\r\n");
                            row.Cells[4].Value = "Hotmail không hoạt động!";
                            row.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            ERROR_REG:;
                string path_error = ScreenShootPathError("i", index.ToString());
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\error\\error.log", true, Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(hotmail)) hotmail += "|";
                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] " + hotmail + "Code:" + result + "|Path: " + path_error);
                    sw.Close();
                }
                row.Cells[4].Value = "Đã xảy ra lỗi reg! (Mã lỗi: " + result + ")";
                row.DefaultCellStyle.BackColor = Color.Red;
                error++;
            DONE_REG:;
                if (checkBox11.Checked) fmu.Remove(name);
                Close("i", index.ToString());
                return;
            });
        }
        public static string RandomFile(string path)
        {
            string file = null;
            if (!string.IsNullOrEmpty(path))
            {
                var extensions = new string[] { ".png", ".jpg", ".gif" };
                try
                {
                    var di = new DirectoryInfo(path);
                    var rgFiles = di.GetFiles("*.*").Where(f => extensions.Contains(f.Extension.ToLower()));
                    Random R = new Random();
                    file = rgFiles.ElementAt(R.Next(0, rgFiles.Count())).FullName;
                }
                catch { }
            }
            return file;
        }
        public async void CreateThread(int thread)
        {
            await Task.Run(async () =>
            {
                if (CountMemu() < thread)
                {
                    button3.Text = "Chạy";
                    isStop = true;
                    try
                    {
                        fmu.Close();
                    }
                    catch { }
                    return;
                }
                if (radioButton2.Checked)
                {
                    while (true)
                    {
                        if (isStop) return;
                        List<Task> Tasks = new List<Task>();
                        if (radioButton7.Checked)
                        {
                            FakeIP.Hma.Reset();
                            Thread.Sleep(30000);
                        }
                        else if (radioButton8.Checked)
                        {
                            FakeIP.NordVPN nord = new FakeIP.NordVPN(textBox14.Text);
                            nord.ConnectServerByName(FakeIP.RandomCountry(), 10);
                            Thread.Sleep(30000);
                        }
                        else if (radioButton9.Checked)
                        {
                            FakeIP.Dcom.Reset(textBox13.Text);
                            Thread.Sleep(15000);
                            FakeIP.Dcom.Start(textBox13.Text);
                            Thread.Sleep(15000);
                        }
                        System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\pictures");
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        for (int j = 0; j < thread; j++)
                        {
                            var k = j;
                            int add = dataGridView1.Rows.Add();
                            DataGridViewRow row = dataGridView1.Rows[add];
                            Tasks.Add(RunThreadMemu(row, k));
                        }
                        await Task.WhenAll(Tasks.ToArray());
                        try
                        {
                            dataGridView1.Rows.Clear();
                        }
                        catch { }
                        if (checkBox11.Checked) fmu.RemoveAll();
                        ExecuteMu("stopall");
                        if (isStop) return;
                    }
                }
                else if (radioButton1.Checked)
                {
                    while (textBox4.Lines.Length > 0)
                    {
                        if (isStop) return;
                        if (radioButton1.Checked && !textBox4.Text.Contains("|"))
                        {
                            button3.Text = "Chạy";
                            isStop = true;
                            try
                            {
                                fmu.Close();
                            }
                            catch { }
                            return;
                        }
                        List<Task> Tasks = new List<Task>();
                        if (radioButton7.Checked)
                        {
                            FakeIP.Hma.Reset();
                            Thread.Sleep(30000);
                        }
                        else if (radioButton8.Checked)
                        {
                            FakeIP.NordVPN nord = new FakeIP.NordVPN(textBox14.Text);
                            nord.ConnectServerByName(FakeIP.RandomCountry(), 10);
                            Thread.Sleep(30000);
                        }
                        else if (radioButton9.Checked)
                        {
                            FakeIP.Dcom.Reset(textBox13.Text);
                            Thread.Sleep(15000);
                            FakeIP.Dcom.Start(textBox13.Text);
                            Thread.Sleep(15000);
                        }
                        System.IO.DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\pictures");
                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        for (int j = 0; j < thread; j++)
                        {
                            var k = j;
                            if (radioButton1.Checked && textBox4.Lines.Length > k)
                            {
                                string hotmail = textBox4.Lines[k];
                                int add = dataGridView1.Rows.Add();
                                DataGridViewRow row = dataGridView1.Rows[add];
                                Tasks.Add(RunThreadMemu(row, k, hotmail));
                            }
                        }
                        await Task.WhenAll(Tasks.ToArray());
                        try
                        {
                            dataGridView1.Rows.Clear();
                        }
                        catch { }
                        if (checkBox11.Checked) fmu.RemoveAll();
                        if (radioButton1.Checked && !textBox4.Text.Contains("|"))
                        {
                            button3.Text = "Chạy";
                            isStop = true;
                            try
                            {
                                fmu.Close();
                            }
                            catch { }
                            return;
                        }
                        if (isStop) return;
                    }
                    button3.Text = "Chạy";
                    isStop = true;
                    try
                    {
                        fmu.Close();
                    }
                    catch { }
                    return;
                }
            });
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath)) textBox1.Text = fbd.SelectedPath;
            }
        }

        private void Btn_CheckLive_Click(object sender, EventArgs e)
        {
            MCommon.ShowForm(new fCheckLive());
        }

        private void btnViewResult_Click(object sender, EventArgs e)
        {
            try
            {
                string text = "output";
                if (Directory.Exists(text))
                {
                    Process.Start("explorer.exe", text);
                }
            }
            catch
            {
                MessageBox.Show("Không tìm thấy folder data", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}
