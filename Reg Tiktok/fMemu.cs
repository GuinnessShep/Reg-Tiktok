using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reg_Tiktok
{
    public partial class fMemu : Form
    {
        public fMemu()
        {
            InitializeComponent();
            Label.CheckForIllegalCrossThreadCalls = false;
            Panel.CheckForIllegalCrossThreadCalls = false;
            FlowLayoutPanel.CheckForIllegalCrossThreadCalls = false;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public IntPtr WinGetHandle(string title)
        {
            return FindWindow(null, title);
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        int GWL_STYLE = -16;
        uint WS_VISIBLE = 0x10000000;
        public void Remove(string nameLD)
        {
            Thread thread = new Thread(() =>
            {
                foreach (var panel in flowLayoutPanel1.Controls.OfType<Panel>().ToList())
                {
                    if (panel.Name == "panel_" + TextSupport.MD5(nameLD.ToLower())) 
                    {
                        flowLayoutPanel1.Controls.Remove(panel); 
                        return; 
                    }
                }
            });
            thread.IsBackground = false;
            thread.Start();
        }
        public void RemoveAll()
        {
            flowLayoutPanel1.Controls.Clear();
        }
        public void Add(string name, string id)
        {
            Thread thread = new Thread(() =>
            {
                //Open memu
                if (string.IsNullOrEmpty(id)) id = "Unknow ID";
                else id = (int.Parse(id) + 1).ToString();
                Panel ptn = new Panel();
                ptn.Width = 320;
                ptn.Height = 480;
                ptn.BackColor = Color.Black;
                ptn.Name = "panel_" + TextSupport.MD5(name.ToLower());
                ptn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ptn.AutoSize = false;
                Label lb = new Label();
                lb.Anchor = AnchorStyles.None;
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Text = "Memu " + id;
                lb.ForeColor = Color.White;
                lb.Name = "label_" + TextSupport.MD5(name);
                lb.Width = 320;
                lb.Height = 20;
                Panel ptn2 = new Panel();
                ptn2.Width = 320;
                ptn2.Height = 480;
                ptn2.Dock = DockStyle.Bottom;
                ptn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                ptn2.AutoSize = false;
                ptn.Controls.Add(lb);
                ptn.Controls.Add(ptn2);
                flowLayoutPanel1.Invoke((Action)delegate
                {
                    flowLayoutPanel1.Controls.Add(ptn);
                });
                int timeout = 30 * 1000;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (WinGetHandle("(" + name + ")") == IntPtr.Zero)
                {
                    System.Threading.Thread.Sleep(500);
                    if (sw.ElapsedMilliseconds > timeout)
                    {
                        sw.Stop();
                        flowLayoutPanel1.Controls.Remove(ptn);
                        return;
                    }
                }
                IntPtr windowhander = WinGetHandle("(" + name + ")");
                Thread.Sleep(1000);
                MoveWindow(windowhander, 0, 0, 320, 480, true);
                Thread.Sleep(1000);
                SetParent(windowhander, ptn2.Handle);
                Thread.Sleep(1000);
                SetWindowLong(windowhander, GWL_STYLE, WS_VISIBLE);
                Thread.Sleep(1000);
                MoveWindow(windowhander, -22, -32, 320, 480, true);
            });
            thread.IsBackground = false;
            thread.Start();
        }
        private void fLDPlayer_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
