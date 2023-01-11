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
            SetEnnemi(this);
            this.PlayAnim("Idle");
            jouable = false;
        }

        public override void JouerTour()
        {
            List<Entite> list = Manageur.EntitesCombat;
            List<Joueur> listJoueur =  this.SeparateJoueur(list);
            List<Case>[] allStar = this.GetAllAstar(listJoueur);
            List<Case> Astar = GetCloserAstar(allStar);
            this.Possible(Manageur.BordureCasePossible, false);
            Astar = GetClosestPointFromAstar(Astar);
            //AfficherStar(allStar);
            this.Chemin = Astar;
            this.Jouable = false;
        }

        List<Joueur> SeparateJoueur(List<Entite> listEntite)
        {
            List<Joueur> j = new List<Joueur>();
            for (int i = 0; i < listEntite.Count; i++)
            {
                if (typeof(Joueur).IsInstanceOfType(listEntite[i]))
                {
                    j.Add((Joueur)listEntite[i]);
                }
            }
            return j;

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
            if (CheckIfPlayerRange())
            {
                //attacks
            }
            else
            {
                Manageur.ProchaineEntite();
            }
        }

        bool CheckIfPlayerRange()
        {
            return false;
            this.Move(this.Grille.TableauCases[(int)this.Position.X+1, 0]);
            this.GameManager.ProchaineEntite();
        }

        public override void EstTuePar(Entite tueur, List<Entite> listeEntitesVivantes)
        {
            listeEntitesVivantes.Remove(this);
        }
    }
}
