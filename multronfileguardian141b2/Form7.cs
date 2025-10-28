using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace multronfileguardian
{
    public partial class Form7 : Form
    {
        Form6 formalti;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 1;
        private const int HTCAPTION = 2;
        private const int HTLEFT = 10;
        private const int HTRIGHT = 11;
        private const int HTTOP = 12;
        private const int HTTOPLEFT = 13;
        private const int HTTOPRIGHT = 14;
        private const int HTBOTTOM = 15;
        private const int HTBOTTOMLEFT = 16;
        private const int HTBOTTOMRIGHT = 17;
        private const int RESIZE_HANDLE_SIZE = 65;
        public byte fullscr = 0;
        public Form7(Form6 formm)
        {
            InitializeComponent();
            formalti = formm;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                if ((int)m.Result == HTCLIENT)
                {
                    var cursor = this.PointToClient(Cursor.Position);
                    bool top = cursor.Y < RESIZE_HANDLE_SIZE;
                    bool left = cursor.X < RESIZE_HANDLE_SIZE;
                    bool right = cursor.X > this.Width - RESIZE_HANDLE_SIZE;
                    bool bottom = cursor.Y > this.Height - RESIZE_HANDLE_SIZE;

                    if (top && left)
                        m.Result = (IntPtr)HTTOPLEFT;
                    else if (top && right)
                        m.Result = (IntPtr)HTTOPRIGHT;
                    else if (bottom && left)
                        m.Result = (IntPtr)HTBOTTOMLEFT;
                    else if (bottom && right)
                        m.Result = (IntPtr)HTBOTTOMRIGHT;
                    else if (left)
                        m.Result = (IntPtr)HTLEFT;
                    else if (right)
                        m.Result = (IntPtr)HTRIGHT;
                    else if (top)
                        m.Result = (IntPtr)HTTOP;
                    else if (bottom)
                        m.Result = (IntPtr)HTBOTTOM;
                    else
                        m.Result = (IntPtr)HTCAPTION; // allow dragging
                }
                return;
            }
            base.WndProc(ref m);
        }
        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            formalti.Show();
            formalti.textBox1.Text = textBox1.Text;
            formalti.isfwopen = 0;
        }

        private void Form7_Shown(object sender, EventArgs e)
        {
            textBox1.Font = formalti.textBox1.Font;
            textBox1.Text = formalti.textBox1.Text;
            textBox1.BackColor = formalti.textBox1.BackColor;
            textBox1.ForeColor = formalti.textBox1.ForeColor;
            if (textBox1.BackColor == Color.FromArgb(30, 30, 30))
            {
                textBox1.BackColor = Color.FromArgb(23,23,23); ;
                panel2.BackColor = Color.FromArgb(40, 40, 40);
            }
            BackColor = textBox1.BackColor;
            rjButton8.BackColor = panel2.BackColor;
            rjButton1.BackColor = panel2.BackColor;
            formalti.Hide();
        }
        private void rjButton8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Form7_SizeChanged(object sender, EventArgs e)
        {
            rjButton1.Location = new Point(rjButton7.Location.X - 54, 0);
            rjButton8.Location = new Point(rjButton7.Location.X - 109, 0);
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (fullscr == 0)
            {
                this.Location = new Point(0, 0);
                this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                fullscr = 1;
                rjButton1.Text = "🗗";
            }
            else
            {
                this.Size = new Size(887,512);
                CenterToScreen();
                fullscr = 0;
                rjButton1.Text = "☐";
            }
        }
    }
}
