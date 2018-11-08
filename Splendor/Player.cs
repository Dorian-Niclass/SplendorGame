using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splendor
{

    /// <summary>
    /// class Player : attributes and methods to deal with a player
    /// </summary>
    public class Player
    {

        /// <summary>
        /// name of the player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// all the precious stones he has
        /// </summary>
        public int[] Ressources { get; set; }

        /// <summary>
        /// all the coins he has
        /// </summary>
        public Dictionary<Ressources, int> Coins = new Dictionary<Ressources, int>();

        /// <summary>
        /// id of the player
        /// </summary>
        public int Id { get; set; }

        public List<Card> Cards { get; set; }

        public int GetPrestige()
        {
            int prestigePt = 0;

            Cards.ForEach(x => prestigePt += x.PrestigePt);

            return prestigePt;
        }

    }
}
