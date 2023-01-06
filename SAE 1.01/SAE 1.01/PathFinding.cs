using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class PathFinding
    {
        private int[][] carte = new int[][] {new int[] {-1,-1,-1,-1,-1,-1,-1},
                                     new int[] {-1,2,0,0,0,0,-1},
                                     new int[] {-1,0,0,1,0,0,-1},
                                     new int[] {-1,1,1,0,0,0,-1},
                                     new int[] {-1,0,0,1,0,0,-1},
                                     new int[] {-1,0,0,0,0,0,-1},
                                     new int[] {-1,-1,-1,-1,-1,-1,-1} };
        private int debutX;
        private int debutY;
        private int finX;
        private int finY;

        public PathFinding(int finX, int finY, int debutY, int debutX, int[][] carte)
        {
            this.FinX = finX;
            this.FinY = finY;
            this.DebutY = debutY;
            this.DebutX = debutX;
            this.Carte = carte;
        }

        public int FinX
        {
            get
            {
                return this.finX;
            }

            set
            {
                this.finX = value;
            }
        }

        public int FinY
        {
            get
            {
                return this.finY;
            }

            set
            {
                this.finY = value;
            }
        }

        public int DebutY
        {
            get
            {
                return this.debutY;
            }

            set
            {
                this.debutY = value;
            }
        }

        public int DebutX
        {
            get
            {
                return this.debutX;
            }

            set
            {
                this.debutX = value;
            }
        }

        public int[][] Carte
        {
            get
            {
                return this.carte;
            }

            set
            {
                this.carte = value;
            }
        }
        static List<Noeud> NoeudAdjacentDeplacable(int x, int y, Case[,] map)
        {
            List<Noeud> noeudProposer = new List<Noeud>() {
            new Noeud (x, y - 1 ),
            new Noeud (x, y + 1),
            new Noeud (x - 1, y),
            new Noeud (x + 1, y)};

            List<Noeud> temp = new List<Noeud>();
            foreach (Noeud noeud in noeudProposer)
            {

                if (noeud.PosX >= 0 && noeud.PosX < map.GetLength(1) && noeud.PosY >=0 && noeud.PosY < map.GetLength(0)&&map[noeud.PosY, noeud.PosX].Collision == false)
                {
                    //Console.WriteLine("noeud DEDANS: " + noeud.PosX + " " + noeud.PosY);
                    temp.Add(noeud);
                }
            }
            return temp;
        }
        static List<int[]> NoeudAdjacentDeplacableAvecPoint(int x, int y, Case[,] map, int cout, int maxPoint)
        {
            List<int[]> noeudProposer = new List<int[]> {
                new int[] {x,y-1, cout},
                new int[] {x,y+1, cout},
                new int[] {x-1,y, cout},
                new int[] {x+1,y, cout}};
            List<int[]> deplacable = new List<int[]> { };
            for (int i = 0; i < noeudProposer.Count; i++)
            {
                if (noeudProposer[i][0] >= 0 && noeudProposer[i][0] < map.GetLength(1) && noeudProposer[i][1] >= 0 && noeudProposer[i][1] < map.GetLength(0) && map[noeudProposer[i][1], noeudProposer[i][0]].Collision == false && noeudProposer[i][2] <= maxPoint)
                {
                    deplacable.Add(noeudProposer[i]);
                }
            }
            return deplacable;
        }
        static List<int[]> Pop(List<int[]> file)
        {
            for (int i = 0; i < file.Count - 1; i++)
            {
                file[i] = file[i + 1];
            }
            file.RemoveAt(file.Count - 1);
            return file;
        }
        public static List<int[]> A_star(Case depart, Case arrive, Case[,] carte)
        {
            //les -1 sont les bordures, 2 le début, 3 la fin, 1 les obstacles, 0 rien
            Noeud current = null;
            Noeud debut = new Noeud(depart.X, depart.Y);
            Noeud fin = new Noeud(arrive.X, arrive.Y);
            List<Noeud> openList = new List<Noeud>();
            List<Noeud> closedList = new List<Noeud>();
            int g = 0; //-> G est la distance parcouru du début

            //Ont commence par ajouter le noeud de départ à la liste en train de parcourir
            openList.Add(debut);
            while (openList.Count > 0)
            {

                //Ont commence par le noeud qui à le plus petit F
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);


                //ont ajoute le noeud actuel à la liste parcouru
                closedList.Add(current);

                //ont l'enlève de la liste en train de parcourir
                openList.Remove(current); //cela permet d'éviter de parcourir un noeud que l'ont à déjà parcouru

                //montre le noeud actuel sur la carte
                //Console.SetCursorPosition(current.PosX, current.PosY);
                //Console.Write('.');
                //Console.SetCursorPosition(current.PosX, current.PosY);
                //System.Threading.Thread.Sleep(200);

                //si nous ajoutons dans la liste parcouru la destination alors ont à trouver un chemin
                if (closedList.FirstOrDefault(l => l.PosX == fin.PosX && l.PosY == fin.PosY) != null)
                    break;

                var adjacentSquares = NoeudAdjacentDeplacable(current.PosX, current.PosY, carte);
                g++;



                foreach (var adjacentSquare in adjacentSquares)
                {
                    //si le noeud adjacent est déjà dans la liste parcouru alors ont continu
                    if (closedList.FirstOrDefault(l => l.PosX == adjacentSquare.PosX
                            && l.PosY == adjacentSquare.PosY) != null)
                        continue;

                    //si non alors ont le parcours
                    if (openList.FirstOrDefault(l => l.PosX == adjacentSquare.PosX
                            && l.PosY == adjacentSquare.PosY) == null)
                    {
                        //Ont met G (la distance du début), H (la disctance à vol d'oiseau de la fin), et F (G + H), et ont met le parent
                        adjacentSquare.G = g;
                        adjacentSquare.H = adjacentSquare.ComputeHScore(fin.PosX, fin.PosY);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        //ont ajoute le noeud dans la liste en train de parcourir
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        //test si le G du noeud que l'ont parcour (la distance du début) est plus petit que F (G + H) du noeud adjacent
                        //si oui alors ce noeud signifie que le chemin est plus court, il faut mettre à jour le noeud parent.
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;

                        }
                    }
                }
            }
            List<int[]> chemin = new List<int[]> { };
            //montre le chemin en partant de la fin
            while (current != null)
            {
                chemin.Add(new int[] { current.PosX, current.PosY });
                //Console.SetCursorPosition(current.PosX, current.PosY);
                //Console.Write('*');
                //Console.SetCursorPosition(current.PosX, current.PosY);
                current = current.Parent;
                //System.Threading.Thread.Sleep(200);
            }
            //Console.SetCursorPosition(fin.PosX + 1, fin.PosY + 1);
            chemin.Reverse();
            return chemin;
        }
        public static List<int[]> findpath(Case depart, Case[,] carte, int maxPoint)
        {
            int cout = 0;


            List<int[]> resultat = new List<int[]> { };
            List<int[]> parcour = new List<int[]> { };
            parcour.Add(new int[] { depart.X, depart.Y, cout });

            while (parcour.Count != 0)
            {
                cout = parcour[0][2] + 1;
                foreach (int[] elt in NoeudAdjacentDeplacableAvecPoint(parcour[0][0], parcour[0][1], carte, cout, maxPoint))
                {
                    //ont part du principe que l'élément adjacent n'est pas déjà dans le résultat et le parcour mais ont va le vérifier
                    bool estPas = true;
                    for (int i = 0; i < resultat.Count; i++)
                    {
                        if (elt[0] == resultat[i][0] && elt[1] == resultat[i][1])
                        {
                            estPas = false;
                        }
                    }
                    for (int i = 0; i < parcour.Count; i++)
                    {
                        if (elt[0] == parcour[i][0] && elt[1] == parcour[i][1])
                        {
                            if (elt[2] >= parcour[i][2])
                            {
                                parcour[i] = elt;
                            }
                            estPas = false;
                        }
                    }
                    if (estPas == true)
                    {
                        parcour.Add(elt);
                    }
                }
                resultat.Add(parcour[0]);
                parcour = Pop(parcour);
            }
            resultat.Reverse();
            return resultat;
        }
    }
}