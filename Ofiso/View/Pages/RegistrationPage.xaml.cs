using Ofiso.AppData;
using Ofiso.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUser();
        }
        public void AddUser()
        {
            if (string.IsNullOrWhiteSpace(NameTb.Text) ||
                string.IsNullOrWhiteSpace(NumberTb.Text) ||
                string.IsNullOrWhiteSpace(EmailTb.Text) ||
                string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены!");
                return;
            }

            try
            {
                Users newUser = new Users()
                {
                    Fname = NameTb.Text,
                    Email = EmailTb.Text,
                    NumberPhone = NumberTb.Text,
                    Password = PasswordPb.Password
                };

                App.context.Users.Add(newUser);
                App.context.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");
                FrameHelper.mainFrame.Navigate(new LoginPage());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
