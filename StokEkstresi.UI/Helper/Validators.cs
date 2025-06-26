using System.Globalization;

namespace StokEkstresi.UI.Helper
{
    public static class Validators
    {
        /// <summary>
        /// İki tarih string'ini (gg.AA.yyyy) formatında kontrol eder.
        /// Eğer her ikisi de boşsa geçerlidir.
        /// Format geçersizse veya bitiş tarihi başlangıçtan küçükse hata mesajı döner.
        /// </summary>
        public static bool DateValidate(string startDate, string finishDate, out string invalidMessage)
        {
            invalidMessage = string.Empty;
            DateTime parsedStart = DateTime.MinValue;
            DateTime parsedFinish = DateTime.MinValue;

            bool startEmpty = string.IsNullOrWhiteSpace(startDate);
            bool finishEmpty = string.IsNullOrWhiteSpace(finishDate);

            string[] formats = { "d.M.yyyy", "dd.MM.yyyy", "d.MM.yyyy", "dd.M.yyyy" };
            var culture = CultureInfo.InvariantCulture;

            // Her iki tarih de girilmemişse → Geçerli kabul et
            if (startEmpty && finishEmpty)
                return true;

            // Başlangıç tarihi doluysa, format kontrolü
            if (!startEmpty && !DateTime.TryParseExact(startDate, formats, culture, DateTimeStyles.None, out parsedStart))
            {
                invalidMessage = "Başlangıç tarihi geçerli bir formatta değil (gg.AA.yyyy)";
                return false;
            }

            // Bitiş tarihi doluysa, format kontrolü
            if (!finishEmpty && !DateTime.TryParseExact(finishDate, formats, culture, DateTimeStyles.None, out parsedFinish))
            {
                invalidMessage = "Bitiş tarihi geçerli bir formatta değil (gg.AA.yyyy)";
                return false;
            }

            // İkisi de varsa ve sıralama yanlışsa
            if (!startEmpty && !finishEmpty && parsedFinish < parsedStart)
            {
                invalidMessage = "Bitiş tarihi, başlangıç tarihinden önce olamaz.";
                return false;
            }

            return true;
        }
    }
}
