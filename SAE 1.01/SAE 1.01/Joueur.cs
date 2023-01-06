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
        public AnimatedSprite sprite;
        public Joueur(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction, int attaque, int defense)
            : base(spritesheet, nom, position, pointVie, pointAction, attaque, defense)
        {
            sprite = new AnimatedSprite(spritesheet);
            this.PlayAnim("attaque");
            //this.sorts = new List<Sort>(sorts);
        }

        public void MovePlayer(Case c)
        {
            this.Position = c;
        }

        public void Afficher(SpriteBatch batch)
        {
            batch.Draw(this.sprite, new Vector2(Position.X, Position.Y));
        }

        public void PlayAnim(string anim)
        {
            sprite.Play(anim);
        }

        public void UpdateAnim(float deltaseconds)
        {
            sprite.Update(deltaseconds);
        }
    }
}
