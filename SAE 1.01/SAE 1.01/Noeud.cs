using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Noeud
    {
        private int posX;
        private int posY;
        private int h;
        private int g;
        private int f;
        private Noeud parent;

        public Noeud(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        public int PosX
        {
            get
            {
                return this.posX;
            }

            set
            {
                this.posX = value;
            }
        }

        public int PosY
        {
            get
            {
                return this.posY;
            }

            set
            {
                this.posY = value;
            }
        }

        public int H
        {
            get
            {
                return this.h;
            }

            set
            {
                this.h = value;
            }
        }

        public int G
        {
            get
            {
                return this.g;
            }

            set
            {
                this.g = value;
            }
        }

        public int F
        {
            get
            {
                return this.f;
            }

            set
            {
                this.f = value;
            }
        }

        internal Noeud Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                this.parent = value;
            }
        }

        // -> calcule de H par une distance à vol d'oiseau de la destination
        public int ComputeHScore(int finX, int finY)
        {
            return Math.Abs(finX - PosX) + Math.Abs(finY - PosY);
        }
    }
}
