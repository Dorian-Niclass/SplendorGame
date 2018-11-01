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
        public AddPlayerForm()
        {
            InitializeComponent();
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
    }
}
