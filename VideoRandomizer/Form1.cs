using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoRandomizer
{
    public partial class Form1 : Form
    {
        private FileRandomizer randomizer;

        public Form1()
        {
            InitializeComponent();
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default.InputPath;
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            if ( folderBrowserDialog1.ShowDialog() == DialogResult.OK )
            {
                var path = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.InputPath = path;
                Properties.Settings.Default.Save();
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            string path = folderBrowserDialog1.SelectedPath;
            if ( randomizer == null && path != null && path != "" )
            {
                randomizer = new FileRandomizer(path);
                randomizer.Go();
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ( randomizer != null )
            {
                randomizer.Close();
            }
        }
    }
}
