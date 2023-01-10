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
        public Ennemi(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction, int defense, int attaque, Carte grille, List<Sort> sorts, GameManager gm)
            : base(spritesheet, nom, position, pointVie, pointAction, defense, attaque, grille, sorts, gm)
        {
            Jouable = false;
        }

        public override void JouerTour()
        {
            this.Move(this.Grille.TableauCases[(int)this.Position.X+1, 0]);
            this.GameManager.ProchaineEntite();
        }

        public override void EstTuePar(Entite tueur, List<Entite> listeEntitesVivantes)
        {
            listeEntitesVivantes.Remove(this);
        }
    }
}
