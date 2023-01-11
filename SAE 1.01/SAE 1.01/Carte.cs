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

        public Carte(int longueur, int hauteur, int tailleXase, Texture2D texturecase, CreateurCarte carte)
        {
            this.Longueur = longueur;
            this.Hauteur = hauteur;
            TailleCase = tailleXase;
            CreateTableau(texturecase, carte);
            
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

        public Case[,] TableauCases
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
                return this.tailleCase;
            }

            set
            {
                this.tailleCase = value;
            }
        }

        void CreateTableau(Texture2D text, CreateurCarte carte)
        {
            this.TableauCases = new Case[Longueur, Hauteur];
            for (int i = 0; i < Longueur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    TableauCases[i, j] = new Case(i, j, text, carte);

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
                    Vector2 pos = new Vector2(c.X * TailleCase, c.Y * TailleCase);
                    spriteB.Draw(c.Texture, pos,Color.White);
                }
            }
        }

        public void resetTextureCases(Texture2D textureBase)
        {
            foreach(Case c in this.tableauCases)
            {
                c.Texture = textureBase;
            }
        }

    }
}
