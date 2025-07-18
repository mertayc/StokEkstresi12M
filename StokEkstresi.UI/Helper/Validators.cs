﻿using Models.Entities;
using System.Globalization;
using System.Windows;

namespace StokEkstresi.UI.Helper
{
    public static class Validators
    {
        /// <summary>
        /// İki tarih string'ini (gg.AA.yyyy) formatında kontrol eder.
        /// - Her iki tarih boşsa geçerli kabul edilir.
        /// - Format hatası varsa veya bitiş < başlangıç ise false döner.
        /// - Geçerli tarihleri out parametre olarak verir.
        /// </summary>
        public static bool DateValidator(string startDate, string finishDate, out string invalidMessage, out DateTime? parsedStartDate, out DateTime? parsedFinishDate)
        {
            invalidMessage = string.Empty;
            parsedStartDate = null;
            parsedFinishDate = null;

            bool isStartEmpty = string.IsNullOrWhiteSpace(startDate);
            bool isFinishEmpty = string.IsNullOrWhiteSpace(finishDate);

            // Her iki tarih boşsa → geçerli kabul edilir
            if (isStartEmpty && isFinishEmpty)
                return true;

            var formats = new[] { "d.M.yyyy", "dd.MM.yyyy", "d.MM.yyyy", "dd.M.yyyy" };
            var culture = CultureInfo.InvariantCulture;
          
            if (!isStartEmpty)
            {
                if (!DateTime.TryParseExact(startDate, formats, culture, DateTimeStyles.None, out DateTime start))
                {
                    invalidMessage = "Başlangıç tarihi geçerli bir formatta değil (gg.AA.yyyy)";
                    return false;
                }

                parsedStartDate = start;
            }

            if (!isFinishEmpty)
            {
                if (!DateTime.TryParseExact(finishDate, formats, culture, DateTimeStyles.None, out DateTime finish))
                {
                    invalidMessage = "Bitiş tarihi geçerli bir formatta değil (gg.AA.yyyy)";
                    return false;
                }

                parsedFinishDate = finish;
            }

            // Eğer her ikisi de varsa, sıralamayı kontrol et
            if (parsedStartDate.HasValue && parsedFinishDate.HasValue &&
                parsedFinishDate.Value < parsedStartDate.Value)
            {
                invalidMessage = "Bitiş tarihi, başlangıç tarihinden önce olamaz.";
                return false;
            }

            return true;
        }

        public static bool MalKoduValidator(string? girilenMalKodu, List<Stk>? allStks,out string invalidMessage,out string selectedMalKodu)
        {
            invalidMessage = string.Empty;
            selectedMalKodu = string.Empty;

            if (string.IsNullOrWhiteSpace(girilenMalKodu))
            {
                invalidMessage = "Lütfen bir Mal Kodu girin veya seçin.";
                return false;
            }

            // Liste dışı kontrolü
            var selected = allStks?.FirstOrDefault(x => string.Equals(x.MalKodu, girilenMalKodu, StringComparison.OrdinalIgnoreCase));

            if (selected is null)
            {
                invalidMessage = "Girilen Mal Kodu listede bulunamadı. Lütfen geçerli bir Mal Kodu seçin.";
                return false;
            }

            selectedMalKodu = selected.MalKodu;
            return true;
        }
    }
}
