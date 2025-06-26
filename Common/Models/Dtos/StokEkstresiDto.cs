using Models.Enums;

namespace Models.Dtos
{
    public class StokEkstresiDto
    {
        public int SiraNo { get; set; }
        public string IslemTur { get; set; } // "Giriş" veya "Çıkış"
        public string EvrakNo { get; set; }
        public string Tarih { get; set; } // dd.MM.yyyy formatında
        public decimal GirisMiktar { get; set; }
        public decimal CikisMiktar { get; set; }
        public decimal Stok { get; set; }

    }
}
