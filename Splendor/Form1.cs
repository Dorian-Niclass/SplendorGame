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

            lblChoiceRubis.Visible = false;
            lblChoiceSaphir.Visible = false;
            lblChoiceOnyx.Visible = false;
            lblChoiceEmeraude.Visible = false;
            lblChoiceDiamand.Visible = false;
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
                                    cmdValidateChoice.Visible = true;
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

                                    cmdValidateChoice.Visible = true;
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
                                    cmdValidateChoice.Visible = true;
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

                                    cmdValidateChoice.Visible = true;
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
                                    cmdValidateChoice.Visible = true;
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

                                    cmdValidateChoice.Visible = true;
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
                                    cmdValidateChoice.Visible = true;
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

                                    cmdValidateChoice.Visible = true;
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
                                    cmdValidateChoice.Visible = true;
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

                                    cmdValidateChoice.Visible = true;
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

            cmdValidateChoice.Visible = false;

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
