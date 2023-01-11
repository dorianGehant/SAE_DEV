using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Sprites;
namespace SAE_1._01
{

    enum effetSort
    {
        MODIF_PV,
        MODIF_DEFENSE,
        MODIF_ATTAQUE,
        MODIF_PA
    }

    abstract internal class Sort
    {
        private string nom;
        private int valeurEffet;
        private int rangeLancement;
        private int cout;
        effetSort effet;
        string nameEffect;

        public Sort(string nom, int valeurEffet, int rangeLancement, int cout, effetSort effet,string nameSpellEffect)
        {
            this.Nom = nom;
            this.ValeurEffet = valeurEffet;
            this.Effet = effet;
            this.RangeLancement = rangeLancement;
            this.Cout = cout;
            this.nameEffect = nameSpellEffect;
        }

        abstract public List<Entite> ObtenirCibles(Case position, List<Entite> entitesCombat);

        public void Lancer(Case caseCiblee, Entite lanceur, List<Entite> entitesCombat,AnimatedSprite effect)
        {
            List<Entite> entitesTouchees = ObtenirCibles(caseCiblee, entitesCombat);
            effect.Play(this.nameEffect);
            switch (this.effet)
            {
                case effetSort.MODIF_PV:
                    foreach(Entite entite in entitesTouchees)
                    {
                        entite.ModifPV(this.valeurEffet, lanceur);
                    }
                    break;
                case effetSort.MODIF_DEFENSE:
                    foreach (Entite entite in entitesTouchees)
                    {
                        entite.ModifDefense(this.valeurEffet);
                    }
                    break;
                case effetSort.MODIF_ATTAQUE:
                    foreach (Entite entite in entitesTouchees)
                    {
                        entite.ModifAttaque(this.valeurEffet);
                    }
                    break;
                case effetSort.MODIF_PA:
                    foreach (Entite entite in entitesTouchees)
                    {
                        entite.ModifPA(this.valeurEffet);
                    }
                    break;
            }

            lanceur.PointAction -= this.Cout;
            lanceur.Jouable = true;
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

        internal effetSort Effet
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

        public int RangeLancement
        {
            get
            {
                return this.rangeLancement;
            }

            set
            {
                this.rangeLancement = value;
            }
        }

        public int Cout
        {
            get
            {
                return this.cout;
            }

            set
            {
                this.cout = value;
            }
        }
    }
}
