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
    public partial class Form1 : Form
    {
        short encryptstatus = 0;
        Form3 formuc;
        Form2 rsakeygen;
        public short closedrsakgen = 1;
        string mfgsfolder = Application.StartupPath + "\\mfgsettings";
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
            getsettings();
        }
        private void getsettings()
        {
            formuc = new Form3(this);
            formuc.Show();
            if (System.IO.Directory.Exists(mfgsfolder))
            {
               foreach(Control cntrlr in formuc.panel1.Controls)
                {
                    if (cntrlr.Name.Contains("groupBox") == true)
                    {
                        foreach(Control cntrltwo in cntrlr.Controls)
                        {
                            if (cntrltwo.GetType().ToString().Contains("RadioButton") == true)
                            {
                                string namez = new String(cntrltwo.Name.ToCharArray()).Substring(0, cntrltwo.Name.Length - 1);
                                if (System.IO.File.Exists(mfgsfolder + "\\"+ namez + ".txt") == true)
                                {
                                    string svalue = System.IO.File.ReadAllText(mfgsfolder + '\\' + namez + ".txt");
                                    if (svalue != "")
                                    {
                                        if (svalue[0] == cntrltwo.Name[cntrltwo.Name.Length - 1])
                                        {
                                            RadioButton rbutton = (RadioButton)cntrltwo;
                                            rbutton.PerformClick();
                                        }
                                    }
                                }
                                else
                                {
                                    System.IO.File.Create(mfgsfolder + "\\" + namez + ".txt").Close();
                                }
                            }
                        }
                    }
                }
                formuc.Hide();
                formuc.swillbesaved = 0;
            }
            else
            {
                System.IO.Directory.CreateDirectory(mfgsfolder);
            }
        }
        public void savesettings()
        {
            if (!System.IO.Directory.Exists(mfgsfolder))

            {
                System.IO.Directory.CreateDirectory(mfgsfolder);
            }
            foreach (Control cntrlr in formuc.panel1.Controls)
            {
                if (cntrlr.Name.Contains("groupBox") == true)
                {
                    foreach (Control cntrltwo in cntrlr.Controls)
                    {
                        if (cntrltwo.GetType().ToString().Contains("RadioButton") == true)
                        {
                            string namez = new String(cntrltwo.Name.ToCharArray()).Substring(0, cntrltwo.Name.Length - 1);
                            RadioButton rbutton = (RadioButton)cntrltwo;
                            if (System.IO.File.Exists(mfgsfolder + "\\"+ namez + ".txt") == false) 
                            {
                                System.IO.File.Create(mfgsfolder + "\\" + namez + ".txt").Close();
                            }
                            if (rbutton.Checked == true)
                            {
                                System.IO.File.WriteAllText(mfgsfolder + "\\" + namez + ".txt", cntrltwo.Name[cntrltwo.Name.Length - 1].ToString());
                            }
                        }
                    }
                }
            }
                        }
                        public void aesrsaprocesses(string key)
        {
            short nstate = 0;
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.Security.Cryptography.CryptoStream sifreler = null;
            try {
                System.Security.Cryptography.AesCryptoServiceProvider rijndael = new System.Security.Cryptography.AesCryptoServiceProvider();
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
                rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                byte[] aeskey = new byte[32];
                if (encryptstatus == 3)
                {
                    aeskey = raes.generaterandomaeskey(67);
                    rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
                    rijndael.IV = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(aeskey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    aeskey = raes.rsaencrypt(aeskey, key);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    if (encryptstatus == 3)
                    {
                        okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                        yazar = new System.IO.BinaryWriter(System.IO.File.Open(dosyalar + ".multronfguardian", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                        yazar.Write(aeskey, 0, 256);
                        tutar = new System.IO.MemoryStream();
                        sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                        byte[] neww = new byte[4096];
                        long filesize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < filesize)
                        {
                            long ig = filesize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)filesize) * 100), 0).ToString();
                            if (ig > 4096)
                            {
                                neww = new byte[4096];
                                okur.Read(neww, 0, 4096);
                                sifreler.Write(neww, 0, 4096);
                                sifreler.Flush();
                                yazar.Write(tutar.ToArray(), 0, (int)tutar.Length);
                                yazar.Flush();
                                tutar.SetLength(0);
                            }
                            else
                            {
                                neww = new byte[(int)ig];
                                okur.Read(neww, 0, (int)ig);
                                sifreler.Write(neww, 0, (int)ig);
                                sifreler.FlushFinalBlock();
                                yazar.Write(tutar.ToArray(), 0, (int)tutar.Length);
                                yazar.Flush();
                                tutar.SetLength(0);
                            }
                        }
                        if (okur != null)
                        {
                            okur.Dispose();
                        }
                        if (yazar != null)
                        {
                            yazar.Dispose();
                        }
                        if (sifreler != null)
                        {
                            sifreler.Dispose();
                        }
                        if (tutar != null)
                        {
                            tutar.Dispose();
                        }
                    }
                    else
                    {
                        okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                        aeskey = new byte[256];
                        okur.Read(aeskey, 0, 256);
                        aeskey = raes.rsadecrypt(aeskey, key);
                        rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
                        rijndael.IV = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(aeskey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                        byte[] neww = new byte[4096];
                        long filesize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < filesize)
                        {
                            long ig = filesize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)filesize) * 100), 0).ToString();
                           
                            if (ig > 4096)
                            {
                                neww = new byte[4096];
                                okur.Read(neww, 0, 4096);
                                sifreler.Write(neww, 0, 4096);
                                sifreler.Flush();
                            }
                            else
                            {
                                neww = new byte[(int)ig];
                                okur.Read(neww, 0, (int)ig);
                                sifreler.Write(neww, 0, (int)ig);
                                sifreler.FlushFinalBlock();
                            }
                        }
                        if (okur != null)
                        {
                            okur.Dispose();
                        }
                        if (yazar != null)
                        {
                            yazar.Dispose();
                        }
                        if (sifreler != null)
                        {
                            sifreler.Dispose();
                        }
                        if (tutar != null)
                        {
                            tutar.Dispose();
                        }
                    }
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception exc) {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + exc.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                        if (okur != null)
                        {
                            okur.Dispose();
                        }
                        if (yazar != null)
                        {
                            yazar.Dispose();
                        }
                        if (sifreler != null)
                        {
                            sifreler.Dispose();
                        }
                        if (tutar != null)
                        {
                            tutar.Dispose();
                        }
                    }
                catch(Exception k)
                {

                }
                finally
                {
                    if (okur != null)
                    {
                        okur.Dispose();
                    }
                    if (yazar != null)
                    {
                        yazar.Dispose();
                    }
                    if (sifreler != null)
                    {
                        sifreler.Dispose();
                    }
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Clear();
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void aesprocesses(string key)
        {
            short nstate = 0;
            System.Security.Cryptography.CryptoStream sifreler = null;
            System.IO.BinaryReader okur = null;
            try
            {
                System.Security.Cryptography.AesCryptoServiceProvider rijndael = new System.Security.Cryptography.AesCryptoServiceProvider();
                rijndael.KeySize = 256;
                rijndael.BlockSize = 128;
                rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
                rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
                rijndael.IV = new System.Security.Cryptography.Rfc2898DeriveBytes(reversestring(key), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    
                    if (encryptstatus == 1)
                    {
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(dosyalar + ".multronfguardian", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                    }
                    else
                    {
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                    }
                    byte[] bxytes = new byte[4096];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (whereweare > 4096)
                        {
                            bxytes = new byte[4096];
                            okur.Read(bxytes, 0, 4096);
                            sifreler.Write(bxytes, 0, 4096);
                            sifreler.Flush();
                        }
                        else
                        {
                            bxytes = new byte[(int)whereweare];
                            okur.Read(bxytes, 0, (int)whereweare);
                            sifreler.Write(bxytes, 0, (int)whereweare);
                            sifreler.FlushFinalBlock();
                        }
                    }
                    okur.Dispose();
                    sifreler.Dispose();
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception exc)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + exc.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    if (okur != null)
                    {
                        okur.Dispose();
                    }
                    if (sifreler != null)
                    {
                        sifreler.Dispose();
                    }
                }
                catch (Exception erf)
                {

                }
                finally
                {
                    if (okur != null)
                    {
                        okur.Dispose();
                    }
                    if (sifreler != null)
                    {
                        sifreler.Dispose();
                    }
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listBox1.Items.Clear();
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public string reversestring(string yazi)
        {
            string ters = "";
            for (int i = yazi.Length - 1; i >= 0;)
            {
                ters += yazi[i];
                --i;
            }
            return ters;
        }
        public byte[] reversebarray(byte[] yazi)
        {
            byte[] ters = new byte[yazi.Length];
            int x = 0;
            for (int i = yazi.Length - 1; i >= 0;)
            {
                ters[x] = yazi[i];
                --i;
                ++x;
            }
            return ters;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.Text == "AES256")
                {
                    encryptstatus = 2;
                    System.Threading.Thread newthread = new System.Threading.Thread(() => aesprocesses(textBox1.Text));
                    newthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    encryptstatus = 4;
                    System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                    newwthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("You need to enter a key.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.Text == "AES256")
                {
                    encryptstatus = 1;
                    System.Threading.Thread newthread = new System.Threading.Thread(() => aesprocesses(textBox1.Text));
                    newthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    encryptstatus = 3;
                    System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                    newwthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("You need to enter a key.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(openFileDialog1.FileNames);
            }
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: you didnot select any file, error = " + exc.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void rjButton6_Click(object sender, EventArgs e)
        {
            if (closedrsakgen == 1)
            {
                rsakeygen = new Form2(this);
                rsakeygen.Show();
                closedrsakgen = 0;
            }
            else
            {
                rsakeygen.Show();
            }
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void rjButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button ==  MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        
        private void rjButton9_Click(object sender, EventArgs e)
        {
            formuc.Show();            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formuc.swillbesaved == 1)
            {
                savesettings();
            }
        }
    }
}
