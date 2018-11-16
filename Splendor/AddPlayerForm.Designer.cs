namespace Splendor
{
    partial class AddPlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstPlayer = new System.Windows.Forms.ListBox();
            this.txtAddPlayer = new System.Windows.Forms.TextBox();
            this.cmdRemovePlayer = new System.Windows.Forms.Button();
            this.cmdFinish = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPlayer
            // 
            this.lstPlayer.FormattingEnabled = true;
            this.lstPlayer.Location = new System.Drawing.Point(12, 12);
            this.lstPlayer.Name = "lstPlayer";
            this.lstPlayer.Size = new System.Drawing.Size(271, 238);
            this.lstPlayer.TabIndex = 0;
            this.lstPlayer.SelectedIndexChanged += new System.EventHandler(this.lstPlayer_SelectedIndexChanged);
            // 
            // txtAddPlayer
            // 
            this.txtAddPlayer.Location = new System.Drawing.Point(12, 256);
            this.txtAddPlayer.Name = "txtAddPlayer";
            this.txtAddPlayer.Size = new System.Drawing.Size(189, 20);
            this.txtAddPlayer.TabIndex = 1;
            this.txtAddPlayer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddPlayer_KeyDown);
            // 
            // cmdRemovePlayer
            // 
            this.cmdRemovePlayer.Enabled = false;
            this.cmdRemovePlayer.Location = new System.Drawing.Point(12, 282);
            this.cmdRemovePlayer.Name = "cmdRemovePlayer";
            this.cmdRemovePlayer.Size = new System.Drawing.Size(128, 20);
            this.cmdRemovePlayer.TabIndex = 2;
            this.cmdRemovePlayer.Text = "Supprimer";
            this.cmdRemovePlayer.UseVisualStyleBackColor = true;
            this.cmdRemovePlayer.Click += new System.EventHandler(this.cmdRemovePlayer_Click);
            // 
            // cmdFinish
            // 
            this.cmdFinish.Location = new System.Drawing.Point(155, 282);
            this.cmdFinish.Name = "cmdFinish";
            this.cmdFinish.Size = new System.Drawing.Size(128, 20);
            this.cmdFinish.TabIndex = 3;
            this.cmdFinish.Text = "Terminé";
            this.cmdFinish.UseVisualStyleBackColor = true;
            this.cmdFinish.Click += new System.EventHandler(this.cmdFinish_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Location = new System.Drawing.Point(207, 255);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(76, 20);
            this.cmdAdd.TabIndex = 4;
            this.cmdAdd.Text = "Ajouter";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // AddPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 314);
            this.Controls.Add(this.cmdAdd);
            this.Controls.Add(this.cmdFinish);
            this.Controls.Add(this.cmdRemovePlayer);
            this.Controls.Add(this.txtAddPlayer);
            this.Controls.Add(this.lstPlayer);
            this.Name = "AddPlayerForm";
            this.Text = "AddPlayerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPlayer;
        private System.Windows.Forms.TextBox txtAddPlayer;
        private System.Windows.Forms.Button cmdRemovePlayer;
        private System.Windows.Forms.Button cmdFinish;
        private System.Windows.Forms.Button cmdAdd;
    }
}