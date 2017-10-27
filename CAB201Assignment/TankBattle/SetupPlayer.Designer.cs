namespace TankBattle
{
    partial class SetupPlayer
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
            this.playerNameBox = new System.Windows.Forms.TextBox();
            this.playerNumberNameLbl = new System.Windows.Forms.Label();
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.AIRButton = new System.Windows.Forms.RadioButton();
            this.humanRButton = new System.Windows.Forms.RadioButton();
            this.nextPlayerButton = new System.Windows.Forms.Button();
            this.controlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerNameBox
            // 
            this.playerNameBox.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameBox.Location = new System.Drawing.Point(212, 45);
            this.playerNameBox.Name = "playerNameBox";
            this.playerNameBox.Size = new System.Drawing.Size(169, 29);
            this.playerNameBox.TabIndex = 7;
            this.playerNameBox.Text = "Player 1";
            // 
            // playerNumberNameLbl
            // 
            this.playerNumberNameLbl.AutoSize = true;
            this.playerNumberNameLbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNumberNameLbl.Location = new System.Drawing.Point(45, 49);
            this.playerNumberNameLbl.Name = "playerNumberNameLbl";
            this.playerNumberNameLbl.Size = new System.Drawing.Size(149, 22);
            this.playerNumberNameLbl.TabIndex = 6;
            this.playerNumberNameLbl.Text = "Player # Name:";
            // 
            // controlBox
            // 
            this.controlBox.Controls.Add(this.AIRButton);
            this.controlBox.Controls.Add(this.humanRButton);
            this.controlBox.Location = new System.Drawing.Point(43, 90);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(119, 51);
            this.controlBox.TabIndex = 8;
            this.controlBox.TabStop = false;
            this.controlBox.Text = "Controller";
            // 
            // AIRButton
            // 
            this.AIRButton.AutoSize = true;
            this.AIRButton.Location = new System.Drawing.Point(73, 21);
            this.AIRButton.Name = "AIRButton";
            this.AIRButton.Size = new System.Drawing.Size(35, 17);
            this.AIRButton.TabIndex = 3;
            this.AIRButton.Text = "AI";
            this.AIRButton.UseVisualStyleBackColor = true;
            // 
            // humanRButton
            // 
            this.humanRButton.AutoSize = true;
            this.humanRButton.Checked = true;
            this.humanRButton.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.humanRButton.Location = new System.Drawing.Point(6, 19);
            this.humanRButton.Name = "humanRButton";
            this.humanRButton.Size = new System.Drawing.Size(60, 20);
            this.humanRButton.TabIndex = 2;
            this.humanRButton.TabStop = true;
            this.humanRButton.Text = "Human";
            this.humanRButton.UseVisualStyleBackColor = true;
            // 
            // nextPlayerButton
            // 
            this.nextPlayerButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPlayerButton.Location = new System.Drawing.Point(168, 96);
            this.nextPlayerButton.Name = "nextPlayerButton";
            this.nextPlayerButton.Size = new System.Drawing.Size(220, 45);
            this.nextPlayerButton.TabIndex = 9;
            this.nextPlayerButton.Text = "Next Player";
            this.nextPlayerButton.UseVisualStyleBackColor = true;
            this.nextPlayerButton.Click += new System.EventHandler(this.nextPlayerButton_Click_1);
            // 
            // SetupPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(431, 187);
            this.Controls.Add(this.playerNumberNameLbl);
            this.Controls.Add(this.nextPlayerButton);
            this.Controls.Add(this.playerNameBox);
            this.Controls.Add(this.controlBox);
            this.Name = "SetupPlayer";
            this.ShowIcon = false;
            this.Text = "Setup Player #";
            this.controlBox.ResumeLayout(false);
            this.controlBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox playerNameBox;
        private System.Windows.Forms.Label playerNumberNameLbl;
        private System.Windows.Forms.GroupBox controlBox;
        private System.Windows.Forms.RadioButton AIRButton;
        private System.Windows.Forms.RadioButton humanRButton;
        private System.Windows.Forms.Button nextPlayerButton;
    }
}