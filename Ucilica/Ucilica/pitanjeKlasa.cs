using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucilica
{
    public class pitanjeKlasa
    {
        public int id;
        public string pitanje;
        public List<string> odgovori;
        public string točan;
        public string slika;
        public string predmet;
        public int razred;

        public pitanjeKlasa() { }
        public pitanjeKlasa(int id, string q, List<string> a, string c, string img)
        {
            pitanje = q;
            odgovori = a;
            točan = c;
            slika = img;
        }

    }
}
