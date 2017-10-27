using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TankBattle
{
    public partial class SetupPlayer : Form
    {
        public string playNumName;
        public string textBoxName;
        public int whichPlayer;

        public Opponent[] playersArray;
        public SetupPlayer()
        {            
            InitializeComponent();         
        }

        public void SetupGamePlay()
        {
            playerNumberNameLbl.Text = playNumName;
            playerNameBox.Text = textBoxName;
        }

        private void nextPlayerButton_Click_1(object sender, EventArgs e)
        {
            if (AIRButton.Checked)
            {
                playersArray[whichPlayer] = new AIPlayer(playerNameBox.Text, TankModel.GetTank(1), Gameplay.GetColour(whichPlayer + 1));
            }
            else
            {
                playersArray[whichPlayer] = new PlayerController(playerNameBox.Text, TankModel.GetTank(1), Gameplay.GetColour(whichPlayer + 1));
            }
            this.Close();
        }
    }
}
