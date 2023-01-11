using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class SortMonocible : Sort
    {
        public SortMonocible(string nom, int valeurEffet, int rangeLancement, int cout, effetSort effet)
            : base(nom, valeurEffet, rangeLancement, cout, effet)
        {

        }

        public override List<Entite> ObtenirCibles(Case position, List<Entite> entitesCombat)
        {
            List<Entite> cibleTouche = new List<Entite>();
            foreach(Entite entite in entitesCombat)
            {
                if(entite.Position == position)
                {
                    cibleTouche.Add(entite);
                }
            }
            return cibleTouche;
        }
    }
}
