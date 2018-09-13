using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor
{
    /// <summary>
    /// class Card : attributes and methods to deal with a card
    /// </summary>
    class Card
    {

        /// <summary>
        /// the precious stone that the card gives
        /// </summary>
        public Ressources Ress { get; set; }

        /// <summary>
        /// number of prestige point of the card
        /// </summary>
        public int PrestigePt { get; set; }

        /// <summary>
        /// level of the card : 1, 2 or 3
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// all the precious stones that are needed to buy the card
        /// </summary>
        //public int[] Cout { get; set; } = new int[4]; //tableau : l'index correspond à l'énumération, la valeur à la ressource requise
        public Dictionary<Ressources, int> Cost { get; set; } = new Dictionary<Ressources, int>();

        public Card()
        {

        }

        public Card(int level, Ressources ressource, int prestige, Dictionary<Ressources, int> cost)
        {
            this.Level = level;
            this.Ress = ressource;
            this.PrestigePt = prestige;
            this.Cost = cost;
        }

        /// <summary>
        /// displays information about the card
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string res = "";
            
            res = Enum.GetName(typeof(Ressources), Ress);
            //Be careful, those \t enables to split the string when clicking on a card
            res += "\t";

            if (PrestigePt != 0)
            {
                res += PrestigePt;
            }
            res += "\t";
            res += "\r\n\r\n";
            int boucle = 0;
            
            foreach (Ressources ress in Cost.Keys)
            {
                
                string ressource = "";

                if (Cost[ress] != 0)
                {
                    ressource = "    ";
                    ressource += ress.ToString() + " ";
                    ressource += Cost[ress] + "\r\n";
                }
                
                res += ressource;
                boucle++;

            }
            return res;
        }

    }
}
