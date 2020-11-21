using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using ClosedXML.Excel;

namespace WindowsFormsApp1
{
    public partial class main : Form
    {

        //Несколько Классов для упрощения создания Элементов Формы
        class CB
        {
            public int CBx;
            public int CBy;
            public int CBwidth;
            public List<String> CBitems;

            public CB(int x, int y, int width,List<String> items)
            {
                CBx = x;
                CBy = y;
                CBwidth = width;
                CBitems = items;
            }

            public ComboBox create()
            {
                ComboBox cb = new ComboBox();
                cb.Location = new Point(CBx, CBy);
                cb.Width = CBwidth;
                foreach(string item in CBitems)
                {
                    cb.Items.Add(item);
                }
                return cb;
            }
        }

        class Lab
        {
            public int Labx;
            public int Laby;
            public int Labwidth;
            public string Labtext;

            public Lab(int x, int y, string text, int width = 95)
            {
                Labx = x;
                Laby = y;
                Labwidth = width;
                Labtext = text;
            }

            public Label create()
            {
                Label lb = new Label();
                lb.Location = new Point(Labx, Laby);
                lb.Width = Labwidth;
                lb.Text = Labtext;
                return lb;
            }
        }

        class BTN
        {
            public int BTNx;
            public int BTNy;
            public int BTNwidth;
            public string BTNtext;

            public BTN(int x,int y,int width,string text)
            {
                BTNx = x;
                BTNy = y;
                BTNwidth = width;
                BTNtext = text;
            }

            public Button create()
            {
                Button btn = new Button();
                btn.Location = new Point(BTNx, BTNy);
                btn.Width = BTNwidth;
                btn.Text = BTNtext;
                return btn;
            }
        }

        class TB
        {
            public int TBx;
            public int TBy;
            public int TBwidth;
            public bool TBreadOnly;
            public string TBtext;

            public TB(int x, int y, string text="", bool readOnly = false, int width = 95)
            {
                TBx = x;
                TBy = y;
                TBtext = text;
                TBreadOnly = readOnly;
                TBwidth = width;
            }

            public TextBox create()
            {
                TextBox tb = new TextBox();
                tb.Location = new Point(TBx, TBy);
                tb.ReadOnly = TBreadOnly;
                tb.Text = TBtext;
                tb.Width = TBwidth;
                return tb;
            }

        }

        //Класс, для создания аккаунта 
        class Acount
        {
            public string Name { get; set; }
            public string Mail { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }

            public Acount(string n,string m,string l,string p)
            {
                Name = n;
                Mail = m;
                Login = l;
                Password = p;
            }

            public void Show()
            {
                MessageBox.Show($"{Name} {Mail} {Login} {Password}");
            }
        }

        //Некоторые статические значия, в основном используются для позиционирования Элементов на форме
        int labelsY = 75;
        int mainY = 100;
        int starterX = 30;
        int marginX = 25;

        CspParameters cspp = new CspParameters();

        //Массив Аккаунтов пользователя
        List<Acount> accounts = new List<Acount>();

        //Пароль для входа в приложение
        public string pass;

        public main()
        {
            InitializeComponent();
        }

        //Базовое заполнение Формы (State - просмотр паролей)
        //Создаются некоторые элементы (Combobox, Textbox, Label), которые будут заполняться нужными данными
        private void FillBasicForm()
        {
            ClearForm();


            List<String> Names = new List<string>();

                List<Acount> Res = JsonConvert.DeserializeObject<List<Acount>>(File.ReadAllText("acount.json"));
                foreach (var item in Res)
                {
                    Names.Add(Decrypt(item.Name));
                }
                MessageBox.Show("null");
            CB cb;
            cb = new CB(30, mainY, 95, Names);
            ComboBox Resourses = cb.create();
            this.Controls.Add(Resourses);
            Resourses.SelectedIndexChanged += new EventHandler(Resourses_selectResource);

            Lab lb;
            lb = new Lab(150, labelsY, "Адрес Электронной почты", 95);
            Label lbMail = lb.create();
            this.Controls.Add(lbMail);

            lb = new Lab(270, labelsY, "Логин");
            Label lbLogin = lb.create();
            this.Controls.Add(lbLogin);

            lb = new Lab(390, labelsY, "Пароль");
            Label lbPassword = lb.create();
            this.Controls.Add(lbPassword);


            TB tb;
            tb = new TB(150, mainY, "mail@mail.com", true);
            TextBox tbMail = tb.create();
            this.Controls.Add(tbMail);

            tb = new TB(270, mainY, "Login", true);
            TextBox tbLogin = tb.create();
            this.Controls.Add(tbLogin);
            tb = new TB(390, mainY, "Password", true);
            TextBox tbPassword = tb.create();
            this.Controls.Add(tbPassword);
            this.Text = "Хранилище паролей";
        }

