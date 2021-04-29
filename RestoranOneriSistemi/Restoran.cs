using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOneriSistemi
{
    class Restoran
    {
        public string restoranKodu;
        public string ortamSikligi;
        public string ortamTemizligi;
        public string yemekKalitesi;
        public string hizmetKalitesi;
        public string fiyatUygunlugu;
        public string ulasimKolayligi;
        public string aracParkOlanagi;
        public Restoran()
        {

        }
        public Restoran(string restoranKodu,string ortamSikligi,string ortamTemizligi,string yemekKalitesi,string hizmetKalitesi,string fiyatUygunlugu,string ulasimKolayligi,string aracParkOlanagi)
        {
            this.restoranKodu = restoranKodu;
            this.ortamSikligi = ortamSikligi;
            this.ortamTemizligi = ortamTemizligi;
            this.yemekKalitesi = yemekKalitesi;
            this.hizmetKalitesi = hizmetKalitesi;
            this.fiyatUygunlugu = fiyatUygunlugu;
            this.ulasimKolayligi = ulasimKolayligi;
            this.aracParkOlanagi = aracParkOlanagi;
        }
        public Restoran(string ortamSikligi, string ortamTemizligi, string yemekKalitesi, string hizmetKalitesi, string fiyatUygunlugu, string ulasimKolayligi, string aracParkOlanagi)
        {
            this.ortamSikligi = ortamSikligi;
            this.ortamTemizligi = ortamTemizligi;
            this.yemekKalitesi = yemekKalitesi;
            this.hizmetKalitesi = hizmetKalitesi;
            this.fiyatUygunlugu = fiyatUygunlugu;
            this.ulasimKolayligi = ulasimKolayligi;
            this.aracParkOlanagi = aracParkOlanagi;
        }
        public void Yazdir()
        {
            Console.WriteLine("Ortam Şıklığı: " + this.ortamSikligi);
            Console.WriteLine("Ortam Temizliği: " + this.ortamTemizligi);
            Console.WriteLine("Yemek Kalitesi: " + this.yemekKalitesi);
            Console.WriteLine("Hizmet Kalitesi: " + this.hizmetKalitesi);
            Console.WriteLine("Fiyat Uygunluğu: " + this.fiyatUygunlugu);
            Console.WriteLine("Ulaşım Kolayligi: " + this.ulasimKolayligi);
            Console.WriteLine("Araç Park Olanağı: " + this.aracParkOlanagi);
        }
    }
}
