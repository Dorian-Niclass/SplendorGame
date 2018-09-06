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
    class Player
    {
        private string name;
        private int id;
        private int[] ressources;
        private int[] coins;

        /// <summary>
        /// name of the player
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// all the precious stones he has
        /// </summary>
        public int[] Ressources
        {
            get
            {
                return ressources;
            }
            set
            {
                ressources = value;
            }
        }

        /// <summary>
        /// all the coins he has
        /// </summary>
        public int[] Coins
        {
            get
            {
                return coins;
            }
            set
            {
                coins = value;
            }
        }

        /// <summary>
        /// id of the player
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }


    }
}
