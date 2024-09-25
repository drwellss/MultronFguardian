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
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            Control.CheckForIllegalCrossThreadCalls = false;
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
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
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
            button1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.Text == "AES256")
                {
                    encryptstatus = 1;
                    System.Threading.Thread newthread = new System.Threading.Thread(() => aesprocesses(textBox1.Text));
                    newthread.Start();
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    encryptstatus = 3;
                    System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                    newwthread.Start();
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("You need to enter a key.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(openFileDialog1.FileNames);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.Text == "AES256")
                {
                    encryptstatus = 2;
                    System.Threading.Thread newthread = new System.Threading.Thread(() => aesprocesses(textBox1.Text));
                    newthread.Start();
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else
                {
                    encryptstatus = 4;
                    System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                    newwthread.Start();
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("You need to enter a key.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
