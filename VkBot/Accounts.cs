using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VkBot
{
    public class Accounts
    {
        public VkApi _api = new VkApi();
        List<Flooder> floods = new List<Flooder>();
        public int delayFlooder { get; set; }
        public static bool StartStop { get; set; }
        Form loginF = new LoginForm();


        public void Auth()
        {

            var data = File.ReadAllLines("accounts.dll");
            foreach (var acc in data)
            {
                var login = acc.Split(':')[0];
                var pass = acc.Split(':')[1];

                try
                {

                    _api.Authorize(new ApiAuthParams()
                    {
                        Login = login,
                        Password = pass,
                        ApplicationId = 2685278

                    });
                }
                catch (Exception)
                {

                    DialogResult result =
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    if (result == DialogResult.Retry)
                    {

                        File.Delete("Accounts.dll");
                        loginF.ShowDialog();


                    }
                }

                if (_api.IsAuthorized)
                    Log.Push("Авторизация прошла успешно");
                else
                    Log.Push("Ошибка авторизации");


            }
        }

        public void SelfLoader(DataGridViewRowCollection rows)
        {
            try
            {
                floods.Clear();
                foreach (DataGridViewRow dataGridView in rows)
                {
                    if (dataGridView.Cells[1].Value != null)
                        floods.Add(new Flooder()
                        {
                            link = dataGridView.Cells[0].Value.ToString(),
                            about = dataGridView.Cells[1].Value.ToString()
                        });
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Заполните все поля", "Ошибка");
            }
        }
        public void Flooders(int index)
        {

            var phrases = File.ReadAllText("phrases.txt");
            var target = floods[index].link;
            var about = floods[index].about;
            var users = new Regex("im\\?sel=([0-9]+)").Match(target);

            if (users.Success)
            {
                _api.Messages.Send(new MessagesSendParams()
                {
                    UserId = long.Parse(users.Groups[1].Value),
                    RandomId = new Random().Next(),
                    Message = about + phrases
                });

                Log.Push("Сообщение отправлено");
            }
        }

        public void Worker()
        {
            try
            {

                int index = 0;
                while (StartStop)
                {
                    index = (index - 1) % floods.Count;
                    Flooders(index);
                    Thread.Sleep(delayFlooder);
                }
            }
            catch (DivideByZeroException)
            {
                DialogResult result = MessageBox.Show("Заполните поля Ссылка на цель и Обращение",
                "Пустые поля", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    StartStop = true;
                }
            }
        }

        public void AsynsWorker() =>
          new Thread(new ThreadStart(Worker)) { IsBackground = true }.Start();




    }
}
