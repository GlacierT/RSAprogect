using System;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Інструкція : Form
    {
        public Інструкція()
        {
            InitializeComponent();
            
            string t = "";
             
            StreamReader sr = new StreamReader(@"C:\Users\User\Desktop\WindowsFormsApp1\Інструкція.txt");
             
            while (!sr.EndOfStream)
            {
                t = sr.ReadLine();
                textBox1.Text += t + Environment.NewLine;
            }
             
            sr.Close();
            
        }
    }
}