using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
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
        private bool collision = false;

        public Case(int x, int y, Texture2D texture,CreateurCarte carte)
        {
            this.X = x;
            this.Y = y;
            this.Texture = texture;
            this.Couleur = Color.Transparent;
            //this.TryCollision(carte);
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

        public bool Collision
        {
            get
            {
                return this.collision;
            }

            set
            {
                this.collision = value;
            }
        }

        public void Action()
        {
            this.x = 500;
        }

        public void TryCollision(CreateurCarte carte)
        {
            TiledMapTile? tile;
            if (carte.CollisionLayer.TryGetTile((ushort)X, (ushort)Y, out tile) == false)
                this.Collision = false;
            if (!tile.Value.IsBlank)
                this.Collision = true;
            this.Collision = false;
        }
    }
}
