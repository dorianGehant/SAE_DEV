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
    internal class Joueur : Entite
    {
        private List<Sort> sorts;

        public Joueur(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction,Carte grille,GameManager gm)
            : base(spritesheet, nom, position, pointVie, pointAction,grille,gm)
        {
            SetJoueur(this);
            
            this.PlayAnim("Idle");
            jouable = true;
            //this.sorts = new List<Sort>(sorts);
        }

        

        public override void JouerTour()
        {

        }
    }
}
