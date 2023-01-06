using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SAE_1._01
{
    internal class Carte
    {
        private int longueur;
        private int hauteur;
        private int tailleCase;
        private Case[,] tableauCases;

        public Carte(int longueur, int hauteur, int tailleXase, Texture2D texturecase)
        {
            this.Longueur = longueur;
            this.Hauteur = hauteur;
            TailleCase = tailleXase;
            CreateTableau(texturecase);
            

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

        public int TailleCase
        {
            get
            {
                return tailleCase;
            }

            set
            {
                tailleCase = value;
            }
        }

        void CreateTableau(Texture2D text)
        {
            this.TableauCases = new Case[Longueur, Hauteur];
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    TableauCases[i, j] = new Case(i * TailleCase, j * TailleCase, text);
                }
            }
        }

        public void AfficherMap(SpriteBatch spriteB)
        {
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    Case c = tableauCases[i, j];
                    Vector2 pos = new Vector2(c.X, c.Y);
                    spriteB.Draw(c.Texture, pos,Color.White);
                }
            }
        }
    }
}
