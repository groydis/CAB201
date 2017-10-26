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
    public partial class Rankings : Form
    {
        private Opponent[] players;

        public Rankings(Gameplay game)
        {
            InitializeComponent();

            int[] playerScores = new int[game.NumPlayers()];
            
            int winner = 1;

            Debug.WriteLine("Populate array of scores");
            for (int i = 0; i < game.NumPlayers(); i++)
            {
                playerScores[i] = game.GetPlayer(i + 1).GetScore();
            }
            Debug.WriteLine("Populate array of scores -> Done");
            Debug.WriteLine("Find winner");
            for (int i = 0; i < game.NumPlayers(); i++)
            {
                int score_to_check = game.GetPlayer(i + 1).GetScore();
                for (int x = 0; i < game.NumPlayers(); i++)
                {
                    if (score_to_check > playerScores[x])
                    {
                        winner = i + 1;
                    }
                }
            }
            Debug.WriteLine("Find winner -> Done Winner: " + winner);

            Debug.WriteLine("Writing Winner String ");
            winnerLabel.Text = game.GetPlayer(winner).Identifier() + " won!";
            Debug.WriteLine("Writing Winner String -> Done ");

            Debug.WriteLine("Compiling list of players with scores");
            string[] playerArray = new string[game.NumPlayers()];

            for (int i = 0; i < game.NumPlayers(); i++)
            {
                String newString = game.GetPlayer(i + 1).Identifier() + " (" + game.GetPlayer(i + 1).GetScore() + " wins)";
                playerArray[i] = newString;
            }
            Debug.WriteLine("Compiling list of players with scores -> Done");
            playerListBox.Items.Clear();

            foreach (String name in playerArray)
            {
                playerListBox.Items.Add(name);

            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
