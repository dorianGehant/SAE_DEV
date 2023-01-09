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
    internal class Ennemi : Entite
{
        public Ennemi(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction,Carte grille,GameManager gm)
            : base(spritesheet, nom, position, pointVie, pointAction,grille,gm)
        {
            SetEnnemi(this);
            jouable = false;
        }

        public override void JouerTour()
        {
            this.Move(this.grille.TableauCases[(int)this.Position.X+1, 0]);
            this.gameManager.ProchaineEntite();
        }
}
}
