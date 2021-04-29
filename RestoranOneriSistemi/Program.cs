using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoranOneriSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            AnaMenu anaMenu = new AnaMenu();
            anaMenu.Calistir();
            Console.ReadKey();
        }
    }
}
