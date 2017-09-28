
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class AIPlayer : Opponent
    {
        private string name;
        private TankModel tank;
        private Color colour;

        public AIPlayer(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        public override void CommenceRound()
        {
            throw new NotImplementedException();
        }

        public override void NewTurn(GameplayForm gameplayForm, Gameplay currentGame)
        {
            throw new NotImplementedException();
        }

        public override void HitPos(float x, float y)
        {
            throw new NotImplementedException();
        }
    }
}
