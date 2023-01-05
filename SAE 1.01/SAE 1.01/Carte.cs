using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Carte
    {
        private int longueur;
        private int hauteur;
        private Case[,] tableauCases;

        public Carte(int longueur, int hauteur, Case[,] tableauCases)
        {
            this.Longueur = longueur;
            this.Hauteur = hauteur;
            this.TableauCases = tableauCases;
        }

        public int Longueur
        {
            get
            {
                return this.longueur;
            }

            set
            {
                this.longueur = value;
            }
        }

        public int Hauteur
        {
            get
            {
                return this.hauteur;
            }

            set
            {
                this.hauteur = value;
            }
        }

        internal Case[,] TableauCases
        {
            get
            {
                return this.tableauCases;
            }

            set
            {
                this.tableauCases = value;
            }
        }
    }
}
