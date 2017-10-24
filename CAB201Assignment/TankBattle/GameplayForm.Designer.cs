namespace TankBattle
{
    partial class GameplayForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameplayForm));
            this.displayPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.powerTrackBar = new System.Windows.Forms.TrackBar();
            this.angleNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.weaponComboBox = new System.Windows.Forms.ComboBox();
            this.fireButton = new System.Windows.Forms.Button();
            this.powerValueLabel = new System.Windows.Forms.Label();
            this.powerLabel = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            this.weaponLabel = new System.Windows.Forms.Label();
            this.currWindLabel = new System.Windows.Forms.Label();
            this.windLabel = new System.Windows.Forms.Label();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.Location = new System.Drawing.Point(0, 32);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(800, 600);
            this.displayPanel.TabIndex = 0;
            this.displayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.displayPanel_Paint);
            // 
            // controlPanel
            // 
            this.controlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlPanel.BackColor = System.Drawing.Color.OrangeRed;
            this.controlPanel.Controls.Add(this.powerTrackBar);
            this.controlPanel.Controls.Add(this.angleNumericUpDown);
            this.controlPanel.Controls.Add(this.weaponComboBox);
            this.controlPanel.Controls.Add(this.fireButton);
            this.controlPanel.Controls.Add(this.powerValueLabel);
            this.controlPanel.Controls.Add(this.powerLabel);
            this.controlPanel.Controls.Add(this.angleLabel);
            this.controlPanel.Controls.Add(this.weaponLabel);
            this.controlPanel.Controls.Add(this.currWindLabel);
            this.controlPanel.Controls.Add(this.windLabel);
            this.controlPanel.Controls.Add(this.playerNameLabel);
            this.controlPanel.Enabled = false;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(800, 32);
            this.controlPanel.TabIndex = 1;
            // 
            // powerTrackBar
            // 
            this.powerTrackBar.AutoSize = false;
            this.powerTrackBar.LargeChange = 10;
            this.powerTrackBar.Location = new System.Drawing.Point(552, 4);
            this.powerTrackBar.Maximum = 100;
            this.powerTrackBar.Minimum = 5;
            this.powerTrackBar.Name = "powerTrackBar";
            this.powerTrackBar.Size = new System.Drawing.Size(120, 28);
            this.powerTrackBar.TabIndex = 20;
            this.powerTrackBar.TickFrequency = 4;
            this.powerTrackBar.Value = 5;
            this.powerTrackBar.Scroll += new System.EventHandler(this.powerTrackBar_Scroll);
            // 
            // angleNumericUpDown
            // 
            this.angleNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.angleNumericUpDown.Location = new System.Drawing.Point(443, 5);
            this.angleNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.angleNumericUpDown.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.angleNumericUpDown.Name = "angleNumericUpDown";
            this.angleNumericUpDown.Size = new System.Drawing.Size(40, 20);
            this.angleNumericUpDown.TabIndex = 8;
            this.angleNumericUpDown.ValueChanged += new System.EventHandler(this.angleNumericUpDown_ValueChanged);
            // 
            // weaponComboBox
            // 
            this.weaponComboBox.FormattingEnabled = true;
            this.weaponComboBox.Location = new System.Drawing.Point(256, 4);
            this.weaponComboBox.Name = "weaponComboBox";
            this.weaponComboBox.Size = new System.Drawing.Size(121, 21);
            this.weaponComboBox.TabIndex = 7;
            this.weaponComboBox.SelectedIndexChanged += new System.EventHandler(this.weaponComboBox_SelectedIndexChanged);
            // 
            // fireButton
            // 
            this.fireButton.Location = new System.Drawing.Point(722, 4);
            this.fireButton.Name = "fireButton";
            this.fireButton.Size = new System.Drawing.Size(75, 23);
            this.fireButton.TabIndex = 6;
            this.fireButton.Text = "Fire";
            this.fireButton.UseVisualStyleBackColor = true;
            this.fireButton.Click += new System.EventHandler(this.fireButton_Click);
            // 
            // powerValueLabel
            // 
            this.powerValueLabel.AutoSize = true;
            this.powerValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerValueLabel.Location = new System.Drawing.Point(678, 6);
            this.powerValueLabel.Name = "powerValueLabel";
            this.powerValueLabel.Size = new System.Drawing.Size(27, 20);
            this.powerValueLabel.TabIndex = 5;
            this.powerValueLabel.Text = "20";
            // 
            // powerLabel
            // 
            this.powerLabel.AutoSize = true;
            this.powerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.powerLabel.Location = new System.Drawing.Point(489, 6);
            this.powerLabel.Name = "powerLabel";
            this.powerLabel.Size = new System.Drawing.Size(57, 20);
            this.powerLabel.TabIndex = 4;
            this.powerLabel.Text = "Power:";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.angleLabel.Location = new System.Drawing.Point(383, 4);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(54, 20);
            this.angleLabel.TabIndex = 3;
            this.angleLabel.Text = "Angle:";
            // 
            // weaponLabel
            // 
            this.weaponLabel.AutoSize = true;
            this.weaponLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponLabel.Location = new System.Drawing.Point(177, 4);
            this.weaponLabel.Name = "weaponLabel";
            this.weaponLabel.Size = new System.Drawing.Size(73, 20);
            this.weaponLabel.TabIndex = 0;
            this.weaponLabel.Text = "Weapon:";
            // 
            // currWindLabel
            // 
            this.currWindLabel.Location = new System.Drawing.Point(102, 16);
            this.currWindLabel.Name = "currWindLabel";
            this.currWindLabel.Size = new System.Drawing.Size(33, 13);
            this.currWindLabel.TabIndex = 2;
            this.currWindLabel.Text = "0 W";
            this.currWindLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windLabel
            // 
            this.windLabel.AutoSize = true;
            this.windLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.windLabel.Location = new System.Drawing.Point(99, 4);
            this.windLabel.Name = "windLabel";
            this.windLabel.Size = new System.Drawing.Size(36, 13);
            this.windLabel.TabIndex = 1;
            this.windLabel.Text = "Wind";
            this.windLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameLabel.Location = new System.Drawing.Point(3, 4);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(73, 20);
            this.playerNameLabel.TabIndex = 0;
            this.playerNameLabel.Text = "Player 1";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GameplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 629);
            this.Controls.Add(this.controlPanel);
            this.Controls.Add(this.displayPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "GameplayForm";
            this.Text = "Form1";
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.angleNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Label powerValueLabel;
        private System.Windows.Forms.Label powerLabel;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.Label weaponLabel;
        private System.Windows.Forms.Label currWindLabel;
        private System.Windows.Forms.Label windLabel;
        private System.Windows.Forms.TrackBar powerTrackBar;
        private System.Windows.Forms.NumericUpDown angleNumericUpDown;
        private System.Windows.Forms.ComboBox weaponComboBox;
        private System.Windows.Forms.Timer timer1;
    }
}

