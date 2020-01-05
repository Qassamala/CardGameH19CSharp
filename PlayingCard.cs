using System;
using System.Collections.Generic;
using System.Text;

namespace CardGameH19CSharp
{
    public enum Färg
    {
        Spader = 3,
        Hjärter = 2,
        Ruter = 1,
        Klöver = 0
    }

    public enum Valör
    {
        Två = 0,
        Tre = 1,
        Fyra = 2,
        Fem = 3,
        Sex = 4,
        Sju = 5,
        Åtta = 6,
        Nio = 7,
        Tio = 8,
        Knekt = 9,
        Dam = 10,
        Kung = 11,
        Ess = 12
    }
    class PlayingCard
    {
        public Färg Färg { get; set; }

        public Valör Valör { get; set; }

        public PlayingCard(Färg färg, Valör valör)
        {
            Färg = färg;
            Valör = valör;
        }

    }
}
