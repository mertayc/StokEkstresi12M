using StokEkstresi.Business.Abstacts;
using StokEkstresi.UI.Helper;
using System.Windows;

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
            if (!Validators.DateValidator(dpStart.Text, dpFinish.Text, out string invalidMessage,out DateTime? startDate,out DateTime? finishDate))
            {
                MessageBox.Show(invalidMessage, "Tarih Hatası", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //await _stokService.xx();
        }

   
    }
}