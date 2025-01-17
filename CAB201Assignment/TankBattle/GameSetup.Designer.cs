﻿namespace TankBattle
{
    partial class GameSetup
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.playerNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.roundsNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.setupGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playerNumUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundsNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "How many players? (2-8)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(332, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "How many gameplay rounds?";
            // 
            // playerNumUpDown
            // 
            this.playerNumUpDown.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNumUpDown.Location = new System.Drawing.Point(350, 7);
            this.playerNumUpDown.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.playerNumUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.playerNumUpDown.Name = "playerNumUpDown";
            this.playerNumUpDown.Size = new System.Drawing.Size(38, 29);
            this.playerNumUpDown.TabIndex = 2;
            this.playerNumUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // roundsNumUpDown
            // 
            this.roundsNumUpDown.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roundsNumUpDown.Location = new System.Drawing.Point(350, 48);
            this.roundsNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.roundsNumUpDown.Name = "roundsNumUpDown";
            this.roundsNumUpDown.Size = new System.Drawing.Size(38, 29);
            this.roundsNumUpDown.TabIndex = 3;
            this.roundsNumUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // setupGame
            // 
            this.setupGame.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setupGame.Location = new System.Drawing.Point(17, 83);
            this.setupGame.Name = "setupGame";
            this.setupGame.Size = new System.Drawing.Size(371, 39);
            this.setupGame.TabIndex = 4;
            this.setupGame.Text = "Setup Players";
            this.setupGame.UseVisualStyleBackColor = true;
            this.setupGame.Click += new System.EventHandler(this.setupGame_Click);
            // 
            // GameSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 133);
            this.Controls.Add(this.setupGame);
            this.Controls.Add(this.roundsNumUpDown);
            this.Controls.Add(this.playerNumUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "GameSetup";
            this.ShowIcon = false;
            this.Text = "GameSetup";
            ((System.ComponentModel.ISupportInitialize)(this.playerNumUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roundsNumUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown playerNumUpDown;
        private System.Windows.Forms.NumericUpDown roundsNumUpDown;
        private System.Windows.Forms.Button setupGame;
    }
}