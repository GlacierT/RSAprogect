using System;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Key;
using ED;
using ImDa;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public static Image image;
        public static int[] mas;
        public byte[] bytes;

        public string s = "";
        public List<string> result = new List<string>();
        public List<string> input = new List<string>();
        public string resalt = "";

        public string strImg = "";
        public string stIm = "";
        public List<string> sI = new List<string>();
        public Image imgFromStream;

        public Form1()
        {
            InitializeComponent();
        }

        private void домопогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ПроПрограму newForm = new ПроПрограму();
            newForm.Show();
        }

        private void Ключі_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Visible = true;
            panel1.Visible = false;
            panel3.Visible = false;
            textBox7.Enabled = false;
            textBox8.Enabled = false;
            textBox9.Enabled = false;
            textBox10.Enabled = false;
        }

        private void Шифрувати_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            textBox14.Enabled = false;
            textBox13.Enabled = false;
            textBox2.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            textBox4.Text = "";
            textBox3.Text = "";
        }

        private void Дешифрувати_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox3.Enabled = false;
            ШифруватиІнф.Enabled = false;
            Зберигти.Enabled = false;
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            textBox4.Text = "";
            textBox3.Text = "";
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = null;
            textBox4.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox4.Text = "Введыть текст для шифрування";//подсказка
            textBox4.ForeColor = Color.Black;
            textBox1.Text = "Введыть текст для розшифрування";//подсказка
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }
        
        private void ЗвантажитиЗображення_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = image;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити зображення",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ImageDate id = new ImageDate();
            bytes = id.MasByte(image);
            mas = bytes.Select(i =>(int) i).ToArray();

            textBox11.Enabled = true;
            textBox12.Enabled = true;
            ШифруватиІнф.Enabled = true;
            ЗавантажитиФайл.Enabled = false;
            ЗвантажитиЗображення.Enabled = false;
        }

        private void ШифрФайл_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(open_dialog.FileName);
                    while (!sr.EndOfStream)
                    {
                        input.Add(sr.ReadLine());
                    }
                    sr.Close();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити документ",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            textBox1.Clear();
            foreach (string item in input)
            {
                textBox1.Text += item + " ";
            }

            textBox13.Enabled = true;
            textBox14.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = false;
        }

        private void ШифрЗоб_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.PNG;)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(open_dialog.FileName);
                    pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox2.Image = image;
                    pictureBox2.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити зображення",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ImageDate id = new ImageDate();
            bytes = id.MasByte(image);
            mas = bytes.Select(i =>(int) i).ToArray();

            OpenFileDialog open_dialog1 = new OpenFileDialog();
            open_dialog1.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
            if (open_dialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(open_dialog1.FileName);
                    while (!sr.EndOfStream)
                    {
                        sI.Add(sr.ReadLine());
                    }
 
                    sr.Close();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити документ",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            textBox13.Enabled = true;
            textBox14.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = false;
        }

        private void ГенеруванняКлючів_Click(object sender, EventArgs e)
        {
            if ((textBox5.Text.Length > 0) && (textBox6.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox5.Text);
                long q = Convert.ToInt64(textBox6.Text);
                RSAkey k = new RSAkey();
                if (k.IsTheNumberSimple(p) && k.IsTheNumberSimple(q))
                {
                    Ключі.Enabled = false;
                    Дешифрувати.Enabled = false;
                    Шифрувати.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;

                    RSAkey pas = new RSAkey();
                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long e_ = pas.Calculate_e();
                    long d = pas.Calculate_d(e_, m);

                    textBox7.Text = d.ToString();
                    textBox8.Text = n.ToString();

                    textBox9.Text = e_.ToString();
                    textBox10.Text = n.ToString();


                    textBox5.Enabled = true;
                    textBox6.Enabled = true;
                    textBox7.Enabled = false;
                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    Ключі.Enabled = true;
                    Дешифрувати.Enabled = true;
                    Шифрувати.Enabled = true;
                }
                else
                    MessageBox.Show("p чи q - не росте число!");
            }
            else
                MessageBox.Show("Введіть p чи q!");
            
            List<string> result = new List<string>();
            
            result.Add("Секретний ключ: ");
            result.Add(textBox7.Text);
            result.Add(textBox8.Text);
            result.Add("Публічний ключ: ");
            result.Add(textBox9.Text);
            result.Add(textBox10.Text);
            
            StreamWriter sw = new StreamWriter(@"C:\Users\User\Desktop\WindowsFormsApp1\key.txt");
            foreach (string item in result)
                sw.WriteLine(item);
            sw.Close();
            MessageBox.Show("Ключі збережено до файлу key.txt");
        }

        private void ШифруватиІнф_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 0 && pictureBox1.Image == null)
            {
                MessageBox.Show("Заповніть текстове поле, чи завантажте зображення!");
            }
            else
            {
                if (textBox4.Text.Length > 0)
                {
                    if ((textBox11.Text.Length > 0) && (textBox12.Text.Length > 0))
                    {
                        textBox11.Enabled = false;
                        textBox12.Enabled = false;
                        textBox4.Enabled = false;
                        textBox3.Enabled = false;
                        ШифруватиІнф.Enabled = false;
                        ЗвантажитиЗображення.Enabled = false;
                        ЗавантажитиФайл.Enabled = false;
                        Зберигти.Enabled = false;
                        
                        long e_ = Convert.ToInt64(textBox11.Text);
                        long n = Convert.ToInt64(textBox12.Text);

                        EncodeDecode ed = new EncodeDecode();
                        result = ed.RSA_Endoce(s, e_, n);
                        

                        for (int i = 0; i < result.Count; i++)
                            textBox3.Text += result[i] + " ";
                        
                        textBox11.Enabled = true;
                        textBox12.Enabled = true;
                        textBox4.Enabled = true;
                        textBox3.Enabled = true;
                        ШифруватиІнф.Enabled = true;
                        ЗвантажитиЗображення.Enabled = true;
                        ЗавантажитиФайл.Enabled = true;
                        Зберигти.Enabled = true;
                    }
                    else
                        MessageBox.Show("Заповніть поля ключів!");
                }
                if (pictureBox1.Image != null)
                {
                    if ((textBox11.Text.Length > 0) && (textBox12.Text.Length > 0))
                    {
                        textBox11.Enabled = false;
                        textBox12.Enabled = false;
                        textBox4.Enabled = false;
                        textBox3.Enabled = false;
                        ШифруватиІнф.Enabled = false;
                        ЗвантажитиЗображення.Enabled = false;
                        ЗавантажитиФайл.Enabled = false;
                        Зберигти.Enabled = false;
                            
                        long e_ = Convert.ToInt64(textBox11.Text);
                        long n = Convert.ToInt64(textBox12.Text);
                        
                        var ran = new Random();
                        int[] mos = new int[20];
                        int[] g = new int[20];
                        
                        for (int i = 0; i < 20; i++)
                        {
                            g[i] = ran.Next(100, mas.Length);
                            mos[i] = mas[g[i]];
                        }
                        
                        strImg = string.Join(" ", mos);
                        
                        EncodeDecode ed = new EncodeDecode();
                        result = ed.RSA_Endoce(strImg, e_, n);

                        string som = string.Join(" ", result);
                        int[] ia = som.Split(' ').Select(w => Convert.ToInt32(w)).ToArray();

                        for (int i = 0; i < 20; i++)
                        {
                            mas[g[i]] = ia[i];
                        }
                        
                        strImg = string.Join(" ", g);
                        stIm = string.Join(" ", ia);
                        
                        ImageDate id = new ImageDate();
                        imgFromStream = id.RetIm(mas);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = imgFromStream;
                        
                        textBox11.Enabled = true;
                        textBox12.Enabled = true;
                        textBox4.Enabled = true;
                        textBox3.Enabled = true;
                        ШифруватиІнф.Enabled = true;
                        ЗвантажитиЗображення.Enabled = true;
                        ЗавантажитиФайл.Enabled = true;
                        Зберигти.Enabled = true;
                    }
                    else
                        MessageBox.Show("Заповніть поля ключів!");
                    
                }
            }
            
        }


        private void ЗавантажитиФайл_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(open_dialog.FileName);
                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }
                    sr.Close();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Неможливо відкрити документ",
                        "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            textBox4.Text = s;
            
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            ШифруватиІнф.Enabled = true;
            ЗавантажитиФайл.Enabled = false;
            ЗвантажитиЗображення.Enabled = false;
        }

        private void Зберигти_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length < 0 && pictureBox1.Image == null)
            {
                MessageBox.Show("Заповніть текстове поле, чи завантажте зображення!");
            }
            else
            {
                if (textBox3.Text.Length > 0)
                {
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Title = "Зберегти як...";
                    savedialog.OverwritePrompt = true;
                    savedialog.CheckPathExists = true;
                    savedialog.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            StreamWriter sw = new StreamWriter(savedialog.FileName);
                            foreach (string item in result)
                                sw.WriteLine(item);
                            sw.Close();
                            MessageBox.Show("Зашифрований текс збережено!");
                        }
                        catch
                        {
                            MessageBox.Show("Неможливо зберегти", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                if (pictureBox1.Image != null)
                {
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Title = "Зберегти зображення як...";
                    savedialog.OverwritePrompt = true;
                    savedialog.CheckPathExists = true;
                    savedialog.Filter = "Image Files(*.jpg)|*.jpg | All files (*.*)|*.*";
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            imgFromStream.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            MessageBox.Show("Зашифроване зображення збережено!");
                        }
                        catch
                        {
                            MessageBox.Show("Неможливо зберегти зображення", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    SaveFileDialog savedialog1 = new SaveFileDialog();
                    savedialog1.Title = "Зберегти як...";
                    savedialog1.OverwritePrompt = true;
                    savedialog1.CheckPathExists = true;
                    savedialog1.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
                    savedialog1.ShowHelp = true;
                    if (savedialog1.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            StreamWriter sw = new StreamWriter(savedialog1.FileName);
                            sw.WriteLine(strImg);
                            sw.WriteLine(stIm);
                            sw.Close();
                            MessageBox.Show("Допоміжну інформацію збережено!");
                        }
                        catch
                        {
                            MessageBox.Show("Неможливо зберегти", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void Дешифрування_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 0 && pictureBox2.Image == null)
            {
                MessageBox.Show("Заповніть текстове поле, чи завантажте зображення!");
            }
            else
            {
                if (textBox4.Text.Length > 0)
                {
                    if ((textBox13.Text.Length > 0) && (textBox14.Text.Length > 0))
                    {
                        textBox13.Enabled = false;
                        textBox14.Enabled = false;
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        button10.Enabled = false;
                        button6.Enabled = false;
                        button5.Enabled = false;
                        button11.Enabled = false;
                        
                        long d = Convert.ToInt64(textBox13.Text);
                        long n = Convert.ToInt64(textBox14.Text);

                        EncodeDecode ed = new EncodeDecode();
                        resalt = ed.RSA_Dedoce(input, d, n);

                        textBox2.Text = resalt;
                            
                        textBox13.Enabled = true;
                        textBox14.Enabled = true;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        button10.Enabled = true;
                        button6.Enabled = true;
                        button5.Enabled = true;
                        button11.Enabled = true;
                    }
                    else
                        MessageBox.Show("Заповніть поля ключів!");
                }
                if (pictureBox2.Image != null)
                {
                    if ((textBox13.Text.Length > 0) && (textBox14.Text.Length > 0))
                    {
                        textBox13.Enabled = false;
                        textBox14.Enabled = false;
                        textBox1.Enabled = false;
                        textBox2.Enabled = false;
                        button10.Enabled = false;
                        button6.Enabled = false;
                        button5.Enabled = false;
                        button11.Enabled = false;
                        
                        long d = Convert.ToInt64(textBox13.Text);
                        long n = Convert.ToInt64(textBox14.Text);

                        long[] ia = new long[20];
                        int[] ar = new int[20];
                        
                        ia = sI[0].Split(' ').Select(v => Convert.ToInt64(v)).ToArray();
                        int[] kj = sI[1].Split(' ').Select(v => Convert.ToInt32(v)).ToArray();
                        ar = ia.Select(i => (int)i).ToArray();
                        int[] ms = new int[20];
                        
                        for (int i = 0; i < 20; i++)
                        {
                            ms[i] = mas[ar[i]];
                        }
                        
                        string rlt = string.Join(" ", kj);
                        List<string> res = rlt.Split(' ').ToList();

                        EncodeDecode ed = new EncodeDecode();
                        resalt = ed.RSA_Dedoce(res, d, n);

                        int[] ai = resalt.Split(' ').Select(k => Convert.ToInt32(k)).ToArray();
                        
                        for (int i = 0; i < 20; i++)
                        {
                            mas[ar[i]] = ai[i]; 
                        }
                        
                        ImageDate id = new ImageDate();
                        imgFromStream = id.RetIm(mas);
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox2.Image = imgFromStream;
                            
                        textBox13.Enabled = true;
                        textBox14.Enabled = true;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        button10.Enabled = true;
                        button6.Enabled = true;
                        button5.Enabled = true;
                        button11.Enabled = true;
                    }
                    else
                        MessageBox.Show("Заповніть поля ключів!");
                }
            }
        }

        private void ЗберигтиДешифр_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 0 && pictureBox2.Image == null)
            {
                MessageBox.Show("Заповніть текстове поле, чи завантажте зображення!");
            }
            else
            {
                if (textBox2.Text.Length > 0)
                {
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Title = "Зберегти як...";
                    savedialog.OverwritePrompt = true;
                    savedialog.CheckPathExists = true;
                    savedialog.Filter = "Image Files(*.txt)|*.txt|All files (*.*)|*.*";
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            StreamWriter sw = new StreamWriter(savedialog.FileName);
                            sw.WriteLine(resalt);
                            sw.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Неможливо зберегти", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    MessageBox.Show("Дешифрований текс збережено!");
                }

                if (pictureBox2.Image != null)
                {
                    SaveFileDialog savedialog = new SaveFileDialog();
                    savedialog.Title = "Зберегти зображення як...";
                    savedialog.OverwritePrompt = true;
                    savedialog.CheckPathExists = true;
                    savedialog.Filter = "Image Files(*.jpg)|*.jpg | All files (*.*)|*.*";
                    savedialog.ShowHelp = true;
                    if (savedialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            imgFromStream.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                            MessageBox.Show("Зображення збережено!");
                        }
                        catch
                        {
                            MessageBox.Show("Неможливо зберегти зображення", "Помилка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void проАвторівToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Про_авторів newForm = new Про_авторів();
            newForm.Show();
        }


        private void Інструкція_Click(object sender, EventArgs e)
        {
            Інструкція newForm = new Інструкція();
            newForm.Show();
        }

        private void проАвторівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ПроАлгоритм newForm = new ПроАлгоритм();
            newForm.Show();
        }
    }
}
