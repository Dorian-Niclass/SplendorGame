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
        private int nbOnyx;
        private int nbEmeraude;
        private int nbDiamand;
        private int nbSaphir;

        //id of the player that is playing
        private int currentPlayerId;
        //boolean to enable us to know if the user can click on a coin or a card
        private bool enableClicLabel;
        //connection to the database
        private ConnectionDB conn;

        private Stack<Card> level1Cards = new Stack<Card>();
        private Stack<Card> level2Cards = new Stack<Card>();
        private Stack<Card> level3Cards = new Stack<Card>();
        private Stack<Card> level4Cards = new Stack<Card>();

        private Card[,] cardsOnTable = new Card[4, 4];

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

            lblGoldCoin.Text = "5";

            lblDiamandCoin.Text = "7";
            lblEmeraudeCoin.Text = "7" ;
            lblOnyxCoin.Text = "7";
            lblRubisCoin.Text = "7";
            lblSaphirCoin.Text = "7";

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

            PutCardsOnTable();

            //load cards from the database
            Stack<Card> listCardOne = conn.GetListCardAccordingToLevel(1);
            //Go through the results
            //Don't forget to check when you are at the end of the stack
            
            //fin TO DO

            this.Width = 680;
            this.Height = 540;

            enableClicLabel = false;

            lblChoiceDiamand.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceEmeraude.Visible = false;
            cmdValidateChoice.Visible = false;
            cmdNextPlayer.Visible = false;

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
            this.Width = 680;
            this.Height = 780;

            int id = 0;
           
            LoadPlayer(id);
        }


        /// <summary>
        /// load data about the current player
        /// </summary>
        /// <param name="id">identifier of the player</param>
        private void LoadPlayer(int id) { 

            enableClicLabel = true;

            string name = conn.GetPlayerName(currentPlayerId);

            //no coins or card selected yet, labels are empty
            lblChoiceDiamand.Text = "";
            lblChoiceOnyx.Text = "";
            lblChoiceRubis.Text = "";
            lblChoiceSaphir.Text = "";
            lblChoiceEmeraude.Text = "";

            lblChoiceCard.Text = "";

            //no coins selected
            nbDiamand = 0;
            nbOnyx = 0;
            nbRubis = 0;
            nbSaphir = 0;
            nbEmeraude = 0;

            Player player = new Player();
            player.Name = name;
            player.Id = id;
            player.Ressources = new int[] { 2, 0, 1, 1, 1 };
            player.Coins = new int[] { 0, 1, 0, 1, 1 };

            lblPlayerDiamandCoin.Text = player.Coins[0].ToString();
            lblPlayerOnyxCoin.Text = player.Coins[1].ToString();
            lblPlayerRubisCoin.Text = player.Coins[2].ToString();
            lblPlayerSaphirCoin.Text = player.Coins[3].ToString();
            lblPlayerEmeraudeCoin.Text = player.Coins[4].ToString();
            currentPlayerId = id;

            lblPlayer.Text = "Jeu de " + name;

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

            DrawCards();
        }

        /// <summary>
        /// Pick a new card for this place on the board
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void PickNewCard(int row, int col)
        {
            switch (row)
            {
                case 0:
                    if (level1Cards.Count == 0) break;
                    cardsOnTable[row, col] = level1Cards.Pop(); break;
                case 1:
                    if (level2Cards.Count == 0) break;
                    cardsOnTable[row, col] = level2Cards.Pop(); break;
                case 2:
                    if (level3Cards.Count == 0) break;
                    cardsOnTable[row, col] = level3Cards.Pop(); break;
                case 3:
                    if (level4Cards.Count == 0) break;
                    cardsOnTable[row, col] = level4Cards.Pop(); break;
            }
        }

        /// <summary>
        /// click on the red coin (rubis) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblRubisCoin_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (lbl.Name.Contains("Rubis"))
            {

            }
            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;
                lblChoiceRubis.Visible = true;
                //TO DO check if possible to choose a coin, update the number of available coin
                nbRubis++;
                lblChoiceRubis.Text = nbRubis + "\r\n";
            }
        }

        /// <summary>
        /// click on the blue coin (saphir) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSaphirCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the black coin (onyx) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblOnyxCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the green coin (emeraude) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblEmeraudeCoin_Click(object sender, EventArgs e)
        {

            
        }

        /// <summary>
        /// click on the white coin (diamand) to tell the player has selected this coin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblDiamandCoin_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// click on the validate button to approve the selection of coins or card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdValidateChoice_Click(object sender, EventArgs e)
        {
            cmdNextPlayer.Visible = true;
            //TO DO Check if card or coins are selected, impossible to do both at the same time
            
            cmdNextPlayer.Enabled = true;
        }

        /// <summary>
        /// click on the insert button to insert player in the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInsertPlayer_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A implémenter");
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
            //Check all the text box for the cards if there is a card associated, if so, print the card properties inside
            foreach (FlowLayoutPanel control in this.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (CardText cardText in control.Controls.OfType<CardText>())
                {
                    cardText.Card = cardsOnTable[cardText.row, cardText.col];
                    cardText.Refresh();
                }
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

            CardText txtCard = (CardText)sender;
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

            Card card = cardsOnTable[cardRow-1, cardCol-1];

            cardsOnTable[cardRow-1, cardCol-1] = null;

            if(cardRow!=4) PickNewCard(cardRow-1, cardCol - 1);

            DrawCards();
        }

        private void cardText1_Click(object sender, EventArgs e)
        {
            CardText ct = (CardText)sender;

            ct.Card = new Card(1, Ressources.Diamand, 666, new Dictionary<Ressources, int> { { Ressources.Saphir, 666 } });

            DrawCards();
        }
    }
}
