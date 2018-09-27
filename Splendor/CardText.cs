using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splendor
{
    class CardText : RichTextBox
    {
        public Card Card;
        public int row;
        public int col;

        public CardText()
        {
            this.ReadOnly = true;
            this.Size = new System.Drawing.Size(102, 98);
            this.Multiline = true;
         
        }

        public void LoadPosition()
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
        }

        public override void Refresh()
        {
            this.Text = this.Card != null ? this.Card.ToString() : "";
            base.Refresh();
        }

    }
}
