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
    public partial class Rankings : Form
    {
        public Rankings(Gameplay game)
        {
            InitializeComponent();

            int[] playerScores = new int[game.NumPlayers()];
            int winner = 01;
            for (int i = 1; i <= game.NumPlayers(); i++)
            {
                int score = game.GetPlayer(i).GetScore();
                playerScores[i - 1] = score;
            }

            for (int i = 0; i < playerScores.Length - 1; i++)
            {
                int j = i + 1;

                while (j > 0)
                {
                    if (playerScores[j - 1] > playerScores[j])
                    {
                        int temp = playerScores[j - 1];
                        playerScores[j - 1] = playerScores[j];
                        playerScores[j] = temp;

                    }
                    j--;
                }
            }

            winnerLabel.Text = game.GetPlayer(winner).Identifier() + " won!";

            string[] playerArray = new string[game.NumPlayers()];

            for (int i = 1; i <= game.NumPlayers(); i++)
            {
                String newString = game.GetPlayer(i).Identifier() + " (" + game.GetPlayer(i).GetScore() + " wins)";
                playerArray[i - 1] = newString;
            }

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
