using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;    //For file read-write

namespace WinFormsAppMyNotePad
{
    public partial class Form1 : Form
    {
        string filePath;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtArea.Clear();
            txtArea.Focus();
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files(*.txt)|*.txt";  //the part before the |
            //is what is displayed on the drop down at bottom right and the part
            //after is the file extension type allowed in it
            openDialog.Title = "Open text Files";
            openDialog.ShowDialog();
            if(openDialog.FileName != string.Empty)
            {
                filePath = openDialog.FileName;
                txtArea.Text = File.ReadAllText(filePath);
            }
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(File.Exists(filePath))
            {
                File.WriteAllText(filePath, txtArea.Text);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter= "Text Files(*.txt)|*.txt";
            var result = saveDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Stream stream = saveDialog.OpenFile();
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine(txtArea.Text);
                sw.Close();
                stream.Close();
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Do you want to save your changes to the file", MessageBoxButtons.YesNoCancel);
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtArea.SelectedText);
            txtArea.SelectedText = String.Empty;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //txtArea.SelectedText = FontStyle.Bold;
            FontDialog fontDialog = new FontDialog();
            fontDialog.ShowDialog();
            txtArea.Font = fontDialog.Font; 
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtArea.ZoomFactor += 0.2f;
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtArea.ZoomFactor -= 0.2f;
        }
    }
}
