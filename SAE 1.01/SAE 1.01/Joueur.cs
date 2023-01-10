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

        public Joueur(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction, int defense, int attaque, Carte grille, List<Sort> sorts, GameManager gm)
            : base(spritesheet, nom, position, pointVie, pointAction, defense, attaque, grille, sorts, gm)
        {
            this.PlayAnim("Idle");
            Jouable = true;
        }

        public override void JouerTour()
        {

        }

        public override void EstTuePar(Entite tueur, List<Entite> listeEntitesVivantes)
        {
            listeEntitesVivantes.Remove(this);
        }
    }
}
