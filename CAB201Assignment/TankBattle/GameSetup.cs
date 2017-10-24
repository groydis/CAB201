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
        public GameSetup()
        {
            InitializeComponent();
        }

        private void setupGame_Click(object sender, EventArgs e)
        {
            Opponent[] players = new Opponent[(int)playerNumUpDown.Value];
            Gameplay game = new Gameplay((int)playerNumUpDown.Value, (int)roundsNumUpDown.Value);
            if (playerNumUpDown.Value > 2)
            {
                players[0] = new PlayerController("Player 1", TankModel.GetTank(1), Gameplay.GetColour(1));
                players[1] = new PlayerController("Player 2", TankModel.GetTank(1), Gameplay.GetColour(2));
                for (int i = 3; i <= playerNumUpDown.Value; i++)
                {
                    players[i - 1] = new PlayerController("Player " + i, TankModel.GetTank(1), Gameplay.GetColour(i));
                }
            }
            else
            {
                players[0] = new PlayerController("Player 1", TankModel.GetTank(1), Gameplay.GetColour(1));
                players[1] = new PlayerController("Player 2", TankModel.GetTank(1), Gameplay.GetColour(2));
            }

            for (int i = 1; i <= playerNumUpDown.Value; i++)
            {
                game.CreatePlayer(i, players[i - 1]);
            }
            game.BeginGame();
            Close();
        }
    }
}
