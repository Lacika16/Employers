using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nepesseg
{
    class Orszag
    {
        public string Orszagnev { get; private set; }
        public int Terulet { get; private set; }
        public int Nepesseg { get; private set; }
        public string Fovaros { get; private set; }
        
        public int FovarosNepesseg {  get; private set; }

            

        public int Népsűrűség => (int)Math.Round((double)Nepesseg / Terulet, MidpointRounding.AwayFromZero);
        public bool fővárosbanLakik30 => FovarosNepesseg * 1000.0 / Nepesseg > 0.3;

        public Orszag(string adatsor)
        {
            string[] m = adatsor.Split(';');
            Orszagnev = m[0];
            Terulet = int.Parse(m[1]);
            string s = m[2];

        }

        static void Main()
    }
}