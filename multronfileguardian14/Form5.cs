﻿using RJCodeAdvance.RJControls;
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
        private void darktheme()
        {
            panel2.BackColor = Color.FromArgb(40, 40, 40);
            this.BackColor = Color.FromArgb(23, 23, 23);
            rjButton9.BackColor = Color.RoyalBlue;
            rjButton8.BackColor = panel2.BackColor;
            panel5.BackColor = panel2.BackColor;
            foreach (Control yazi in this.Controls)
            {
                if (yazi.Name.Contains("rjButton"))
                {
                        yazi.BackColor = Color.FromArgb(43, 40, 40);
                        yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("checkBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("label"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("linkLabel"))
                {
                    LinkLabel label = (LinkLabel)yazi;
                    label.LinkColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("textBox"))
                {
                    yazi.BackColor = Color.FromArgb(30, 30, 30);
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("comboBox"))
                {
                    ComboBox cbox = (ComboBox)yazi;
                    cbox.FlatStyle = FlatStyle.Popup;
                    cbox.BackColor = Color.FromArgb(30, 30, 30);
                    cbox.ForeColor = Color.WhiteSmoke;
                }
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            if (forum1.formuc.theme1.Checked == true)
            {
                System.Threading.Thread threddd = new System.Threading.Thread(() => darktheme());
                threddd.Start();
            }
            comboBox1.Text = forum1.comboBox1.Text;
            comboBox2.Text = forum1.comboBox2.Text;
            comboBox3.Text = forum1.comboBox3.Text;
            textBox1.PasswordChar = forum1.textBox1.PasswordChar;
            label5.Text = forum1.label5.Text;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Focus();
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
                forum1.checkBox1.Checked = checkBox1.Checked;
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

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Focus();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            byte equalopen = 0;
            if (forum1.formuc.Visible == true)
            {
                equalopen = 1;
            }
            if (equalopen == 0)
            {

                forum1.formuc.WindowState = FormWindowState.Minimized;
                forum1.formuc.Show();
            }
            if (checkBox1.Checked)
            {
                forum1.formuc.showpassword1.PerformClick();
            }
            else
            {
                forum1.formuc.showpassword0.PerformClick();

            }
            if (equalopen == 0)
            {
                forum1.formuc.Hide();
            }
        }

        private void Form5_Shown(object sender, EventArgs e)
        {
            checkBox1.Checked = forum1.checkBox1.Checked;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            rjButton9.PerformClick();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
                label12.Text = "⬆";
            }
            else
            {
                panel5.Visible = false;
                label12.Text = "⬇";
            }
        }
    }
}
