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
    public partial class GameSetup : Form
    {
        public static int numOfPlayers;
        public static int numOfRounds;

        public GameSetup()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Allows user to define the number of players and the number of rounds
        /// Creates the players whether it be as AI or Player controlled as determined by the player
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        private void setupGame_Click(object sender, EventArgs e)
        {
            Opponent[] players = new Opponent[(int)playerNumUpDown.Value];
            numOfPlayers = (int)playerNumUpDown.Value;
            numOfRounds = (int)roundsNumUpDown.Value;
            Gameplay game = new Gameplay(numOfPlayers, numOfRounds);

            for (int i = 1; i <= numOfPlayers; i++)
            {
               
                SetupPlayer form = new SetupPlayer();

                //Give the new form the Correct Values
                form.BackColor = Gameplay.GetColour(i);
                form.Text = "Setup Player #" + i;
                form.playNumName = "Player # "+ i + " Name:";
                form.textBoxName = "Player " + i;
                form.whichPlayer = i - 1;
                form.playersArray = players;
                form.SetupGamePlay();
                form.ShowDialog();

                //Return Values and Create player
                players = form.playersArray;
                game.CreatePlayer(i, players[i - 1]);
            }

            game.BeginGame();
            Close();
        }
    }
}
