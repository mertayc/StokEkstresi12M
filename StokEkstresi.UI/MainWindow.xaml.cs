using StokEkstresi.Business.Abstacts;
using System.Windows;
using System.Windows.Media;

namespace StokEkstresi.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IStokEkstresiService _stokService;

        public MainWindow(IStokEkstresiService stokEkstresiService)
        {
            InitializeComponent();
            _stokService = stokEkstresiService;
        }

        private async void Listele_Click(object sender, RoutedEventArgs e)
        {
            await _stokService.xx();
        }

        private void txtMalKodu_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMalKodu.Text == "MalKodu")
            {
                txtMalKodu.Text = "";
                txtMalKodu.Foreground = Brushes.Black;
            }
        }

        private void txtMalKodu_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMalKodu.Text))
            {
                txtMalKodu.Text = "MalKodu";
                txtMalKodu.Foreground = Brushes.Gray;
            }
        }
    }
}