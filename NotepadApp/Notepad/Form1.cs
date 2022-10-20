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

namespace Notepad
{
    public partial class Notepad : Form
    {
        private string currentFile;
        private bool IsFileOpened() => string.IsNullOrEmpty(currentFile) ? false : true;
        public Notepad()
        {
            InitializeComponent();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";
        }
        public void NewFile()
        {
            DialogResult answer = MessageBox.Show("Вы хотите сохранить текущий файл перед созданием нового?",
                    "Подтвердите", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (answer == DialogResult.OK)
            {
                SaveFile();
            }
            richTextBox.Text = "";
            currentFile = "";
        }
        public void ExeptionHandler(Exception e)
        {
            MessageBox.Show(e.Message);
        }
       

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void OpenFile()
        {
            DialogResult answer = MessageBox.Show("Вы хотите сохранить текущий файл перед открытием нового?",
                    "Подтвердите", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (answer == DialogResult.OK)
            {
                SaveFile();
            }
            
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt|rtf files (*.rtf)|*.rtf";
            if (dialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            if (Path.GetExtension(openFileDialog.FileName) == ".txt")
            {
                richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
            }
            richTextBox.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
            currentFile = dialog.FileName;
            richTextBox.Text = File.ReadAllText(currentFile);
        }
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void SaveFile()
        {
            if (File.Exists(currentFile))
            {
                File.WriteAllText(currentFile, richTextBox.Text);
                MessageBox.Show("Файл успешно сохранен");
            }
            else
            {
                SaveAsFile();
            }
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        //TODO: сохранение перед выходом
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Вы хотите сохранить текущий файл перед выходом?",
                    "Подтвердите", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (answer == DialogResult.OK)
            {
                SaveFile();
            }
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBox.SelectedText);

            }
            catch (Exception ex)
            {
                ExeptionHandler(ex);
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fd = new FontDialog();
            fd.ShowColor = true;
            fd.ShowDialog();
            richTextBox.SelectionFont = fd.Font;
            richTextBox.SelectionColor = fd.Color;
        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox_RightMouseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void SaveAsFile()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            File.WriteAllText(saveFileDialog.FileName, richTextBox.Text);
            currentFile = saveFileDialog.FileName;
            MessageBox.Show("Файл успешно сохранен");
        }
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void allFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowDialog();
            richTextBox.Font = fontDialog.Font;
            richTextBox.ForeColor = fontDialog.Color;
        }


        private void themeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            colorDialog.ShowDialog();
            richTextBox.BackColor = colorDialog.Color;
        }
    }
}
