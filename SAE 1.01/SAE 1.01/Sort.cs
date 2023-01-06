using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    enum typeEffetSort
    {
        SORT_MODIF_VIE,
        SORT_MODIF_ATTAQUE,
        SORT_MODIF_DEFENSE,
        SORT_MODIF_POINTACTION
    }

    enum typeSort
    {
        SORT_MONOCIBLE,
        SORT_CROIX,
        SORT_LIGNE
    }

    internal class Sort
    {
        private typeEffetSort effet;
        private typeSort type;
        private string nom;
        private int valeurEffet;
        private int portee;

        public Sort(typeEffetSort effet, string nom, int valeurEffet)
        {
            this.Effet = effet;
            this.Nom = nom;
            this.ValeurEffet = valeurEffet;
        }
        /*
        public void Lancer(int attaqueLanceur, Vector2 position)
        {
            List<Case> casesTouchees;

            switch (this.typeSort)
            {
                case typeSort.SORT_MONOCIBLE:

                    break;

                case typeEffetSort.SORT_CHANGEMENT_STAT
            }
        }
        */

        public int InfligeDegats(Entite cible, int degats)
        {
            int degatsInfliges = degats - cible.Defense;
            cible.PointVie -= degatsInfliges;
            return degatsInfliges;
        }

        public int Soigne(Entite cible, int soin)
        {
            cible.PointVie += soin;
            return soin;
        }

        public int ModificationStat(Entite cible, int pourcModif, typeEffetSort effet)
        {
            float multiplicateur = 1 + (pourcModif / 100);
            switch (effet)
            {
                case typeEffetSort.SORT_MODIF_ATTAQUE:
                    cible.Attaque = (int)Math.Floor(cible.Attaque * multiplicateur);
                    break;
                case typeEffetSort.SORT_MODIF_DEFENSE:
                    cible.Defense = (int)Math.Floor(cible.Defense * multiplicateur);
                    break;
                case typeEffetSort.SORT_MODIF_POINTACTION:
                    cible.PointAction = (int)Math.Floor(cible.PointAction * multiplicateur);
                    break;
            }

            return pourcModif;
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

        internal typeEffetSort Effet
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
