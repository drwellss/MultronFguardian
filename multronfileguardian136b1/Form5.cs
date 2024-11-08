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
    public partial class Form5 : Form
    {
        Form1 forum1 = null;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Form5(Form1 foum1)
        {
            InitializeComponent();
            this.forum1 = foum1;
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void rjButton6_Click(object sender, EventArgs e)
        {
            forum1.rjButton6_Click(sender, e);
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            comboBox1.Text = forum1.comboBox1.Text;
            comboBox2.Text = forum1.comboBox2.Text;
            textBox1.PasswordChar = forum1.textBox1.PasswordChar;
            label5.Text = forum1.label5.Text;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
            if (comboBox1.Text.Contains("AES") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "128", "192", "256" });
            }
            else if (comboBox1.Text.Contains("Serpent") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "128", "192", "256" });
            }
            else if (comboBox1.Text.Contains("Twofish") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "128", "192", "256" });
            }
            else if (comboBox1.Text.Contains("ThreeFish") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "256", "512", "1024" });
            }
            else if (comboBox1.Text.Contains("Camellia") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "128", "192", "256" });
            }
            else if (comboBox1.Text.Contains("ChaCha20") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "256" });
            }
            comboBox3.SelectedIndex = comboBox3.Items.Count - 1;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You need to enter a key.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                forum1.Show();
                forum1.comboBox1.Text = comboBox1.Text;
                forum1.comboBox2.Text = comboBox2.Text;
                forum1.comboBox3.Text = comboBox3.Text;
                forum1.textBox1.Text = textBox1.Text;
                if (forum1.encryptstatus == 1)
                {
                   forum1.rjButton2.PerformClick();
                }
                else if (forum1.encryptstatus ==2)
                {
                    forum1.rjButton1.PerformClick();
                }
                this.Hide();

            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void rjButton9_Click(object sender, EventArgs e)
        {
            forum1.formuc.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            forum1.label6_Click(sender, e);
        }
    }
}
