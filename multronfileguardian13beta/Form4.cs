using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multronfileguardian
{
    public partial class Form4 : Form
    {
        string mfgkeys = Application.StartupPath + "\\mfgsettings\\" + "publickeys.txt";
        Form1 frm1;
        public Form4(Form1 nform)
        {
            InitializeComponent();
            frm1 = nform;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (File.Exists(mfgkeys))
            {
                listBox1.Items.AddRange(File.ReadAllLines(mfgkeys));
            }
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text + "=" + textBox2.Text);
        }

        private void rjButton2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void rjButton3_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (string s in listBox1.Items) 
            {
                list.Add(s); 
            }
            File.WriteAllLines(mfgkeys, list.ToArray());
        }

        private void rjButton4_Click(object sender, EventArgs e)
        {
            string pbkey = "";
            short keystate = 0;
            foreach (char c in listBox1.SelectedItem.ToString())
            {
                if (keystate == 1)
                {
                    pbkey = pbkey + c;
                }
                if (c == '=')
                {
                    keystate = 1;
                }
            }
            frm1.textBox1.Text = pbkey;
        }
    }
}
