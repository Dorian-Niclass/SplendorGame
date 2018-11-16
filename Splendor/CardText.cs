using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    class CardText : TextBox
    {
        public Card Card { get; private set; }
        public int row;
        public int col;

        private Label ressLabel = new Label();
        private Label prestigeLabel = new Label();
        private RichTextBox costTxt = new RichTextBox();

        public CardText()
        {
            this.ReadOnly = true;
            this.Size = new System.Drawing.Size(102, 98);
            this.Multiline = true;
            this.Cursor = Cursors.Hand;
            this.Card = null;
        }

        /// <summary>
        /// Set the current card of this case
        /// </summary>
        /// <param name="card"></param>
        public void SetCard(Card card)
        {
            this.Card = card;

            this.RefreshCard();
        }

        /// <summary>
        /// Load the case
        /// </summary>
        public void Load()
        {
            try
            {
                if (this.Name.Contains("Noble"))
                {
                    this.row = 3;
                }
                else
                {
                    this.row = Int32.Parse(this.Name.Substring(this.Name.Length - 2, 1)) - 1;
                }

                this.col = Int32.Parse(this.Name.Substring(this.Name.Length - 1, 1)) - 1;
            }
            catch (Exception e)
            {

            }

            //Load the controls used to display the card informations
            this.ressLabel.Location = new Point(0, 0);
            this.ressLabel.Size = new Size(70, 20);
            this.ressLabel.Click += new EventHandler(childControl_Click);
            this.Controls.Add(ressLabel);

            this.prestigeLabel.Location = new Point(70, 0);
            this.prestigeLabel.Size = new Size(32, 20);
            this.prestigeLabel.Click += new EventHandler(childControl_Click);
            this.Controls.Add(prestigeLabel);

            this.costTxt.Location = new Point(0, 20);
            this.costTxt.Size = new Size(98, 70);
            this.costTxt.BorderStyle = BorderStyle.None;
            this.costTxt.ReadOnly = true;
            this.costTxt.BackColor = Color.LightGray;
            this.costTxt.Click += new EventHandler(childControl_Click);
            this.costTxt.Cursor = Cursors.Hand;
            this.Controls.Add(costTxt);
        }

        /// <summary>
        /// Click on the case
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void childControl_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        /// <summary>
        /// Refresh the card display
        /// </summary>
        public void RefreshCard()
        {
            if (this.Card != null)
            {
                //Set a color for each type of ressources
                switch (this.Card.Ress)
                {
                    case Ressources.Rubis: this.ressLabel.ForeColor = Color.Red; break;
                    case Ressources.Saphir: this.ressLabel.ForeColor = Color.Blue; break;
                    case Ressources.Emeraude: this.ressLabel.ForeColor = Color.Green; break;
                    case Ressources.Diamand: this.ressLabel.ForeColor = Color.SkyBlue; break;
                    case Ressources.Onyx: this.ressLabel.ForeColor = Color.Black; break;
                }

                this.ressLabel.Text = this.Card.Ress.ToString();
                this.prestigeLabel.Text = this.Card.PrestigePt != 0 ? this.Card.PrestigePt.ToString() : "";
                this.costTxt.Text = "";
                foreach (Ressources ress in this.Card.Cost.Keys)
                {
                    this.costTxt.SelectionStart = this.costTxt.TextLength;
                    this.costTxt.SelectionLength = 0;
                    switch (ress)
                    {
                        case Ressources.Rubis: this.costTxt.SelectionColor = Color.Red; break;
                        case Ressources.Saphir: this.costTxt.SelectionColor = Color.Blue; break;
                        case Ressources.Emeraude: this.costTxt.SelectionColor = Color.Green; break;
                        case Ressources.Diamand: this.costTxt.SelectionColor = Color.SkyBlue; break;
                        case Ressources.Onyx: this.costTxt.SelectionColor = Color.Black; break;

                    }
                    if (this.Card.Cost[ress] != 0)
                        this.costTxt.AppendText(ress.ToString() + " - " + this.Card.Cost[ress] + Environment.NewLine);
                }
            }
            else
            {
                //There is no card in this case, nothing is displayed
                this.ressLabel.Text = "";
                this.prestigeLabel.Text = "";
                this.costTxt.Text = "";
            }            
        }

    }
}
