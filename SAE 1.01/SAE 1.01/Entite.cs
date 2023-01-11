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
        private int defense;
        private int attaque;
        private int pointActionMax;
        private List<Sort> sorts;
        private AnimatedSprite texture;
        private Case position;
        private bool jouable;
        private Sort sortEnLancement;
        private Carte grille;
        private GameManager gameManager;
        List<Case> chemin;
        List<int[]> cases_possibles;

        protected Entite(SpriteSheet spriteSheet, string nom, Case position, int pointVie, int pointAction, int defense, int attaque, Carte carte, List<Sort> sorts, GameManager gm)
        {
            this.texture = new AnimatedSprite(spriteSheet);
            this.Nom = nom;
            this.Position = position;
            this.PointVie = pointVie;
            this.PointAction = pointAction;
            this.Defense = defense;
            this.Attaque = attaque;
            this.pointActionMax = pointAction;
            this.Sorts = sorts;
            this.Grille = carte;
            this.Manageur = gm;
            Vector2 pos = this.GetPositionCase(Grille.TailleCase);
            this.Move(Grille.TableauCases[(int)pos.X, (int)pos.Y]);
            this.Jouable = true;
            this.SortEnLancement = null;
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
                   this.PointAction == entite.pointAction &&
                   this.Position == entite.Position;
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

        internal List<Sort> Sorts
        {
            get
            {
                return this.sorts;
            }

            set
            {
                this.sorts = value;
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

        public bool Jouable
        {
            get
            {
                return this.jouable;
            }

            set
            {
                this.jouable = value;
            }
        }

        public Sort SortEnLancement
        {
            get
            {
                return this.sortEnLancement;
            }

            set
            {
                this.sortEnLancement = value;
            }
        }

        internal Carte Grille
        {
            get
            {
                return this.grille;
            }

            set
            {
                this.grille = value;
            }
        }

        internal List<Case> Chemin
        {
            get
            {
                return this.chemin;
            }

            set
            {
                this.chemin = value;
            }
        }

        public List<int[]> Cases_possibles
        {
            get
            {
                return this.cases_possibles;
            }

            set
            {
                this.cases_possibles = value;
            }
        }

        public int Defense
        {
            get
            {
                return this.defense;
            }

            set
            {
                this.defense = value;
            }
        }

        public int Attaque
        {
            get
            {
                return this.attaque;
            }

            set
            {
                this.attaque = value;
            }
        }

        internal GameManager Manageur
        {
            get
            {
                return this.gameManager;
            }

            set
            {
                this.gameManager = value;
            }
        }

        abstract public void JouerTour();
      

        public void Move(Case c)
        {
            if(c.Collision == false)
            {
                Vector2 pos = new Vector2(this.position.X,this.position.Y);
                Grille.TableauCases[(int)pos.X, (int)pos.Y].Collision = false;
                c.Collision = true;
                this.Position = c;
                this.PointAction--;
            }
            
        }
       


        public Vector2 GetPositionCase(int tailleCase)
        {
            return new Vector2(this.position.X / tailleCase,this.position.Y / tailleCase);
        }

        public void Afficher(SpriteBatch batch)
        {
            batch.Draw(this.texture, new Vector2(Position.X * this.Grille.TailleCase + Grille.TailleCase/2, Position.Y * this.Grille.TailleCase + Grille.TailleCase / 2));
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

        public void Possible(Texture2D _bordureCasePossible, bool afficher = true)
        {
            List<int[]> deplacementPossible = PathFinding.findpath(this.Position, Grille.TableauCases , this.PointAction, false);
            for (int i = 0; i < deplacementPossible.Count-1; i++)
            {
                if (afficher)
                {
                    Grille.TableauCases[deplacementPossible[i][0], deplacementPossible[i][1]].Texture = _bordureCasePossible;
                }
            }
            this.Cases_possibles = deplacementPossible;
        }

        public void Possible(Texture2D _bordureCasePossible, Sort sort, bool afficher = true)
        {
            List<int[]> casesLancementPossible = PathFinding.findpath(this.Position, Grille.TableauCases, sort.RangeLancement, true);
            for (int i = 0; i < casesLancementPossible.Count - 1; i++)
            {
                if (afficher)
                {
                    Grille.TableauCases[casesLancementPossible[i][0], casesLancementPossible[i][1]].Texture = _bordureCasePossible;
                }
            }
            this.Cases_possibles = casesLancementPossible;
        }

        public bool clicDansZonePossible(Case possition_clic)
        {
            for (int i = 0; i < this.Cases_possibles.Count - 1; i++)
            {
                if (this.Cases_possibles[i][0] == possition_clic.X && this.Cases_possibles[i][1] == possition_clic.Y)
                {
                    return true;
                }
            }
            return false;
        }
        public void enleverPossible(Texture2D _bordureCase)
        {
            for (int i = 0; i < this.Cases_possibles.Count - 1; i++)
            {
                Grille.TableauCases[this.Cases_possibles[i][0], this.Cases_possibles[i][1]].Texture = _bordureCase;
            }
        }
        abstract public void MoveChemin(float deltaSeconds);

        abstract public void DeplacementFini();

        public void Chemin_A_Star(Case depart,Case arrive)
        {

            List<Case> chemin = GetChemin_A_Star(depart, arrive);
            this.Chemin = chemin;
            this.Jouable = false;
            Console.WriteLine("Coordonne depart" + chemin[0].X + " " + chemin[0].Y);
            Console.WriteLine("Coordonne arrive" + chemin[chemin.Count-1].X + " " + chemin[chemin.Count-1].Y);

        }

        public List<Case> GetChemin_A_Star(Case depart,Case arrive)
        {
            List<int[]> res = PathFinding.A_star(depart, arrive, this.Grille.TableauCases);
            List<Case> chemin = new List<Case>();
            for (int i = 0; i < res.Count; i++)
            {
                int x = res[i][1];
                int y = res[i][0];
                chemin.Add(this.Grille.TableauCases[x, y]);
                Console.WriteLine(x + "" + y);
            }
            return chemin;
        }

        public void EstTuePar(Entite tueur, List<Entite> listeEntitesVivantes)
        {
            Grille.TableauCases[this.Position.X, this.Position.Y].Collision = false; 
            listeEntitesVivantes.Remove(this);
        }

        public void ModifPV(int valeurModif, Entite lanceur)
        {
            //si c'est des dégats on en enlève à la valeur la défense de la cible et on ajoute l'attaque du lanceur
            if(valeurModif < 0)
            {
                valeurModif = valeurModif - lanceur.Attaque + this.Defense;
            }
            //sinon c'est un sort de soin et on ajoute jusque l'attaque du lanceur
            else
            {
                valeurModif = valeurModif + lanceur.Attaque;
            }
            this.pointVie += valeurModif;

            if(this.PointVie <= 0)
            {
                this.EstTuePar(lanceur, Manageur.EntitesCombat);
            }
        }

        public void ModifAttaque(int valeurModif)
        {
            this.Attaque += valeurModif;
        }

        public void ModifDefense(int valeurModif)
        {
            this.Defense += valeurModif;
        }
        public void GetCaracteristique(out string name,out string atq,out string def,out string pv)
        {
            name = this.Nom;
            atq = this.Attaque.ToString();
            def = this.Defense.ToString();
            pv = this.PointVie.ToString();
            
        }

        

        public void ModifPA(int valeurModif)
        {
            this.PointActionMax += valeurModif;
        }
    }
}