        //Очищает все созданные нами элементы, кроме менюшки
        public void ClearForm()
        {
            accounts = JsonConvert.DeserializeObject<List<Acount>>(File.ReadAllText("acount.json"));
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textbox in tbs)
            {
                this.Controls.Remove(textbox);
                textbox.Dispose();
            }
            List<Label> lbs = this.Controls.OfType<Label>().ToList();
            foreach (Label label in lbs)
            {
                this.Controls.Remove(label);
                label.Dispose();
            }
            List<ComboBox> cbs = this.Controls.OfType<ComboBox>().ToList();
            foreach (ComboBox combobox in cbs)
            {
                this.Controls.Remove(combobox);
                combobox.Dispose();
            }
            List<Button> btns = this.Controls.OfType<Button>().ToList();
            foreach (Button button in btns)
            {
                this.Controls.Remove(button);
                button.Dispose();
            }
        }






        //При загрузке нашего приложения инстантно скрывается меню
        //Поле ввода пароля автоматически вводит * вместо символов
        private void main_Load(object sender, EventArgs e)
        {
            List<Acount> js = new List<Acount>();
            js.Add(new Acount(Encrypt("n"), Encrypt("n"), Encrypt("n"), Encrypt("n")));
            MessageBox.Show(File.ReadAllText("password.json"));
            File.WriteAllText("acount.json", JsonConvert.SerializeObject(js));
            menu.Hide();
            if (File.Exists("password.json"))
            {
                accessPassCopy.Dispose();
                pass = File.ReadAllText("password.json");
                cspp.KeyContainerName = File.ReadAllText("password.json");
                accessPass.TextChanged += new EventHandler(accessPass_TextChanged);
            }
            else
            {
                accessPassCopy.PasswordChar = '*';
                accessPassCopy.TextChanged += new EventHandler(accessPassCopy_TextChanged);
            }
            accessPass.PasswordChar = '*';
        }

        //Если при вводе главного пароля он совпадает с определнным в программе - пускаем в основное приложение
        private void accessPass_TextChanged(object sender, EventArgs e)
        {
            if (accessPass.Text == File.ReadAllText("password.json"))
            {
                menu.Show();
                FillBasicForm();
            }
        }

        private void accessPassCopy_TextChanged(object sender, EventArgs e)
        {
            if (accessPassCopy.Text == accessPass.Text)
            {
                menu.Show();
                File.WriteAllText("password.json", accessPass.Text);
                File.WriteAllText("acount.json", "");
                FillBasicForm();
            }
        }


        //При выборе какого-то аккаунта, все созданные элементы заполняются данными из соответствующего аккаунта массива, опрделенного в корне приложения
        private void Resourses_selectResource(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            tbs[0].Text = accounts[cb[0].SelectedIndex].Mail;
            tbs[1].Text = accounts[cb[0].SelectedIndex].Login;
            tbs[2].Text = accounts[cb[0].SelectedIndex].Password;
        }




        //При выборе вкладки "Добавить Аккаунт"
        //Создаются немного другие Элементы формы
        private void добавитьАккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();

            Lab lb;
            lb = new Lab(30, labelsY, "Введите Название ресурса", 95);
            Label lbAddResource = lb.create();
            this.Controls.Add(lbAddResource);
            lb = new Lab(150, labelsY, "Введите Адрес Электронной почты", 95);
            Label lbAddMail = lb.create();
            this.Controls.Add(lbAddMail);
            lb = new Lab(270, labelsY, "Введите Логин", 95);
            Label lbAddLogin = lb.create();
            this.Controls.Add(lbAddLogin);
            lb = new Lab(390, labelsY, "Введите Пароль", 95);
            Label lbAddPassword= lb.create();
            this.Controls.Add(lbAddPassword);


            TB tb;
            tb = new TB(30, mainY, "Название Ресурса");
            TextBox tbAddResource = tb.create();
            this.Controls.Add(tbAddResource);

            tb = new TB(150, mainY, "mail@mail.com");
            TextBox tbAddMail = tb.create();
            this.Controls.Add(tbAddMail);

            tb = new TB(270, mainY, "Login");
            TextBox tbAddLogin = tb.create();
            this.Controls.Add(tbAddLogin);

            tb = new TB(390, mainY, "Password");
            TextBox tbAddPassword = tb.create();
            this.Controls.Add(tbAddPassword);

