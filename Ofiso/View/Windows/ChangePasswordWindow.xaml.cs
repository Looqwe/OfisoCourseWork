using Ofiso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ofiso.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private readonly int _userId;
        private readonly Entities _context = new Entities();
        public ChangePasswordWindow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(OldPasswordBox.Password))
        {
                MessageBox.Show("Введите старый пароль!");
                return;
            }

            if (string.IsNullOrWhiteSpace(NewPasswordBox.Password))
            {
                MessageBox.Show("Введите новый пароль!");
                return;
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.ID == _userId);

                if (user == null)
                {
                    MessageBox.Show("Пользователь не найден!");
                    return;
                }

                if (user.Password != OldPasswordBox.Password)
                {
                    MessageBox.Show("Старый пароль неверен!");
                    return;
                }

                user.Password = NewPasswordBox.Password;
                _context.SaveChanges();

                MessageBox.Show("Пароль успешно изменен!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении пароля: {ex.Message}");
            }
        }
    }
}
