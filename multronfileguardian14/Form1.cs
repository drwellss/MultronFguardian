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
using Org.BouncyCastle.Crypto.IO;
using System.Runtime.InteropServices;
namespace multronfileguardian
{
    public partial class Form1 : Form
    {
        public short encryptstatus = 0;
        public Form3 formuc;
        public Form5 formbes;
        Form2 rsakeygen;
        Form4 holdkey;
        public Form6 forum6;
        public short closedholdkey = 1;
        public short closedrsakgen = 1;
        public short closedmng = 1;
        int keysize = 2048;
        int symkeysize = 256;
        public int iterationrate = 217685;
        short rcm = 0;
        public short rtoperation = 1;
        public short rttoperation = 0;
        List<string> cmprsdf = new List<string>();
        string mfgsfolder = Application.StartupPath + "\\mfgsettings";
        string mfgalg = Application.StartupPath + "\\mfgsettings" + "\\algorithm.txt";
        string mfgalgr = Application.StartupPath + "\\mfgsettings" + "\\algorithmr.txt";
        string mfgksize = Application.StartupPath + "\\mfgsettings" + "\\salgksize.txt";
        string mfgiterate = Application.StartupPath + "\\mfgsettings" + "\\iterate.txt";
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

        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0)
            {
                if (args[0] == "-E")
                {
                    encryptstatus = 1;
                    rcm = 1;
                }
                else if (args[0] == "-D")
                {
                    encryptstatus = 2;
                    rcm = 1;
                }
                if (rcm == 1)
                {
                    int fs = 1;
                    for (fs = 1; fs < args.Length;)
                    {
                        listBox1.Items.Add(args[fs]);
                        ++fs;
                    }
                }
            }
            else
            {

            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            getsettings();

        }
        private void darktheme()
        {
            panel2.BackColor = Color.FromArgb(40, 40, 40);
            this.BackColor = Color.FromArgb(23,23,23);
            rjButton2.BackColor = Color.FromArgb(100, 0, 0);
            rjButton1.BackColor = Color.FromArgb(0, 70, 0);
            rjButton9.BackColor = panel2.BackColor;
            rjButton8.BackColor = panel2.BackColor;
            panel5.BackColor = panel2.BackColor;
            foreach (Control syazi in panel1.Controls)
            {
                if (syazi.Name.Contains("label"))
                {
                    syazi.ForeColor = Color.WhiteSmoke;
                }
            }
                foreach (Control yazi in this.Controls)
            {
                if (yazi.Name.Contains("rjButton"))
                {
                    if (yazi.Name != "rjButton1" && yazi.Name != "rjButton2")
                    {
                        yazi.BackColor = Color.FromArgb(43,40,40);
                        yazi.ForeColor = Color.WhiteSmoke;
                    }
                }
                if (yazi.Name.Contains("label"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("checkBox"))
                {
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("linkLabel"))
                {
                    LinkLabel label = (LinkLabel)yazi;
                    label.LinkColor = Color.WhiteSmoke;
                }
                if(yazi.Name.Contains("textBox"))
                {
                    yazi.BackColor = Color.FromArgb(30, 30, 30);
                    yazi.ForeColor = Color.WhiteSmoke;
                }
                if (yazi.Name.Contains("listBox"))
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
            foreach (Control cntwqe in panel4.Controls)
            {
                if (cntwqe.Name.Contains("rjButton"))
                {
                    cntwqe.BackColor = Color.FromArgb(43, 40, 40);
                    cntwqe.ForeColor = Color.WhiteSmoke;
                }
                if (cntwqe.Name.Contains("groupBox"))
                {
                    cntwqe.ForeColor = Color.WhiteSmoke;
                    foreach (Control cntrlq in cntwqe.Controls)
                    {
                        if (cntrlq.Name.Contains("comboBox"))
                        {
                            ComboBox cbox = (ComboBox)cntrlq;
                            cbox.FlatStyle = FlatStyle.Popup;
                            cbox.BackColor = Color.FromArgb(30, 30, 30);
                            cbox.ForeColor = Color.WhiteSmoke;
                        }
                        if (cntrlq.Name.Contains("linkLabel"))
                        {
                            LinkLabel label = (LinkLabel)cntrlq;
                            label.LinkColor = Color.WhiteSmoke;
                        }
                        if (cntrlq.Name.Contains("label"))
                        {
                            cntrlq.ForeColor = Color.WhiteSmoke;
                        }
                    }
                }
            }
            foreach (Control pyayzi in panel3.Controls)
            {
                if (pyayzi.Name.Contains("rjButton"))
                {
                    if (pyayzi.Name != "rjButton12")
                    {
                        pyayzi.BackColor = Color.FromArgb(43, 40, 40);
                        pyayzi.ForeColor = Color.WhiteSmoke;
                    }
                }
                if (pyayzi.Name.Contains("label"))
                {
                    pyayzi.ForeColor = Color.WhiteSmoke;
                }
                if (pyayzi.Name.Contains("textBox"))
                {
                    pyayzi.BackColor = Color.FromArgb(50, 50, 50);
                    pyayzi.ForeColor = Color.WhiteSmoke;
                }
            }
        }
        private void getsettings()
        {
            formuc = new Form3(this);
            formuc.Show();
            if (System.IO.Directory.Exists(mfgsfolder))
            {
                foreach (Control cntrlr in formuc.panel1.Controls)
                {
                    if (cntrlr.Name.Contains("groupBox") == true)
                    {
                        foreach (Control cntrltwo in cntrlr.Controls)
                        {
                            if (cntrltwo.GetType().ToString().Contains("RadioButton") == true)
                            {
                                string namez = new String(cntrltwo.Name.ToCharArray()).Substring(0, cntrltwo.Name.Length - 1);
                                if (System.IO.File.Exists(mfgsfolder + "\\" + namez + ".txt") == true)
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
                foreach (Control cntrlr in formuc.panel3.Controls)
                {
                    if (cntrlr.Name.Contains("groupBox") == true)
                    {
                        foreach (Control cntrltwo in cntrlr.Controls)
                        {
                            if (cntrltwo.GetType().ToString().Contains("RadioButton") == true)
                            {
                                string namez = new String(cntrltwo.Name.ToCharArray()).Substring(0, cntrltwo.Name.Length - 1);
                                if (System.IO.File.Exists(mfgsfolder + "\\" + namez + ".txt") == true)
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
                if (formuc.theme1.Checked == true)
                {
                    darktheme();
                    formuc.sdarktheme();
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
                            comboBox1.Text = "AES";
                        }
                    }
                    else
                    {
                        comboBox1.Text = "AES";
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
                    if (System.IO.File.Exists(mfgksize) == true)
                    {
                        string keysize = System.IO.File.ReadAllText(mfgksize);
                        foreach (string algg in comboBox3.Items)
                        {
                            if (algg == keysize)
                            {
                                comboBox3.Text = keysize;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                System.IO.Directory.CreateDirectory(mfgsfolder);
            }
            if (formuc.iterationaut1.Checked == true)
            {
                if (System.IO.File.Exists(mfgiterate) == true)
                {
                    string iterat = System.IO.File.ReadAllText(mfgiterate);
                    if (iterat.Length > 8)
                    {
                        formuc.textBox1.Text = iterationrate.ToString();
                    }
                    else
                    {
                        formuc.textBox1.Text = iterat;
                        iterationrate = int.Parse(iterat);
                    }

                }
            }
            formuc.Hide();
            formuc.swillbesaved = 0;
            checkBox1.Checked = formuc.showpassword1.Checked;
            formuc.firststart = 1;
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
            foreach (Control cntrlr in formuc.panel3.Controls)
            {
                if (cntrlr.Name.Contains("groupBox") == true)
                {
                    foreach (Control cntrltwo in cntrlr.Controls)
                    {
                        if (cntrltwo.GetType().ToString().Contains("RadioButton") == true)
                        {
                            string namez = new String(cntrltwo.Name.ToCharArray()).Substring(0, cntrltwo.Name.Length - 1);
                            RadioButton rbutton = (RadioButton)cntrltwo;
                            if (System.IO.File.Exists(mfgsfolder + "\\" + namez + ".txt") == false)
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
                System.IO.File.WriteAllText (mfgksize, comboBox3.Text);
            }
            if (formuc.iterationaut1.Checked == true)
            {
                System.IO.File.WriteAllText(mfgiterate, iterationrate.ToString());
            }
        }

        public byte cmpcontroller()
        {
            byte iszip = 0;
            foreach(string filex in listBox1.Items)
            {
                if (filex.Contains(".zip"))
                {
                    iszip = 1;
                    cmprsdf.Add(filex);
                }
            }
            if (iszip == 1)
            {
                return 1;
            }
            return 0;
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
                rijndael.KeySize = symkeysize;
                rijndael.BlockSize = 128;
                rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
                rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                byte[] aeskey = new byte[symkeysize / 8];
                byte[] ivtg = new byte[16];
                if (encryptstatus == 1)
                {
                    aeskey = raes.generaterandomkey(67);
                    label4.Text = "Iterating Password...";
                    rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);                    
                    aeskey = raes.rsaencrypt(aeskey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    if (encryptstatus == 1)
                    {
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        rijndael.IV = ivtg;
                        okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                        yazar = new System.IO.BinaryWriter(System.IO.File.Open(dosyalar + ".mfg", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                        yazar.Write(aeskey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                        okur.Read(ivtg, 0, 16);
                        aeskey = raes.rsadecrypt(aeskey, key, keysize);
                        label4.Text = "Iterating Password...";
                        rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(aeskey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
                        rijndael.IV = ivtg;
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void chachaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstate = false;
            label4.Text = "Iterating Password...";
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32);
            byte[] ivtg = new byte[12];
            try
            {
                ChaCha7539Engine chatea = new ChaCha7539Engine();
                if (encryptstatus == 1)
                {
                    cryptstate = true;
                }
                else
                {
                    cryptstate = false;
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(12);
                        yazar.Write(ivtg);
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        okur.Read(ivtg, 0, 12);
                    }
                    chatea.Init(cryptstate, new ParametersWithIV(new KeyParameter(keyt), ivtg));
                    byte[] bytesx = new byte[1000000];
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
                            bytesx = new byte[1000000];
                            okur.Read(bytesx, 0, 1000000);
                            byte[] encdata = new byte[1000000];
                            chatea.ProcessBytes(bytesx, 0, 1000000, encdata, 0);
                            yazar.Write(encdata, 0, 1000000);
                            yazar.Flush();
                        }
                        else
                        {
                            bytesx = new byte[(int) whereweare];
                            okur.Read(bytesx, 0, (int) whereweare);
                            byte[] encdata = new byte[(int) whereweare];
                            chatea.ProcessBytes(bytesx, 0, (int) whereweare, encdata, 0);
                            yazar.Write(encdata, 0, (int) whereweare);
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
            catch (Exception x)
            {
                nstate = 0;
                MessageBox.Show("Operation ended with a error, these may cause an error's occure: file dont exists, program haven't access to file, your password is wrong or something else, error = " + x.Message + " | You can contact programmer, Discord = dr_wellss", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
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
                byte[] serkey = { 0 };
                byte[] ivtg = new byte[16];
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomkey(67);
                    serkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        label4.Text = "Iterating Password...";
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serkey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                        okur.Read(ivtg, 0, 16);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        label4.Text = "Iterating Password...";
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void chacharsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            ChaCha7539Engine sifreler = new ChaCha7539Engine();
            try
            {
                byte[] serpentkey = { 0 };
                byte[] serkey = { 0 };
                byte[] ivtg = new byte[12];
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomkey(67);
                    serkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        label4.Text = "Iterating Password...";
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(12);
                        sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), ivtg));
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serkey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                                byte[] encrypted = new byte[1000000];
                                sifreler.ProcessBytes(bxynew, 0, 1000000, encrypted,0);
                                yazar.Write(encrypted, 0, 1000000);
                                yazar.Flush();
                            }
                            else
                            {
                                bxynew = new byte[(int) whereewearee];
                                okur.Read(bxynew, 0, (int) whereewearee);
                                byte[] encrypted = new byte[(int) whereewearee];
                                sifreler.ProcessBytes(bxynew, 0, (int) whereewearee, encrypted, 0);
                                yazar.Write(encrypted, 0, (int) whereewearee);
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
                        okur.Read(ivtg, 0, 12);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        label4.Text = "Iterating Password...";
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(32)), ivtg));
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
                                byte[] encrypted = new byte[1000000];
                                sifreler.ProcessBytes(bynetx, 0, 1000000, encrypted, 0);
                                yazar.Write(encrypted, 0, 1000000);
                                yazar.Flush();
                            }
                            else
                            {
                                bynetx = new byte[(int) whweare];
                                okur.Read(bynetx, 0, (int) whweare);
                                byte[] encrypted = new byte[(int) whweare];
                                sifreler.ProcessBytes(bynetx, 0, (int) whweare, encrypted, 0);
                                yazar.Write(encrypted, 0, (int) whweare);
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
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
                byte[] serkey = { 0 };
                byte[] ivtg = new byte[16];
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomkey(67);
                    serkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach(string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        label4.Text = "Iterating Password...";
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serkey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                        okur.Read(ivtg, 0, 16);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        label4.Text = "Iterating Password...";
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
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
                byte[] serkey = { 0 };
                byte[] ivtg = new byte[16];
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomkey(67);
                    serkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        label4.Text = "Iterating Password...";
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serkey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                        okur.Read(ivtg, 0, 16);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        label4.Text = "Iterating Password...";
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void twofishprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new TwofishEngine()), new Pkcs7Padding());
            label4.Text = "Iterating Password...";
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
            byte[] ivtg = new byte[16];
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        yazar.Write(ivtg);
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        okur.Read(ivtg, 0, 16);
                    }
                    sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void camelliaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new CamelliaEngine()), new Pkcs7Padding());
            label4.Text = "Iterating Password...";
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
            byte[] ivtg = new byte[16];
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        yazar.Write(ivtg);
                        yazar.Flush();
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        okur.Read(ivtg, 0, 16);
                    }
                    sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void thfishrsaprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new ThreefishEngine(symkeysize)), new Pkcs7Padding());
            try
            {
                byte[] serpentkey = { 0 };
                byte[] serkey = { 0 };
                byte[] ivtg = new byte[symkeysize / 8];
                if (encryptstatus == 1)
                {
                    serpentkey = raes.generaterandomkey(67);
                    serkey = raes.rsaencrypt(serpentkey, key, keysize);
                }
                foreach (string dosyalar in listBox1.Items)
                {
                    if (encryptstatus == 1)
                    {
                        nstate = 1;
                        label4.Text = "Iterating Password...";
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
                        sifreler.Init(true, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(serpentkey, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
                        okur = new BinaryReader(File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        yazar.Write(serkey, 0, keysize / 8);
                        yazar.Write(ivtg);
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
                        okur.Read(ivtg, 0, symkeysize / 8);
                        byte[] dsk = raes.rsadecrypt(serpentkey, key, keysize);
                        label4.Text = "Iterating Password...";
                        sifreler.Init(false, new ParametersWithIV(new KeyParameter(new System.Security.Cryptography.Rfc2898DeriveBytes(dsk, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8)), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void serpentprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new SerpentEngine()), new Pkcs7Padding());
            label4.Text = "Iterating Password...";
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
            byte[] ivtg = new byte[16];
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        yazar.Write(ivtg);
                        yazar.Flush();
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        okur.Read(ivtg, 0, 16);
                    }

                    sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void thfishprocesses(string key)
        {
            BinaryReader okur = null;
            BinaryWriter yazar = null;
            short nstate = 0;
            Boolean cryptstatus;
            PaddedBufferedBlockCipher sifreler = new PaddedBufferedBlockCipher(new CbcBlockCipher(new ThreefishEngine(symkeysize)), new Pkcs7Padding());
            label4.Text = "Iterating Password...";
            byte[] keyt = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
            byte[] ivtg = new byte[symkeysize / 8];
            if (encryptstatus == 1)
            {
                cryptstatus = true;
            }
            else
            {
                cryptstatus = false;
            }
            try
            {
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new BinaryReader(System.IO.File.Open(dosyalar, FileMode.Open, FileAccess.Read));
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(File.Open(dosyalar + ".mfg", FileMode.OpenOrCreate, FileAccess.Write));
                        ivtg = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
                        yazar.Write(ivtg);
                        yazar.Flush();
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), FileMode.OpenOrCreate, FileAccess.Write));
                        okur.Read(ivtg, 0, symkeysize /8);
                    }

                    sifreler.Init(cryptstatus, new ParametersWithIV(new KeyParameter(keyt), ivtg));
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
        }
        public void aesprocesses(string key)
        {
            short nstate = 0;
            System.Security.Cryptography.CryptoStream sifreler = null;
            System.IO.BinaryReader okur = null;
            System.IO.MemoryStream tutar = null;
            System.IO.BinaryWriter yazar = null;
            try
            {
                System.Security.Cryptography.AesCryptoServiceProvider rijndael = new System.Security.Cryptography.AesCryptoServiceProvider();
                rijndael.KeySize = symkeysize;
                rijndael.BlockSize = 128;
                rijndael.Mode = System.Security.Cryptography.CipherMode.CBC;
                rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                label4.Text = "Iterating Password...";
                rijndael.Key = new System.Security.Cryptography.Rfc2898DeriveBytes(key, System.Text.Encoding.ASCII.GetBytes("youcanjoinsaltinfutureversionsmsmsms"), iterationrate, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(symkeysize / 8);
                foreach (string dosyalar in listBox1.Items)
                {
                    nstate = 1;
                    okur = new System.IO.BinaryReader(System.IO.File.Open(dosyalar, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                    tutar = new MemoryStream();
                    if (encryptstatus == 1)
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(dosyalar + ".mfg", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                        byte[] geniv = new System.Security.Cryptography.Rfc2898DeriveBytes(reversebarray(raes.generaterandomkey(67)), System.Text.Encoding.ASCII.GetBytes(reversestring("youcanjoinsaltinfutureversivonsmsmsms")), 143, System.Security.Cryptography.HashAlgorithmName.SHA512).GetBytes(16);
                        rijndael.IV = geniv;
                        yazar.Write(geniv);
                        yazar.Flush();
                        sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
                    }
                    else
                    {
                        yazar = new BinaryWriter(System.IO.File.Open(System.IO.Path.GetDirectoryName(dosyalar) + "\\" + System.IO.Path.GetFileNameWithoutExtension(dosyalar), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write));
                        byte[] genniv = new byte[16];
                        okur.Read(genniv, 0, 16);
                        rijndael.IV = genniv;
                        sifreler = new System.Security.Cryptography.CryptoStream(tutar, rijndael.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
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
                            yazar.Write(tutar.ToArray(), 0, (int) tutar.Length);
                            yazar.Flush();
                            tutar.SetLength(0);
                        }
                        else
                        {
                            bxytes = new byte[(int)whereweare];
                            okur.Read(bxytes, 0, (int)whereweare);
                            sifreler.Write(bxytes, 0, (int)whereweare);
                            sifreler.FlushFinalBlock();
                            yazar.Write(tutar.ToArray(), 0, (int)tutar.Length);
                            yazar.Flush();
                            tutar.SetLength(0);
                        }
                    }
                    okur.Dispose();
                    sifreler.Dispose();
                    tutar.Dispose();
                    yazar.Dispose();
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
                    if (yazar != null)
                    {  
                        yazar.Dispose(); 
                    }
                    if (tutar  != null)
                    { 
                        tutar.Dispose(); 
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
                    if (yazar != null)
                    {
                        yazar.Dispose();
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
                    if (cmpcontroller() == 1)
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
                    else
                    {
                        listBox1.Items.Clear();
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
            if (rcm == 1)
            {
                Application.Exit();
            }
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
                    if (comboBox1.Text == "AES")
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
                else if (comboBox1.Text == "ChaCha20")
                {
                    encryptstatus = 2;
                    System.Threading.Thread nesawthread = new System.Threading.Thread(() => chachaprocesses(textBox1.Text));
                    nesawthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else if (comboBox1.Text == "ChaCha20-RSA Hybrid Algorithm")
                {
                    encryptstatus = 2;
                    System.Threading.Thread nessawthread = new System.Threading.Thread(() => chacharsaprocesses(textBox1.Text));
                    nessawthread.Start();
                    rjButton2.Enabled = false;
                    rjButton3.Enabled = false;
                    rjButton1.Enabled = false;
                    rjButton5.Enabled = false;
                    rjButton4.Enabled = false;
                    comboBox2.Enabled = false;
                    comboBox1.Enabled = false;
                    textBox1.Enabled = false;
                }
                else if (comboBox1.Text == "AES-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Serpent")
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
                    else if (comboBox1.Text == "Serpent-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Twofish")
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
                    else if (comboBox1.Text == "Camellia")
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
                    else if (comboBox1.Text == "Camellia-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Twofish-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "ThreeFish")
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
                    else if (comboBox1.Text == "ThreeFish-RSA Hybrid Algorithm")
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
                    if (comboBox1.Text == "AES")
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
                    else if (comboBox1.Text == "ChaCha20-RSA Hybrid Algorithm")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread nessawthread = new System.Threading.Thread(() => chacharsaprocesses(textBox1.Text));
                        nessawthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "ChaCha20")
                    {
                        encryptstatus = 1;
                        System.Threading.Thread nesawthread = new System.Threading.Thread(() => chachaprocesses(textBox1.Text));
                        nesawthread.Start();
                        rjButton2.Enabled = false;
                        rjButton3.Enabled = false;
                        rjButton1.Enabled = false;
                        rjButton5.Enabled = false;
                        rjButton4.Enabled = false;
                        comboBox2.Enabled = false;
                        comboBox1.Enabled = false;
                        textBox1.Enabled = false;
                    }
                    else if (comboBox1.Text == "AES-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Serpent")
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
                    else if (comboBox1.Text == "Serpent-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Twofish")
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
                    else if (comboBox1.Text == "Camellia")
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
                    else if (comboBox1.Text == "Camellia-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "ThreeFish")
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
                    else if (comboBox1.Text == "ThreeFish-RSA Hybrid Algorithm")
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
                    else if (comboBox1.Text == "Twofish-RSA Hybrid Algorithm")
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
            int boyt = listBox1.HorizontalExtent;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.AddRange(openFileDialog1.FileNames);
            }
            foreach (string liist in listBox1.Items)
            {
                int boyut_size = TextRenderer.MeasureText(liist, listBox1.Font).Width;
                if (boyut_size > boyt)
                {
                    boyt = boyut_size;
                }
            }
            listBox1.HorizontalExtent = boyt;
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
            listBox1.HorizontalExtent = 0;
        }

        public void rjButton6_Click(object sender, EventArgs e)
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
            formuc.WindowState = FormWindowState.Normal;
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
            label1.Focus();
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
            if (comboBox1.Text.Contains("AES") == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Items.AddRange(new string[] { "128", "192", "256"});
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These algorithms are encryption algorithms, Your choices, If you have no idea, use AES or AES-RSA (In case you will use hybrid algorithm)", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void label6_Click(object sender, EventArgs e)
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
            label1.Focus();
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
            MessageBox.Show("These are RSA Key Lengths, RSA's security increasing as its key size increased, but it also get slower, If you've no idea what to close, 3072 Key Length is best at balance of Security and Speed. 16384 Key Length is the slowest key length", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            cmprsdf.Clear();
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(fnam);
                        }
                    }
                    zibb.Dispose();
                    listBox1.Items.Clear();
                    listBox1.Items.Add(oputzipf);
                }
                else
                {
                    foreach (string filepath in cmprsdf)
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
                        if (formuc.dfafterencrypted1.Checked == true)
                        {
                            System.IO.File.Delete(fph);
                        }
                    }

                    listBox1.Items.Clear();
                    cmprsdf.Clear();
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

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            label1.Focus();
            symkeysize = int.Parse(comboBox3.Text);
            if (formuc.savealgorithm1.Checked == true)
            {
                formuc.swillbesaved = 1;
            }
        }

        private void rjButton1_EnabledChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = rjButton1.Enabled;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("This option offer you select symmetric-algorithm key size, If you have no idea, Select biggest key length.", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (formuc.savealgorithm1.Checked == false)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
            }
            if (rcm == 1)
            {
                formbes = new Form5(this);
                formbes.Show();
                this.Hide();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This Software is made by Dexter Morgan aka Dr. Wells\nYour Files/Datas will not be collected for anything, And it's %100 free and open-source.\nDo you want to copy Github page of programmer?", "Multron File Guardian", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                System.Windows.Forms.Clipboard.SetText("https://github.com/drwellss");
                MessageBox.Show("Copied successfully!", "Multron File Guardian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panel4.Visible = false;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            byte equalopen = 0;
            if (formuc.Visible == true)
            {
                equalopen = 1;
            }
            if (equalopen == 0)
            {

                formuc.WindowState = FormWindowState.Minimized;
                formuc.Show();
            }
            if (checkBox1.Checked)
            {
                formuc.showpassword1.PerformClick();
            }
            else
            {
                formuc.showpassword0.PerformClick();

            }
            if (equalopen == 0)
            {
                formuc.Hide();
            }
            if (closedmng != 1)
            {
                forum6.checkBox2.Checked = checkBox1.Checked;
            }    
        }

        private void label11_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
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

        private void label14_Click(object sender, EventArgs e)
        {
            if (closedmng == 1)
            {
                forum6 = new Form6(this);
                forum6.Show();
                closedmng = 0;
            }
            else
            {
                forum6.Show();
            }
        }
    }
}


