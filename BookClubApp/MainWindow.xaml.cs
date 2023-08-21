using BookClubApp.Class;
using BookClubApp.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using BookClubApp.Connection;

namespace BookClubApp
{
    public partial class MainWindow : Window
    {
        DataBase dataBase = new DataBase();
        public static int statusBtn = 0; // 0 - log, 1 - reg

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e) { // Кнопка зарегистрировать или войти в профиль
            var login = fieldLogin.Text;
            var password = Hash.hashPassword(fieldPassword.Password);
            
            if (login == "") { MessageBox.Show("Вы не ввели в поле свой логин.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }
            if (password == "") { MessageBox.Show("Вы не ввели в поле свой пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return; }

            if (statusBtn == 0) { // log
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string query = $"SELECT * FROM Clients WHERE Login = '{login}' AND Password = '{password}'";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count == 1) {
                    string roleName;
                    //var user = new checkUser(table.Rows[0].ItemArray[1]);
                    if (table.Rows[0].ItemArray[3].ToString() == "1")
                    {
                        Databank.pId = table.Rows[0].ItemArray[0].ToString();
                        Databank.pLogin = login;
                        Databank.pPassword = password;
                        Databank.pRole = "Гость";
                        roleName = "Гость";
                        MessageBox.Show($"{login}, вы успешно вошли как {roleName}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (table.Rows[0].ItemArray[3].ToString() == "2")
                    {
                        Databank.pId = table.Rows[0].ItemArray[0].ToString();
                        Databank.pLogin = login;
                        Databank.pPassword = password;
                        Databank.pRole = "Клиент";
                        roleName = "Клиент";
                        MessageBox.Show($"{login}, вы успешно вошли как {roleName}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (table.Rows[0].ItemArray[3].ToString() == "3")
                    {
                        Databank.pId = table.Rows[0].ItemArray[0].ToString();
                        Databank.pLogin = login;
                        Databank.pPassword = password;
                        Databank.pRole = "Менеджер";
                        roleName = "Менеджер";
                        MessageBox.Show($"{login}, вы успешно вошли как {roleName}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    if (table.Rows[0].ItemArray[3].ToString() == "4")
                    {
                        Databank.pId = table.Rows[0].ItemArray[0].ToString();
                        Databank.pLogin = login;
                        Databank.pPassword = password;
                        Databank.pRole = "Администратор";
                        roleName = "Администратор";
                        MessageBox.Show($"{login}, вы успешно вошли как {roleName}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        MenuAdminWindow menuAdminWindow = new MenuAdminWindow();
                        this.Hide();
                        menuAdminWindow.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Вы не вошли в свой профиль, так как не указали правильно свой логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                return;
            }
            else if (statusBtn == 1) { // reg
                if (checker() != false) { return; }
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string query = $"INSERT INTO Clients (Login, Password, RoleId) VALUES ('{login}', '{password}', '1')";
                SqlCommand command = new SqlCommand(query, dataBase.getConnection());
                dataBase.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Вы успешно зарегистрировали аккаунт.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    fieldLogin.Text = "";
                    fieldPassword.Password = "";
                }
                else
                {
                    MessageBox.Show("Вы не вошли в свой профиль, так как не указали правильно свой логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataBase.closeConnection();
                return;
            }

        }

        private Boolean checker() // проверка на уникальность логина
        {
            var login = fieldLogin.Text;
            //var password = Hash.hashPassword(fieldPassword.Password);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string query = $"SELECT * FROM Clients WHERE Login = '{login}'";
            SqlCommand command = new SqlCommand(query, dataBase.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0) {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            else { return false; }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e) { // кнопка зарегистрироваться или авторизироваться (не в профиль)

            if (statusBtn == 0) {
                statusBtn = 1;
                btnRegister.Content = "Авторизироваться";
                btnLogin.Content = "Зарегистрировать";
                fieldLogin.Text = "";
                fieldPassword.Password = "";
            }
            else if (statusBtn == 1) {
                statusBtn = 0;
                btnRegister.Content = "Зарегистрироваться";
                btnLogin.Content = "Войти в профиль";
                fieldLogin.Text = "";
                fieldPassword.Password = "";
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e) { // гость
            MessageBox.Show("В данный момент времени данная система находится в стадии разработки.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e) { // кнопка выхода из приложения
            Application.Current.Shutdown();
        }

        private void Window_Closed(object sender, EventArgs e) { // кнопка выхода из приложения через системную кнопку
            Application.Current.Shutdown();
        }
    }
}
