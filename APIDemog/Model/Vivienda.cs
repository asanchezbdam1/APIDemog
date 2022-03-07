using System;
using System.Collections.Generic;
using System.Text;

namespace APIDemog.Model
{
    public class Vivienda
    {
        public string Localidad { get; set; }
        public string Promotora { get; set; }
        public int Ofertadas { get; set; }
        public int Coord_X { get; set; }
        public int Coord_Y { get; set; }
        public bool HasCoords { get; set; }
        public string TelfPromo { get; set; }
        public string MailPromo { get; set; }
        public string PagPromo { get; set; }
    }
}
