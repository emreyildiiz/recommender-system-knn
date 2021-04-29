using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace RestoranOneriSistemi
{
    class AnaMenu
    {
        public List<Restoran> restoranlar;
        private Restoran istenilenRestoran;
        private int oneriSayisi;
        public AnaMenu()
        {
            restoranlar = new List<Restoran>();
        }
        public void Calistir()// ana menüyü çalıştırır.
        {
            Metodlar metodlar = new Metodlar();
            Console.Clear();
            Console.WriteLine("~~Restoran Oneri Sistemine Hosgeldiniz~~");
            Console.WriteLine("Devam Etmek İçin Herhangi Bir Tuşa Basın");
            Console.ReadKey();
            Console.Clear();
            restoranlar = metodlar.OkuVeAktar();
            istenilenRestoran = metodlar.IstenilenTipRestoranOlustur();
            oneriSayisi = metodlar.OneriSayisiAl();
            Console.WriteLine(oneriSayisi);
            if(!metodlar.RestoranOner(oneriSayisi, istenilenRestoran, restoranlar))
            {
                Calistir();
            }
            Console.WriteLine("\nBaşa Dönmek için Bir Tuşa basınız...");
            Console.ReadKey();
            Calistir();
        }
    }
}
