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
    public partial class Form3 : Form
    {

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

        Form1 form;
        public short swillbesaved = 0;
        public short firststart = 0;
        string mfgalg = Application.StartupPath + "\\mfgsettings" + "\\algorithm.txt";
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

        public void showpassword1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            form.textBox1.PasswordChar = '\0';
            if (form.formbes != null)
            {
                form.formbes.textBox1.PasswordChar = '\0';
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void showpassword0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            form.textBox1.PasswordChar = '*';
            if (form.formbes != null)
            {
                form.formbes.textBox1.PasswordChar = '*';
            }
        }

        private void dfafterencrypted0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void dfafterencrypted1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void savealgorithm0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            if (System.IO.File.Exists(mfgalg))
            {
                System.IO.File.Delete(mfgalg);
            }
        }

        private void savealgorithm1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void ckh0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void ckh1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void cmpression0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
        }

        private void cmpression1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            form.rtoperation = 1;
            form.rttoperation = 0;
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string[] regkeys = { ".mfg", "mfgg\\shell\\open\\command", ".mfg\\DefaultIcon", "mfgg\\shell\\open", "mfgg\\DefaultIcon" };
                IWshRuntimeLibrary.WshShortcut scut = new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\Encrypt With Multron File Guardian.lnk");
                scut.TargetPath = Application.ExecutablePath;
                scut.IconLocation = Application.ExecutablePath;
                scut.WorkingDirectory = Application.StartupPath;
                scut.Arguments = "-E";
                scut.Save();
                scut = new IWshRuntimeLibrary.WshShell().CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\Decrypt With Multron File Guardian.lnk");
                scut.TargetPath = Application.ExecutablePath;
                scut.IconLocation = Application.ExecutablePath ;
                scut.WorkingDirectory = Application.StartupPath;
                scut.Arguments = "-D";
                scut.Save();
                foreach (string key in regkeys) {
                    if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(key) == null)
                    {
                        Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(key).Close();
                    }
                }
                Microsoft.Win32.RegistryKey mfgkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkeys[2], true);
                mfgkey.SetValue(null, "\"" + Application.StartupPath + "\\pneeded\\iconmfg.ico" + "\"");
                mfgkey.Close();
                mfgkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkeys[0], true);
                mfgkey.SetValue(null, "mfgg");
                mfgkey.Close();
                mfgkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkeys[3], true);
                mfgkey.SetValue(null, "Open");
                mfgkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkeys[4], true);
                mfgkey.SetValue(null, "\"" + Application.StartupPath + "\\pneeded\\iconmfg.ico" + "\"");
                mfgkey.Close();
                mfgkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkeys[1], true);
                mfgkey.SetValue(null, "\"" + Application.ExecutablePath + "\"" + " -D " + "\"" + "%1" + "\"");
                mfgkey.Close();
                MessageBox.Show("Succesfully Added or Updated! You can find shortcut from Right-Click SendTo. To make changes applied, restart explorer.exe", "Multron File Guardian", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            try
            {
                short nstate = 0;
                string[] files = { Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\Encrypt With Multron File Guardian.lnk", Environment.GetFolderPath(Environment.SpecialFolder.SendTo) + "\\Decrypt With Multron File Guardian.lnk" };
                foreach (string file in files)
                {
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                        nstate = 1;
                    }
                }
                string[] regkeys = { ".mfg", "mfgg" };
                foreach (string regkey in regkeys)
                {
                    if (Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(regkey) != null)
                    {
                        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(regkey);
                        nstate = 1;
                    }
                }
                if (nstate == 1)
                {
                    MessageBox.Show("Succesfully Removed! To make changes applied, restart explorer.exe", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Program isn't added to Windows Explorer. Files not found", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace, "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void theme1_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            if (firststart == 1)
            {
                if (MessageBox.Show("This change need restart, Do you want to restart Multron File Guardian now?", "Multron File Guardian", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("cmd.exe", "/c timeout /t 2 /nobreak & start \"\" " + "\"" + Application.ExecutablePath + "\"");
                    Application.Exit();
                }
            }
        }
        public void sdarktheme()
        {
            panel2.BackColor = Color.FromArgb(40, 40, 40);
            panel1.BackColor = Color.FromArgb(23, 23, 23);
            rjButton8.BackColor = Color.LightGray;
            foreach (Control yazi in panel1.Controls)
            {

                if (yazi.Name.Contains("groupBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                    foreach (Control btnnn in yazi.Controls)
                    {
                        if (btnnn.Name.Contains("linkLabel"))
                        {
                            LinkLabel ll = (LinkLabel)btnnn;
                            ll.LinkColor  = Color.WhiteSmoke;
                        }
                        if (btnnn.Name.Contains("textBox"))
                        {
                            btnnn.BackColor = Color.FromArgb(30, 30, 30);
                            btnnn.ForeColor = Color.WhiteSmoke;
                        }
                        if (btnnn.Name.Contains("rjButton"))
                        {
                            btnnn.BackColor = Color.FromArgb(43, 40, 40);
                            btnnn.ForeColor = Color.WhiteSmoke;

                        }
                    }
                }
            }
        }
        private void theme0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            if (firststart == 1)
            {
                if (MessageBox.Show("This change need restart, Do you want to restart Multron File Guardian now?", "Multron File Guardian", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("cmd.exe", "/c timeout /t 2 /nobreak & start \"\" " + "\"" + Application.ExecutablePath + "\"");
                    Application.Exit();
                }
            }
        }

        private void iterationaut0_Click(object sender, EventArgs e)
        {
            swillbesaved = 1;
            textBox1.Enabled = false;
            rjButton3.Enabled = false;
            form.iterationrate = 217685;
        }

        private void iterationaut1_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            rjButton3.Enabled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Textbox shouldn't be empty", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox1.Text[0] != '0')
                {
                    swillbesaved = 1;
                    form.iterationrate = int.Parse(textBox1.Text);
                    MessageBox.Show("Password iteration rate changed.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                else
                {
                    MessageBox.Show("Number shouldn't start with 0", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This settings let you change how many time your password will be hashed, if you haven't any information about this, dont touch. Recommended iteration rate is between 200000 and 1500000. Default iteration rate is 217685. \n\nCAUTION! = If you encrypted files with older version of Multron File Guardian, decrypt your files with 125 iteration rate otherwise you wont decrypt properly.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This settings let you add program to right-click menu and make program associate with .mfg extension. (This feature need program to must stay in same path to work.)", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
