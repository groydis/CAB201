using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TankBattle
{
    /// <summary>
    /// This is a concrete class that extends the Opponent class, providing functionality specific to human-controlled Opponents.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public class PlayerController : Opponent
    {
        public PlayerController(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            
        }

        public override void CommenceRound()
        {
            
        }

        public override void NewTurn(GameplayForm gameplayForm, Gameplay currentGame)
        {
            gameplayForm.EnableControlPanel();
        }

        public override void HitPos(float x, float y)
        {
            
        }
    }
}
