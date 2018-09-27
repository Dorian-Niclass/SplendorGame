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

            Card card11 = new Card();
            card11.Level = 1;
            card11.PrestigePt = 1;
            card11.Cout = new int[] { 1, 0, 2, 0, 2 };
            card11.Ress = Ressources.Rubis;

            Card card12 = new Card();
            card12.Level = 1;
            card12.PrestigePt = 0;
            card12.Cout = new int[] { 0, 1, 2, 1, 0 };
            card12.Ress = Ressources.Saphir;

            txtLevel11.Text = card11.ToString();
            txtLevel12.Text = card12.ToString();

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
            txtLevel11.Click += ClickOnCard;
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
        /// click on one of the pieces (rubis, saphir, onyx, emeraude, diamand) to tell the player has selected one of them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCoin_Click(object sender, EventArgs e)
        {
            Label label = (Label)sender;        

            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;

                switch (label.Name)
                {
                    case "lblRubisCoin":
                        lblChoiceRubis.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbRubis++;
                        lblChoiceRubis.Text = nbRubis + "\r\n";
                        break;
                    case "lblSaphirCoin":
                        lblChoiceSaphir.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbSaphir++;
                        lblChoiceSaphir.Text = nbSaphir + "\r\n";
                        break;
                    case "lblOnyxCoin":
                        lblChoiceOnyx.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbOnyx++;
                        lblChoiceOnyx.Text = nbOnyx + "\r\n";
                        break;
                    case "lblEmeraudeCoin":
                        lblChoiceEmeraude.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbEmeraude++;
                        lblChoiceEmeraude.Text = nbEmeraude + "\r\n";
                        break;
                    case "lblDiamandCoin":
                        lblChoiceDiamand.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbDiamand++;
                        lblChoiceDiamand.Text = nbDiamand + "\r\n";
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

            if (enableClicLabel)
            {
                cmdValidateChoice.Visible = true;

                switch (label.Name)
                {
                    case "lblChoiceRubis":
                        lblChoiceRubis.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbRubis--;
                        lblChoiceRubis.Text = nbRubis + "\r\n";

                        if (nbRubis == 0)
                        {
                            lblChoiceRubis.Text = null;

                            if (nbSaphir == 0 && nbOnyx == 0 && nbEmeraude == 0 && nbDiamand == 0)
                            {
                                cmdValidateChoice.Visible = false;
                            }
                        }
                        break;
                    case "lblChoiceSaphir":
                        lblChoiceSaphir.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbSaphir--;
                        lblChoiceSaphir.Text = nbSaphir + "\r\n";

                        if (nbSaphir == 0)
                        {
                            lblChoiceSaphir.Text = null;

                            if (nbRubis == 0 && nbOnyx == 0 && nbEmeraude == 0 && nbDiamand == 0)
                            {
                                cmdValidateChoice.Visible = false;
                            }
                        }
                        break;
                    case "lblChoiceOnyx":
                        lblChoiceOnyx.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbOnyx--;
                        lblChoiceOnyx.Text = nbOnyx + "\r\n";

                        if (nbOnyx == 0)
                        {
                            lblChoiceOnyx.Text = null;

                            if (nbRubis == 0 && nbSaphir == 0 && nbEmeraude == 0 && nbDiamand == 0)
                            {
                                cmdValidateChoice.Visible = false;
                            }
                        }
                        break;
                    case "lblChoiceEmeraude":
                        lblChoiceEmeraude.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbEmeraude--;
                        lblChoiceEmeraude.Text = nbEmeraude + "\r\n";

                        if (nbEmeraude == 0)
                        {
                            lblChoiceEmeraude.Text = null;

                            if (nbRubis == 0 && nbSaphir == 0 && nbOnyx == 0 && nbDiamand == 0)
                            {
                                cmdValidateChoice.Visible = false;
                            }
                        }
                        break;
                    case "lblChoiceDiamand":
                        lblChoiceDiamand.Visible = true;
                        //TO DO check if possible to choose a coin, update the number of available coin
                        nbDiamand--;
                        lblChoiceDiamand.Text = nbDiamand + "\r\n";

                        if (nbDiamand == 0)
                        {
                            lblChoiceDiamand.Text = null;

                            if (nbRubis == 0 && nbSaphir == 0 && nbOnyx == 0 && nbEmeraude == 0)
                            {
                                cmdValidateChoice.Visible = false;
                            }
                        }
                        break;
                    default: break;
                }
            }
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

    }
}
