using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multronfileguardian
{
    public partial class Form3 : Form
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        Form1 form;
        public short swillbesaved = 0;
        public Form3(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void showpassword1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            form.textBox1.PasswordChar = '\0';
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void showpassword0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            form.textBox1.PasswordChar = '*';
        }

        private void dfafterencrypted0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void dfafterencrypted1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }
    }
}
