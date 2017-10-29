
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    /// <summary>
    /// This is a concrete class that extends the Opponent class, providing functionality specific to computer-controlled Opponents.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public class AIPlayer : Opponent
    {
        private string name;
        private TankModel tank;
        private Color colour;

        private GameplayForm gameplayForm;
        private Gameplay currentGame;

        private Random rng = new Random();
        int angle_min;
        int angle_max;
        float angle;
        int power;

        List<BattleTank> opponentTank;

        /// <summary>
        /// Creates a player that has properties branching from the Opponent class 
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public AIPlayer(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        /// <summary>
        /// Overrides Gameplay.CommenceRound for the current player to be 
        /// assigned these values in this method.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void CommenceRound()
        {
            angle_min = 0;
            angle_max = 0;
            power = 20;
            
        }

        /// <summary>
        /// Overrides the processes of the Opponent.NewTurn to allow the AI to play the game
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void NewTurn(GameplayForm gameplayForm, Gameplay currentGame)
        {
            this.gameplayForm = gameplayForm;

            gameplayForm.SetAngle(0);
            gameplayForm.SetPower(20);
            gameplayForm.Fire();
        }

        public override void HitPos(float x, float y)
        {
            
        }
    }
}
