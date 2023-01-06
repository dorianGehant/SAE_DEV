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
        public bool jouable;
        public Carte grille;
        public GameManager gameManager;
        Joueur j;
        Ennemi e;

        protected Entite(SpriteSheet spriteSheet, string nom, Case position, int pointVie, int pointAction,Carte carte,GameManager gm)
        {
            this.texture = new AnimatedSprite(spriteSheet);
            this.Nom = nom;
            this.Position = position;
            this.PointVie = pointVie;
            this.PointAction = pointAction;
            this.grille = carte;
            this.gameManager = gm;
            Vector2 pos = this.GetPositionCase(grille.TailleCase);
            this.Move(grille.TableauCases[(int)pos.X, (int)pos.Y]);
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

        public void JouerTour()
        {
            if(jouable == true)
            {
                j.PeutAction();
            }
            else
            {
                e.Reflechis();
            }
        }

        public void Move(Case c)
        {
            if(c.movable == true)
            {
                Vector2 pos = this.GetPositionCase(grille.TailleCase);
                grille.TableauCases[(int)pos.X, (int)pos.Y].movable = true;
                c.movable = false;
                this.Position = c;
            }
            
        }

        public Vector2 GetPositionCase(int tailleCase)
        {
            return new Vector2(this.position.X / tailleCase,this.position.Y / tailleCase);
        }

        public void SetJoueur(Joueur player)
        {
            j = player;
        }

        public void SetEnnemi(Ennemi ennemi)
        {
            e = ennemi;
        }

        public void Afficher(SpriteBatch batch)
        {
            batch.Draw(this.texture, new Vector2(Position.X, Position.Y));
        }

        public void PlayAnim(string anim)
        {
            texture.Play(anim);
        }

        public void UpdateAnim(float deltaseconds)
        {
            texture.Update(deltaseconds);
        }

    }
}
