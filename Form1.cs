using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Lab05_Bai1
{
    public partial class Form1 : Form
    {
        private string currentFile = null;   // Đường dẫn file hiện tại
        private bool isSaved = true;         // Trạng thái đã lưu hay chưa

        public Form1()
        {
            InitializeComponent();
        }

        // ================== FORM LOAD ==================
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // ================== NEW ==================
        private void tsbNew_Click(object sender, EventArgs e)
        {
            richText.Clear();
            currentFile = null;
            isSaved = true;
            toolStripComboBox1.Text = "Tahoma";
            toolStripComboBox2.Text = "14";
            richText.Font = new Font("Tahoma", 14);

            UpdateWordCount();
        }

        // ================== OPEN ==================
        private void tsbOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Rich Text File|*.rtf|Text File|*.txt|All files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(ofd.FileName).ToLower();
                    if (ext == ".rtf")
                        richText.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                    else
                        richText.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);

                    currentFile = ofd.FileName;
                    isSaved = true;

                    UpdateWordCount();
                }
            }
        }

        // ================== SAVE ==================
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFile))
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Rich Text File|*.rtf|Text File|*.txt";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        SaveToFile(sfd.FileName);
                    }
                }
            }
            else
            {
                SaveToFile(currentFile);
            }
        }

        private void SaveToFile(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            if (ext == ".txt")
                richText.SaveFile(filePath, RichTextBoxStreamType.PlainText);
            else
                richText.SaveFile(filePath, RichTextBoxStreamType.RichText);

            currentFile = filePath;
            isSaved = true;
            MessageBox.Show("Đã lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ================== BOLD ==================
        private void tsbBold_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold);
        }

        // ================== ITALIC ==================
        private void tsbItalic_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic);
        }

        // ================== UNDERLINE ==================
        private void tsbUnderline_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline);
        }

        private void ToggleFontStyle(FontStyle style)
        {
            if (richText.SelectionFont != null)
            {
                Font currentFont = richText.SelectionFont;
                FontStyle newStyle;

                if (currentFont.Style.HasFlag(style))
                    newStyle = currentFont.Style & ~style;
                else
                    newStyle = currentFont.Style | style;

                richText.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newStyle);
            }
        }

        // ================== FONT COMBOBOX ==================
        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            string fontName = toolStripComboBox1.Text;
            float size = richText.SelectionFont?.Size ?? 14;
            FontStyle style = richText.SelectionFont?.Style ?? FontStyle.Regular;

            richText.SelectionFont = new Font(fontName, size, style);
        }

        // ================== SIZE COMBOBOX ==================
        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(toolStripComboBox2.Text, out int newSize))
            {
                string fontName = richText.SelectionFont?.FontFamily.Name ?? "Tahoma";
                FontStyle style = richText.SelectionFont?.Style ?? FontStyle.Regular;

                richText.SelectionFont = new Font(fontName, newSize, style);
            }
        }

        // ================== ĐẾM TỪ ==================
        private void richText_TextChanged(object sender, EventArgs e)
        {
            isSaved = false;
            UpdateWordCount();
        }

        private void UpdateWordCount()
        {
            string text = richText.Text.Trim();
            int count = text == "" ? 0 : Regex.Split(text, @"\s+").Length;
            toolStripStatusLabel1.Text = "Số từ: " + count;
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đây là thanh trạng thái. " + toolStripStatusLabel1.Text,
                    "Thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // Font mặc định
            toolStripComboBox1.Text = "Tahoma";
            toolStripComboBox2.Text = "14";

            // Nạp tất cả font trong máy
            foreach (FontFamily font in new InstalledFontCollection().Families)
            {
                toolStripComboBox1.Items.Add(font.Name);
            }

            // Nạp các size chữ phổ biến
            List<int> listSize = new List<int> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (var s in listSize)
            {
                toolStripComboBox2.Items.Add(s);
            }

            // Set font mặc định cho RichTextBox
            richText.Font = new Font("Tahoma", 14);

            UpdateWordCount();

        }
    }
}