using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    /// <summary>
    /// This abstract class represents a generic effect created by a BattleTank's attack. Both Blast and Shell come under this umbrella.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public abstract class Effect
    {
        protected Gameplay effectGame;

        public void ConnectGame(Gameplay game)
        {
            effectGame = game;
        }

        public abstract void Tick();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
