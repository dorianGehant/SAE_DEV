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
        public Ennemi(SpriteSheet spritesheet, string nom, Case position, int pointVie, int pointAction,Carte grille,GameManager gm)
            : base(spritesheet, nom, position, pointVie, pointAction,grille,gm)
        {
            SetEnnemi(this);
            jouable = true;
        }

        public override void JouerTour()
        {
            List<Entite> list = gameManager.GetListEntite();
            List<Joueur> listJoueur =  this.SeparateJoueur(list);
            List<Case>[] allStar = this.GetAllAstar(listJoueur);
            List<Case> Astar = GetCloserAstar(allStar);
            //Possible(gameManager._bordureCasePossible);
            //Astar = GetClosestPointFromAstar(Astar);
            AfficherStar(allStar);
            this.chemin = Astar;
            this.jouable = false;
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
                Console.WriteLine("Ia pos : " + this.grille.TableauCases[this.Position.Y, this.Position.X]);
                Console.WriteLine(j[i].Nom + " : " + this.grille.TableauCases[j[i].Position.Y, j[i].Position.X]);
                Allstar[i] = this.GetChemin_A_Star(this.grille.TableauCases[this.Position.Y,this.Position.X],this.grille.TableauCases[j[i].Position.Y, j[i].Position.X]);
            }
            return Allstar;
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
                this.Chemin_A_Star(this.grille.TableauCases[this.Position.Y, this.Position.X], this.grille.TableauCases[j.Position.Y, j.Position.X]);
        }

        List<Case> GetClosestPointFromAstar(List<Case> Astar)
        {
            Astar.Reverse();
            for (int i = 0; i < Astar.Count; i++)
            {
                
                if (!clicDansZonePossible(Astar[i]))
                {
                    Astar.RemoveAt(i);
                }
                else
                    break;
            }
            Astar.Reverse();
            return Astar;
        }
    }
}
