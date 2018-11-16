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
    public partial class AddPlayerForm : Form
    {
        frmSplendor frmSplendor;

        public AddPlayerForm(frmSplendor form)
        {
            frmSplendor = form;
            InitializeComponent();

            foreach (Player player in frmSplendor.players)
                lstPlayer.Items.Add(player.Name);

            txtAddPlayer.Select(); 
        }

        /// <summary>
        /// Add a player on the list
        /// </summary>
        /// <param name="Text"></param>
        private void AddPlayer(string Text)
        {
            if (Text != "")
            {
                lstPlayer.Items.Add(txtAddPlayer.Text);
                txtAddPlayer.Clear();
            }
        }

        /// <summary>
        /// When you press on the button, go in the method AddPlayer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            AddPlayer(txtAddPlayer.Text);
        }

        /// <summary>
        /// When you press Enter, go in the method AddPlayer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAddPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AddPlayer(txtAddPlayer.Text);
        }

        /// <summary>
        /// Remove the player selects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdRemovePlayer_Click(object sender, EventArgs e)
        {
            lstPlayer.Items.Remove(lstPlayer.SelectedItem);
            cmdRemovePlayer.Enabled = false;
        }

        /// <summary>
        /// Enable the button when you select a player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPlayer.SelectedIndex >= 0)
                cmdRemovePlayer.Enabled = true;
        }

        /// <summary>
        /// When the finish button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFinish_Click(object sender, EventArgs e)
        {
            if (lstPlayer.Items.Count >= 2 && lstPlayer.Items.Count <= 4)
            {
                frmSplendor.players.Clear();

                foreach (string item in lstPlayer.Items)
                {
                    Player player = new Player();

                    player.Name = item;
                    player.Coins = new Dictionary<Ressources, int>()
                    {
                        {Ressources.Diamand, 0},
                        {Ressources.Onyx, 0},
                        {Ressources.Rubis, 0},
                        {Ressources.Saphir, 0},
                        {Ressources.Emeraude, 0}
                    };
                    player.Cards = new List<Card>();

                    frmSplendor.players.Add(player);
                }

                frmSplendor.enableClicPlay = true;

                Form.ActiveForm.Close();
            }
            else
                MessageBox.Show("Vous devez inserer entre deux et quatre joueurs.");
        }
    }
}
