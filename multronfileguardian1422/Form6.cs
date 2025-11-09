using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace multronfileguardian
{
    public partial class Form6 : Form
    {
        short keysize = 256;
        Form1 formbir;
        string filepathh = "";
        object s = null;
        byte drawed = 0;
        EventArgs esa = null;
        HMACSHA512 hsha512 = null;
        public Form6(Form1 formb)
        {
            InitializeComponent();
            formbir = formb;
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private static bool UseImmersiveDarkMode(IntPtr handle)
        {
            int val = 20;
            int y = 1;
            return DwmSetWindowAttribute(handle, val, ref y, sizeof(int)) == 0;
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
            this.BackColor = Color.FromArgb(23, 23, 23);
            toolStrip1.BackColor = Color.FromArgb(30, 30, 30);
            UseImmersiveDarkMode(this.Handle);
            foreach (ToolStripItem cns in toolStrip1.Items)
            {
                if (cns.Name.Contains("Button"))
                {
                    cns.ForeColor = Color.WhiteSmoke;
                }
                if (cns.Name.Contains("DropDown"))
                {
                    ToolStripDropDownButton cnss = (ToolStripDropDownButton)cns;
                    foreach (ToolStripMenuItem tsmim in cnss.DropDownItems)
                    {
                        tsmim.BackColor = Color.FromArgb(30, 30, 30);
                        tsmim.ForeColor = Color.WhiteSmoke;
                        foreach(ToolStripMenuItem tsmimm in tsmim.DropDownItems)
                        {
                            tsmimm.BackColor = Color.FromArgb(30, 30, 30);
                            tsmimm.ForeColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
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
                if (yazi.Name.Contains("label"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("checkBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                }
            }
        
        private void Form6_Load(object sender, EventArgs e)
        {
            s = sender;
            esa = e;
            ToolStripMenuItem tsmiay = (ToolStripMenuItem)toolStripDropDownButton1.DropDownItems[0];
            foreach (ToolStripMenuItem toolsmi in tsmiay.DropDownItems)
            {
                toolsmi.Click += (s, esa) => { bclickerfnc(toolsmi); };
            }
            if (formbir.formuc.theme1.Checked == true)
            {
                System.Threading.Thread threddd = new System.Threading.Thread(() => darktheme());
                threddd.Start();
            }
            aESToolStripMenuItem0.PerformClick();
            textBox2.PasswordChar = formbir.textBox1.PasswordChar;
            checkBox2.Checked = formbir.checkBox1.Checked;
        }
        public void bclickerfnc(ToolStripMenuItem tsmi)
        {
            comboBox1.SelectedIndex = int.Parse(tsmi.Name[tsmi.Name.Length - 1].ToString());
            ToolStripMenuItem tsmiay = (ToolStripMenuItem)toolStripDropDownButton1.DropDownItems[0];
            foreach (ToolStripMenuItem tsmitemm in tsmiay.DropDownItems)
            {
                if (tsmitemm.Name != tsmi.Name)
                {
                    tsmitemm.Checked = false;
                }
                else
                {
                    tsmitemm.Checked = true;
                }
            }
            symmetricKeyLengthToolStripMenuItem.DropDownItems.Clear();
            byte turn = 0;
            foreach (string ss in comboBox3.Items)
            {
                ToolStripMenuItem tsmii = new ToolStripMenuItem();
                tsmii.CheckOnClick = true;
                tsmii.Name = raes.getrandomstring(20) + turn.ToString();
                tsmii.Text = ss;
                tsmii.Click += (s,esa) => skclickerfnc(tsmii);
                if (formbir.formuc.theme1.Checked == true)
                {
                    tsmii.BackColor = Color.FromArgb(30, 30, 30);
                    tsmii.ForeColor = Color.WhiteSmoke;
                }
                symmetricKeyLengthToolStripMenuItem.DropDownItems.Add(tsmii);
                ++turn;
                if (tsmii.Text == comboBox3.Text)
                {
                    tsmii.PerformClick();
                }
            }
        }
        public void skclickerfnc(ToolStripMenuItem tsmi)
        {
            ToolStripMenuItem tsmiay = (ToolStripMenuItem)toolStripDropDownButton1.DropDownItems[1];
            foreach (ToolStripMenuItem tsmitemm in tsmiay.DropDownItems)
            {
                if (tsmitemm.Name != tsmi.Name)
                {
                    tsmitemm.Checked = false;
                }
                else
                {
                    tsmitemm.Checked = true;
                }
            }
            comboBox3.SelectedIndex = int.Parse(tsmi.Name[tsmi.Name.Length - 1].ToString());
        }

        private string ciphersoperations(string pkey, IBlockCipher bc, string fpath, byte encryptstatus, byte[] textb = null, short blocksize = 16)
        {
            PaddedBufferedBlockCipher sencryptor = new PaddedBufferedBlockCipher(new CbcBlockCipher(bc), new Pkcs7Padding());
            byte[] keyt = new byte[keysize / 8];
            byte[] seasalt = new byte[32];
            byte[] ivtg = new byte[blocksize];
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
                    okur.Read(ivtg, 0,blocksize);
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 96;
                    }
                    else
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 32;
                    }
                    okur.Read(seasalt, 0, 32);
                    okur.BaseStream.Position = blocksize;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        keyt = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, blocksize, null, 0);
                        string afname = new FileInfo(fpath).Name;
                        long afsz = new FileInfo(fpath).Length - 96;
                        byte[] nbyers = new byte[1000000];
                        byte[] machash = null;
                        while (okur.BaseStream.Position < afsz)
                        {
                            long whwearee = afsz - okur.BaseStream.Position;
                            if (whwearee > 1000000)
                            {
                                nbyers = new byte[1000000];
                                okur.Read(nbyers, 0, 1000000);
                                hsha512.TransformBlock(nbyers, 0, 1000000, null, 0);
                            }
                            else
                            {
                                nbyers = new byte[(int)whwearee];
                                okur.Read(nbyers, 0, (int)whwearee);
                                hsha512.TransformFinalBlock(nbyers, 0, (int)whwearee);
                            }
                        }
                        machash = hsha512.Hash;
                        byte[] mhashinf = new byte[64];
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 64;
                        okur.Read(mhashinf, 0, 64);
                        short hcnt = 0;
                        byte notauthed = 0;
                        foreach (byte b in machash)
                        {
                            if (b == mhashinf[hcnt])
                            {

                            }
                            else
                            {
                                notauthed = 1;
                                break;
                            }
                            ++hcnt;
                        }
                        if (notauthed == 1)
                        {
                            okur.Close();
                            yazar.Close();
                            MessageBox.Show(afname + " Cannot be authenticated, Decryption process denied!" + "\n\n" + "Cause: Password may be wrong or file may compromised/corrupted", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            throw new Exception("Auth Failed");
                        }
                    }
                    else
                    {
                        keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
                    okur.BaseStream.Position = blocksize;
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length - 32;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        fsize = fsize - 64;
                    }
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
                    seasalt = raes.generaterandomkey(32);
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(blocksize);
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        keyt = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, blocksize, null, 0);
                    }
                    else
                    {
                        keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
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
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.Create, System.IO.FileAccess.Write));
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
                            if (encryptthenMACToolStripMenuItem.Checked == true) 
                            {
                                hsha512.TransformBlock(encryptddata, 0, encryptddata.Length, null, 0);
                            }
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = sencryptor.DoFinal(bytr);

                            if (encryptthenMACToolStripMenuItem.Checked == true)
                            {
                                hsha512.TransformFinalBlock(encryptddaata, 0, encryptddaata.Length);
                            }
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
                            yazar.Flush();
                        }
                    }
                    yazar.Write(seasalt);
                    yazar.Flush();
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        yazar.Write(hsha512.Hash);
                        yazar.Flush();
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
            byte[] keyt = new byte[32];
            byte[] seasalt = new byte[32];
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
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 96;
                    }
                    else
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 32;
                    }
                    okur.Read(seasalt, 0, 32);
                    okur.BaseStream.Position = 12;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        keyt = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, 12, null, 0);
                        string afname = new FileInfo(fpath).Name;
                        long afsz = new FileInfo(fpath).Length - 96;
                        byte[] nbyers = new byte[1000000];
                        byte[] machash = null;
                        while (okur.BaseStream.Position < afsz)
                        {
                            long whwearee = afsz - okur.BaseStream.Position;
                            if (whwearee > 1000000)
                            {
                                nbyers = new byte[1000000];
                                okur.Read(nbyers, 0, 1000000);
                                hsha512.TransformBlock(nbyers, 0, 1000000, null, 0);
                            }
                            else
                            {
                                nbyers = new byte[(int)whwearee];
                                okur.Read(nbyers, 0, (int)whwearee);
                                hsha512.TransformFinalBlock(nbyers, 0, (int)whwearee);
                            }
                        }
                        machash = hsha512.Hash;
                        byte[] mhashinf = new byte[64];
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 64;
                        okur.Read(mhashinf, 0, 64);
                        short hcnt = 0;
                        byte notauthed = 0;
                        foreach (byte b in machash)
                        {
                            if (b == mhashinf[hcnt])
                            {

                            }
                            else
                            {
                                notauthed = 1;
                                break;
                            }
                            ++hcnt;
                        }
                        if (notauthed == 1)
                        {
                            okur.Close();
                            yazar.Close();
                            MessageBox.Show(afname + " Cannot be authenticated, Decryption process denied!" + "\n\n" + "Cause: Password may be wrong or file may compromised/corrupted", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            throw new Exception("Auth Failed");
                        }
                    }
                    else
                    {
                        keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
                    okur.BaseStream.Position = 12;
                    sencryptor.Init(cstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    yazar = new System.IO.BinaryWriter(tutar);
                    long fsize = new System.IO.FileInfo(fpath).Length -32;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        fsize = fsize - 64; 
                    }
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
                    seasalt = raes.generaterandomkey(32);
                    ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(12);
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        keyt = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, 12, null, 0);
                    }
                    else
                    {
                        keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
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
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.Create, System.IO.FileAccess.Write));
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
                            if (encryptthenMACToolStripMenuItem.Checked == true)
                            {
                                hsha512.TransformBlock(encryptddata, 0, encryptddata.Length, null, 0);
                            }
                            yazar.Write(encryptddata, 0, (int)encryptddata.Length);
                            yazar.Flush();
                        }
                        else
                        {
                            bytr = new byte[(int)ig];
                            okur.Read(bytr, 0, (int)ig);
                            byte[] encryptddaata = new byte[(int)ig];
                            sencryptor.ProcessBytes(bytr,0,(int)ig, encryptddaata,0);
                            if (encryptthenMACToolStripMenuItem.Checked == true)
                            {
                                hsha512.TransformFinalBlock(encryptddaata, 0, encryptddaata.Length);
                            }
                            yazar.Write(encryptddaata, 0, (int)encryptddaata.Length);
                            yazar.Flush();
                        }
                    }
                    yazar.Write(seasalt);
                    yazar.Flush();
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        yazar.Write(hsha512.Hash);
                        yazar.Flush();
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
            byte[] seasalt = new byte[32];
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
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 96;
                    }
                    else
                    {
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 32;
                    }
                    okur.Read(seasalt, 0, 32);
                    okur.BaseStream.Position = 16;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        rijndael.Key = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, 16, null, 0);
                        string afname = new FileInfo(fpath).Name;
                        long afsz = new FileInfo(fpath).Length - 96;
                        byte[] nbyers = new byte[1000000];
                        byte[] machash = null;
                        while (okur.BaseStream.Position < afsz)
                        {
                            long whwearee = afsz - okur.BaseStream.Position;
                            if (whwearee > 1000000)
                            {
                                nbyers = new byte[1000000];
                                okur.Read(nbyers, 0, 1000000);
                                hsha512.TransformBlock(nbyers, 0, 1000000, null, 0);
                            }
                            else
                            {
                                nbyers = new byte[(int)whwearee];
                                okur.Read(nbyers, 0, (int)whwearee);
                                hsha512.TransformFinalBlock(nbyers, 0, (int)whwearee);
                            }
                        }
                        machash = hsha512.Hash;
                        byte[] mhashinf = new byte[64];
                        okur.BaseStream.Position = new FileInfo(fpath).Length - 64;
                        okur.Read(mhashinf, 0, 64);
                        short hcnt = 0;
                        byte notauthed = 0;
                        foreach (byte b in machash)
                        {
                            if (b == mhashinf[hcnt])
                            {

                            }
                            else
                            {
                                notauthed = 1;
                                break;
                            }
                            ++hcnt;
                        }
                        if (notauthed == 1)
                        {
                            okur.Close();
                            yazar.Close();
                            MessageBox.Show(afname + " Cannot be authenticated, Decryption process denied!" + "\n\n" + "Cause: Password may be wrong or file may compromised/corrupted", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            throw new Exception("Auth Failed");
                        }
                    }
                    else
                    {
                        rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
                    okur.BaseStream.Position = 16;
                    rijndael.IV = ivtg;
                    tutar = new System.IO.MemoryStream();
                    byte[] bytts = new byte[1000000];
                    sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
                    long fsize = new System.IO.FileInfo(fpath).Length - 32;
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        fsize = fsize - 64;
                    }
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
                    seasalt = raes.generaterandomkey(32);
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        byte[] scrkey = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt.Concat(System.Text.Encoding.ASCII.GetBytes(comboBox1.Text + "-" + comboBox3.Text)).ToArray(), formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes((keysize / 8) * 2);
                        hsha512 = new HMACSHA512(scrkey.Skip(scrkey.Length / 2).ToArray());
                        rijndael.Key = scrkey.Take(scrkey.Length / 2).ToArray();
                        scrkey = null;
                        hsha512.TransformBlock(ivtg, 0, 16, null, 0);
                    }
                    else
                    {
                        rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(pkey, seasalt, formbir.iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(keysize / 8);
                    }
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
                    yazar = new System.IO.BinaryWriter(System.IO.File.Open(fffpath, System.IO.FileMode.Create, System.IO.FileAccess.Write));
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
                            if (encryptthenMACToolStripMenuItem.Checked == true)
                            {
                                hsha512.TransformBlock(tutar.ToArray(), 0, (int)tutar.Length, null, 0);
                            }
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
                            if (encryptthenMACToolStripMenuItem.Checked == true)
                            {
                                hsha512.TransformFinalBlock(tutar.ToArray(), 0, (int)tutar.Length);
                            }
                            yazar.Write(tutar.ToArray(), 0, (int)tutar.Length);
                            yazar.Flush();
                            tutar.SetLength(0);
                        }
                    }
                    yazar.Write(seasalt);
                    yazar.Flush();
                    if (encryptthenMACToolStripMenuItem.Checked == true)
                    {
                        yazar.Write(hsha512.Hash);
                        yazar.Flush();
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
                            textBox1.Text = ciphersoperations(textBox2.Text, new SerpentEngine(), openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "Camellia")
                        {
                            textBox1.Text = ciphersoperations(textBox2.Text, new CamelliaEngine(), openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "Twofish")
                        {
                            textBox1.Text = ciphersoperations(textBox2.Text, new TwofishEngine(), openFileDialog1.FileName, 2);
                        }
                        if (comboBox1.Text == "ThreeFish")
                        {
                            textBox1.Text = ciphersoperations(textBox2.Text, new ThreefishEngine(keysize), openFileDialog1.FileName, 2, null, (short)(keysize / 8));
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
                                ciphersoperations(textBox2.Text, new SerpentEngine(), filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                            }
                        if (comboBox1.Text == "Camellia")
                        {
                            ciphersoperations(textBox2.Text, new CamelliaEngine(), filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                        if (comboBox1.Text == "Twofish")
                        {
                            ciphersoperations(textBox2.Text, new TwofishEngine(), filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text));
                        }
                        if (comboBox1.Text == "ThreeFish")
                        {
                            ciphersoperations(textBox2.Text, new ThreefishEngine(keysize), filepathh, 1, System.Text.Encoding.UTF8.GetBytes(textBox1.Text), (short) (keysize /8));
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

        private void Form6_Shown(object sender, EventArgs e)
        {
            drawed = 1;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rjButton3.PerformClick();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            rjButton1.PerformClick();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Multron NoteGuard is a notepad program that has encrypt feature, you can easily open your encrypted note without writing to disk.", "Multron NoteGuard", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void encrypttWhileSavingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = encrypttWhileSavingToolStripMenuItem.Checked;
        }

        private void Form6_Resize(object sender, EventArgs e)
        {
            if (drawed == 1)
            {
                label2.Location = new Point(-2, textBox1.Bottom + 4);
                textBox2.Location = new Point(label2.Right + 4, textBox1.Bottom + 4);
                checkBox2.Location = new Point(textBox2.Right + 10, textBox1.Bottom+ 5);
            }
        }
    }
}
