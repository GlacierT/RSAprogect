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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
       char[] characters = new char[] {'а',' ', 'б', 'в', 'г', 'ґ', 'д', 'е', 'є', 'ж', 'з', 'и', 'і', 'ї', 'й', 'к', 
           'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ь', 'ю', 'я', '-', ',',
           '.',  'А', 'Б', 'В', 'Г', 'Ґ', 'Д', 'Е', 'Є', 'Ж', 'З', 'И', 'І', 'Ї', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 
           'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ю', 'Я', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '!', '?' };
        
        
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
        
    private bool IsTheNumberSimple(long n)
        {
            if (n < 2)
                return false;
 
            if (n == 2)
                return true;
 
            for (long i = 2; i < n; i++)
                if (n % i == 0)
                    return false;
 
            return true;
        }
        
        private List<string> RSA_Endoce(string s, long e, long n)
        {
            List<string> result = new List<string>();
 
            BigInteger bi;
            
            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);
 
                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);
 
                BigInteger n_ = new BigInteger((int)n);
 
                bi = bi % n_;
 
                result.Add(bi.ToString());
            }
 
            return result;
        }
        
        private string RSA_Dedoce(List<string> input, long d, long n)
        {
            string result = "";
 
            BigInteger bi;
 
            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);
 
                BigInteger n_ = new BigInteger((int)n);
 
                bi = bi % n_;
 
                int index = Convert.ToInt32(bi.ToString());
 
                result += characters[index].ToString();
            }
 
            return result;
        }
        
        private long Calculate_e()
        {
            long d = 0;
            long[] mas = {3, 5, 7, 11, 17, 31};
 
            var ran = new Random();
            d = mas[ran.Next(0, 5)];

            return d;
            
        }
        
        private long Calculate_d(long d, long m)
        {
            long e = 10;
 
            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }
 
            return e;
        }
        
        private void домопогаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа візуального представлення RSA","О проекті", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel3.Visible = true;
            panel2.Visible = false;
            panel1.Visible = false;
            textBox11.Enabled = false;
            textBox12.Enabled = false;
            textBox3.Enabled = false;
            button8.Enabled = false;
            button7.Enabled = false;
        }

        private void textBox4_Enter(object sender, EventArgs e)
        {
            textBox4.Text = null;
            textBox4.ForeColor = Color.Black;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            textBox4.Text = "Введыть текст для шифрування";//подсказка
            textBox4.ForeColor = Color.Gray;
            textBox1.Text = "Введыть текст для розшифрування";//подсказка
            textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox1.ForeColor = Color.Black;
        }
        
        private void button9_Click(object sender, EventArgs e)
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
            var ms = new MemoryStream();
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            bytes = ms.ToArray();
            mas = bytes.Select(i =>(int) i).ToArray();

            textBox11.Enabled = true;
            textBox12.Enabled = true;
            button8.Enabled = true;
            button12.Enabled = false;
            button9.Enabled = false;
        }

        private void button10_Click(object sender, EventArgs e)
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
            //button11.Enabled = false;
        }

        private void button11_Click(object sender, EventArgs e)
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
            var ms = new MemoryStream();
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            }
            if (image.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            bytes = ms.ToArray();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox5.Text.Length > 0) && (textBox6.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox5.Text);
                long q = Convert.ToInt64(textBox6.Text);
                if (IsTheNumberSimple(p) && IsTheNumberSimple(q))
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    textBox5.Enabled = false;
                    textBox6.Enabled = false;

                    long n = p * q;
                    long m = (p - 1) * (q - 1);
                    long e_ = Calculate_e();
                    long d = Calculate_d(e_, m);

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
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
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

        private void button8_Click(object sender, EventArgs e)
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
                        button8.Enabled = false;
                        button9.Enabled = false;
                        button12.Enabled = false;
                        button7.Enabled = false;
                        
                        long e_ = Convert.ToInt64(textBox11.Text);
                        long n = Convert.ToInt64(textBox12.Text);

                        result = RSA_Endoce(s, e_, n);
                        

                        for (int i = 0; i < result.Count; i++)
                            textBox3.Text += result[i] + " ";
                        
                        textBox11.Enabled = true;
                        textBox12.Enabled = true;
                        textBox4.Enabled = true;
                        textBox3.Enabled = true;
                        button8.Enabled = true;
                        button9.Enabled = true;
                        button12.Enabled = true;
                        button7.Enabled = true;
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
                        button8.Enabled = false;
                        button9.Enabled = false;
                        button12.Enabled = false;
                        button7.Enabled = false;
                            
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
                        
                        result = RSA_Endoce(strImg, e_, n);

                        string som = string.Join(" ", result);
                        int[] ia = som.Split(' ').Select(w => Convert.ToInt32(w)).ToArray();

                        for (int i = 0; i < 20; i++)
                        {
                            mas[g[i]] = ia[i];
                        }
                        
                        strImg = string.Join(" ", g);
                        stIm = string.Join(" ", ia);
                        
                        byte[] b = mas.Select(i => (byte)i).ToArray();
                        var imageMemoryStream = new MemoryStream(b);
                        imgFromStream = Image.FromStream(imageMemoryStream);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Image = imgFromStream;
                        
                        textBox11.Enabled = true;
                        textBox12.Enabled = true;
                        textBox4.Enabled = true;
                        textBox3.Enabled = true;
                        button8.Enabled = true;
                        button9.Enabled = true;
                        button12.Enabled = true;
                        button7.Enabled = true;
                    }
                    else
                        MessageBox.Show("Заповніть поля ключів!");
                    
                }
            }
            
        }


        private void button12_Click(object sender, EventArgs e)
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
            button8.Enabled = true;
            button12.Enabled = false;
            button9.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
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

                        resalt = RSA_Dedoce(input, d, n);

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

                        resalt = RSA_Dedoce(res, d, n);

                        int[] ai = resalt.Split(' ').Select(k => Convert.ToInt32(k)).ToArray();
                        
                        for (int i = 0; i < 20; i++)
                        {
                            mas[ar[i]] = ai[i]; 
                        }
                        
                        byte[] b = mas.Select(i => (byte)i).ToArray();
                        var imageMemoryStream = new MemoryStream(b);
                        imgFromStream = Image.FromStream(imageMemoryStream);
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

        private void button6_Click(object sender, EventArgs e)
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
    }
}
