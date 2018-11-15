/**
 * \file      frmAddVideoGames.cs
 * \author    F. Andolfatto
 * \version   1.0
 * \date      August 22. 2018
 * \brief     Form to play.
 *
 * \details   This form enables to choose coins or cards to get ressources (precious stones) and prestige points 
 * to add and to play with other players
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    /// <summary>
    /// manages the form that enables to play with the Splendor
    /// </summary>
    public partial class frmSplendor : Form
    {
        //used to store the number of coins selected for the current round of game
        private int[] coins;

        private int[] choosedCoins;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel = false;
        //boolean to enable us to know if the user can click on button play
        public bool enableClicPlay = false;
        //connection to the database
        private ConnectionDB conn;

        private Stack<Card> level1Cards = new Stack<Card>();
        private Stack<Card> level2Cards = new Stack<Card>();
        private Stack<Card> level3Cards = new Stack<Card>();
        private Stack<Card> level4Cards = new Stack<Card>();

        private Card[,] cardsOnTable = new Card[4, 4];

        public List<Player> players = new List<Player>();

        private AddPlayerForm addPlayerForm;

        /// <summary>
        /// constructor
        /// </summary>
        public frmSplendor()
        {
            InitializeComponent();
        }

        /// <summary>
        /// loads the form and initialize data in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < cardsOnTable.Rank; i++)
            {
                for (int j = 0; j < cardsOnTable.GetLength(i); j++)
                {
                    cardsOnTable[i, j] = null;
                }
            }

            coins = new int[] { 7, 7, 7, 7, 7 };

            lblGoldCoin.Text = "5";
            
            lblRubisCoin.Text = coins[(int)Ressources.Rubis].ToString();
            lblSaphirCoin.Text = coins[(int)Ressources.Saphir].ToString();
            lblOnyxCoin.Text = coins[(int)Ressources.Onyx].ToString();
            lblEmeraudeCoin.Text = coins[(int)Ressources.Emeraude].ToString();
            lblDiamandCoin.Text = coins[(int)Ressources.Diamand].ToString();

            conn = new ConnectionDB();

            //load cards from the database
            level1Cards = conn.GetListCardAccordingToLevel(1);
            level2Cards = conn.GetListCardAccordingToLevel(2);
            level3Cards = conn.GetListCardAccordingToLevel(3);
            level4Cards = conn.GetListCardAccordingToLevel(4);

            level1Cards = Shuffle<Card>(level1Cards);
            level2Cards = Shuffle<Card>(level2Cards);
            level3Cards = Shuffle<Card>(level3Cards);
            level4Cards = Shuffle<Card>(level4Cards);
            
            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceEmeraude.Visible = false;
            lblChoiceDiamand.Visible = false;
            cmdValidateChoice.Enabled = false;
            cmdNextPlayer.Enabled = false;

            foreach (FlowLayoutPanel control in this.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (CardText cardText in control.Controls.OfType<CardText>())
                {
                    cardText.LoadPosition();
                }
            }

        }

        /// <summary>
        /// Update the coins number
        /// </summary>
        private void UpdateCoins()
        {
            lblRubisCoin.Text = coins[(int)Ressources.Rubis].ToString();
            lblSaphirCoin.Text = coins[(int)Ressources.Saphir].ToString();
            lblOnyxCoin.Text = coins[(int)Ressources.Onyx].ToString();
            lblEmeraudeCoin.Text = coins[(int)Ressources.Emeraude].ToString();
            lblDiamandCoin.Text = coins[(int)Ressources.Diamand].ToString();

            lblChoiceDiamand.Text = choosedCoins[(int)Ressources.Diamand].ToString();
            lblChoiceOnyx.Text = choosedCoins[(int)Ressources.Onyx].ToString();
            lblChoiceRubis.Text = choosedCoins[(int)Ressources.Rubis].ToString();
            lblChoiceSaphir.Text = choosedCoins[(int)Ressources.Saphir].ToString();
            lblChoiceEmeraude.Text = choosedCoins[(int)Ressources.Emeraude].ToString();

            lblPlayerRubisCoin.Text = players[currentPlayerId].Coins[Ressources.Rubis].ToString();
            lblPlayerSaphirCoin.Text = players[currentPlayerId].Coins[Ressources.Saphir].ToString();
            lblPlayerEmeraudeCoin.Text = players[currentPlayerId].Coins[Ressources.Emeraude].ToString();
            lblPlayerDiamandCoin.Text = players[currentPlayerId].Coins[Ressources.Diamand].ToString();
            lblPlayerOnyxCoin.Text = players[currentPlayerId].Coins[Ressources.Onyx].ToString();
        }

        /// <summary>
        /// Shuffle a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <returns></returns>
        private Stack<T> Shuffle<T>(Stack<T> stack)
        {
            Random rnd = new Random();
            return new Stack<T>(stack.OrderBy(x => rnd.Next()));
        }

        /// <summary>
        /// click on the play button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPlay_Click(object sender, EventArgs e)
        {
            if (enableClicPlay)
            {
                cmdInsertPlayer.Enabled = false;
                this.Width = 680;
                this.Height = 800;

                LoadPlayer(0);
                PutCardsOnTable();
            }
            else
                MessageBox.Show("Vous devez inserer entre deux et quatre joueurs pour pouvoir jouer.");
        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) {
            Player player = players[id];

            enableClicLabel = true;

            //no coins selected
            choosedCoins = new int[] { 0, 0, 0, 0, 0 };

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = choosedCoins[(int)Ressources.Diamand].ToString();
            lblChoiceOnyx.Text = choosedCoins[(int)Ressources.Onyx].ToString();
            lblChoiceRubis.Text = choosedCoins[(int)Ressources.Rubis].ToString();
            lblChoiceSaphir.Text = choosedCoins[(int)Ressources.Saphir].ToString();
            lblChoiceEmeraude.Text = choosedCoins[(int)Ressources.Emeraude].ToString();

            lblChoiceCard.Text = "";

            lblPlayerDiamandCoin.Text = player.Coins[Ressources.Diamand].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[Ressources.Onyx].ToString();
            lblPlayerRubisCoin.Text = player.Coins[Ressources.Rubis].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[Ressources.Saphir].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[Ressources.Emeraude].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + player.Name;

            lblNbPtPrestige.Text = player.GetPrestige().ToString();

            cmdPlay.Enabled = false;

            lblChoiceRubis.Visible = true;
            lblChoiceSaphir.Visible = true;
            lblChoiceOnyx.Visible = true;
            lblChoiceEmeraude.Visible = true;
            lblChoiceDiamand.Visible = true;
        }

        /// <summary>
        /// Pick a new card from the stack for every missing cards on the table
        /// </summary>
        private void PutCardsOnTable()
        {
            for (int i = 0; i < cardsOnTable.GetLength(0); i++)
            {
                for (int j = 0; j < cardsOnTable.GetLength(1); j++)
                {
                    if(cardsOnTable[i,j] == null)
                    { 
                        switch (i)
                        {
                            case 0:
                                if (level1Cards.Count == 0) continue;
                                cardsOnTable[i, j] = level1Cards.Pop(); break;
                            case 1:
                                if (level2Cards.Count == 0) continue;
                                cardsOnTable[i, j] = level2Cards.Pop(); break;
                            case 2:
                                if (level3Cards.Count == 0) continue;
                                cardsOnTable[i, j] = level3Cards.Pop(); break;
                            case 3:
                                if (level4Cards.Count == 0) continue;
                                cardsOnTable[i, j] = level4Cards.Pop(); break;
                        }
                    }
                }
            }

            foreach (FlowLayoutPanel control in this.Controls.OfType<FlowLayoutPanel>())
            {
                if (control == flwPalyerCards) continue;

                foreach (CardText cardText in control.Controls.OfType<CardText>())
                {
                    cardText.SetCard(cardsOnTable[cardText.row, cardText.col]);
                }
            }

            DrawCards();
        }

        /// <summary>
        /// Pick a new card for this place on the board
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private Card PickNewCard(int row, int col)
        {
            Card card = null;
            switch (row)
            {
                case 0:
                    if (level1Cards.Count == 0) break;
                    card = level1Cards.Pop(); break;
                case 1:
                    if (level2Cards.Count == 0) break;
                    card = level2Cards.Pop(); break;
                case 2:
                    if (level3Cards.Count == 0) break;
                    card = level3Cards.Pop(); break;
                case 3:
                    if (level4Cards.Count == 0) break;
                    card = level4Cards.Pop(); break;
            }
            cardsOnTable[row, col] = card;
            return card;
        }

        /// <summary>
        /// click on one of the pieces (rubis, saphir, onyx, emeraude, diamand) to tell the player has selected one of them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCoin_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string ressourceName = label.Name.Substring(3).Replace("Coin", "");
            Ressources ressource = (Ressources)Enum.Parse(typeof(Ressources), ressourceName);

            if (enableClicLabel)
            {
                int nbDiffCoin = choosedCoins.Where(x => x != 0).Count();

                switch (nbDiffCoin)
                {
                    case 0:
                    case 1:
                        if (choosedCoins.Sum() < 2)
                        {
                            if (choosedCoins[(int)ressource] == 0)
                            {  
                                if (coins[(int)ressource] > 0)
                                {
                                    choosedCoins[(int)ressource] += 1;
                                    coins[(int)ressource] -= 1;
                                }
                                else
                                {
                                    MessageBox.Show("Il n'y a plus de ce jeton");
                                }

                            }else if (choosedCoins[(int)ressource] == 1)
                            {
                                if (coins[(int)ressource] > 2)
                                {
                                    choosedCoins[(int)ressource] += 1;
                                    coins[(int)ressource] -= 1;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux même jetons si il en reste 3 ou moins");
                                }
                            }
                        }
                        else if(choosedCoins[(int)ressource] != 0)
                        {
                            if (choosedCoins[(int)ressource] < 2)
                            {
                                choosedCoins[(int)ressource] += 1;
                                coins[(int)ressource] -= 1;
                            }
                            else MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                        }
                        else MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                        break;
                    case 2:

                        if (choosedCoins[(int)ressource] == 0)
                        {
                            if (coins[(int)ressource] > 0)
                            {
                                choosedCoins[(int)ressource] += 1;
                                coins[(int)ressource] -= 1;
                            }
                            else
                            {
                                MessageBox.Show("Il n'y a plus de ce jeton");
                            }

                        }
                        else MessageBox.Show("Vous ne pouvez pas prendre ce jeton.");
                        break;
                    default:
                        MessageBox.Show("Vous avez déjà trois jetons différents.");
                        break;         
                }

                if ((choosedCoins.Where(x => x != 0).Count() == 1 && choosedCoins.Sum() == 2) || (choosedCoins.Where(x => x != 0).Count() == 3 && choosedCoins.Sum() == 3))
                    cmdValidateChoice.Enabled = true;

                UpdateCoins();
            }
        }

        /// <summary>
        /// click on one of the coin (rubis, saphir, onyx, emeraude, diamand) to tell the player has unselected one of them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblChoice_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            string ressourceName = label.Name.Substring(9);
            Ressources ressource = (Ressources)Enum.Parse(typeof(Ressources), ressourceName);
            
            if (choosedCoins[(int)ressource] != 0)
            {
                cmdValidateChoice.Enabled = false;
                choosedCoins[(int)ressource] -= 1;
                coins[(int)ressource] += 1;
            }

            UpdateCoins();       
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdValidateChoice.Enabled = false;
            enableClicLabel = false;

            lblChoiceOnyx.Visible = false;
            lblChoiceDiamand.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;

            players[currentPlayerId].Coins[Ressources.Rubis] += choosedCoins[(int)Ressources.Rubis];
            players[currentPlayerId].Coins[Ressources.Saphir] += choosedCoins[(int)Ressources.Saphir];
            players[currentPlayerId].Coins[Ressources.Emeraude] += choosedCoins[(int)Ressources.Emeraude];
            players[currentPlayerId].Coins[Ressources.Diamand] += choosedCoins[(int)Ressources.Diamand];
            players[currentPlayerId].Coins[Ressources.Onyx] += choosedCoins[(int)Ressources.Onyx];

            UpdateCoins();
            lblNbPtPrestige.Text = players[currentPlayerId].GetPrestige().ToString();

            if (players[currentPlayerId].GetPrestige() >= 15)
            {
                MessageBox.Show(players[currentPlayerId].Name + " a gagné !", "Victoire");            
            }
            else
            {
                cmdNextPlayer.Enabled = true;
                cmdNextPlayer.Visible = true;
            }
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {

            if (addPlayerForm == null || !addPlayerForm.Visible)
            {
                addPlayerForm = new AddPlayerForm(this);
                addPlayerForm.Show();
            }
        }

        /// <summary>
        /// click on the next player to tell him it is his turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNextPlayer_Click(object sender, EventArgs e)
        {
            //TO DO in release 1.0 : 3 is hard coded (number of players for the game), it shouldn't. 
            //TO DO Get the id of the player : in release 0.1 there are only 3 players
            //Reload the data of the player
            //We are not allowed to click on the next button

            cmdNextPlayer.Enabled = false;
            cmdValidateChoice.Enabled = false;

            if (currentPlayerId < players.Count-1)
                currentPlayerId++;
            else
                currentPlayerId = 0;

            LoadPlayer(currentPlayerId);
            DrawCards();
        }

        /// <summary>
        /// Draw the cards on the board
        /// </summary>
        private void DrawCards()
        {
            RefreshPlayerCards();

            //Check all the text box for the cards if there is a card associated, if so, print the card properties inside
            foreach (FlowLayoutPanel control in this.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (CardText cardText in control.Controls.OfType<CardText>())
                {
                    Application.DoEvents();
                    cardText.Refresh();
                }
            }        
        }

        /// <summary>
        /// Where the player click on a card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickOnCard(object sender, EventArgs e)
        {
            if (enableClicLabel) {
                CardText txtCard = (CardText)sender;
                Card card = txtCard.Card;

                bool canBuy = true;

                foreach (Ressources ress in card.Cost.Keys)
                {
                    if ((txtCard.Name.Contains("Noble") ? 0 : players[currentPlayerId].Coins[ress]) + players[currentPlayerId].GetRessources()[ress] < card.Cost[ress])
                    {
                        canBuy = false;
                        break;
                    }
                }

                if (canBuy)
                {
                    DialogResult result = MessageBox.Show("Voulez-vous acheter cette carte ?", "Acheter", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        players[currentPlayerId].Cards.Add(card);



                        int cardRow = 0;
                        int cardCol = Int32.Parse(txtCard.Name.Substring(txtCard.Name.Length - 1, 1));

                        if (txtCard.Name.Contains("Noble"))
                        {
                            cardRow = 4;
                        }
                        else
                        {
                            cardRow = Int32.Parse(txtCard.Name.Substring(txtCard.Name.Length - 2, 1));
                            foreach (Ressources ress in card.Cost.Keys)
                            {
                                int coins = card.Cost[ress] - players[currentPlayerId].GetRessources()[ress];
                                players[currentPlayerId].Coins[ress] -= coins >= 0 ? coins : 0;
                                this.coins[(int)ress] += coins >= 0 ? coins : 0;
                            }
                        }

                        cardsOnTable[cardRow - 1, cardCol - 1] = null;
                        txtCard.SetCard(null);

                        if (cardRow != 4)
                        {
                            txtCard.SetCard(PickNewCard(cardRow - 1, cardCol - 1));
                        }

                        enableClicLabel = false;
                        cmdValidateChoice.Enabled = true;

                        UpdateCoins();
                        DrawCards();
                    }
                }
                else
                {
                    MessageBox.Show("Vous n'avez pas assez de ressources", "Acheter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Refresh the display of player's cards
        /// </summary>
        private void RefreshPlayerCards()
        {
            if (players.Count != 0)
            {
                List<Card> rubisCard = players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Rubis).ToList();
                List<Card> saphirCard = players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Saphir).ToList();
                List<Card> onyxCard = players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Onyx).ToList();
                List<Card> emeraudeCard = players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Emeraude).ToList();
                List<Card> diamandCard = players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Diamand).ToList();
                List<Card> nobleCard = players[currentPlayerId].Cards.Where(x => x.Level == 4).ToList();

                txtPlayerRubisCard.SetCard(rubisCard.Count > 0 ? rubisCard[(int)numCardRubis.Value] : null);
                txtPlayerSaphirCard.SetCard(saphirCard.Count > 0 ? saphirCard[(int)numCardSaphir.Value] : null);
                txtPlayerOnyxCard.SetCard(onyxCard.Count > 0 ? onyxCard[(int)numCardOnyx.Value] : null);
                txtPlayerEmeraudeCard.SetCard(emeraudeCard.Count > 0 ? emeraudeCard[(int)numCardEmeraude.Value] : null);
                txtPlayerDiamandCard.SetCard(diamandCard.Count > 0 ? diamandCard[(int)numCardDiamand.Value] : null);
                txtPlayerNobleCard.SetCard(nobleCard.Count > 0 ? nobleCard[(int)numCardNoble.Value] : null);
            }
        }

        /// <summary>
        /// When the value of the displayed player card change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbCards_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            string ress = num.Name.Substring(7);

            if (ress == "Noble")
            {
                if ((int)num.Value > players[currentPlayerId].Cards.Where(x => x.Level == 4).ToList().Count - 1)
                {
                    num.Value = 0;
                }
            }
            else
            {
                if ((int)num.Value > players[currentPlayerId].Cards.Where(x => x.Ress == (Ressources)Enum.Parse(typeof(Ressources), ress)).ToList().Count - 1)
                {
                    num.Value = 0;
                }
            }

            DrawCards();
        }
    }
}
