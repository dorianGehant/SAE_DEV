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
        public const int ID_TILE_COLLISION = 578;

        private int x;
        private int y;
        private Texture2D texture;
        private bool collision = false;

        public Case(int x, int y, Texture2D texture, CreateurCarte carte)
        {
            this.X = x;
            this.Y = y;
            this.Texture = texture;
            this.Collision = this.testeCollision(carte);
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

        public override bool Equals(object obj)
        {
            return obj is Case caseCompare &&
               this.X == caseCompare.X &&
               this.Y == caseCompare.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Texture);
        }

        public override string ToString()
        {
            return "posX : " + X + " posY : " + Y + " texture : " + Texture;
        }

        private bool testeCollision(CreateurCarte carte)
        {
            int test = (int)carte.CollisionLayer.Tiles[190].GlobalIdentifier;
            //si en dehors de la grille
            if (this.x < 0 || this.y < 0 || this.x > carte.TailleCarte().X || this.Y > carte.TailleCarte().Y)
                return false;

            if (carte.CollisionLayer.Tiles[this.y * (int) carte.CollisionLayer.Width + this.x].GlobalIdentifier == 578)
                return true;

            return false;
        }
    }
}