            BTN btn;
            btn = new BTN(510, mainY, 95, "Добавить");
            Button btnAdd= btn.create();
            this.Controls.Add(btnAdd);
            btnAdd.Click += new EventHandler(btnAdd_Click);
        }

        //При добавлении Аккаунта, формируется новый объект класса Acount и добавляется в массив аккаунтов
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавленно!");
            List<string> values = new List<string>();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textbox in tbs)
            {
                values.Add(textbox.Text);
            }
            MessageBox.Show(Encrypt(values[2]));

            /*string JSONData = JsonConvert.SerializeObject(new Acount(values[0], values[1], values[2], values[3]));

            List<Acount> js = new List<Acount>();
            js.Add(new Acount(values[0], values[1], values[2], values[3]));
            js.Add(new Acount(values[0], values[1], values[2], values[3]));

            File.WriteAllText("acount.json", JsonConvert.SerializeObject(js));

            JsonConvert.DeserializeObject<List<Acount>>(File.ReadAllText("acount.json"))[0].Show();
            File.Delete("acount.json");
            var ac = JsonConvert.DeserializeObject<Acount>(JSONData);*/
            accounts.Add(new Acount(Encrypt(values[0]), Encrypt(values[1]), Encrypt(values[2]), Encrypt(values[3])));
            File.WriteAllText("acount.json", JsonConvert.SerializeObject(accounts));
        }




        //При выборе вкладки "Изменить Данные Аккаунта"
        //Создаются нужные нам элементы формы
        private void изменитьДанныеАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();

            List<String> Res = new List<string>();
            foreach (var item in accounts)
            {
                Res.Add(item.Name);
            }

            CB cb;
            cb = new CB(30, mainY, 95, Res);
            ComboBox Resourses = cb.create();
            Resourses.SelectedIndexChanged += new EventHandler(Resourses_selectResource);
            this.Controls.Add(Resourses);

            Lab lb;
            lb = new Lab(150, labelsY, "Адрес Электронной почты", 95);
            Label lbMail = lb.create();
            this.Controls.Add(lbMail);

            lb = new Lab(270, labelsY, "Логин");
            Label lbLogin = lb.create();
            this.Controls.Add(lbLogin);

            lb = new Lab(390, labelsY, "Пароль");
            Label lbPassword = lb.create();
            this.Controls.Add(lbPassword);


            TB tb;
            tb = new TB(150, mainY, "mail@mail.com");
            TextBox tbMail = tb.create();
            this.Controls.Add(tbMail);

            tb = new TB(270, mainY, "Login");
            TextBox tbLogin = tb.create();
            this.Controls.Add(tbLogin);

            tb = new TB(390, mainY, "Password");
            TextBox tbPassword = tb.create();
            this.Controls.Add(tbPassword);

            BTN btn;
            btn = new BTN(510, mainY, 95, "Изменить");
            Button btnChange= btn.create();
            btnChange.Click += new EventHandler(btnChange_Click);
            this.Controls.Add(btnChange);
            this.Text = "Хранилище паролей";
        }

        //При изменении данных об аккаунте, соответствующий объект массива перезаписывается в соответствии введенными в поля данными
        private void btnChange_Click(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            accounts[cb[0].SelectedIndex].Mail = Encrypt(tbs[0].Text);
            accounts[cb[0].SelectedIndex].Login = Encrypt(tbs[1].Text);
            accounts[cb[0].SelectedIndex].Password = Encrypt(tbs[2].Text);

            File.WriteAllText("acount.json", JsonConvert.SerializeObject(accounts));
        }





        //При выборе вкладки получить Данные Аккаунта 
        //Форма заполняется Также как и начальная стадия формы
        private void получитьДанныеАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillBasicForm();
        }

        public string Encrypt(string p)
        {
            byte[] byteContent;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            byteContent = rsa.Encrypt(toByte(p),false);
            return toString(byteContent);
        }

        public string Decrypt(string p)
        {
            byte[] byteContent;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048,cspp);
            MessageBox.Show(rsa.KeySize.ToString());
            byteContent = rsa.Decrypt(toByte(p), true);
            return toString(byteContent);
        }

        private static string toString(byte[] p)
        {
            return Encoding.UTF8.GetString(p);
        }

        private static byte[] toByte(string p)
        {
            return Encoding.UTF8.GetBytes(p);
        }

        private void учетнаяЗаписьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();

            TB tb = new TB(270, mainY);
            TextBox tbNewPassword = tb.create();
            this.Controls.Add(tbNewPassword);

            BTN btn = new BTN(250, mainY + 50, 95, "Изменить");
            Button btnChangePass = btn.create();
            this.Controls.Add(btnChangePass);
            btnChangePass.Click += new EventHandler(btnChangePass_Click);
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            List < TextBox > tbs= this.Controls.OfType<TextBox>().ToList();
            File.WriteAllText("password.json", tbs[0].Text);
        }
    }
}
