﻿using System;
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

        private void newGameButton_Click(object sender, EventArgs e)
        {
            Gameplay game = new Gameplay(2, 1);
            Opponent player1 = new PlayerController("Player 1", TankModel.GetTank(1), Gameplay.GetColour(1));
            Opponent player2 = new PlayerController("Player 2", TankModel.GetTank(1), Gameplay.GetColour(2));
            game.CreatePlayer(1, player1);
            game.CreatePlayer(2, player2);
            game.BeginGame();
        }
    }
}
