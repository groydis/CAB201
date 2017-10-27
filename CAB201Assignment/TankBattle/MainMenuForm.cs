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
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Allows user to define the number of players and the number of rounds
        /// Creates the players whether it be as AI or Player controlled as determined by the player
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        private void newGameButton_Click(object sender, EventArgs e)
        {            
            GameSetup setupGame = new GameSetup();
            setupGame.Show();
        }
    }
}
