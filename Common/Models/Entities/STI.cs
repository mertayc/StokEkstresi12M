using Models.Enums;

namespace Models.Entities
{
    public class STI 
    {
        public int Id { get; set; }
        public IslemTur IslemTur { get; set; }
        public string EvrakNo { get; set; }
        public int Tarih { get; set; }
        public string MalKodu { get; set; }
        public decimal Miktar { get; set; }
        public decimal Fiyat { get; set; }
        public decimal Tutar {  get; set; }
        public string Birim {  get; set; }

    }
}
