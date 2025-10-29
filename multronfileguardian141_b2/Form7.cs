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
    public partial class Form7 : Form
    {
        Form6 formsix;
        public Form7(Form6 formalti)
        {
            InitializeComponent();
            formsix = formalti;
        }

        private void Form7_Shown(object sender, EventArgs e)
        {
            textBox1.Font = formsix.textBox1.Font;
            textBox1.Text = formsix.textBox1.Text;
            textBox1.ForeColor  = formsix.textBox1.ForeColor;
            textBox1.BackColor = formsix.textBox1.BackColor;
            formsix.Hide();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            formsix.textBox1.Text = textBox1.Text;
            formsix.Show();
            formsix.fwopen = 0;
        }
    }
}
