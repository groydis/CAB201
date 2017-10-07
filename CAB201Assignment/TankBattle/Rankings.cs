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
            int[] playerScores = new int[game.NumPlayers()];
            int winner = 0;
            for (int i = 0; i < game.NumPlayers(); i++)
            {
                int score = game.GetPlayer(i).GetScore();
                if (score > winner)
                {
                    winner = score;
                }
            }

            winnerLabel.Text = game.GetPlayer(winner).Identifier() + " won!";


            string[] playerArray = new string[game.NumPlayers()];

            for (int i = 0; i < game.NumPlayers(); i++)
            {
                String newString = game.GetPlayer(i).Identifier() + "(" + game.GetPlayer(i).GetScore() + "wins)";
                playerArray[i] = newString;
            }


            playerListBox.Items.Clear();

            foreach (String name in playerArray)
            {
                playerListBox.Items.Add(name);
    
            }
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            MainMenuForm newMainMenu = new MainMenuForm();
            newMainMenu.Show();
        }
    }
}
