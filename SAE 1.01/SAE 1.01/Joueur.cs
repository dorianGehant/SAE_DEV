using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Joueur : Entite
    {
        private List<Sort> sorts;

        public Joueur(string nom, int pointVie, int pointAction, List<Sort> sorts)
            : base(nom, pointVie, pointAction)
        {
            this.sorts = new List<Sort>(sorts);
        }
    }
}
