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
        public Dictionary<Ressources, int> GetRessources()
        {
            int nbSaphir = this.Cards.Count(x => x.Ress == Ressources.Saphir);
            int nbRubis = this.Cards.Count(x => x.Ress == Ressources.Rubis);
            int nbOnyx = this.Cards.Count(x => x.Ress == Ressources.Onyx);
            int nbEmeraude = this.Cards.Count(x => x.Ress == Ressources.Emeraude);
            int nbDiamand = this.Cards.Count(x => x.Ress == Ressources.Diamand);

            return new Dictionary<Ressources, int> {
                { Ressources.Saphir, nbSaphir },
                { Ressources.Rubis, nbRubis },
                { Ressources.Onyx, nbOnyx },
                { Ressources.Emeraude, nbEmeraude },
                { Ressources.Diamand, nbDiamand }
            };

        }

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
