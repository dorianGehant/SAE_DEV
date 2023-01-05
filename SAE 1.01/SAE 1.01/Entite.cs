using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Sprites;

namespace SAE_1._01
{
    internal class Entite
    {
        private string nom;
        private int pointVie;
        private int pointAction;
        private AnimatedSprite texture;
        private Case position;

        protected Entite(SpriteSheet spriteSheet, string nom, Case position, int pointVie, int pointAction)
        {
            this.texture = new AnimatedSprite(spriteSheet);
            this.Nom = nom;
            this.Position = position;
            this.PointVie = pointVie;
            this.PointAction = pointAction;
        }

        public void Deplacer(Vector2 position, Carte carte)
        {
            this.position = carte.TableauCases[(int)position.X, (int)position.Y];
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

        public AnimatedSprite Texture
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

        internal Case Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }
    }
}
