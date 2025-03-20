using Ofiso.AppData;
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

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private ObservableCollection<Offices> _allOffices = new ObservableCollection<Offices>(); // Инициализация коллекции
        private string _currentSearch = "";
        private string _currentTypeFilter = "Все типы";
        private decimal _currentPriceFilter = 50000;
        private int _currentUserId; // Добавляем поле для хранения ID

        public MainPage(int userId)
        {
            InitializeComponent();
            _currentUserId = userId; // Сохраняем ID пользователя
            LoadDataFromDatabase(); // Сначала загрузка данных
            ApplyFilters(); // Затем применение фильтров

        }

        
        
        private void ApplyFilters()
        {
            var filtered = _allOffices
        .Where(o => o.Title.IndexOf(_currentSearch, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    o.Address.IndexOf(_currentSearch, StringComparison.OrdinalIgnoreCase) >= 0)
        
        .Where(o => o.PricePerMont <= _currentPriceFilter)
        .ToList();

            OfficesList.ItemsSource = filtered;
            UpdatePriceText();
        }
        private void LoadDataFromDatabase()
        {
            try
            {
                using (var context = new Entities())
                {
                    // Используйте правильное название навигационного свойства
                    _allOffices = new ObservableCollection<Offices>(context.Offices
                        .Include(o => o.Users) // Обычно называется во множественном числе
                        .ToList());

                    OfficesList.ItemsSource = _allOffices;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void UpdatePriceText()

        {
                PriceText.Text = $"До {_currentPriceFilter:N0} руб/мес";
        }

            // Обработчики событий
            private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                _currentSearch = SearchTextBox.Text;
                ApplyFilters();
            }

           

            private void DetailsButton_Click(object sender, RoutedEventArgs e)
            {
                
            }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddOfficeWindow(_currentUserId); // Используем сохраненный ID
            if (addWindow.ShowDialog() == true)
            {
                LoadDataFromDatabase();
                ApplyFilters();
            }
        }

        private void PriceSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _currentPriceFilter = (decimal)PriceSlider.Value;
            ApplyFilters();
        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            // Сбрасываем значение слайдера
             PriceSlider.Value = 50000;

            // Сбрасываем текстовый поиск
            SearchTextBox.Clear();

            // Принудительно обновляем фильтры
            _currentPriceFilter = 50000;
            _currentSearch = "";
            _currentTypeFilter = "Все типы";

            ApplyFilters();
            UpdatePriceText();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var office = button.DataContext as Offices;
            if (office == null) return;

            var detailsWindow = new OfficeDetailsWindow(office);
            detailsWindow.Owner = Application.Current.MainWindow;
            detailsWindow.ShowDialog();
        }

    }
}

