using ClosedXML.Excel;
using Microsoft.Win32;
using Models.Dtos;
using Models.Entities;
using StokEkstresi.Business.Abstacts;
using StokEkstresi.UI.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace StokEkstresi.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IStokEkstresiService _stokService;
        private List<Stk>? allStks = new();
        public MainWindow(IStokEkstresiService stokEkstresiService)
        {
            InitializeComponent();
            _stokService = stokEkstresiService;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allStks = await _stokService.GetStksAsync();
            cbMalSecim.ItemsSource = allStks;
        }

        private void cbMalSecim_KeyUp(object sender, KeyEventArgs e)
        {
            string text = cbMalSecim.Text?.ToLower() ?? "";

            var filteredList = allStks.Where(x => x.MalKodu.ToLower().Contains(text) || x.MalAdi.ToLower().Contains(text)).ToList();

            cbMalSecim.ItemsSource = filteredList;
            cbMalSecim.IsDropDownOpen = true;
            cbMalSecim.Items.Refresh();
        }

        private async void Listele_Click(object sender, RoutedEventArgs e)
        {
            if (!Validators.DateValidator(dpStart.Text, dpFinish.Text, out string invalidMessage, out DateTime? startDate, out DateTime? finishDate))
            {
                MessageBox.Show(invalidMessage, "Tarih Hatası", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string? girilenMalKodu = cbMalSecim.Text?.Trim();

            if (!Validators.MalKoduValidator(girilenMalKodu, allStks, out string invalidMessage2, out string selectedMalKodu))
            {
                MessageBox.Show(invalidMessage2, "Geçersiz Mal Kodu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var stokEkstresiDtos = await _stokService.GetStokEkstresiAsync(startDate, finishDate, selectedMalKodu);
            dataGrid.ItemsSource = stokEkstresiDtos;
        }


        // Excel'e Aktar butonuna tıklandığında çağrılır.ClosedXML kullandım.
        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.Items.Count == 0)
                {
                    MessageBox.Show("Export edilecek veri yok!", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Dosyası (*.xlsx)|*.xlsx",
                    FileName = $"StokEkstresi-{DateTime.Now:yyyyMMdd}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add("Stok Ekstresi");

                    // Header yaz
                    for (int i = 0; i < dataGrid.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataGrid.Columns[i].Header?.ToString() ?? "";
                    }

                    // Veriyi ItemsSource'tan oku
                    var itemsSource = dataGrid.ItemsSource.Cast<object>().ToList();

                    for (int i = 0; i < itemsSource.Count; i++)
                    {
                        var item = itemsSource[i];
                        for (int j = 0; j < dataGrid.Columns.Count; j++)
                        {
                            if (dataGrid.Columns[j] is DataGridBoundColumn boundColumn)
                            {
                                var bindingPath = (boundColumn.Binding as System.Windows.Data.Binding)?.Path?.Path;
                                if (!string.IsNullOrEmpty(bindingPath))
                                {
                                    var prop = item.GetType().GetProperty(bindingPath);
                                    if (prop != null)
                                    {
                                        var value = prop.GetValue(item)?.ToString() ?? "";
                                        worksheet.Cell(i + 2, j + 1).Value = value;
                                    }
                                }
                            }
                        }
                    }

                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Excel dosyası başarıyla oluşturuldu.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Yazdır butonuna tıklandığında çağrılır
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource is List<StokEkstresiDto> data && data.Any())
            {
                PrintData(data);
                MessageBox.Show("Yazdırma işlemi başarılı oldu.", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Yazdırılacak veri bulunamadı.");
            }
        }

        private void PrintData(List<StokEkstresiDto> data)
        {
            // 1. FlowDocument oluşturuldu 
            FlowDocument doc = new FlowDocument();
            doc.PagePadding = new Thickness(50);
            doc.ColumnWidth = double.PositiveInfinity;
            doc.FontSize = 12;
            doc.FontFamily = new FontFamily("Segoe UI");

            // 2. Tablo oluşturuldu
            Table table = new Table();
            doc.Blocks.Add(table);

            // Kolon sayısı kadar sütun eklendi
            int columnCount = 7;
            for (int i = 0; i < columnCount; i++)
                table.Columns.Add(new TableColumn());

            // 3. Başlık satırı
            TableRowGroup headerGroup = new TableRowGroup();
            table.RowGroups.Add(headerGroup);

            TableRow headerRow = new TableRow();
            headerGroup.Rows.Add(headerRow);

            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Sıra"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("İşlem Türü"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Evrak No"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Tarih"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Giriş"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Çıkış"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Stok"))));

            foreach (var cell in headerRow.Cells)
            {
                cell.FontWeight = FontWeights.Bold;
                cell.Padding = new Thickness(4);
                cell.BorderBrush = Brushes.Gray;
                cell.BorderThickness = new Thickness(0.5);
            }

            // 4. Veriler
            TableRowGroup bodyGroup = new TableRowGroup();
            table.RowGroups.Add(bodyGroup);

            int siraNo = 1;
            foreach (var item in data)
            {
                TableRow row = new TableRow();
                bodyGroup.Rows.Add(row);

                row.Cells.Add(new TableCell(new Paragraph(new Run(siraNo++.ToString()))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.IslemTur))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.EvrakNo))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.Tarih))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.GirisMiktar.ToString("N2")))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.CikisMiktar.ToString("N2")))));
                row.Cells.Add(new TableCell(new Paragraph(new Run(item.Stok.ToString("N2")))));

                foreach (var cell in row.Cells)
                {
                    cell.Padding = new Thickness(4);
                    cell.BorderBrush = Brushes.LightGray;
                    cell.BorderThickness = new Thickness(0.5);
                }
            }

            // 5. PrintDialog ile yazdır
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog() == true)
            {
                IDocumentPaginatorSource dps = doc;
                pd.PrintDocument(dps.DocumentPaginator, "Stok Ekstresi");
            }
        }

    }
}