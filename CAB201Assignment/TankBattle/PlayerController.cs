using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    public class PlayerController : Opponent
    {
        public PlayerController(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            throw new NotImplementedException();
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
