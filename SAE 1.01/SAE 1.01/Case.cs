using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Case
    {
        private int x;
        private int y;
        private Texture2D texture;
        private Color couleur;

        public Case(int x, int y, Texture2D texture)
        {
            this.X = x;
            this.Y = y;
            this.Texture = texture;
            this.couleur = Color.Transparent;
        }

        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        public Color Couleur
        {
            get
            {
                return this.couleur;
            }

            set
            {
                this.couleur = value;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }

            set
            {
                this.texture = value;
            }
        }

        public void Action()
        {
            this.x = 500;
        }
    }
}
