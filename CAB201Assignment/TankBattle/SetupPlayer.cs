using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        /// <summary>
        /// Sets the values passed in from GameSetup to the correct Form attributes
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public void SetupGamePlay()
        {
            playerNumberNameLbl.Text = playNumName;
            playerNameBox.Text = textBoxName;
        }

        /// <summary>
        /// Changes the players in the array playersArray so that they are either playercontrolled or AIControlled
        /// Checks the AI or Human button checked with playerNameBox
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
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
