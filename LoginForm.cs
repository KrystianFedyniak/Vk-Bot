using System;
using System.IO;
using System.Windows.Forms;

namespace VkBot
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!File.Exists("accounts.dll"))
            {
                Application.Exit();
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checker();
            File.Create("accounts.dll").Close();
            File.AppendAllText("accounts.dll", textBox1.Text + ":" + textBox2.Text);
            Form form1 = new Form1();
            form1.Show();
            this.Close();
        }



        public void checker()
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Введите логин", "Пустое поле");
            }
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Введите пароль", "Пустое поле");
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                button1_Click(button1, null);
            }
        }
    }
}
