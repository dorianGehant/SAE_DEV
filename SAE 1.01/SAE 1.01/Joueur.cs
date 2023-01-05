using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class Joueur : Entite
    {
        private List<Sort> sorts;

        public Joueur(Texture2D texture, string nom, Case position, int pointVie, int pointAction)
            : base(texture, nom, position, pointVie, pointAction)
        {
            //this.sorts = new List<Sort>(sorts);
        }
    }
}
