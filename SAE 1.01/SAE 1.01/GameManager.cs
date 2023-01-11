using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_1._01
{
    internal class GameManager
{
        List<Entite> entitesCombat = new List<Entite>();
        Entite entiteTour;
        Texture2D _bordureCasePossible;

        internal List<Entite> EntitesCombat
        {
            get
            {
                return this.entitesCombat;
            }

            set
            {
                this.entitesCombat = value;
            }
        }

        public Texture2D BordureCasePossible
        {
            get
            {
                return this._bordureCasePossible;
            }

            set
            {
                this._bordureCasePossible = value;
            }
        }

        public GameManager(Texture2D _bordureCasePossible)
        {
            this.BordureCasePossible = _bordureCasePossible;
        }
        public void CommencerJeu()
        {
            SetTour(EntitesCombat[0]);
        }

        public void ProchaineEntite()
        {
            int index = ChercherEntiteIndex(entiteTour);
            if (index + 1 == EntitesCombat.Count)
            {
                index = 0;
            }
            else
                index += 1;
            SetTour(EntitesCombat[index]);
        }

        void SetTour(Entite e)
        {
            entiteTour = e;
            e.ResetPA();
            e.JouerTour();
        }
        int ChercherEntiteIndex(Entite entite)
        {
            for (int i = 0; i < EntitesCombat.Count; i++)
            {
                if(EntitesCombat[i] == entite)
                {
                    return i;
                }  
            }
            return -1;
        }

        public int GetIndexTurn()
        {
            return ChercherEntiteIndex(entiteTour);
        }

        public void AjouterCombattant(Entite e)
        {
            EntitesCombat.Add(e);
        }

        public Entite GetEntiteTour()
        {
            return entiteTour;
        }

}
}
