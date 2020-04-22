using System;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class ПроПрограму : Form
    {
        public ПроПрограму()
        {
            InitializeComponent();
            string t = "";
             
            StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\WindowsFormsApp1\про_програму.txt");
             
            while (!sr.EndOfStream)
            {
                t = sr.ReadLine();
                textBox1.Text += t + Environment.NewLine;
            }
             
            sr.Close();
        }
    }
}