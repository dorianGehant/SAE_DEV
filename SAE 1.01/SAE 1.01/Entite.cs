using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Entite
    {
        private string nom;
        private int pointVie;
        private int pointAction;

        protected Entite(string nom, int pointVie, int pointAction)
        {
            this.Nom = nom;
            this.PointVie = pointVie;
            this.PointAction = pointAction;
        }




        public override bool Equals(object obj)
        {
            return obj is Entite entite &&
                   this.Nom == entite.Nom &&
                   this.PointVie == entite.pointVie &&
                   this.PointAction == entite.pointAction;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Nom, this.pointVie, this.pointAction);
        }

        public override string ToString()
        {
            return this.Nom + this.PointVie + this.PointAction;
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

        public int PointVie
        {
            get
            {
                return this.pointVie;
            }

            set
            {
                this.pointVie = value;
            }
        }

        public int PointAction
        {
            get
            {
                return this.pointAction;
            }

            set
            {
                this.pointAction = value;
            }
        }

    }
}
