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


        public MainPage()
        {
            InitializeComponent();
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
                    _allOffices = new ObservableCollection<Offices>(context.Offices.ToList());
                    ApplyFilters();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
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

            private void TypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (TypeFilterComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    _currentTypeFilter = selectedItem.Content.ToString();
                    ApplyFilters();
                }
            }

            private void DetailsButton_Click(object sender, RoutedEventArgs e)
            {
                
            }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddOfficeWindow();
            if (addWindow.ShowDialog() == true)
            {
                // Перезагружаем данные после добавления
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
            TypeFilterComboBox.SelectedIndex = 0;
            PriceSlider.Value = 50000;
            SearchTextBox.Clear();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}

