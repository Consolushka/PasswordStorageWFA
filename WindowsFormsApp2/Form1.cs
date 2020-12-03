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
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp2
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        //Несколько Классов для упрощения создания Элементов Формы
        class CB
        {
            public int CBx;
            public int CBy;
            public int CBwidth;
            public List<String> CBitems;

            public CB(int x, int y, int width, List<String> items)
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
                foreach (string item in CBitems)
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

            public BTN(int x, int y, int width, string text)
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

            public TB(int x, int y, string text = "", bool readOnly = false, int width = 95)
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
        class Account
        {
            public string Name { get; set; }
            public string Mail { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }

            public Account(string n, string m, string l, string p)
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
        List<Account> accounts = new List<Account>();

        //Пароль для входа в приложение
        public string pass;

        //Базовое заполнение Формы (State - просмотр паролей)
        //Создаются некоторые элементы (Combobox, Textbox, Label), которые будут заполняться нужными данными
        private void FillBasicForm()
        {
            ClearForm();


            CB cb;
            List<String> Names = new List<string>();
            if (accounts == null)
            {

                MessageBox.Show("You have got no accounts yet");
                cb = new CB(30, mainY, 150, Names);
            }
            else
            {
                foreach (var item in accounts)
                {
                    Names.Add(Decrypt(item.Name, pass));
                }
                cb = new CB(30, mainY, 150, Names);
            }
            ComboBox Resourses = cb.create();
            this.Controls.Add(Resourses);
            Resourses.SelectedIndexChanged += new EventHandler(Resourses_selectResource);

            Lab lb;
            lb = new Lab(30, labelsY, "Resource Name", 150);
            Label lbRes = lb.create();
            this.Controls.Add(lbRes);

            lb = new Lab(200, labelsY, "Mail", 180);
            Label lbMail = lb.create();
            this.Controls.Add(lbMail);

            lb = new Lab(410, labelsY, "Login");
            Label lbLogin = lb.create();
            this.Controls.Add(lbLogin);

            lb = new Lab(535, labelsY, "Password");
            Label lbPassword = lb.create();
            this.Controls.Add(lbPassword);


            TB tb;
            tb = new TB(200, mainY, "mail@mail.com", true, 180);
            TextBox tbMail = tb.create();
            this.Controls.Add(tbMail);

            tb = new TB(410, mainY, "Login", true);
            TextBox tbLogin = tb.create();
            this.Controls.Add(tbLogin);
            tb = new TB(535, mainY, "Password", true);
            TextBox tbPassword = tb.create();
            this.Controls.Add(tbPassword);
            this.Text = "Password Storage";
        }

        //Очищает все созданные нами элементы, кроме менюшки
        public void ClearForm()
        {
            accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("accounts.json"));
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

        private void main_Load(object sender, EventArgs e)
        {
            menu.Hide();
            if (File.Exists("login.json"))
            {
                Account acc = JsonConvert.DeserializeObject<Account>(File.ReadAllText("login.json"));
                string login = Decrypt(acc.Login, acc.Password);
                string password = Decrypt(acc.Password, login);
                pass = password;
                accessPassCopy.Dispose();
                loginTB.Dispose();
                label1.Dispose();
                label2.Dispose();
                label3.Dispose();
                accounts = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText("accounts.json"));
                accessPass.TextChanged += new EventHandler(accessPass_TextChanged);
            }
            else
            {
                File.WriteAllText("accounts.json", "");
                accessPassCopy.PasswordChar = '*';
                accessPassCopy.TextChanged += new EventHandler(accessPassCopy_TextChanged);
            }
            accessPass.PasswordChar = '*';
        }

        //Если при вводе главного пароля он совпадает с определнным в программе - пускаем в основное приложение
        private void accessPass_TextChanged(object sender, EventArgs e)
        {
            if (accessPass.Text == pass)
            {
                menu.Show();
                FillBasicForm();
            }
        }

        private void accessPassCopy_TextChanged(object sender, EventArgs e)
        {
            if (accessPassCopy.Text == accessPass.Text && loginTB.Text != "")
            {
                menu.Show();
                string password = Encrypt(accessPass.Text, loginTB.Text);
                string login = Encrypt(loginTB.Text, password);
                Account acc = new Account("", "", login, password);
                File.WriteAllText("login.json", JsonConvert.SerializeObject(acc));
                File.WriteAllText("accounts.json", "");
                pass = accessPass.Text;
                FillBasicForm();
            }
        }


        //При выборе какого-то аккаунта, все созданные элементы заполняются данными из соответствующего аккаунта массива, опрделенного в корне приложения
        private void Resourses_selectResource(object sender, EventArgs e)
        {
                List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
                List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
                tbs[0].Text = Decrypt(accounts[cb[0].SelectedIndex].Mail, pass);
                tbs[1].Text = Decrypt(accounts[cb[0].SelectedIndex].Login, pass);
                tbs[2].Text = Decrypt(accounts[cb[0].SelectedIndex].Password, pass);
        }




        //При выборе вкладки "Добавить Аккаунт"
        //Создаются немного другие Элементы формы


        //При добавлении Аккаунта, формируется новый объект класса Acount и добавляется в массив аккаунтов
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Added!");
            List<string> values = new List<string>();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            foreach (TextBox textbox in tbs)
            {
                values.Add(textbox.Text);
                textbox.Text = "";
            }
            if (accounts == null)
            {
                accounts = new List<Account>();
            }
            accounts.Add(new Account(Encrypt(values[0], pass), Encrypt(values[1], pass), Encrypt(values[2], pass), Encrypt(values[3], pass)));
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(accounts));

        }




        //При выборе вкладки "Изменить Данные Аккаунта"
        //Создаются нужные нам элементы формы

        //При изменении данных об аккаунте, соответствующий объект массива перезаписывается в соответствии введенными в поля данными
        private void btnChange_Click(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();

            accounts[cb[0].SelectedIndex].Mail = Encrypt(tbs[0].Text, pass);
            accounts[cb[0].SelectedIndex].Login = Encrypt(tbs[1].Text, pass);
            accounts[cb[0].SelectedIndex].Password = Encrypt(tbs[2].Text, pass);
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(accounts));
            MessageBox.Show("Data Of <" + cb[0].SelectedItem + "> has been Changed");
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            List<ComboBox> cb = this.Controls.OfType<ComboBox>().ToList();
            accounts.RemoveAt(cb[0].SelectedIndex);
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(accounts));
            MessageBox.Show("Data of <" + cb[0].SelectedItem + "> has been Removed");
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            foreach (TextBox tb in tbs)
            {
                tb.Text = "";
            }
            cb[0].Items.RemoveAt(cb[0].SelectedIndex);
        }




        //При выборе вкладки получить Данные Аккаунта 
        //Форма заполняется Также как и начальная стадия формы






        private void btnChangePass_Click(object sender, EventArgs e)
        {
            List<TextBox> tbs = this.Controls.OfType<TextBox>().ToList();
            if (tbs[0].Text == tbs[1].Text)
            {
                Account acc = JsonConvert.DeserializeObject<Account>(File.ReadAllText("login.json"));
                string login = Decrypt(acc.Login, acc.Password);
                string password = Decrypt(acc.Password, login);
                acc.Password = Encrypt(tbs[0].Text, login);
                acc.Login = Encrypt(login, acc.Password);
                File.WriteAllText("login.json", JsonConvert.SerializeObject(acc));
                RewriteAccs(tbs[0].Text);
                pass = tbs[0].Text;
                MessageBox.Show("Password is Changed!");
            }
            else
            {
                MessageBox.Show("There password Doesn't Match!");
            }
        }


        public void RewriteAccs(string newPass)
        {
            foreach (Account acc in accounts)
            {
                acc.Login = Encrypt(Decrypt(acc.Login, pass), newPass);
                acc.Name = Encrypt(Decrypt(acc.Name, pass), newPass);
                acc.Password = Encrypt(Decrypt(acc.Password, pass), newPass);
                acc.Mail = Encrypt(Decrypt(acc.Mail, pass), newPass);
            }
            File.WriteAllText("accounts.json", JsonConvert.SerializeObject(accounts));
        }

        public static string Encrypt(string str, string keyCrypt)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        ////// Расшифроывает данные из строки value
        public static string Decrypt(string str, string keyCrypt)
        {
            string Result;
            try
            {
                CryptoStream Cs = InternalDecrypt(Convert.FromBase64String(str), keyCrypt);
                StreamReader Sr = new StreamReader(Cs);
                Result = Sr.ReadToEnd();
                Cs.Close();
                Cs.Dispose();
                Sr.Close();
                Sr.Dispose();
            }
            catch (CryptographicException)
            {
                return null;
            }
            return Result;
        }

        private static byte[] Encrypt(byte[] key, string value)

        {

            SymmetricAlgorithm Sa = Rijndael.Create();

            ICryptoTransform Ct = Sa.CreateEncryptor((new PasswordDeriveBytes(value, null)).GetBytes(16), new byte[16]);



            MemoryStream Ms = new MemoryStream();

            CryptoStream Cs = new CryptoStream(Ms, Ct, CryptoStreamMode.Write);



            Cs.Write(key, 0, key.Length);

            Cs.FlushFinalBlock();



            byte[] Result = Ms.ToArray();



            Ms.Close();

            Ms.Dispose();



            Cs.Close();

            Cs.Dispose();



            Ct.Dispose();



            return Result;

        }

        private static CryptoStream InternalDecrypt(byte[] key, string value)

        {

            SymmetricAlgorithm sa = Rijndael.Create();

            ICryptoTransform ct = sa.CreateDecryptor((new PasswordDeriveBytes(value, null)).GetBytes(16), new byte[16]);



            MemoryStream ms = new MemoryStream(key);

            return new CryptoStream(ms, ct, CryptoStreamMode.Read);

        }

        private void addAccMenu_Click(object sender, EventArgs e)
        {
            ClearForm();

            Lab lb;
            lb = new Lab(30, labelsY, "Enter Resource Name", 150);
            Label lbAddResource = lb.create();
            this.Controls.Add(lbAddResource);
            lb = new Lab(190, labelsY, "Enter Mail", 95);
            Label lbAddMail = lb.create();
            this.Controls.Add(lbAddMail);
            lb = new Lab(300, labelsY, "Enter Login", 95);
            Label lbAddLogin = lb.create();
            this.Controls.Add(lbAddLogin);
            lb = new Lab(420, labelsY, "Enter Password", 95);
            Label lbAddPassword = lb.create();
            this.Controls.Add(lbAddPassword);


            TB tb;
            tb = new TB(30, mainY, "", false, 150);
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
            btn = new BTN(540, mainY, 95, "Add");
            Button btnAdd = btn.create();
            this.Controls.Add(btnAdd);
            btnAdd.Click += new EventHandler(btnAdd_Click);
        }

        private void AccPasswordStorageMenu_Click(object sender, EventArgs e)
        {

            ClearForm();

            TB tb = new TB(310, mainY - 50, "", false, 150);
            TextBox tbNewPassword = tb.create();
            tbNewPassword.PasswordChar = '*';
            tbNewPassword.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(tbNewPassword);
            tb = new TB(310, mainY - 25, "", false, 150);
            TextBox tbNewPassCopy = tb.create();
            tbNewPassCopy.PasswordChar = '*';
            tbNewPassCopy.TextAlign = HorizontalAlignment.Center;
            this.Controls.Add(tbNewPassCopy);

            BTN btn = new BTN(310, mainY + 25, 150, "Change");
            Button btnChangePass = btn.create();
            this.Controls.Add(btnChangePass);
            btnChangePass.Click += new EventHandler(btnChangePass_Click);
        }

        private void GetAccDataMenu_Click(object sender, EventArgs e)
        {
            FillBasicForm();
        }

        private void ChangeAccDataMenu_Click(object sender, EventArgs e)
        {

            ClearForm();


            List<String> Names = new List<string>();
            if (accounts == null)
            {

                MessageBox.Show("You have not accounts yet");
                Names.Add("");
            }
            else
            {
                foreach (var item in accounts)
                {
                    Names.Add(Decrypt(item.Name, pass));
                }
            }

            CB cb;
            cb = new CB(30, mainY, 95, Names);
            ComboBox Resourses = cb.create();
            Resourses.SelectedIndexChanged += new EventHandler(Resourses_selectResource);
            this.Controls.Add(Resourses);

            Lab lb;
            lb = new Lab(150, labelsY, "Mail", 95);
            Label lbMail = lb.create();
            this.Controls.Add(lbMail);

            lb = new Lab(270, labelsY, "Login");
            Label lbLogin = lb.create();
            this.Controls.Add(lbLogin);

            lb = new Lab(390, labelsY, "Password");
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
            btn = new BTN(500, mainY, 95, "Change");
            Button btnChange = btn.create();
            btnChange.Click += new EventHandler(btnChange_Click);
            this.Controls.Add(btnChange);
            btn = new BTN(610, mainY, 95, "Remove");
            Button btnRemove = btn.create();
            this.Controls.Add(btnRemove);
            btnRemove.Click += new EventHandler(btnRemove_Click);
            this.Text = "Password Storage";
        }
    }
}
