using Common;
using HttpRequest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reg_Tiktok
{
    public partial class fCheckLive : Form
    {
        public fCheckLive()
        {
            InitializeComponent();
        }
        public RequestHTTP request;
        public static string CheckLiveWallTiktok(string uid)
        {
            RequestHTTP requestHTTP = new RequestHTTP();
            requestHTTP.SetSSL(SecurityProtocolType.Tls12);
            requestHTTP.SetKeepAlive(k: true);
            requestHTTP.SetDefaultHeaders(new string[2] { "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8", "user-agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/82.0.144 Chrome/76.0.3809.144 Safari/537.36" });
            try
            {
                string text = requestHTTP.Request("GET", "https://m.tiktok.com/node/share/user/@" + uid + "?aid=1988");
                if (!string.IsNullOrEmpty(text))
                {
                    if (text.Contains("pageId") && text.Contains("userInfo"))
                    {
                        return "1|";
                    }
                    return "0|";
                }
            }
            catch
            {
            }
            return "2|";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                list = txtInput.Lines.ToList();
                int iThread = 0;
                int num = Convert.ToInt32(nudThread.Value);
                if (num == 0)
                {
                    MessageBoxHelper.ShowMessageBox(Language.GetValue("Số luồng phải >0, vui lòng nhập lại!"), 3);
                    return;
                }
                if (list.Count < num)
                {
                    num = list.Count;
                }
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Visible = true;
                });
                int num2 = 0;
                while (num2 < list.Count)
                {
                    if (iThread < num)
                    {
                        Interlocked.Increment(ref iThread);
                        string uid = list[num2++];
                        new Thread((ThreadStart)delegate
                        {
                            if (rduid.Checked)
                            {
                                string text = CheckLiveWallTiktok(uid).ToString();
                                if (text.StartsWith("0|"))
                                {
                                    AddRowIntoTextbox(txtDie, uid);
                                }
                                else if (text.StartsWith("1|"))
                                {
                                    AddRowIntoTextbox(txtLive, uid);
                                }
                                else
                                {
                                    AddRowIntoTextbox(txtKhongCheckDuoc, uid);
                                }
                                Interlocked.Decrement(ref iThread);
                            }
                            else
                            {
                                var infor = uid.Split('|');
                                string text = CheckLiveWallTiktok(infor[0]).ToString();
                                if (text.StartsWith("0|"))
                                {
                                    AddRowIntoTextbox(txtDie, uid);
                                }
                                else if (text.StartsWith("1|"))
                                {
                                    AddRowIntoTextbox(txtLive, uid);
                                }
                                else
                                {
                                    AddRowIntoTextbox(txtKhongCheckDuoc, uid);
                                }
                                Interlocked.Decrement(ref iThread);
                            }


                        }).Start();
                    }
                    else
                    {
                        Application.DoEvents();
                        MCommon.DelayTime(1.0);
                    }
                }
                while (iThread > 0)
                {
                    MCommon.DelayTime(1.0);
                }
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Visible = false;
                });
                MessageBoxHelper.ShowMessageBox(Language.GetValue("Đã check xong!"));
            }
            catch (Exception)
            {
                MessageBoxHelper.ShowMessageBox(Language.GetValue("Đã có lỗi xảy ra, vui lòng thử lại sau!"), 2);
            }
        }
        private void AddRowIntoTextbox(RichTextBox txt, string content)
        {
            txt.Invoke((MethodInvoker)delegate
            {
                Application.DoEvents();
                RichTextBox richTextBox = txt;
                richTextBox.Text = richTextBox.Text + content + "\r\n";
            });
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            txtLive.Text = "";
            txtDie.Text = "";
            txtKhongCheckDuoc.Text = "";
        }
        private void txtKhongCheckDuoc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> lst = txtKhongCheckDuoc.Lines.ToList();
                lst = MCommon.RemoveEmptyItems(lst);
                grKhongCheckDuoc.Text = string.Format(Language.GetValue("Không check được ({0})"), lst.Count.ToString());
            }
            catch
            {
            }
        }
        private void txtDie_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> lst = txtDie.Lines.ToList();
                lst = MCommon.RemoveEmptyItems(lst);
                grChuaTao.Text = "DIE (" + lst.Count + ")";
            }
            catch
            {
            }
        }
        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> lst = txtInput.Lines.ToList();
                lst = MCommon.RemoveEmptyItems(lst);
                groupBox7.Text = "List Uid (" + lst.Count + ")";
            }
            catch
            {
            }
        }

        private void txtLive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> lst = txtLive.Lines.ToList();
                lst = MCommon.RemoveEmptyItems(lst);
                grDaTao.Text = "LIVE (" + lst.Count + ")";
            }
            catch
            {
            }
        }
        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}
