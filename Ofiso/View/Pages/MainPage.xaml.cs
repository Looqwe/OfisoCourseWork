using Ofiso.AppData;
using Ofiso.Models;
using Ofiso.View.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ofiso.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
            private List<Office> _allOffices = new List<Office>();
            private string _currentSearch = "";
            private string _currentTypeFilter = "Все типы";
            private decimal _currentPriceFilter = 50000;
            private List<Offices> _allAdvertisements = new List<Offices>();

        public MainPage()
            {
                InitializeComponent();
                LoadSampleData();
                ApplyFilters();
                LoadDataFromDatabase();

        }

        private void LoadSampleData()
            {
                // Пример данных для тестирования
                _allOffices = new List<Office>
            {
                new Office
                {
                    Title = "Офис в бизнес-центре",
                    Address = "Москва, ул. Тверская, 12",
                    Description = "Современный офис с видом на центр города, 50 кв.м.",
                    Price = 45000,
                    Type = "Кабинеты",
                    ImageUrl = "https://avatars.mds.yandex.net/get-altay/6145759/2a00000182e0304cd81e67949dafa25e486a/XXL_height"
                },
                new Office
                {
                    Title = "Коворкинг Чепуха",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://static.tildacdn.com/tild6437-6330-4837-b264-343431376330/hd_cfadc7f2134d4e357.jpg"
                },
                new Office
                {
                    Title = "БДСМ",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 5000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://avatars.mds.yandex.net/i?id=478feb1a2d6f61663fb86ef2a5dd51e9_l-5190143-images-thumbs&n=13"
                },
                new Office
                {
                    Title = "Коворкинг Жопки",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 325000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://images.cdn-cian.ru/images/ofis-moskva-ulica-bolshaya-yakimanka-1841664659-1.jpg"
                },
                 new Office
                {
                    Title = "Офис ШЕРЕК",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://i.pinimg.com/736x/bf/ed/26/bfed26c3679b7b281554ddcbf75a3fb1.jpg"
                },
                  new Office
                {
                    Title = "ОФИСО",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://avatars.mds.yandex.net/get-altay/10845257/2a0000018c348c4fcf2b222315560243d3ab/XXL_height"
                },
                   new Office
                {
                    Title = "УУУУУУУУЛЕТАЮ НА ГАИТИ",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://avatars.mds.yandex.net/i?id=3a8c48886b21b7de04d733767c6bfccf_l-5247043-images-thumbs&n=13"
                },
                    new Office
                {
                    Title = "КАКОЙ БОЛЬШОЙ КАБИНА НРАААААВИЦААА",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://a.d-cd.net/hWAAAgF7beA-1920.jpg"
                },
                     new Office
                {
                    Title = "я устал",
                    Address = "Санкт-Петербург, Невский пр., 45",
                    Description = "Открытое пространство с зонами для работы и отдыха",
                    Price = 25000,
                    Type = "Открытые офисы",
                    ImageUrl = "https://avatars.dzeninfra.ru/get-zen_doc/271828/pub_67350804b44a61017086b0ae_673508a97562952dc539766b/scale_1200"
                }
            };
            }

        private void ApplyFilters()
        {
            var filtered = _allOffices
                .Where(o => o.Title.IndexOf(_currentSearch, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            o.Address.IndexOf(_currentSearch, StringComparison.OrdinalIgnoreCase) >= 0)
                .Where(o => _currentTypeFilter == "Все типы" || o.Type == _currentTypeFilter)
                .Where(o => o.Price <= _currentPriceFilter)
                .ToList();

            OfficesList.ItemsSource = filtered;
            UpdatePriceText();
            var filtered1 = _allAdvertisements
            .Where(a => a.Title.IndexOf(_currentSearch, StringComparison.OrdinalIgnoreCase) >= 0)
            .Where(a => a.PricePerMont <= _currentPriceFilter)
            .ToList();

            OfficesList.ItemsSource = filtered;
        }
        private void LoadDataFromDatabase()
        {
            // Загрузка из БД
            _allAdvertisements = App.context.Offices.ToList();
            try
            {
                _allAdvertisements = App.context.Offices
                    .ToList();
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
                if ((sender as Button)?.DataContext is Office selectedOffice)
                {
                    // Переход на страницу деталей
                   
                }
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void PriceSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            _currentPriceFilter = (decimal)PriceSlider.Value;
            ApplyFilters();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetFilter_Click(object sender, RoutedEventArgs e)
        {
            TypeFilterComboBox.SelectedIndex = 0;
            PriceSlider.Value = 50000;
            SearchTextBox.Clear();
        }
    }

    public class Office
        {
            public string Title { get; set; }
            public string Address { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public string ImageUrl { get; set; }
            public string Type { get; set; }
        }
}

