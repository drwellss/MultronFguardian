using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace multronfileguardian
{
    public partial class Form6 : Form
    {
        short keysize = 256;
        Form1 formbir;
        string filepathh = "";
        public Form6(Form1 formb)
        {
            InitializeComponent();
            formbir = formb;
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
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
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

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {

        }
        public void darktheme()
        {
            panel2.BackColor = Color.FromArgb(40, 40, 40);
            rjButton8.BackColor = panel2.BackColor;

            this.BackColor = Color.FromArgb(23, 23, 23);
            foreach (Control yazi in this.Controls)
            {
                    if (yazi.Name.Contains("linkLabel"))
                    {
                        LinkLabel ll = (LinkLabel)yazi;
                        ll.LinkColor = Color.WhiteSmoke;
                    }
                    if (yazi.Name.Contains("textBox"))
                    {
                        yazi.BackColor = Color.FromArgb(30, 30, 30);
                        yazi.ForeColor = Color.WhiteSmoke;
                    }
                    if (yazi.Name.Contains("rjButton"))
                    {
                        yazi.BackColor = Color.FromArgb(43, 40, 40);
                        yazi.ForeColor = Color.WhiteSmoke;

                    }
                    if (yazi.Name.Contains("label"))
                    {
                        yazi.ForeColor = Color.WhiteSmoke;
                    }
                if (yazi.Name.Contains("checkBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("groupBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                    foreach (Control btnnn in yazi.Controls)
                    {
                        if (btnnn.Name.Contains("comboBox"))
                        {
                            ComboBox cbox = (ComboBox)btnnn;
                            cbox.FlatStyle = FlatStyle.Popup;
                            cbox.BackColor = Color.FromArgb(30, 30, 30);
                            cbox.ForeColor = Color.WhiteSmoke;
                        }
                        if (btnnn.Name.Contains("linkLabel"))
                        {
                            LinkLabel ll = (LinkLabel)btnnn;
                            ll.LinkColor = Color.WhiteSmoke;
                        }
                        if (btnnn.Name.Contains("textBox"))
                        {
                            btnnn.BackColor = Color.FromArgb(30, 30, 30);
                            btnnn.ForeColor = Color.WhiteSmoke;
                        }
                        if (btnnn.Name.Contains("label"))
                        {
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
        private void Form6_Load(object sender, EventArgs e)
        {
            if (formbir.formuc.theme1.Checked == true)
            {
                System.Threading.Thread threddd = new System.Threading.Thread(() => darktheme());
                threddd.Start();
            }

            comboBox1.SelectedIndex = 0;
            textBox2.PasswordChar = formbir.textBox1.PasswordChar;
            checkBox2.Checked = formbir.checkBox1.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These algorithms are encryption algorithms, Your choices, If you have no idea, use AES.", "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This option offer you select symmetric-algorithm key size, If you have no idea, Select biggest key length.", "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string serpentoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            PaddedBufferedBlockCipher sencryptor = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SerpentEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[16];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            bool cstatus = false;
            try
            {
                if (encryptstatus == 2)
                {
                    cstatus = false;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, 16);
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            byte[] decryptdata = sencryptor.ProcessBytes(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            byte[] decryptdata = sencryptor.DoFinal(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
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
                    okur.Close();
                    yazar.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    cstatus = true;
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            byte[] encryptddata = sencryptor.ProcessBytes(bytr);
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = sencryptor.DoFinal(bytr);
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
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
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }

                    yazar.Close();
                    okur.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
        }

        private string twofishoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            PaddedBufferedBlockCipher sencryptor = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[16];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            bool cstatus = false;
            try
            {
                if (encryptstatus == 2)
                {
                    cstatus = false;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, 16);
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            byte[] decryptdata = sencryptor.ProcessBytes(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            byte[] decryptdata = sencryptor.DoFinal(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
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
                    okur.Close();
                    yazar.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    cstatus = true;
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            byte[] encryptddata = sencryptor.ProcessBytes(bytr);
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = sencryptor.DoFinal(bytr);
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
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
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }

                    yazar.Close();
                    okur.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
        }
        private string camelliaoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            PaddedBufferedBlockCipher sencryptor = new PaddedBufferedBlockCipher(new CbcBlockCipher(new CamelliaEngine()), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[16];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            bool cstatus = false;
            try
            {
                if (encryptstatus == 2)
                {
                    cstatus = false;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, 16);
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            byte[] decryptdata = sencryptor.ProcessBytes(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            byte[] decryptdata = sencryptor.DoFinal(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
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
                    okur.Close();
                    yazar.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    cstatus = true;
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            byte[] encryptddata = sencryptor.ProcessBytes(bytr);
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = sencryptor.DoFinal(bytr);
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
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
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }

                    yazar.Close();
                    okur.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
        }
        private string thfishoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            PaddedBufferedBlockCipher sencryptor = new PaddedBufferedBlockCipher(new CbcBlockCipher(new ThreefishEngine(keysize)), new Pkcs7Padding());
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[keysize / 8];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            bool cstatus = false;
            try
            {
                if (encryptstatus == 2)
                {
                    cstatus = false;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, keysize / 8);
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            byte[] decryptdata = sencryptor.ProcessBytes(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            byte[] decryptdata = sencryptor.DoFinal(bytts);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
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
                    okur.Close();
                    yazar.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    cstatus = true;
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            byte[] encryptddata = sencryptor.ProcessBytes(bytr);
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = sencryptor.DoFinal(bytr);
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
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
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }

                    yazar.Close();
                    okur.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
        }
        private string chachaoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            ChaCha7539Engine sencryptor = new ChaCha7539Engine();
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[12];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            bool cstatus = false;
            try
            {
                if (encryptstatus == 2)
                {
                    cstatus = false;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, 12);
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            byte[] decryptdata = new byte[1000000];
                            sencryptor.ProcessBytes(bytts,0,1000000,decryptdata,0);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            byte[] decryptdata = new byte[(int) ig];
                            sencryptor.ProcessBytes(bytts, 0, (int)ig, decryptdata, 0);
                            yazar.Write(decryptdata, 0, decryptdata.Length);
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
                    okur.Close();
                    yazar.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    cstatus = true;
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(12);
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            byte[] encryptddata = new byte[1000000];
                            sencryptor.ProcessBytes(bytr,0,1000000,encryptddata,0);
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = new byte[(int)ig];
                            sencryptor.ProcessBytes(bytr,0,(int)ig, encryptddaata,0);
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
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
                    if (tutar != null)
                    {
                        tutar.Dispose();
                    }

                    yazar.Close();
                    okur.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
        }
        private string aesoperations(string pkey, string fpath, byte encryptstatus, byte[] textb = null)
        {
            AesCryptoServiceProvider rijndael = new AesCryptoServiceProvider();
            rijndael.KeySize = keysize;
            rijndael.BlockSize = 128;
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
            byte[] ivtg = new byte[16];
            System.IO.BinaryReader okur = null;
            System.IO.BinaryWriter yazar = null;
            System.IO.MemoryStream tutar = null;
            System.IO.MemoryStream tutrtwo = null;
            System.Security.Cryptography.CryptoStream sifreler = null;
            try
            {
                if (encryptstatus == 2)
                {
                    okur = new System.IO.BinaryReader(System.IO.File.Open(fpath, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    okur.Read(ivtg, 0, 16);
                    rijndael.IV = ivtg;
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
                    long fsize = new System.IO.FileInfo(fpath).Length;
                    while (okur.BaseStream.Position < fsize)
                    {
                        long ig = fsize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytts = new byte[1000000];
                            okur.Read(bytts, 0, 1000000);
                            sifreler.Write(bytts, 0, 1000000);
                            sifreler.Flush();
                        }
                        else
                        {
                            bytts = new byte[(int)ig];
                            okur.Read(bytts, 0, (int)ig);
                            sifreler.Write(bytts, 0, (int)ig);
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
                    okur.Close();
                    sifreler.Close();
                    return System.Text.Encoding.UTF8.GetString(tutar.ToArray());
                }
                else
                {
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                    rijndael.IV = ivtg;
                    tutrtwo = new System.IO.MemoryStream(textb);
                    okur = new System.IO.BinaryReader(tutrtwo);
                    string fffpath = "";
                    if (System.IO.Path.GetExtension(fpath).Contains("mng"))
                    {
                        fffpath = fpath;
                    }
                    else
                    {
                        fffpath = fpath + ".txt.mng";
                        filepathh = fffpath;
                    }
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                    yazar.Write(ivtg);
                    yazar.Flush();
                    tutar = new System.IO.MemoryStream();
                    sifreler = new CryptoStream(tutar, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
                    byte[] bytr = new byte[1000000];
                    long filesize = tutrtwo.Length;
                    while (okur.BaseStream.Position < filesize)
                    {
                        long ig = filesize - okur.BaseStream.Position;
                        if (ig > 1000000)
                        {
                            bytr = new byte[1000000];
                            okur.Read(bytr, 0, 1000000);
                            sifreler.Write(bytr, 0, 1000000);
                            sifreler.Flush();
                            yazar.Write(tutar.ToArray(), 0, (int)tutar.Length);
                            yazar.Flush();
                            tutar.SetLength(0);
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            sifreler.Write(bytr, 0, (int)ig);
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

                    yazar.Close();
                    okur.Close();
                    sifreler.Close();
                    tutar.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                filepathh = "";
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
            return "";
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
        private void rjButton3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (openFileDialog1.FileName.Contains(".mng"))
                    {
                        if (comboBox1.Text == "AES")
                        {
                            textBox1.Text = aesoperations(textBox2.Text, openFileDialog1.FileName,2);
                        }

                        if (comboBox1.Text == "Serpent")
                        {
                            textBox1.Text = serpentoperations(textBox2.Text, openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "Camellia")
                        {
                            textBox1.Text = camelliaoperations(textBox2.Text, openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "Twofish")
                        {
                            textBox1.Text = twofishoperations(textBox2.Text, openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "ThreeFish")
                        {
                            textBox1.Text = thfishoperations(textBox2.Text, openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "ChaCha20")
                        {
                            textBox1.Text = chachaoperations(textBox2.Text, openFileDialog1.FileName, 2);
                        }
                    }
                    else
                    {
                        textBox1.Text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                    }
                    filepathh = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Operation ended with a error, these may cause an error's occure: file may not be a text file, program haven't access to file, your password is wrong or something else, Error = " + ex.Message + "\n" + ex.StackTrace.ToString(), "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            byte checkfs = 0;
            if (filepathh != "")
            {
                if (System.IO.File.Exists(filepathh) == true)
                {
                    checkfs = 1;
                }
                else
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        filepathh = saveFileDialog1.FileName;
                        checkfs = 1;
                    }

                }
            }

            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filepathh = saveFileDialog1.FileName;
                    checkfs = 1;
                }
            }
                if (checkfs == 1)
                {
                    if (checkBox1.Checked == true)
                    {
                        if (textBox2.Text != "")
                        {
                            if (comboBox1.Text == "AES")
                            {
                                aesoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                            }
                            if (comboBox1.Text == "Serpent")
                            {
                                serpentoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                            }
                        if (comboBox1.Text == "Camellia")
                        {
                            camelliaoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                        if (comboBox1.Text == "Twofish")
                        {
                            twofishoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                        if (comboBox1.Text == "ThreeFish")
                        {
                            thfishoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                        if (comboBox1.Text == "ChaCha20")
                        {
                            chachaoperations(textBox2.Text, filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                    }
                        else
                        {
                            MessageBox.Show("You need to enter a key.", "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        if (filepathh.Contains(".txt"))
                        {

                        }
                        else
                        {
                            if (!System.IO.File.Exists(filepathh))
                            {
                                filepathh = filepathh + ".txt";
                            }
                            else
                            {
                                System.IO.File.Move(filepathh, new System.IO.FileInfo(filepathh).Directory.FullName + "\\"+ System.IO.Path.GetFileName(filepathh) + ".txt");
                                filepathh = filepathh + ".txt";
                            }

                        }
                            System.IO.File.WriteAllText(filepathh, textBox1.Text);
                        if (new System.IO.FileInfo(filepathh).Extension.Contains("mng") == true)
                        {
                            System.IO.File.Move(filepathh, new System.IO.FileInfo(filepathh).Directory.FullName + "\\" + System.IO.Path.GetFileNameWithoutExtension(filepathh));
                            filepathh = new System.IO.FileInfo(filepathh).Directory.FullName + "\\" + System.IO.Path.GetFileNameWithoutExtension(filepathh);
                        }

                    }
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

        private void rjButton8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void rjButton7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            formbir.closedmng = 1;
        }

        private void checkBox2_Click(object sender, EventArgs e)
        {

            byte equalopen = 0;
            if (formbir.formuc.Visible == true)
            {
                equalopen = 1;
            }
            if (equalopen == 0)
            {

                formbir.formuc.WindowState = FormWindowState.Minimized;
                formbir.formuc.Show();
            }
            if (checkBox2.Checked)
            {
                formbir.formuc.showpassword1.PerformClick();
            }
            else
            {
                formbir.formuc.showpassword0.PerformClick();

            }
            if (equalopen == 0)
            {
                formbir.formuc.Hide();
            }
            formbir.checkBox1.Checked = checkBox2.Checked;
        }

        private void label10_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Multron NoteGuard is a notepad program that has encrypt feature, you can easily open your encrypted note without writing to disk.", "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
