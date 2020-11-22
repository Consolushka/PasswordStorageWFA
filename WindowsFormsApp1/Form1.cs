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


            CB cb;
            List<String> Names = new List<string>();
            if (accounts == null)
            {

                MessageBox.Show("У вас еще нет аккаунтов");
                Names.Add("");
            }
            else
            {
                foreach (var item in accounts)
                {
                    Names.Add(item.Name);
                }
            }
            cb = new CB(30, mainY, 150, Names);
            ComboBox Resourses = cb.create();
            this.Controls.Add(Resourses);
            Resourses.SelectedIndexChanged += new EventHandler(Resourses_selectResource);

            Lab lb;
            lb = new Lab(30, labelsY, "Название Ресурса", 150);
            Label lbRes = lb.create();
            this.Controls.Add(lbRes);

            lb = new Lab(200, labelsY, "Адрес Электронной почты", 180);
            Label lbMail = lb.create();
            this.Controls.Add(lbMail);

            lb = new Lab(410, labelsY, "Логин");
            Label lbLogin = lb.create();
            this.Controls.Add(lbLogin);

            lb = new Lab(535, labelsY, "Пароль");
            Label lbPassword = lb.create();
            this.Controls.Add(lbPassword);


            TB tb;
            tb = new TB(200, mainY, "mail@mail.com", true,180);
            TextBox tbMail = tb.create();
            this.Controls.Add(tbMail);

            tb = new TB(410, mainY, "Login", true);
            TextBox tbLogin = tb.create();
            this.Controls.Add(tbLogin);
            tb = new TB(535, mainY, "Password", true);
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
            menu.Hide();
            if (File.Exists("password.json"))
            {
                accessPassCopy.Dispose();
                pass = File.ReadAllText("password.json");
                cspp.KeyContainerName = File.ReadAllText("password.json");
                accounts = JsonConvert.DeserializeObject<List<Acount>>(File.ReadAllText("acount.json"));
                accessPass.TextChanged += new EventHandler(accessPass_TextChanged);
            }
            else
            {
                File.WriteAllText("acount.json", "");
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
            if(accounts == null)
            {
                MessageBox.Show("Это была просто заглушка :):):):)");
            }
            else
            {
                List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
                List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
                tbs[0].Text = accounts[cb[0].SelectedIndex].Mail;
                tbs[1].Text = accounts[cb[0].SelectedIndex].Login;
                tbs[2].Text = accounts[cb[0].SelectedIndex].Password;
            }
        }




        //При выборе вкладки "Добавить Аккаунт"
        //Создаются немного другие Элементы формы
        private void добавитьАккаунтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();

            Lab lb;
            lb = new Lab(30, labelsY, "Введите Название ресурса", 150);
            Label lbAddResource = lb.create();
            this.Controls.Add(lbAddResource);
            lb = new Lab(190, labelsY, "Введите Адрес Электронной почты", 95);
            Label lbAddMail = lb.create();
            this.Controls.Add(lbAddMail);
            lb = new Lab(300, labelsY, "Введите Логин", 95);
            Label lbAddLogin = lb.create();
            this.Controls.Add(lbAddLogin);
            lb = new Lab(420, labelsY, "Введите Пароль", 95);
            Label lbAddPassword= lb.create();
            this.Controls.Add(lbAddPassword);


            TB tb;
            tb = new TB(30, mainY, "",false,150);
            TextBox tbAddResource = tb.create();
            this.Controls.Add(tbAddResource);

            tb = new TB(190, mainY, "");
            TextBox tbAddMail = tb.create();
            this.Controls.Add(tbAddMail);

            tb = new TB(300, mainY, "");
            TextBox tbAddLogin = tb.create();
            this.Controls.Add(tbAddLogin);

            tb = new TB(420, mainY, "");
            TextBox tbAddPassword = tb.create();
            this.Controls.Add(tbAddPassword);

            BTN btn;
            btn = new BTN(540, mainY, 95, "Добавить");
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
                textbox.Text = "";
            }

            /*string JSONData = JsonConvert.SerializeObject(new Acount(values[0], values[1], values[2], values[3]));

            List<Acount> js = new List<Acount>();
            js.Add(new Acount(values[0], values[1], values[2], values[3]));
            js.Add(new Acount(values[0], values[1], values[2], values[3]));

            File.WriteAllText("acount.json", JsonConvert.SerializeObject(js));

            JsonConvert.DeserializeObject<List<Acount>>(File.ReadAllText("acount.json"))[0].Show();
            File.Delete("acount.json");
            var ac = JsonConvert.DeserializeObject<Acount>(JSONData);
            
            accounts.Add(new Acount(Encrypt(values[0]), Encrypt(values[1]), Encrypt(values[2]), Encrypt(values[3])));*/
            if (accounts == null)
            {
                accounts = new List<Acount>();
            }
            accounts.Add(new Acount(values[0], values[1], values[2], values[3]));
            File.WriteAllText("acount.json", JsonConvert.SerializeObject(accounts));

        }




        //При выборе вкладки "Изменить Данные Аккаунта"
        //Создаются нужные нам элементы формы
        private void изменитьДанныеАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearForm();


            List<String> Names = new List<string>();
            if (accounts == null)
            {

                MessageBox.Show("У вас еще нет аккаунтов");
                Names.Add("");
            }
            else
            {
                foreach (var item in accounts)
                {
                    Names.Add(item.Name);
                }
            }

            CB cb;
            cb = new CB(30, mainY, 95, Names);
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
            btn = new BTN(500, mainY, 95, "Изменить");
            Button btnChange= btn.create();
            btnChange.Click += new EventHandler(btnChange_Click);
            this.Controls.Add(btnChange);
            btn = new BTN(610, mainY, 95, "Удалить");
            Button btnRemove = btn.create();
            this.Controls.Add(btnRemove);
            btnRemove.Click += new EventHandler(btnRemove_Click);
            this.Text = "Хранилище паролей";
        }

        //При изменении данных об аккаунте, соответствующий объект массива перезаписывается в соответствии введенными в поля данными
        private void btnChange_Click(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            /*
            accounts[cb[0].SelectedIndex].Mail = Encrypt(tbs[0].Text);
            accounts[cb[0].SelectedIndex].Login = Encrypt(tbs[1].Text);
            accounts[cb[0].SelectedIndex].Password = Encrypt(tbs[2].Text);*/

            accounts[cb[0].SelectedIndex].Mail = tbs[0].Text;
            accounts[cb[0].SelectedIndex].Login =tbs[1].Text;
            accounts[cb[0].SelectedIndex].Password = tbs[2].Text;
            File.WriteAllText("acount.json", JsonConvert.SerializeObject(accounts));
            MessageBox.Show("Данные Ресурса <"+ cb[0].SelectedItem+ "> Изменены");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            accounts.RemoveAt(cb[0].SelectedIndex);
            File.WriteAllText("acount.json", JsonConvert.SerializeObject(accounts));
            MessageBox.Show("Данные Ресурса <" + cb[0].SelectedItem + "> Удалены");
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            foreach(TextBox tb in tbs)
            {
                tb.Text = "";
            }
            MessageBox.Show(cb[0].SelectedIndex.ToString());
            cb[0].Items.RemoveAt(cb[0].SelectedIndex);
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

            TB tb = new TB(310, mainY-50,"",false,150);
            TextBox tbNewPassword = tb.create();
            tbNewPassword.PasswordChar = '*';
            tbNewPassword.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(tbNewPassword);
            tb = new TB(310, mainY - 25,"",false,150);
            TextBox tbNewPassCopy = tb.create();
            tbNewPassCopy.PasswordChar = '*';
            tbNewPassCopy.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(tbNewPassCopy);

            BTN btn = new BTN(310, mainY, 95, "Изменить");
            Button btnChangePass = btn.create();
            this.Controls.Add(btnChangePass);
            btnChangePass.Click += new EventHandler(btnChangePass_Click);
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            List < TextBox > tbs= this.Controls.OfType<TextBox>().ToList();
            if (tbs[0].Text == tbs[1].Text)
            {
                File.WriteAllText("password.json", tbs[0].Text);
                MessageBox.Show("Пароль изменен!");
            }
            else
            {
                MessageBox.Show("Введеные пароли не совпадают!");
            }
        }
    }
}
