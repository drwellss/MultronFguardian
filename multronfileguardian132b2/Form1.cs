using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;
using System.Security.Policy;
namespace multronfileguardian
{
    public partial class Form1 : Form
    {
        short encryptstatus = 0;
        public Form3 formuc;
        Form2 rsakeygen;
        Form4 holdkey;
        public short closedholdkey = 1;
        public short closedrsakgen = 1;
        int keysize = 2048;
        public short rtoperation = 1;
        public short rttoperation = 0;
        string mfgsfolder = Application.StartupPath + "\\mfgsettings";
        string mfgalg = Application.StartupPath + "\\mfgsettings" + "\\algorithm.txt";
        string mfgalgr = Application.StartupPath + "\\mfgsettings" + "\\algorithmr.txt";
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
            Control.CheckForIllegalCrossThreadCalls = false;
            getsettings();
            if (formuc.savealgorithm1.Checked == false)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
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
               if (formuc.savealgorithm1.Checked == true)
                {
                    if (System.IO.File.Exists(mfgalg) == true)
                    {
                        string algorithm = System.IO.File.ReadAllText(mfgalg);
                        short ty = 0;
                        foreach (string algg in comboBox1.Items)
                        {
                            if (algg == algorithm)
                            {
                                comboBox1.Text = algorithm;
                                ty = 1;
                                break;
                            }
                        }
                        if (ty == 0)
                        {
                            comboBox1.Text = "AES256";
                        }
                    }
                    else
                    {
                        comboBox1.Text = "AES256";
                    }
                    if (System.IO.File.Exists(mfgalgr) == true)
                    {
                        string algorithm = System.IO.File.ReadAllText(mfgalgr);
                        short ty = 0;
                        foreach (string algg in comboBox2.Items)
                        {
                            if (algg == algorithm)
                            {
                                comboBox2.Text = algorithm;
                                ty = 1;
                                break;
                            }
                        }
                        if (ty == 0)
                        {
                            comboBox2.Text = "2048";
                        }
                    }
                    else
                    {
                        comboBox2.Text = "2048";
                    }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(mfgsfolder);
            }
            formuc.Hide();
            formuc.swillbesaved = 0;
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
            if (formuc.savealgorithm1.Checked == true)
            {
                System.IO.File.WriteAllText(mfgalg, comboBox1.Text);
                System.IO.File.WriteAllText(mfgalgr, comboBox2.Text);
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
                if (encryptstatus == 1)
                {
                    aeskey = raes.generaterandomaeskey(67);
                    rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
                    rijndael.IV = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(aeskey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    aeskey = raes.rsaencrypt(aeskey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    if (encryptstatus == 1)
                    {
                        okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                        yazar = new System.IO.BinaryWriter(System.IO.File.Open(dosyalar + ".mfg", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                        yazar.Write(aeskey, 0, keysize / 8);
                        tutar = new System.IO.MemoryStream();
                        sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                        byte[] neww = new byte[1000000];
                        long filesize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < filesize)
                        {
                            long ig = filesize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)filesize) * 100), 0).ToString();
                            if (ig > 1000000)
                            {
                                neww = new byte[1000000];
                                okur.Read(neww, 0, 1000000);
                                sifreler.Write(neww, 0, 1000000);
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
                        aeskey = new byte[keysize / 8];
                        okur.Read(aeskey, 0, keysize / 8);
                        aeskey = raes.rsadecrypt(aeskey, key, keysize);
                        rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
                        rijndael.IV = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(aeskey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                        byte[] neww = new byte[1000000];
                        long filesize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < filesize)
                        {
                            long ig = filesize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)filesize) * 100), 0).ToString();
                           
                            if (ig > 1000000)
                            {
                                neww = new byte[1000000];
                                okur.Read(neww, 0, 1000000);
                                sifreler.Write(neww, 0, 1000000);
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
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }

        public void tfishrsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()), new Pkcs7Padding());
            try
            {
                byte[] serpentkey = { 0 };
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomaeskey(67);
                    sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(serpentkey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                    serpentkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serpentkey, 0, keysize / 8);
                        byte[] bxynew = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whereewearee = fsize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whereewearee > 1000000)
                            {
                                bxynew = new byte[1000000];
                                okur.Read(bxynew, 0, 1000000);
                                byte[] encrypted = sifreler.ProcessBytes(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bxynew = new byte[(int)whereewearee];
                                okur.Read(bxynew, 0, (int)whereewearee);
                                byte[] encrypted = sifreler.DoFinal(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                    else
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        serpentkey = new byte[keysize / 8];
                        okur.Read(serpentkey, 0, keysize / 8);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(dsk), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                        dsk = new byte[] { 0 };
                        byte[] bynetx = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whweare = fsize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whweare > 1000000)
                            {
                                bynetx = new byte[1000000];
                                okur.Read(bynetx, 0, 1000000);
                                byte[] n = sifreler.ProcessBytes(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bynetx = new byte[(int)whweare];
                                okur.Read(bynetx, 0, (int)whweare);
                                byte[] n = sifreler.DoFinal(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void serpentrsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SerpentEngine()), new Pkcs7Padding());
            try
            {
                byte[] serpentkey = {0};
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomaeskey(67);
                    sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(serpentkey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                    serpentkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach(string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serpentkey, 0, keysize / 8);
                        byte[] bxynew = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whereewearee = fsize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whereewearee > 1000000)
                            {
                                bxynew = new byte[1000000];
                                okur.Read(bxynew, 0, 1000000);
                                byte[] encrypted = sifreler.ProcessBytes(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bxynew = new byte[(int)whereewearee];
                                okur.Read(bxynew, 0, (int)whereewearee);
                                byte[] encrypted = sifreler.DoFinal(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                    else
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        serpentkey = new byte[keysize / 8];
                        okur.Read(serpentkey, 0, keysize / 8);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(dsk), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                        dsk = new byte[] { 0 };
                        byte[] bynetx = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whweare = fsize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whweare > 1000000)
                            {
                                bynetx = new byte[1000000];
                                okur.Read(bynetx, 0, 1000000);
                                byte[] n = sifreler.ProcessBytes(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bynetx = new byte[(int)whweare];
                                okur.Read(bynetx, 0, (int)whweare);
                                byte[] n = sifreler.DoFinal(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                }
            } catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void camelliarsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new CamelliaEngine()), new Pkcs7Padding());
            try
            {
                byte[] serpentkey = { 0 };
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomaeskey(67);
                    sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(serpentkey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                    serpentkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serpentkey, 0, keysize / 8);
                        byte[] bxynew = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whereewearee = fsize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whereewearee > 1000000)
                            {
                                bxynew = new byte[1000000];
                                okur.Read(bxynew, 0, 1000000);
                                byte[] encrypted = sifreler.ProcessBytes(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bxynew = new byte[(int)whereewearee];
                                okur.Read(bxynew, 0, (int)whereewearee);
                                byte[] encrypted = sifreler.DoFinal(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                    else
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        serpentkey = new byte[keysize / 8];
                        okur.Read(serpentkey, 0, keysize / 8);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(dsk), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16)));
                        dsk = new byte[] { 0 };
                        byte[] bynetx = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whweare = fsize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whweare > 1000000)
                            {
                                bynetx = new byte[1000000];
                                okur.Read(bynetx, 0, 1000000);
                                byte[] n = sifreler.ProcessBytes(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bynetx = new byte[(int)whweare];
                                okur.Read(bynetx, 0, (int)whweare);
                                byte[] n = sifreler.DoFinal(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void twofishprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            byte[] ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversestring(key), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    byte[] neww = new byte[1000000];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        if (whereweare > 1000000)
                        {
                            neww = new byte[1000000];
                            okur.Read(neww, 0, 1000000);
                            byte[] encrypted = sifreler.ProcessBytes(neww);
                            yazar.Write(encrypted, 0, encrypted.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            neww = new byte[(int)whereweare];
                            okur.Read(neww, 0, (int)whereweare);
                            byte[] encryptted = sifreler.DoFinal(neww);
                            yazar.Write(encryptted, 0, encryptted.Length);
                            yazar.Flush();
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
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }

            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void camelliaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new CamelliaEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            byte[] ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversestring(key), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    byte[] neww = new byte[1000000];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        if (whereweare > 1000000)
                        {
                            neww = new byte[1000000];
                            okur.Read(neww, 0, 1000000);
                            byte[] encrypted = sifreler.ProcessBytes(neww);
                            yazar.Write(encrypted, 0, encrypted.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            neww = new byte[(int)whereweare];
                            okur.Read(neww, 0, (int)whereweare);
                            byte[] encryptted = sifreler.DoFinal(neww);
                            yazar.Write(encryptted, 0, encryptted.Length);
                            yazar.Flush();
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
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }

            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void thfishrsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new ThreefishEngine(256)), new Pkcs7Padding());
            try
            {
                byte[] serpentkey = { 0 };
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomaeskey(67);
                    sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(serpentkey), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)));
                    serpentkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serpentkey, 0, keysize / 8);
                        byte[] bxynew = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whereewearee = fsize - okur.BaseStream.Position;
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whereewearee > 1000000)
                            {
                                bxynew = new byte[1000000];
                                okur.Read(bxynew, 0, 1000000);
                                byte[] encrypted = sifreler.ProcessBytes(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bxynew = new byte[(int)whereewearee];
                                okur.Read(bxynew, 0, (int)whereewearee);
                                byte[] encrypted = sifreler.DoFinal(bxynew);
                                yazar.Write(encrypted, 0, encrypted.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                    else
                    {
                        nstate = 1;
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        serpentkey = new byte[keysize / 8];
                        okur.Read(serpentkey, 0, keysize / 8);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(dsk), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)));
                        dsk = new byte[] { 0 };
                        byte[] bynetx = new byte[1000000];
                        long fsize = new System.IO.FileInfo(dosyalar).Length;
                        string filenamew = new System.IO.FileInfo(dosyalar).Name;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long whweare = fsize - okur.BaseStream.Position;
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                            if (whweare > 1000000)
                            {
                                bynetx = new byte[1000000];
                                okur.Read(bynetx, 0, 1000000);
                                byte[] n = sifreler.ProcessBytes(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
                            }
                            else
                            {
                                bynetx = new byte[(int)whweare];
                                okur.Read(bynetx, 0, (int)whweare);
                                byte[] n = sifreler.DoFinal(bynetx);
                                yazar.Write(n, 0, n.Length);
                                yazar.Flush();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(dosyalar);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }
            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void serpentprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SerpentEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            byte[] ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversestring(key), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    byte[] neww = new byte[1000000];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        if (whereweare > 1000000)
                        {
                            neww = new byte[1000000];
                            okur.Read(neww, 0, 1000000);
                            byte[] encrypted = sifreler.ProcessBytes(neww);
                            yazar.Write(encrypted,0, encrypted.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            neww = new byte[(int)whereweare];
                            okur.Read(neww, 0, (int)whereweare);
                            byte[] encryptted = sifreler.DoFinal(neww);
                            yazar.Write(encryptted, 0, encryptted.Length);
                            yazar.Flush();
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
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    
                }
                catch (Exception k)
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
                }
            
        }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            comboBox1.Enabled = true;
            textBox1.Enabled = true;
            label4.Text = "";
        }
        public void thfishprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new ThreefishEngine(256)), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), 125, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            byte[] ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversestring(key), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                    }
                    byte[] neww = new byte[1000000];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        if (whereweare > 1000000)
                        {
                            neww = new byte[1000000];
                            okur.Read(neww, 0, 1000000);
                            byte[] encrypted = sifreler.ProcessBytes(neww);
                            yazar.Write(encrypted, 0, encrypted.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            neww = new byte[(int)whereweare];
                            okur.Read(neww, 0, (int)whereweare);
                            byte[] encryptted = sifreler.DoFinal(neww);
                            yazar.Write(encryptted, 0, encryptted.Length);
                            yazar.Flush();
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
                    if (formuc.dfafterencrypted1.Checked == true)
                    {
                        System.IO.File.Delete(dosyalar);
                    }
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + ex.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                }
                catch (Exception k)
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
                }

            }
            if (nstate == 1)
            {
                MessageBox.Show("Operation Done!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
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
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(dosyalar + ".mfg", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                    }
                    else
                    {
                        sifreler = new System.Security.Cryptography.CryptoStream(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write), rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                    }
                    byte[] bxytes = new byte[1000000];
                    long fsize = new System.IO.FileInfo(dosyalar).Length;
                    string filenamew = new System.IO.FileInfo(dosyalar).Name;
                    while (okur.BaseStream.Position < fsize)
                    {
                        if (encryptstatus == 1)
                        {
                            label4.Text = "Encrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        else
                        {
                            label4.Text = "Decrypting: " + filenamew + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                        }
                        long whereweare = fsize - okur.BaseStream.Position;
                        if (whereweare > 1000000)
                        {
                            bxytes = new byte[1000000];
                            okur.Read(bxytes, 0, 1000000);
                            sifreler.Write(bxytes, 0, 1000000);
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
                if (formuc.cmpression1.Checked == false || encryptstatus == 1)
                {
                    listBox1.Items.Clear();
                }
                if (formuc.cmpression1.Checked == true && encryptstatus == 2)
                {
                    if (panel3.InvokeRequired)
                    {
                        panel3.Invoke(new MethodInvoker(() => panel3.Visible = true));
                    }
                    else
                    {
                        panel3.Visible = true;
                    }
                }
            }
            rjButton2.Enabled = true;
            rjButton3.Enabled = true;
            rjButton1.Enabled = true;
            rjButton5.Enabled = true;
            rjButton4.Enabled = true;
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
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
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "AES256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                        newwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Serpent256")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newwwthread = new System.Threading.Thread(() => serpentprocesses(textBox1.Text));
                        newwwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Serpent256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newwwwwthread = new System.Threading.Thread(() => serpentrsaprocesses(textBox1.Text));
                        newwwwwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Twofish256")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newbwthread = new System.Threading.Thread(() => twofishprocesses(textBox1.Text));
                        newbwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Camellia256")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newbwcthread = new System.Threading.Thread(() => camelliaprocesses(textBox1.Text));
                        newbwcthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Camellia256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newbwcrthread = new System.Threading.Thread(() => camelliarsaprocesses(textBox1.Text));
                        newbwcrthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Twofish256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newbwtthread = new System.Threading.Thread(() => tfishrsaprocesses(textBox1.Text));
                        newbwtthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "ThreeFish256")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread newbwtcthread = new System.Threading.Thread(() => thfishprocesses(textBox1.Text));
                        newbwtcthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "ThreeFish256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 2;
                        System.Threading.Thread nettwbwcrthread = new System.Threading.Thread(() => thfishrsaprocesses(textBox1.Text));
                        nettwbwcrthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
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
                if (formuc.cmpression1.Checked == true)
                {
                    if (rttoperation == 0)
                    {
                        rtoperation = 0;
                        encryptstatus = 1;
                        panel3.Visible = true;
                    }
                    else
                    {
                        rttoperation = 0;
                    }
                }
                if (rtoperation == 1)
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
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "AES256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newwthread = new System.Threading.Thread(() => aesrsaprocesses(textBox1.Text));
                        newwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Serpent256")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newwwthread = new System.Threading.Thread(() => serpentprocesses(textBox1.Text));
                        newwwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Serpent256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newwwwwthread = new System.Threading.Thread(() => serpentrsaprocesses(textBox1.Text));
                        newwwwwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Twofish256")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newbwthread = new System.Threading.Thread(() => twofishprocesses(textBox1.Text));
                        newbwthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Camellia256")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newbwcthread = new System.Threading.Thread(() => camelliaprocesses(textBox1.Text));
                        newbwcthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Camellia256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newbwcrthread = new System.Threading.Thread(() => camelliarsaprocesses(textBox1.Text));
                        newbwcrthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "ThreeFish256")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newbwtcthread = new System.Threading.Thread(() => thfishprocesses(textBox1.Text));
                        newbwtcthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "ThreeFish256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread nettwbwcrthread = new System.Threading.Thread(() => thfishrsaprocesses(textBox1.Text));
                        nettwbwcrthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "Twofish256-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread newbwtthread = new System.Threading.Thread(() => tfishrsaprocesses(textBox1.Text));
                        newbwtthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (formuc.savealgorithm1.Checked == true)
            {
                formuc.swillbesaved = 1;
            }
            if (comboBox1.Text.Contains("RSA"))
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These algorithms are encryption algorithms, Your choices, If you have no idea, use AES or AES-RSA (In case you will use hybrid algorithm)", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (closedholdkey == 1)
            {
                holdkey = new Form4(this);
                holdkey.Show();
                closedholdkey = 0;
            }
            else
            {
                holdkey.Show();
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (formuc.savealgorithm1.Checked == true)
            {
                formuc.swillbesaved = 1;
            }
            keysize = int.Parse(comboBox2.Text);
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                listBox1.Items.AddRange((string[])e.Data.GetData(DataFormats.FileDrop, false));
            }
        }
        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These are RSA Key Lengths, RSA's security increasing as its key size increased, but it also get slower, If you've no idea what to close, 2048 Key Length is best at balance of Security and Speed. 16384 Key Length is the slowest key length", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void rjButton10_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void rjButton12_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            textBox2.Text = "";
            listBox1.Items.Clear();
            rtoperation = 1;
            rttoperation = 0;
        }
        public void compressionoperationwithzip()
        {
            short nstate = 0;
            ZipArchive zibb = null;
            Stream sbn = null;
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            try
            {
                if (encryptstatus == 1)
                {
                    string oputzipf = textBox2.Text + "\\" + raes.getrandomstring(10) + ".zip";
                    zibb = new ZipArchive(File.Open(oputzipf, FileMode.OpenOrCreate), ZipArchiveMode.Create, false);
                    foreach (string fnam in listBox1.Items)
                    {
                        nstate = 1;
                        string entry = new FileInfo(fnam).Name;
                        sbn = zibb.CreateEntry(entry).Open();
                        okur = new BinaryReader(File.Open(fnam, FileMode.Open));
                        byte[] bytessx = new byte[1000000];
                        long fsize = new FileInfo(fnam).Length;
                        while (okur.BaseStream.Position < fsize)
                        {
                            long wherweare = fsize - okur.BaseStream.Position;
                            label4.Text = "Compressing: " + entry + " | %" + Math.Round((((double)okur.BaseStream.Position / (double)fsize) * 100), 0).ToString();

                            if (wherweare > 1000000)
                            {
                                bytessx = new byte[1000000];
                                okur.Read(bytessx, 0, 1000000);
                                sbn.Write(bytessx, 0, 1000000);
                                sbn.Flush();
                            }
                            else
                            {
                                bytessx = new byte[(int)wherweare];
                                okur.Read(bytessx, 0, (int)wherweare);
                                sbn.Write(bytessx, 0, (int)wherweare);
                                sbn.Flush();
                            }
                        }
                        sbn.Close();
                        okur.Close();
                    }
                    zibb.Dispose();
                    listBox1.Items.Clear();
                    listBox1.Items.Add(oputzipf);
                }
                else
                {
                    foreach (string filepath in listBox1.Items)
                    {
                        nstate = 1;
                        string fph = Path.GetDirectoryName(filepath) + "\\" + Path.GetFileNameWithoutExtension(filepath);
                        zibb = new ZipArchive(File.Open(fph, FileMode.Open), ZipArchiveMode.Read, false);
                        string path_file = textBox2.Text + "\\";
                        foreach (ZipArchiveEntry zae in zibb.Entries)
                        {
                            sbn = zae.Open();
                            string filea = path_file + zae.Name;
                            yazar = new BinaryWriter(File.Open(filea, FileMode.OpenOrCreate));
                            byte[] btyx = new byte[1000000];
                            long fsize = zae.Length;
                            while (yazar.BaseStream.Position < fsize)
                            {
                                long wherewere = fsize - yazar.BaseStream.Position;
                                label4.Text = "Decompressing: " + zae.Name + " | %" + Math.Round((((double)yazar.BaseStream.Position / (double)fsize) * 100), 0).ToString();
                                if (wherewere > 1000000)
                                {
                                    btyx = new byte[1000000];
                                    sbn.Read(btyx, 0, 1000000);
                                    yazar.Write(btyx, 0, 1000000);
                                    yazar.Flush();
                                }
                                else
                                {
                                    btyx = new byte[(int)wherewere];
                                    sbn.Read(btyx, 0, (int)wherewere);
                                    yazar.Write(btyx, 0, (int)wherewere);
                                    yazar.Flush();

                                }
                            }
                            yazar.Close();
                            sbn.Close();

                        }
                        zibb.Dispose();
                    }

                    listBox1.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                nstate = 0;
                MessageBox.Show("An error occurred in compression/decompression! Error = " + ex.Message + ex.StackTrace, "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rjButton2.Enabled = true;
                rjButton3.Enabled = true;
                rjButton1.Enabled = true;
                rjButton5.Enabled = true;
                textBox1.Enabled = true;
                rjButton4.Enabled = true;
                if (comboBox1.Text.Contains("RSA"))
                {
                    comboBox2.Enabled = true;
                }
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                label4.Text = "";
                rtoperation = 1;
                rttoperation = 0;
            }
            finally
            {
                if (zibb != null)
                {
                    zibb.Dispose();
                }
                if (sbn != null)
                {
                    sbn = null;
                }
                if (okur != null)
                {
                    okur.Dispose();
                }
                if (yazar != null)
                {
                    yazar.Dispose();
                }
            }
            if (nstate == 1) {
                rjButton2.Enabled = true;
                rjButton3.Enabled = true;
                rjButton1.Enabled = true;
                rjButton5.Enabled = true;
                textBox1.Enabled = true;
                rjButton4.Enabled = true;
                if (comboBox1.Text.Contains("RSA"))
                {
                    comboBox2.Enabled = true;
                }
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                label4.Text = "";
                rtoperation = 1;
                rttoperation = 1;
                if (encryptstatus == 1)
                {
                    rjButton2.PerformClick();
                }
            }
            textBox2.Text = "";
        }
        private void rjButton11_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox2.Text) == true)
            {
                Thread xthread = new Thread(() => compressionoperationwithzip());
                xthread.Start();
                panel3.Visible = false;
                rjButton2.Enabled = false;
                textBox1.Enabled = false;
                rjButton3.Enabled = false;
                rjButton1.Enabled = false;
                rjButton5.Enabled = false;
                rjButton4.Enabled = false;
                comboBox2.Enabled = false;
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("Cant find the path!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


