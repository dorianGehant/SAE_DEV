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

        public override void DeplacementFini()
        {
            this.PlayAnim("Idle");
        }

        public override void MoveChemin(float deltaSeconds)
        {
            if (Chemin == null || Chemin.Count == 0)
            {
                this.Jouable = true;
                PlayAnim("Idle");
            }

            TimeNextCase -= deltaSeconds;
            if (TimeNextCase <= 0)
            {
                this.Move(Chemin[0]);
                Chemin.RemoveAt(0);
                TimeNextCase = SPEED_BETWEEN_CASE;
                if (Chemin.Count == 0)
                {
                    DeplacementFini();
                }

            }
        }
    }
}
