using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    enum typeSort
    {
        SORT_DEGAT,
        SORT_CHANGEMENT_STAT,
    }

    internal class Sort
    {
        private typeSort effet;
        private string nom;
        private int valeurEffet;

        public Sort(typeSort effet, string nom, int valeurEffet)
        {
            this.Effet = effet;
            this.Nom = nom;
            this.ValeurEffet = valeurEffet;
        }

        public void Lancer(Vector2 position)
        {

        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public int ValeurEffet
        {
            get
            {
                return this.valeurEffet;
            }

            set
            {
                this.valeurEffet = value;
            }
        }

        internal typeSort Effet
        {
            get
            {
                return this.effet;
            }

            set
            {
                this.effet = value;
            }
        }
    }
}
