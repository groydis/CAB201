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
        public void ConnectGame(Gameplay game)
        {
            throw new NotImplementedException();
        }

        public abstract void Tick();
        public abstract void Display(Graphics graphics, Size displaySize);
    }
}
