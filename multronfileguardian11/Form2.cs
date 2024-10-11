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
    public partial class Form2 : Form
    {
        public Form2(Form1 form)
        {
            InitializeComponent();
            mainform = form;
        }
        Form1 mainform;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These are your RSA Public/Private Key. Public Key is the key that will be used to encrypt but cant be used to decrypt. Private Key is only for decrypt, Cant be used to encrypt. So protect your Private key, Dont give it anyone", "Multron RSA Key Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            System.Threading.Thread nthread = new System.Threading.Thread(() => genkey());
            nthread.Start();
        }



        private void rjButton7_Click(object sender, EventArgs e)
        {
            if (rjButton4.Enabled == true)
            {
                this.Close();
            }
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        public void genkey()
        {
            rjButton4.Enabled = false;
            rjButton4.Text = "Generating...";
            string rsakey = raes.rsageneratekey();
            short pkey = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            foreach (char chr in rsakey)
            {
                if (chr == ' ')
                {
                    pkey = 1;
                }
                if (pkey == 1 && chr != ' ')
                {
                    pkey = 2;
                }
                if (pkey == 0)
                {
                    textBox1.Text += chr;
                    label1.Text = "Public Key %" + Math.Round(((double)textBox1.Text.Length - 1) / ((double)414) * 100, 0).ToString();
                }
                if (pkey == 2)
                {
                    textBox2.Text += chr;
                    label2.Text = "Private Key %" + Math.Round(((double)textBox2.Text.Length - 1) / ((double)1678) * 100, 0).ToString();

                }
                rsakey = "";
            }
            rjButton4.Text = "Generate RSA Key";
            rjButton4.Enabled = true;
            label1.Text = "Public Key";
            label2.Text = "Private Key";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainform.closedrsakgen = 1;
        }
    }
}
