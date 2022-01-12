using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UltraBalaton
{
    internal class Program
    {
        static List<Eredmeny> Eredmenyek = new List<Eredmeny>();
        class Eredmeny
        {
            public string Nev { get; set; }
            public int Rajtszam { get; set; }
            public kategoria Kategoria { get; set; }
            public string VersenyIdo { get; set; }  
            public int TavSzazalek { get; set; }
     
            public Eredmeny(string sor)
            {
                var darabolt =  sor.Split(';');
                Nev = darabolt[0];
                Rajtszam = int.Parse(darabolt[1]);
                if (darabolt[2] == "Ferfi")
                    Kategoria = kategoria.ferfi;
                else Kategoria = kategoria.no;
                VersenyIdo = darabolt[3];
        

                TavSzazalek = int.Parse(darabolt[4]);
            }
        }
        enum kategoria
        {
            ferfi,
            no
        }

        static void Main(string[] args)
        {
            Feladat02();
            Feladat03();
            Feladat04();
            Feladat05();
            Feladat07();
            Feladat08();


            Console.ReadKey();
        }

        private static void Feladat08()
        {
            float fmin = float.MaxValue;
            float nmin = float.MaxValue;
   
            string gynNev="";
            int gynRajtszam=0;
            string gynIdo="";
        
            string gyfNev="";
            int gyfRajtszam=0;
            string gyfIdo="";


            foreach (var item in Eredmenyek)
            {
                if(item.Kategoria== kategoria.ferfi)
                {
                    if( IdoOraban(item.VersenyIdo) < fmin & item.TavSzazalek==100)
                    {
                        fmin = IdoOraban(item.VersenyIdo);
                        gyfNev = item.Nev;
                        gyfRajtszam = item.Rajtszam;
                        gyfIdo = item.VersenyIdo;
                    }
                }
                else
                {
                    if(IdoOraban(item.VersenyIdo) < nmin & item.TavSzazalek ==100)
                    {
                        nmin = IdoOraban(item.VersenyIdo);
                        gynNev = item.Nev;
                        gynRajtszam = item.Rajtszam;
                        gynIdo = item.VersenyIdo;

                    }
                }

            }
            Console.WriteLine("8. Feladat: Verseny gyoztesei");
            Console.WriteLine($"\tNok: {gynNev} ({gynRajtszam}) - {gynIdo}");
            Console.WriteLine($"\tFerfiak: {gyfNev} ({gyfRajtszam}) - {gyfIdo}");
        }

        private static void Feladat07()
        {
            int mennyiseg = 0;
            float osszeg = 0;
            float atlagosIdo=0;

            foreach (var item in Eredmenyek)
            {


                if (item.TavSzazalek == 100 & item.Kategoria == kategoria.ferfi)
                {
                    mennyiseg++;
                    osszeg += IdoOraban(item.VersenyIdo);
                }
            atlagosIdo = osszeg / mennyiseg;
            }
            Console.WriteLine($"7. Feladat: Atlagos ido: {atlagosIdo}");
        }



        private static void Feladat05()
        {
            
            Console.Write("5. Feladat: Kerem a sportolo nevet: ");
            string sportoloNeve = Console.ReadLine();
            Console.Write("\tIndult egyeniben a sportolo? ");
            bool talalat = false;
            foreach (var item in Eredmenyek)
            {
                if (item.Nev == sportoloNeve)
                {
                    talalat = true; 
                    Console.WriteLine("igen");
                    Console.Write("\tTeljesitette a teljes tavot? ");
                    if (item.TavSzazalek == 100)
                        Console.WriteLine("igen");
                    else
                        Console.WriteLine("Nem");
                    break;
                }
              
            }
            if (!talalat) Console.WriteLine("Nem");
        }

        private static void Feladat04()
        {
            int celbaErkezoNok = Eredmenyek.Count(x=> x.Kategoria ==kategoria.no & x.TavSzazalek==100);
            Console.WriteLine($"4. Feladat: Celba erkezo noi sportolok: {celbaErkezoNok} fo");

        }

        private static void Feladat03()
        {
            Console.WriteLine($"3. Feladat: Egyeni indulok: {Eredmenyek.Count} fo");
        }

        private static void Feladat02()
        {
            using (var sr = new StreamReader(@"..\..\Resources\ub2017egyeni.txt"))
            {
                string elsoSor = sr.ReadLine();
                Console.WriteLine(elsoSor); 
                while (!sr.EndOfStream)
                {
                    Eredmenyek.Add(new Eredmeny(sr.ReadLine()));
                }
            }


        }

        private static float IdoOraban(string ido)
        {
            var Darabolt = ido.Split(':');
            float Ora = int.Parse(Darabolt[0]);
            float Perc = int.Parse(Darabolt[1]);
            float Masodperc = int.Parse(Darabolt[2]);

            float eredmeny = Ora + (Perc / 60) + (Masodperc / 3600);
            return eredmeny;
        }

    }
}
