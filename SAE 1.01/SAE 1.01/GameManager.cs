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
        public GameManager()
        {

        }
        public void CommencerJeu()
        {
            SetTour(entitesCombat[0]);
        }

        public void ProchaineEntite()
        {
            int index = ChercherEntiteIndex(entiteTour);
            if (index + 1 == entitesCombat.Count)
            {
                index = 0;
            }
            else
                index += 1;
            SetTour(entitesCombat[index]);
        }

        void SetTour(Entite e)
        {
            e.jouable = true;
            e.JouerTour();
        }
        int ChercherEntiteIndex(Entite entite)
        {
            for (int i = 0; i < entitesCombat.Count; i++)
            {
                if(entitesCombat[i] == entite)
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
            entitesCombat.Add(e);
        }

        public Entite GetEntiteTour()
        {
            return entiteTour;
        }
}
}
