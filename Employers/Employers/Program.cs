using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    // Osztály a dolgozók adatainak tárolására
    public class Dolgozo
    {
        public int ID { get; set; }
        public string Nev { get; set; }
        public int Eletkor { get; set; }
        public decimal Ber { get; set; }

        public Dolgozo(int id, string nev, int eletkor, decimal ber)
        {
            ID = id;
            Nev = nev;
            Eletkor = eletkor;
            Ber = ber;
        }
    }

    // Osztály a dolgozók kezeléséhez
    public class DolgozoKezelo
    {
        public List<Dolgozo> Dolgozok { get; set; } = new List<Dolgozo>();

        public void BeolvasFajlbol(string fileNev)
        {
            foreach (var sor in File.ReadLines(fileNev))
            {
                var reszek = sor.Split(';');
                if (reszek.Length == 4)
                {
                    try
                    {
                        Dolgozok.Add(new Dolgozo(
                            int.Parse(reszek[0]),
                            reszek[1].Trim(),
                            int.Parse(reszek[2]),
                            decimal.Parse(reszek[3])
                        ));
                    }
                    catch
                    {
                        Console.WriteLine($"Hibás sor: {sor}");
                    }
                }
            }
        }

        public void ListazNevek()
        {
            Console.WriteLine("Dolgozók nevei:");
            Dolgozok.ForEach(d => Console.WriteLine(d.Nev));
        }

        public void LegnagyobbBeruDolgozok()
        {
            var maxBer = Dolgozok.Max(d => d.Ber);
            Console.WriteLine("\nLegnagyobb bérrel rendelkező dolgozók:");
            Dolgozok.Where(d => d.Ber == maxBer)
                    .ToList()
                    .ForEach(d => Console.WriteLine($"ID: {d.ID}, Név: {d.Nev}"));
        }

        public void NyugdijhozKozelAllok()
        {
            Console.WriteLine("\nNyugdíjhoz közel álló dolgozók:");
            Dolgozok.Where(d => 65 - d.Eletkor <= 10 && 65 - d.Eletkor > 0)
                    .ToList()
                    .ForEach(d => Console.WriteLine($"Név: {d.Nev}, Kor: {d.Eletkor}"));
        }

        public void MagasBeruDolgozokSzama()
        {
            var magasBeruSzam = Dolgozok.Count(d => d.Ber > 50000);
            Console.WriteLine($"\n50000 forint felett keresők száma: {magasBeruSzam}");
        }
    }

    static void Main()
    {
        // Dolgozó kezelő példányosítása
        DolgozoKezelo dolgozoKezelo = new DolgozoKezelo();

        // Fájl beolvasása
        dolgozoKezelo.BeolvasFajlbol("tulajdonsagok_100sor.txt");

        if (dolgozoKezelo.Dolgozok.Any())
        {
            dolgozoKezelo.ListazNevek();
            dolgozoKezelo.LegnagyobbBeruDolgozok();
            dolgozoKezelo.NyugdijhozKozelAllok();
            dolgozoKezelo.MagasBeruDolgozokSzama();
        }
        else
        {
            Console.WriteLine("Nincs adat.");
        }
    }
}
