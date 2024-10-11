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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rsakey = raes.rsageneratekey();
            short pkey = 0;
            textBox1.Text = "";
            textBox2.Text = "";
            foreach(char chr in rsakey)
            {
                if (chr == ' ')
                {
                    pkey = 1;
                }
                if (pkey == 1 && chr != ' ')
                {
                    pkey = 2;
                }
                if (pkey == 0)
                {
                    textBox1.Text += chr;
                }
                if (pkey == 2)
                {
                    textBox2.Text += chr;
                }
                rsakey = "";
                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("These are your RSA Public/Private Key. Public Key is the key that will be used to encrypt but cant be used to decrypt. Private Key is only for decrypt, Cant be used to encrypt. So protect your Private key, Dont give it anyone", "Multron RSA Key Generator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
