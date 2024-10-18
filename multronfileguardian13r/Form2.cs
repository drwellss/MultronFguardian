﻿using System;
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
        int keysize = 2048;
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
            comboBox2.Text = mainform.comboBox2.Text;
        }
        public string getstring(string data, char tag, short indx)
        {
            string reassemblied = "";
            short done = 0;
            if (indx == 0)
            {
                foreach (char c in data)
                {
                    if (c == tag)
                    {
                        break;
                    }
                    else
                    {
                        reassemblied = reassemblied + c;
                    }
                }
            }
            else
            {
                foreach (char c in data)
                {
                    if (done == 1)
                    {
                        reassemblied = reassemblied + c;
                    }
                    if (c == tag)
                    {
                        done = 1;
                    }
                }
            }
            return reassemblied;
        }
        public void genkey()
        {
            rjButton4.Enabled = false;
            rjButton4.Text = "Generating...";
            string rsakey = raes.rsageneratekey(keysize);
            textBox1.Text = getstring(rsakey, '#', 0);
            textBox2.Text = getstring(rsakey, '#', 1);
            rsakey = "";
            rjButton4.Text = "Generate RSA Key";
            rjButton4.Enabled = true;
            label1.Text = "Public Key";
            label2.Text = "Private Key";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainform.closedrsakgen = 1;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            keysize = int.Parse(comboBox2.Text);
        }
    }
}