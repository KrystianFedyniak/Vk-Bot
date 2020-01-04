using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace VkBot
{
    public partial class Form1 : Form
    {

        Form AboutF = new About();
        Accounts accounts = new Accounts();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            CreateTxt("phrases.txt");


        }
        public void CreateTxt(string file2)
        {

            if (!File.Exists(file2))
            {
                File.Create(file2).Close();
            }
            accounts.Auth();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            Message m = Message.Create(base.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (Log.tuPush.Count != 0)
            {
                textBox1.Text = $"{Log.tuPush.Dequeue()}\r\n{textBox1.Text}";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Accounts.StartStop == true)
            {
                Accounts.StartStop = false;
                button1.Text = "Start";
                button1.BackColor = Color.Black;
                Log.Push("Отправка сообщений приостановлена");
            }
            else
            {
                Accounts.StartStop = true;
                button1.Text = "Stop";
                button1.BackColor = Color.DarkRed;


                accounts.SelfLoader(dataGridView1.Rows);
                accounts.delayFlooder = (int)numericUpDown1.Value;
                accounts.AsynsWorker();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutF.ShowDialog();
        }
    }
}
