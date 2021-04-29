using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace RestoranOneriSistemi
{
    class Metodlar
    {
        public List<Restoran> OkuVeAktar() // txtyi okur restoranları oluşturur ve Restoran Listesi döndürür.
        {
            List<Restoran> restoranlar = new List<Restoran>();
            try
            {
                StreamReader streamReader = new StreamReader("restoran-oneri.txt");
                string satir;
                satir = streamReader.ReadLine();//ilk satır alındı.
                while ((satir = streamReader.ReadLine()) != null)
                {
                    String[] ozellikler = satir.Split(',');
                    Restoran restoran = new Restoran(ozellikler[0], ozellikler[1], ozellikler[2], ozellikler[3], ozellikler[4], ozellikler[5], ozellikler[6], ozellikler[7]);
                    restoranlar.Add(restoran);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Dosya Bulunamadı.");
            }
            
            return restoranlar;
        }
        public Restoran IstenilenTipRestoranOlustur()//Kullanıcının girdiği kıstaslara göre bir restoran oluşturup geri döndürür.
        {
            Restoran restoran = new Restoran();
            string[] restoranBilgileri = new string[7]; // kullancının gireceği restoran bilgileri
            string[] restoranDegiskenleri = { "Ortam Şıklığı", "Ortam Temizliği", "Yemek Kalitesi", "Hizmet Kalitesi", "Fiyat Uygunluğu", "Ulaşım Kolayligi", "Araç Park Olanağı" };
            int degisken = 0;
            bool kayitBasarili = false;
            while (kayitBasarili == false)
            {
                string giris;
                Console.Write(restoranDegiskenleri[degisken] + " bilgisini girin(1 ile 10 arasında):");
                giris = Console.ReadLine();
                try
                {
                    if (Convert.ToInt32(giris) <= 10 && Convert.ToInt32(giris) >= 1)
                    {
                        restoranBilgileri[degisken] = giris;
                        degisken++;
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("(Hata!)Lütfen 1 ile 10 arasında sayı giriniz.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("(Hata!)Lütfen sayı girin.");
                }
                if(degisken == restoranDegiskenleri.Length)
                {
                    restoran = new Restoran(restoranBilgileri[0], restoranBilgileri[1], restoranBilgileri[2], restoranBilgileri[3], restoranBilgileri[4], restoranBilgileri[5], restoranBilgileri[6]);
                    kayitBasarili = true;
                }
            }
            return restoran;
            
        }

        public int OneriSayisiAl()
        {
            
            int oneriSayisi = 1;
            bool basarili = false;
            while (basarili != true)
            {
                Console.Clear();
                Console.Write("Kaç Adet Öneri İstiyorsunuz(1 ile 15 arasında bir deger girin): ");
                try
                {
                    oneriSayisi = Convert.ToInt32(Console.ReadLine());
                    if(oneriSayisi<=15 && oneriSayisi >= 1)
                    {
                        basarili = true;
                    }
                    else
                    {
                        Console.WriteLine("(Hata!)Lütfen 1 ile 15 arasında bir deger girin.");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Lütfen Sayi Girin");
                }
            }
            return oneriSayisi;
        }

        public bool RestoranOner(int oneriSayisi,Restoran istenilenRestoran,List<Restoran> restoranlar)//İstenilen tip restorana göre restoran önerir başarılı bi şekilde listelendi veya listelenmedi diye bool değer döndürür.
        {
            bool listelendi;
            List<double> cosineDegerleri = new List<double>();
            List<int> onerilenRestoranIndex = new List<int>();
            List<double> onerilenRestoranCosineDegerleri = new List<double>();
            for (int i = 0; i < restoranlar.Count; i++)
            {
                 cosineDegerleri.Add(CosineHesapla(istenilenRestoran, restoranlar[i]));
            }
            for (int i = 0; i < oneriSayisi; i++)
            {
                double maxCos = 0;
                int maxIndex = 0;
                for (int j = 0; j < cosineDegerleri.Count; j++)
                {
                    if (cosineDegerleri[j] >= maxCos && !onerilenRestoranIndex.Contains(j))
                    {
                        maxCos = cosineDegerleri[j];
                        maxIndex = j;
                        
                    }
                }
                if (onerilenRestoranIndex.Contains(maxIndex))// aynı cos değerlerine sahip önerilecek restoran var ise öneri sayısını i degiskenini bir azaltarak arttırıyoruz.
                {
                    i--;
                }
                onerilenRestoranIndex.Add(maxIndex);
                onerilenRestoranCosineDegerleri.Add(cosineDegerleri[maxIndex]);
            }
            if (onerilenRestoranCosineDegerleri[0] < 0.5)
            {
                string tercih;
                Console.WriteLine("Tercihlerinize Uygun Restoran Pek Yok Gibi. Yine de listelemek ister misiniz?(Evet,Hayir yaziniz)");
                tercih = Console.ReadLine().ToLower();
                if (tercih.Equals("hayir"))
                {
                    listelendi = false;
                    return listelendi;
                }
            }

            Console.WriteLine("~~Sizin tercihleriniz~~");
            istenilenRestoran.Yazdir();
            Console.WriteLine("~~Öneriler~~");
            for (int i = 0; i < onerilenRestoranIndex.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine((i+1)+".öneri");
                Console.WriteLine("Restoran Kodu:"+restoranlar[onerilenRestoranIndex[i]].restoranKodu);
                Console.WriteLine("Restoran Cosine Benzerliği:"+onerilenRestoranCosineDegerleri[i]);
            }
            listelendi = true;
            return listelendi;
        }

        public double CosineHesapla(Restoran istenilenRestoran,Restoran kayitliRestoran)
        {
            double cos = 0;
            double pay = 0;
            double paydaKayitA = 0;
            double paydaKayitB = 0;
            double payda = 0;
            if (!kayitliRestoran.aracParkOlanagi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.aracParkOlanagi) * Convert.ToDouble(istenilenRestoran.aracParkOlanagi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.aracParkOlanagi) * Convert.ToDouble(istenilenRestoran.aracParkOlanagi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.aracParkOlanagi) * Convert.ToDouble(kayitliRestoran.aracParkOlanagi);
            }
            if (!kayitliRestoran.yemekKalitesi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.yemekKalitesi) * Convert.ToDouble(istenilenRestoran.yemekKalitesi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.yemekKalitesi) * Convert.ToDouble(istenilenRestoran.yemekKalitesi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.yemekKalitesi) * Convert.ToDouble(kayitliRestoran.yemekKalitesi);
            }
            if (!kayitliRestoran.fiyatUygunlugu.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.fiyatUygunlugu) * Convert.ToDouble(istenilenRestoran.fiyatUygunlugu);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.fiyatUygunlugu) * Convert.ToDouble(istenilenRestoran.fiyatUygunlugu);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.fiyatUygunlugu) * Convert.ToDouble(kayitliRestoran.fiyatUygunlugu);
            }
            if (!kayitliRestoran.ortamSikligi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.ortamSikligi) * Convert.ToDouble(istenilenRestoran.ortamSikligi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.ortamSikligi) * Convert.ToDouble(istenilenRestoran.ortamSikligi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.ortamSikligi) * Convert.ToDouble(kayitliRestoran.ortamSikligi);
            }
            if (!kayitliRestoran.ortamTemizligi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.ortamTemizligi) * Convert.ToDouble(istenilenRestoran.ortamTemizligi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.ortamTemizligi) * Convert.ToDouble(istenilenRestoran.ortamTemizligi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.ortamTemizligi) * Convert.ToDouble(kayitliRestoran.ortamTemizligi);
            }
            if (!kayitliRestoran.hizmetKalitesi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.hizmetKalitesi) * Convert.ToDouble(istenilenRestoran.hizmetKalitesi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.hizmetKalitesi) * Convert.ToDouble(istenilenRestoran.hizmetKalitesi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.hizmetKalitesi) * Convert.ToDouble(kayitliRestoran.hizmetKalitesi);
            }
            if (!kayitliRestoran.ulasimKolayligi.Equals("?"))
            {
                pay += Convert.ToDouble(kayitliRestoran.ulasimKolayligi) * Convert.ToDouble(istenilenRestoran.ulasimKolayligi);
                paydaKayitA += Convert.ToDouble(istenilenRestoran.ulasimKolayligi) * Convert.ToDouble(istenilenRestoran.ulasimKolayligi);
                paydaKayitB += Convert.ToDouble(kayitliRestoran.ulasimKolayligi) * Convert.ToDouble(kayitliRestoran.ulasimKolayligi);
            }

            payda = Math.Sqrt(paydaKayitA) * Math.Sqrt(paydaKayitB);
            cos = pay / payda;

            return cos;
        }//Cosine benzerliğini hesaplar.
    }
}
