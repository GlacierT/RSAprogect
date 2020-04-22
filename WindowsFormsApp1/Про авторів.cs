using System;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Про_авторів : Form
    {
        public Про_авторів()
        {
            InitializeComponent();
            
            string t = "";
             
            StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\WindowsFormsApp1\про_авторів.txt");
             
            while (!sr.EndOfStream)
            {
                t = sr.ReadLine();
                textBox1.Text += t + Environment.NewLine;
            }
             
            sr.Close();

            textBox1.Enabled = false;
        }
        
    }
}