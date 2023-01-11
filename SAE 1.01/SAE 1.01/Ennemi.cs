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
            this.PlayAnim("Idle");
            grille.TableauCases[position.X, position.Y].Collision = true; 
            Jouable = false;
        }

        public override void JouerTour()
        {
            List<Joueur> listJoueur =  Manageur.SeparateJoueur();
            List<Case>[] allStar = this.GetAllAstar(listJoueur);
            List<Case> Astar = GetCloserAstar(allStar);
            this.Possible(Manageur.BordureCasePossible, false);
            Astar = GetClosestPointFromAstar(Astar);
            //AfficherStar(allStar);
            this.Chemin = Astar;
            this.PointAction -= Astar.Count();
            this.Jouable = false;
        }

        private void EssaiAttaqueJoueur()
        {
            List<Joueur> joueurs = Manageur.SeparateJoueur();
            List<int[]> coordCasesAdjacentes = PathFinding.findpath(this.Position, Grille.TableauCases, 1, true);
            foreach (int[] coord in coordCasesAdjacentes)
            {
                foreach (Joueur joueur in joueurs)
                {
                    if (coord[0] == joueur.Position.X && coord[1] == joueur.Position.Y && this.PointAction >= 1)
                    {
                        this.PlayAnim("attaque");
                        this.Sorts[0].Lancer(joueur.Position, this, Manageur.EntitesCombat);
                    }
                }
            }
        }

        public override void MoveChemin(float deltaSeconds)
        {
            if (Chemin == null || Chemin.Count == 0)
            {
                this.EssaiAttaqueJoueur();
                Manageur.ProchaineEntite();
                return;
            }
            else
            {
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

        List<Case>[] GetAllAstar(List<Joueur> j) 
        {
            List<Case>[] Allstar = new List<Case>[j.Count];
            for (int i = 0; i < Allstar.Length; i++)
            {
                Vector2 pos = GetCaseNextJoueur(j[i]);
                Allstar[i] = this.GetChemin_A_Star(this.Grille.TableauCases[this.Position.Y,this.Position.X],this.Grille.TableauCases[(int)pos.Y,(int) pos.X]);
            }
            return Allstar;
        }

        Vector2 GetCaseNextJoueur(Joueur j)
        {

            Vector2 posHere = new Vector2(this.Position.X, this.Position.Y);
            int y = j.Position.Y;
            int x = j.Position.X;
            List<Noeud> cote =  PathFinding.NoeudAdjacentDeplacable(x, y, this.Grille.TableauCases);
            Vector2 closer = new Vector2(cote[0].PosX,cote[0].PosY);
            for (int i = 1; i < cote.Count; i++)
            {
                if(Vector2.Distance(posHere, new Vector2(cote[i].PosX, cote[i].PosY)) < Vector2.Distance(posHere, new Vector2(cote[i].PosX, cote[i].PosY)))
                {
                    closer = new Vector2(cote[i].PosX, cote[i].PosY);
                }
            }

            return closer;
            

        }

        List<Case> GetCloserAstar(List<Case>[] AllaStar)
        {
            List<Case> Astar = AllaStar[0];
            for (int i = 1; i < AllaStar.Length; i++)
            {
                if(AllaStar[i].Count < Astar.Count)
                {
                    Astar = AllaStar[i];
                }
            }

            return Astar;
        }

        void AfficherStar(List<Case> Astar)
        {
            for (int i = 0; i < Astar.Count; i++)
            {
                Console.Write(Astar[i] + " ");
            }
        }

        void AfficherStar(List<Case>[] AllStar)
        {
            for (int i = 0; i < AllStar.Length; i++)
            {
                for (int j = 0; j < AllStar[i].Count; j++)
                {
                    Console.Write(AllStar[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        void SetAstar(Joueur j)
        {

                Console.WriteLine("Ia pos : " + this.Position.Y + " " + this.Position.X);
                Console.WriteLine(j.Nom + " : " + j.Position.Y + " " + j.Position.X);
                this.Chemin_A_Star(this.Grille.TableauCases[this.Position.Y, this.Position.X], this.Grille.TableauCases[j.Position.Y, j.Position.X]);
        }

        List<Case> GetClosestPointFromAstar(List<Case> Astar)
        {
            List<Case> result = new List<Case>();
            for (int i = 0; i < Astar.Count; i++)
            {
                int x = Astar[i].X;
                int y = Astar[i].Y;
                Console.WriteLine(this.clicDansZonePossible(this.Grille.TableauCases[x,y]));
                if (this.clicDansZonePossible(this.Grille.TableauCases[x, y]))
                {
                    Console.WriteLine("add " + Astar[i]);
                    result.Add(Astar[i]);
                }
            }
            return result;
        }

        public override void DeplacementFini()
        {
            this.PlayAnim("Idle");
            this.EssaiAttaqueJoueur();
        }
    }
}
