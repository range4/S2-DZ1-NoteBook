using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms.VisualStyles;

namespace Notepad
{
    public partial class Notepad : Form
    {
        private string _openFile;
        private string filename;
        private bool isFileChanged;
        public Notepad()
        {
            InitializeComponent();
        }

        private void темнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.DimGray;
            menuStrip1.BackColor = Color.Gray;
        }

        private void светлаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            menuStrip1.BackColor = Color.Transparent;
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog myFont = new FontDialog();
            if (myFont.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = myFont.Font;
            }
            ColorDialog colordialog = new ColorDialog();
            if (colordialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colordialog.Color;
            }

        }

        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += DateTime.Now;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog Fdialog = new OpenFileDialog();
            Fdialog.Filter = "all (*.*) |*.*";
            if (Fdialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(Fdialog.FileName);
                _openFile = Fdialog.FileName;
            }
        }
        public void Save(string _filename)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            if (_filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(_filename);
                sw.Write(richTextBox1.Text);
                sw.Close();
                filename = _filename;
                isFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл!");
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save(filename);
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pDocument = new PrintDocument();
            pDocument.PrintPage += PrintPageH;
            PrintDialog pDialog = new PrintDialog();
            pDialog.Document = pDocument;
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                pDialog.Document.Print();
            }
        }
        public void PrintPageH(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, 0, 0);
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Sdialog = new SaveFileDialog();
            Sdialog.Filter = "all (*.*) |*.*";
            if (Sdialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Sdialog.FileName, richTextBox1.Text);
                _openFile = Sdialog.FileName;
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("О программе :\n\n\nТы говоришь им, что у меня кроме молитвы ничего нет. Болван, мне больше и ничего не надо. Если та молитва достигнет правильного человека - упадёт не только Форман, но и гора.\r\nИ ты всё время говоришь: «Мохаммед, ты не тот мужик, каким был 10 лет назад». Хорошо, а я спросил твою жену и она сказала, что ты не тот мужик, каким был 2 года назад!\n\n\n© Muhammad Ali");
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.SelectAll();
            }
        }
    }
}
