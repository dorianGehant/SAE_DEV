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
    abstract internal class Entite
    {
        public const float SPEED_BETWEEN_CASE = 0.3f;
        public float TimeNextCase = SPEED_BETWEEN_CASE;
        private string nom;
        private int pointVie;
        private int pointAction;
        private int pointActionMax;
        private AnimatedSprite texture;
        private Case position;
        public bool jouable;
        public Carte grille;
        public GameManager gameManager;
        Joueur j;
        Ennemi e;
        List<Case> chemin;
        List<int[]> cases_possibles;

        protected Entite(SpriteSheet spriteSheet, string nom, Case position, int pointVie, int pointAction,Carte carte,GameManager gm)
        {
            this.texture = new AnimatedSprite(spriteSheet);
            this.Nom = nom;
            this.Position = position;
            this.PointVie = pointVie;
            this.PointAction = pointAction;
            this.pointActionMax = pointAction;
            this.grille = carte;
            this.gameManager = gm;
            Vector2 pos = this.GetPositionCase(grille.TailleCase);
            this.Move(grille.TableauCases[(int)pos.X, (int)pos.Y]);
            this.jouable = true;
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

        public int PointActionMax
        {
            get
            {
                return this.pointActionMax;
            }

            set
            {
                this.pointActionMax = value;
            }
        }

        abstract public void JouerTour();

        public void Move(Case c)
        {
            if(c.Collision == false)
            {
                Vector2 pos = new Vector2(this.position.X,this.position.Y);
                grille.TableauCases[(int)pos.X, (int)pos.Y].Collision = false;
                c.Collision = true;
                this.Position = c;
                this.PointAction--;
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
            batch.Draw(this.texture, new Vector2(Position.X * this.grille.TailleCase + grille.TailleCase/2, Position.Y * this.grille.TailleCase + grille.TailleCase / 2));
        }

        public void PlayAnim(string anim)
        {
            texture.Play(anim);
        }

        public void UpdateAnim(float deltaseconds)
        {
            texture.Update(deltaseconds);
        }

        public void ResetPA()
        {
            pointAction = pointActionMax;
        }

        public void Possible(Texture2D _bordureCasePossible)
        {
            List<int[]> deplacementPossible = PathFinding.findpath(this.Position, grille.TableauCases , this.PointAction);
            for (int i = 0; i < deplacementPossible.Count-1; i++)
            {
                grille.TableauCases[deplacementPossible[i][0],deplacementPossible[i][1]].Texture = _bordureCasePossible;
            }
            this.cases_possibles = deplacementPossible;
        }
        public bool clicDansZonePossible(Case possition_clic)
        {
            for (int i = 0; i < this.cases_possibles.Count - 1; i++)
            {
                if (this.cases_possibles[i][0] == possition_clic.X && this.cases_possibles[i][1] == possition_clic.Y)
                {
                    return true;
                }
            }
            return false;
        }
        public void enleverPossible(Texture2D _bordureCase)
        {
            for (int i = 0; i < this.cases_possibles.Count - 1; i++)
            {
                grille.TableauCases[this.cases_possibles[i][0], this.cases_possibles[i][1]].Texture = _bordureCase;
            }
        }
        public void MoveChemin(float deltaSeconds)
        {
            if (chemin == null || chemin.Count == 0)
            {
                this.jouable = true;
            }
                
            
            TimeNextCase -= deltaSeconds;
            if (TimeNextCase <= 0)
            {
                this.Move(chemin[0]);
                chemin.RemoveAt(0);
                TimeNextCase = SPEED_BETWEEN_CASE;

            }

        }

        public void Chemin_A_Star(Case depart,Case arrive)
        {
           
            List<int[]> res = PathFinding.A_star(depart, arrive, this.grille.TableauCases);
            List<Case> chemin = new List<Case>();
            for (int i = 0; i < res.Count; i++)
            {
                int x = res[i][1];
                int y = res[i][0];
                chemin.Add(this.grille.TableauCases[x, y]);
                Console.WriteLine(x+""+y);
            }
            this.chemin = chemin;
            this.jouable = false;
            Console.WriteLine("Coordonne depart" + chemin[0].X + " " + chemin[0].Y);
            Console.WriteLine("Coordonne arrive" + chemin[chemin.Count-1].X + " " + chemin[chemin.Count-1].Y);

        }

    }
}
