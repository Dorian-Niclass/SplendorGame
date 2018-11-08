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
        private int nbRubis;
        private int nbSaphir;
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;

        private int nbMyRubis;
        private int nbMySaphir;
        private int nbMyOnyx;
        private int nbMyEmeraude;
        private int nbMyDiamand;

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

            nbDiamand = 7;
            nbOnyx = 7;
            nbRubis = 7;
            nbSaphir = 7;
            nbEmeraude = 7;

            lblGoldCoin.Text = "5";
            
            lblRubisCoin.Text = nbRubis.ToString();
            lblSaphirCoin.Text = nbSaphir.ToString();
            lblOnyxCoin.Text = nbOnyx.ToString();
            lblEmeraudeCoin.Text = nbEmeraude.ToString();
            lblDiamandCoin.Text = nbDiamand.ToString();

            conn = new ConnectionDB();

            //load cards from the database
            //they are not hard coded any more
            //TO DO

            level1Cards = conn.GetListCardAccordingToLevel(1);
            level2Cards = conn.GetListCardAccordingToLevel(2);
            level3Cards = conn.GetListCardAccordingToLevel(3);
            level4Cards = conn.GetListCardAccordingToLevel(4);

            level1Cards = Shuffle<Card>(level1Cards);
            level2Cards = Shuffle<Card>(level2Cards);
            level3Cards = Shuffle<Card>(level3Cards);
            level4Cards = Shuffle<Card>(level3Cards);

            /*cardsOnTable[1,1] = level1Cards.Pop();
            cardsOnTable[2,1] = level2Cards.Pop();
            cardsOnTable[3,1] = level3Cards.Pop();*/

            //Go through the results
            //Don't forget to check when you are at the end of the stack
            
            //fin TO DO

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

            //we wire the click on all cards to the same event
            //TO DO for all cards
            txtLevel13.Click += ClickOnCard;

            foreach (FlowLayoutPanel control in this.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (CardText cardText in control.Controls.OfType<CardText>())
                {
                    cardText.LoadPosition();
                }
            }

            PutCardsOnTable();
        }

        private Stack<T> Shuffle<T>(Stack<T> stack)
        {
            Random rnd = new Random();
            return new Stack<T>(stack.OrderBy(x => rnd.Next()));
        }

        private void ClickOnCard(object sender, EventArgs e)
        {
            //We get the value on the card and we split it to get all the values we need (number of prestige points and ressource)
            //Enable the button "Validate"
            //TO DO
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

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins selected
            nbMyDiamand = 0;
            nbMyOnyx = 0;
            nbMyRubis = 0;
            nbMySaphir = 0;
            nbMyEmeraude = 0;

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = nbMyDiamand.ToString();
            lblChoiceOnyx.Text = nbMyOnyx.ToString();
            lblChoiceRubis.Text = nbMyRubis.ToString();
            lblChoiceSaphir.Text = nbMySaphir.ToString();
            lblChoiceEmeraude.Text = nbMyEmeraude.ToString();

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

            if (enableClicLabel)
            {
                int nbMyCoin = nbMyRubis + nbMySaphir + nbMyOnyx + nbMyEmeraude + nbMyDiamand;

                switch (label.Name)
                {
                    case "lblRubisCoin":

                        if (nbMyCoin <= 2)
                        {
                            if (nbMyRubis == 0 && nbMySaphir <= 1 && nbMyOnyx <= 1 && nbMyEmeraude <= 1 && nbMyDiamand <= 1)
                            {
                                lblChoiceRubis.Visible = true;
                                nbRubis--;
                                nbMyRubis++;

                                lblRubisCoin.Text = nbRubis.ToString();
                                lblChoiceRubis.Text = nbMyRubis + "\r\n";

                                if (nbMyCoin == 2)
                                {
                                    cmdValidateChoice.Enabled = true;
                                }
                            }
                            else if (nbMyRubis == 1 && nbMySaphir == 0 && nbMyOnyx == 0 && nbMyEmeraude == 0 && nbMyDiamand == 0)
                            {
                                if (nbRubis >= 3)
                                {
                                    nbRubis--;
                                    nbMyRubis++;

                                    lblRubisCoin.Text = nbRubis.ToString();
                                    lblChoiceRubis.Text = nbMyRubis + "\r\n";

                                    cmdValidateChoice.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux mêmes jetons s'il n'en reste pas au minimum quatre.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous avez déjà trois jetons différents.");
                        }

                        break;
                    case "lblSaphirCoin":

                        if (nbMyCoin <= 2)
                        {
                            if (nbMySaphir == 0 && nbMyRubis <= 1 && nbMyOnyx <= 1 && nbMyEmeraude <= 1 && nbMyDiamand <= 1)
                            {
                                lblChoiceSaphir.Visible = true;
                                nbSaphir--;
                                nbMySaphir++;

                                lblSaphirCoin.Text = nbSaphir.ToString();
                                lblChoiceSaphir.Text = nbMySaphir + "\r\n";

                                if (nbMyCoin == 2)
                                {
                                    cmdValidateChoice.Enabled = true;
                                }
                            }
                            else if (nbMySaphir == 1 && nbMyRubis == 0 && nbMyOnyx == 0 && nbMyEmeraude == 0 && nbMyDiamand == 0)
                            {                                
                                if (nbSaphir >= 3)
                                {
                                    nbSaphir--;
                                    nbMySaphir++;

                                    lblSaphirCoin.Text = nbSaphir.ToString();
                                    lblChoiceSaphir.Text = nbMySaphir + "\r\n";

                                    cmdValidateChoice.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux mêmes jetons s'il n'en reste pas au minimum quatre.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous avez déjà trois jetons différents.");
                        }

                        break;
                    case "lblOnyxCoin":

                        if (nbMyCoin <= 2)
                        {
                            if (nbMyOnyx == 0 && nbMyRubis <= 1 && nbMySaphir <= 1 && nbMyEmeraude <= 1 && nbMyDiamand <= 1)
                            {
                                lblChoiceOnyx.Visible = true;
                                nbOnyx--;
                                nbMyOnyx++;

                                lblOnyxCoin.Text = nbOnyx.ToString();
                                lblChoiceOnyx.Text = nbMyOnyx + "\r\n";

                                if (nbMyCoin == 2)
                                {
                                    cmdValidateChoice.Enabled = true;
                                }
                            }
                            else if (nbMyOnyx == 1 && nbMyRubis == 0 && nbMySaphir == 0 && nbMyEmeraude == 0 && nbMyDiamand == 0)
                            {
                                if (nbOnyx >= 3)
                                {
                                    nbOnyx--;
                                    nbMyOnyx++;

                                    lblOnyxCoin.Text = nbOnyx.ToString();
                                    lblChoiceOnyx.Text = nbMyOnyx + "\r\n";

                                    cmdValidateChoice.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux mêmes jetons s'il n'en reste pas au minimum quatre.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous avez déjà trois jetons différents.");
                        }

                        break;
                    case "lblEmeraudeCoin":

                        if (nbMyCoin <= 2)
                        {
                            if (nbMyEmeraude == 0 && nbMyRubis <= 1 && nbMySaphir <= 1 && nbMyOnyx <= 1 && nbMyDiamand <= 1)
                            {
                                lblChoiceEmeraude.Visible = true;
                                nbEmeraude--;
                                nbMyEmeraude++;

                                lblEmeraudeCoin.Text = nbEmeraude.ToString();
                                lblChoiceEmeraude.Text = nbMyEmeraude + "\r\n";

                                if (nbMyCoin == 2)
                                {
                                    cmdValidateChoice.Enabled = true;
                                }
                            }
                            else if (nbMyEmeraude == 1 && nbMyRubis == 0 && nbMySaphir == 0 && nbMyOnyx == 0 && nbMyDiamand == 0)
                            {
                                if (nbEmeraude >= 3)
                                {
                                    nbEmeraude--;
                                    nbMyEmeraude++;

                                    lblEmeraudeCoin.Text = nbEmeraude.ToString();
                                    lblChoiceEmeraude.Text = nbMyEmeraude + "\r\n";

                                    cmdValidateChoice.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux mêmes jetons s'il n'en reste pas au minimum quatre.");
                                }                                
                            }
                            else
                            {
                                MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous avez déjà trois jetons différents.");
                        }

                        break;
                    case "lblDiamandCoin":

                        if (nbMyCoin <= 2)
                        {
                            if (nbMyDiamand == 0 && nbMyRubis <= 1 && nbMySaphir <= 1 && nbMyOnyx <= 1 && nbMyEmeraude <= 1)
                            {
                                lblChoiceDiamand.Visible = true;
                                nbDiamand--;
                                nbMyDiamand++;

                                lblDiamandCoin.Text = nbDiamand.ToString();
                                lblChoiceDiamand.Text = nbMyDiamand + "\r\n";

                                if (nbMyCoin == 2)
                                {
                                    cmdValidateChoice.Enabled = true;
                                }
                            }
                            else if (nbMyDiamand == 1 && nbMyRubis == 0 && nbMySaphir == 0 && nbMyOnyx == 0 && nbMyEmeraude == 0)
                            {
                                if (nbDiamand >= 3)
                                {
                                    nbDiamand--;
                                    nbMyDiamand++;

                                    lblDiamandCoin.Text = nbDiamand.ToString();
                                    lblChoiceDiamand.Text = nbMyDiamand + "\r\n";

                                    cmdValidateChoice.Enabled = true;
                                }
                                else
                                {
                                    MessageBox.Show("Vous ne pouvez pas prendre deux mêmes jetons s'il n'en reste pas au minimum quatre.");
                                }                                
                            }
                            else
                            {
                                MessageBox.Show("Vous avez déjà deux mêmes jetons.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vous avez déjà trois jetons différents.");
                        }

                        break;
                    default: break;
                }
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

            cmdValidateChoice.Enabled = false;

            switch (label.Name)
            {
                case "lblChoiceRubis":

                    nbRubis++;
                    nbMyRubis--;
                    lblRubisCoin.Text = nbRubis.ToString();
                    lblChoiceRubis.Text = nbMyRubis + "\r\n";
                    
                    if (nbMyRubis == 0)
                    {
                        lblChoiceRubis.Visible = false;
                    }

                    break;
                case "lblChoiceSaphir":

                    nbSaphir++;
                    nbMySaphir--;
                    lblSaphirCoin.Text = nbSaphir.ToString();
                    lblChoiceSaphir.Text = nbMySaphir + "\r\n";
                    
                    if (nbMySaphir == 0)
                    {
                        lblChoiceSaphir.Visible = false;
                    }

                    break;
                case "lblChoiceOnyx":

                    nbOnyx++;
                    nbMyOnyx--;
                    lblOnyxCoin.Text = nbOnyx.ToString();
                    lblChoiceOnyx.Text = nbMyOnyx + "\r\n";

                    if (nbMyOnyx == 0)
                    {
                        lblChoiceOnyx.Visible = false;
                    }
                    
                    break;
                case "lblChoiceEmeraude":

                    nbEmeraude++;
                    nbMyEmeraude--;
                    lblEmeraudeCoin.Text = nbEmeraude.ToString();
                    lblChoiceEmeraude.Text = nbMyEmeraude + "\r\n";

                    if (nbMyEmeraude == 0)
                    {
                        lblChoiceEmeraude.Visible = false;
                    }

                    break;
                case "lblChoiceDiamand":

                    nbDiamand++;
                    nbMyDiamand--;
                    lblDiamandCoin.Text = nbDiamand.ToString();
                    lblChoiceDiamand.Text = nbMyDiamand + "\r\n";

                    if (nbMyDiamand == 0)
                    {
                        lblChoiceDiamand.Visible = false;
                    }

                    break;
                default: break;
            }
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

            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time

            players[currentPlayerId].Coins[Ressources.Rubis] += nbMyRubis;
            players[currentPlayerId].Coins[Ressources.Saphir] += nbMySaphir;
            players[currentPlayerId].Coins[Ressources.Emeraude] += nbMyEmeraude;
            players[currentPlayerId].Coins[Ressources.Diamand] += nbMyDiamand;
            players[currentPlayerId].Coins[Ressources.Onyx] += nbMyOnyx;

            DrawCoins();
            lblNbPtPrestige.Text = players[currentPlayerId].GetPrestige().ToString();
            cmdNextPlayer.Enabled = true;
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
            {
                currentPlayerId++;
            }
            else
            {
                currentPlayerId = 0;
            }

            LoadPlayer(currentPlayerId);

        }

        /// <summary>
        /// Everytime the form is paint
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSplendor_Paint(object sender, PaintEventArgs e)
        {
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
                    cardText.Refresh();
                }
            }
        }

        private void DrawCoins()
        {
            try
            {
                lblPlayerRubisCoin.Text = players[currentPlayerId].Coins[Ressources.Rubis].ToString();
                lblPlayerSaphirCoin.Text = players[currentPlayerId].Coins[Ressources.Saphir].ToString();
                lblPlayerEmeraudeCoin.Text = players[currentPlayerId].Coins[Ressources.Emeraude].ToString();
                lblPlayerDiamandCoin.Text = players[currentPlayerId].Coins[Ressources.Diamand].ToString();
                lblPlayerOnyxCoin.Text = players[currentPlayerId].Coins[Ressources.Onyx].ToString();
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Where the player click on a card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCards_Click(object sender, EventArgs e)
        {
            //TODO: do the logic (if the game started, is the player has enough coins, etc...)
            if (enableClicLabel) {
                CardText txtCard = (CardText)sender;
                Card card = txtCard.Card;

                bool canBuy = true;

                foreach (Ressources ress in card.Cost.Keys)
                {
                    if (players[currentPlayerId].Coins[ress] < card.Cost[ress])
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

                        foreach (Ressources ress in card.Cost.Keys)
                        {
                            players[currentPlayerId].Coins[ress] -= card.Cost[ress];

                            switch (ress)
                            {
                                case Ressources.Rubis:
                                    nbRubis += card.Cost[ress];
                                    lblRubisCoin.Text = nbRubis.ToString();
                                    break;
                                case Ressources.Saphir:
                                    nbSaphir += card.Cost[ress];
                                    lblSaphirCoin.Text = nbSaphir.ToString();
                                    break;
                                case Ressources.Onyx:
                                    nbOnyx += card.Cost[ress];
                                    lblOnyxCoin.Text = nbOnyx.ToString();
                                    break;
                                case Ressources.Emeraude:
                                    nbEmeraude += card.Cost[ress];
                                    lblEmeraudeCoin.Text = nbEmeraude.ToString();
                                    break;
                                case Ressources.Diamand:
                                    nbDiamand += card.Cost[ress];
                                    lblDiamandCoin.Text = nbDiamand.ToString();
                                    break;
                            }
                        }

                        int cardRow = 0;
                        int cardCol = Int32.Parse(txtCard.Name.Substring(txtCard.Name.Length - 1, 1));

                        if (txtCard.Name.Contains("Noble"))
                        {
                            cardRow = 4;
                        }
                        else
                        {
                            cardRow = Int32.Parse(txtCard.Name.Substring(txtCard.Name.Length - 2, 1));
                        }

                        cardsOnTable[cardRow - 1, cardCol - 1] = null;
                        txtCard.SetCard(null);

                        if (cardRow != 4)
                        {
                            txtCard.SetCard(PickNewCard(cardRow - 1, cardCol - 1));
                        }

                        enableClicLabel = false;
                        cmdValidateChoice.Enabled = true;

                        DrawCards();
                    }
                }
                else
                {
                    MessageBox.Show("Vous n'avez pas assez de ressources pour acheter cette carte.", "Acheter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void RefreshPlayerCards()
        {
            if (players != null)
            {
                try { txtPlayerRubisCard.SetCard(players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Rubis).ToList()[(int)numCardRubis.Value]); } catch { txtPlayerRubisCard.SetCard(null); }
                try { txtPlayerSaphirCard.SetCard(players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Saphir).ToList()[(int)numCardSaphir.Value]); } catch { txtPlayerSaphirCard.SetCard(null); }
                try { txtPlayerOnyxCard.SetCard(players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Onyx).ToList()[(int)numCardOnyx.Value]); } catch { txtPlayerOnyxCard.SetCard(null); }
                try { txtPlayerEmeraudeCard.SetCard(players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Emeraude).ToList()[(int)numCardEmeraude.Value]); } catch { txtPlayerEmeraudeCard.SetCard(null); }
                try { txtPlayerDiamandCard.SetCard(players[currentPlayerId].Cards.Where(x => x.Ress == Ressources.Diamand).ToList()[(int)numCardDiamand.Value]); } catch { txtPlayerDiamandCard.SetCard(null); }
            }
        }

        private void nbCards_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown num = (NumericUpDown)sender;
            string ress = num.Name.Substring(7);

            if((int)num.Value > players[currentPlayerId].Cards.Where(x => x.Ress == (Ressources)Enum.Parse(typeof(Ressources), ress)).ToList().Count-1)
            {
                num.Value = 0;
            }

            DrawCards();
        }
    }
}
