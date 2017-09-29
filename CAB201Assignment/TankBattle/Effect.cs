using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public abstract class Effect
    {
        protected Gameplay game;
        public void ConnectGame(Gameplay game)
        {
            this.game = game;
        }

        public abstract void Tick();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
