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
    /// Логика взаимодействия для OfficeDetailsWindow.xaml
    /// </summary>
    public partial class OfficeDetailsWindow : Window
    {
        public OfficeDetailsWindow(Offices office)
        {
            InitializeComponent();
            DataContext = office;
        }
    }
}
