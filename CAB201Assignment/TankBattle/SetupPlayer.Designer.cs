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
            this.playerNumberNameLbl = new System.Windows.Forms.Label();
            this.playerNameBox = new System.Windows.Forms.TextBox();
            this.humanRButton = new System.Windows.Forms.RadioButton();
            this.controlBox = new System.Windows.Forms.GroupBox();
            this.AIRButton = new System.Windows.Forms.RadioButton();
            this.tankBox = new System.Windows.Forms.GroupBox();
            this.tankFourRButton = new System.Windows.Forms.RadioButton();
            this.tankThreeRButton = new System.Windows.Forms.RadioButton();
            this.tankTwoRButton = new System.Windows.Forms.RadioButton();
            this.tankOneRButton = new System.Windows.Forms.RadioButton();
            this.nextPlayerButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.controlBox.SuspendLayout();
            this.tankBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // playerNumberNameLbl
            // 
            this.playerNumberNameLbl.AutoSize = true;
            this.playerNumberNameLbl.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNumberNameLbl.Location = new System.Drawing.Point(35, 16);
            this.playerNumberNameLbl.Name = "playerNumberNameLbl";
            this.playerNumberNameLbl.Size = new System.Drawing.Size(139, 22);
            this.playerNumberNameLbl.TabIndex = 0;
            this.playerNumberNameLbl.Text = "Player # Name:";
            // 
            // playerNameBox
            // 
            this.playerNameBox.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameBox.Location = new System.Drawing.Point(180, 12);
            this.playerNameBox.Name = "playerNameBox";
            this.playerNameBox.Size = new System.Drawing.Size(169, 29);
            this.playerNameBox.TabIndex = 1;
            this.playerNameBox.Text = "Player 1";
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
            // controlBox
            // 
            this.controlBox.Controls.Add(this.AIRButton);
            this.controlBox.Controls.Add(this.humanRButton);
            this.controlBox.Location = new System.Drawing.Point(11, 59);
            this.controlBox.Name = "controlBox";
            this.controlBox.Size = new System.Drawing.Size(119, 51);
            this.controlBox.TabIndex = 3;
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
            // tankBox
            // 
            this.tankBox.Controls.Add(this.tankFourRButton);
            this.tankBox.Controls.Add(this.tankThreeRButton);
            this.tankBox.Controls.Add(this.tankTwoRButton);
            this.tankBox.Controls.Add(this.tankOneRButton);
            this.tankBox.Location = new System.Drawing.Point(180, 59);
            this.tankBox.Name = "tankBox";
            this.tankBox.Size = new System.Drawing.Size(169, 52);
            this.tankBox.TabIndex = 4;
            this.tankBox.TabStop = false;
            this.tankBox.Text = "Tank";
            // 
            // tankFourRButton
            // 
            this.tankFourRButton.AutoSize = true;
            this.tankFourRButton.Location = new System.Drawing.Point(125, 23);
            this.tankFourRButton.Name = "tankFourRButton";
            this.tankFourRButton.Size = new System.Drawing.Size(14, 13);
            this.tankFourRButton.TabIndex = 3;
            this.tankFourRButton.UseVisualStyleBackColor = true;
            // 
            // tankThreeRButton
            // 
            this.tankThreeRButton.AutoSize = true;
            this.tankThreeRButton.Location = new System.Drawing.Point(85, 23);
            this.tankThreeRButton.Name = "tankThreeRButton";
            this.tankThreeRButton.Size = new System.Drawing.Size(14, 13);
            this.tankThreeRButton.TabIndex = 2;
            this.tankThreeRButton.UseVisualStyleBackColor = true;
            // 
            // tankTwoRButton
            // 
            this.tankTwoRButton.AutoSize = true;
            this.tankTwoRButton.Location = new System.Drawing.Point(47, 23);
            this.tankTwoRButton.Name = "tankTwoRButton";
            this.tankTwoRButton.Size = new System.Drawing.Size(14, 13);
            this.tankTwoRButton.TabIndex = 1;
            this.tankTwoRButton.UseVisualStyleBackColor = true;
            // 
            // tankOneRButton
            // 
            this.tankOneRButton.AutoSize = true;
            this.tankOneRButton.Checked = true;
            this.tankOneRButton.Location = new System.Drawing.Point(6, 23);
            this.tankOneRButton.Name = "tankOneRButton";
            this.tankOneRButton.Size = new System.Drawing.Size(14, 13);
            this.tankOneRButton.TabIndex = 0;
            this.tankOneRButton.TabStop = true;
            this.tankOneRButton.UseVisualStyleBackColor = true;
            // 
            // nextPlayerButton
            // 
            this.nextPlayerButton.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextPlayerButton.Location = new System.Drawing.Point(11, 126);
            this.nextPlayerButton.Name = "nextPlayerButton";
            this.nextPlayerButton.Size = new System.Drawing.Size(338, 25);
            this.nextPlayerButton.TabIndex = 5;
            this.nextPlayerButton.Text = "Next Player";
            this.nextPlayerButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.nextPlayerButton);
            this.panel1.Controls.Add(this.tankBox);
            this.panel1.Controls.Add(this.controlBox);
            this.panel1.Controls.Add(this.playerNameBox);
            this.panel1.Controls.Add(this.playerNumberNameLbl);
            this.panel1.Location = new System.Drawing.Point(35, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 160);
            this.panel1.TabIndex = 6;
            // 
            // SetupPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(431, 226);
            this.Controls.Add(this.panel1);
            this.Name = "SetupPlayer";
            this.ShowIcon = false;
            this.Text = "Setup Player #";
            this.controlBox.ResumeLayout(false);
            this.controlBox.PerformLayout();
            this.tankBox.ResumeLayout(false);
            this.tankBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label playerNumberNameLbl;
        private System.Windows.Forms.TextBox playerNameBox;
        private System.Windows.Forms.RadioButton humanRButton;
        private System.Windows.Forms.GroupBox controlBox;
        private System.Windows.Forms.RadioButton AIRButton;
        private System.Windows.Forms.GroupBox tankBox;
        private System.Windows.Forms.Button nextPlayerButton;
        private System.Windows.Forms.RadioButton tankFourRButton;
        private System.Windows.Forms.RadioButton tankThreeRButton;
        private System.Windows.Forms.RadioButton tankTwoRButton;
        private System.Windows.Forms.RadioButton tankOneRButton;
        private System.Windows.Forms.Panel panel1;
    }
}