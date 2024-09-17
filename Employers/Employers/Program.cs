using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
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

    static void Main()
    {
        var dolgozok = File.ReadLines("tulajdonsagok_100sor.txt")
            .Select(sor => sor.Split(';'))
            .Where(reszek => reszek.Length == 4)
            .Select(reszek => {
                try
                {
                    return new Dolgozo(
                        int.Parse(reszek[0]),
                        reszek[1].Trim(),
                        int.Parse(reszek[2]),
                        decimal.Parse(reszek[3]));
                }
                catch
                {
                    Console.WriteLine($"Hibás sor: {string.Join(";", reszek)}");
                    return null;
                }
            })
            .Where(d => d != null)
            .ToList();

        if (!dolgozok.Any())
        {
            Console.WriteLine("Nincs adat.");
            return;
        }

        // 3. Alkalmazottak neveinek listázása
        Console.WriteLine("Dolgozók nevei:");
        dolgozok.ForEach(d => Console.WriteLine(d.Nev));

        // 4. Legmagasabb bérű dolgozók
        var maxBer = dolgozok.Max(d => d.Ber);
        Console.WriteLine("\nLegnagyobb bérrel rendelkező dolgozók:");
        dolgozok.Where(d => d.Ber == maxBer)
                .ToList()
                .ForEach(d => Console.WriteLine($"ID: {d.ID}, Név: {d.Nev}"));

        // 5. Nyugdíj előtt állók (10 éven belül)
        Console.WriteLine("\nNyugdíjhoz közel álló dolgozók:");
        dolgozok.Where(d => 65 - d.Eletkor <= 10 && 65 - d.Eletkor > 0)
                .ToList()
                .ForEach(d => Console.WriteLine($"Név: {d.Nev}, Kor: {d.Eletkor}"));

        // 6. Magas bérű dolgozók száma (50000 Ft felett)
        var magasBeruDolgozokSzama = dolgozok.Count(d => d.Ber > 50000);
        Console.WriteLine($"\nDolgozók száma, akik 50,000 forint felett keresnek: {magasBeruDolgozokSzama}");
    }
}
