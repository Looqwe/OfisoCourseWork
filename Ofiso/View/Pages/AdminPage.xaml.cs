using Ofiso.Models;
using Ofiso.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity;
using System.Globalization;

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private ObservableCollection<Users> _users;
        private Users _currentUser;

        public AdminPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new Entities())
            {
                _users = new ObservableCollection<Users>(
                    context.Users
                        .Include(u => u.Offices) // Важно!
                        .Include(u => u.UserType1)
                        .ToList()
                );
                UsersList.ItemsSource = _users;
            }
        }

        private void DeleteUser_Click_1(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedItem is Users selectedUser)
            {
                // Используем правильное имя свойства ID
                if (selectedUser.ID == App.currentUser?.ID)
                {
                    MessageBox.Show("ХАХАХАХА ну ты и бибизян себя удалить низя)");
                    return;
                }

                var confirm = MessageBox.Show($"Удалить {selectedUser.Fname}?", "Подтвердите", MessageBoxButton.YesNo);
                if (confirm == MessageBoxResult.Yes)
                {
                    using (var context = new Entities())
                    {
                        var userToDelete = context.Users.Find(selectedUser.ID);
                        if (userToDelete != null)
                        {
                            context.Users.Remove(userToDelete);
                            context.SaveChanges();
                            _users.Remove(selectedUser);
                        }
                    }
                }
            }
        }

        private void ChangePassword_Click_1(object sender, RoutedEventArgs e)
        {
            if (UsersList.SelectedItem is Users selectedUser)
            {
                var changePassWindow = new ChangePasswordAdminWindow(selectedUser.ID);
                changePassWindow.ShowDialog();

                // Обновляем список пользователей
                LoadUsers();
            }
        }
    }
}
