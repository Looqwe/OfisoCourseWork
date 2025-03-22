using Ofiso.AppData;
using Ofiso.Models;
using Ofiso.View.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private Users _currentUser;
        private ObservableCollection<Offices> _userOffices = new ObservableCollection<Offices>();


        public UserPage(int userId)
        {
            InitializeComponent();
            LoadUserAndOffices(userId); // Объединяем загрузку данных

        }

        private void ChangePass_Click(object sender, RoutedEventArgs e)
        {

            if (_currentUser == null) return;

            var changePassWindow = new ChangePasswordWindow(_currentUser.ID);
            changePassWindow.ShowDialog();

            // Обновляем данные после изменения
            LoadUserAndOffices(_currentUser.ID);

        }

        private void LoadUserAndOffices(int userId)
        {
            try
            {
                using (var context = new Entities())
                {
                    // Загружаем пользователя с двумя коллекциями офисов
                    _currentUser = context.Users
                        .Include(u => u.Offices)    // Офисы, связанные через UserID
                        .FirstOrDefault(u => u.ID == userId);

                    if (_currentUser == null)
                    {
                        MessageBox.Show("Пользователь не найден!");
                        return;
                    }

                    DataContext = _currentUser;

                    // Объединяем обе коллекции офисов
                    var allUserOffices = _currentUser.Offices
                        .Concat(_currentUser.Offices)
                        .Distinct()
                        .ToList();

                    _userOffices = new ObservableCollection<Offices>(allUserOffices);
                    OfficesList.ItemsSource = _userOffices;

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}\n{ex.InnerException?.Message}");
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var office = button.DataContext as Offices;
            if (office == null) return;

            var detailsWindow = new OfficeDetailsUserWindow(office);
            detailsWindow.DataUpdated += () => LoadUserAndOffices(_currentUser.ID); // Подписываемся на обновление
            detailsWindow.Owner = Application.Current.MainWindow;
            detailsWindow.ShowDialog();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            // Очистить данные
            _currentUser = null;
            _userOffices.Clear();
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.SetBackButtonVisibility(true);
            FrameHelper.mainFrame.Navigate(new AuthorizationPage());
        }
    }
}
