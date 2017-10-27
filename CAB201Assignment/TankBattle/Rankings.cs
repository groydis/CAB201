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
    /// <summary>
    ///
    /// Form for displaying rankings
    /// Author Greyden Scott October 2017
    ///
    /// </summary>
    public partial class Rankings : Form
    {
        private Opponent[] players;

        /// <summary>
        ///
        /// Form for displaying rankings
        /// Creates a list of player scores
        /// Checks scores against each other searching for highest score
        /// Finds associated player for that score and sets them as the winner
        /// Then populates form fields with relevant data
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        ///
        /// </summary>
        public Rankings(Gameplay game)
        {
            InitializeComponent();

            int[] playerScores = new int[game.NumPlayers()];
            
            int winner = 1;

            bool tie_occured = false;

            for (int i = 0; i < game.NumPlayers(); i++)
            {
                playerScores[i] = game.GetPlayer(i + 1).GetScore();
            }
            int maxValue = playerScores.Max();
            winner = playerScores.ToList().IndexOf(maxValue) + 1;


            //for (int i = 0; i < game.NumPlayers(); i++)
            //{
            //    int score_to_check = game.GetPlayer(i + 1).GetScore();
            //    for (int x = 0; i < game.NumPlayers(); i++)
            //    {
            //        Debug.WriteLine("Score to check: " + score_to_check);
            //        Debug.WriteLine("VS this player: " + playerScores[x]);
            //        if (score_to_check < playerScores[x])
            //        {
            //            winner = i + 1;
            //        } else if (score_to_check == playerScores[x]) {
            //            tie_occured = true;
            //        }
            //    }
            //}

            for (int i = 0; i < playerScores.Length; i++)
            {
                for (int j = i + 1; j < playerScores.Length; j++)
                {
                    if (playerScores[i] == playerScores[j] && playerScores[i] == maxValue)
                    {
                        tie_occured = true;
                    }
                    
                }
            }

            if (tie_occured)
            {
                winnerLabel.Text = "Tie!";
            } else {
                winnerLabel.Text = game.GetPlayer(winner).Identifier() + " won!";
            }


            string[] playerArray = new string[game.NumPlayers()];

            for (int i = 0; i < game.NumPlayers(); i++)
            {
                String newString = game.GetPlayer(i + 1).Identifier() + " (" + game.GetPlayer(i + 1).GetScore() + " wins)";
                playerArray[i] = newString;
            }
            playerListBox.Items.Clear();

            foreach (String name in playerArray)
            {
                playerListBox.Items.Add(name);

            }
        }
        /// <summary>
        /// Closes the window
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
