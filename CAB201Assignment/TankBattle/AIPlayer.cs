
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


        public AIPlayer(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
        }

        public override void CommenceRound()
        {
            angle_min = 0;
            angle_max = 0;
            power = 20;
            
        }

        public override void NewTurn(GameplayForm gameplayForm, Gameplay currentGame)
        {
            this.gameplayForm = gameplayForm;
            this.currentGame = currentGame;

            int playerPos = currentGame.GetCurrentPlayerTank().GetX();
            if (playerPos > Map.WIDTH / 2)
            {
                angle_min = 0;
                angle_max = 18;
            } else
            {
                angle_min = -18;
                angle_max = 0;

            }
            angle = rng.Next(angle_min, angle_max) * 5;
            gameplayForm.SetAngle(angle);

            power = rng.Next(5, 100);
            gameplayForm.SetPower(20);

            gameplayForm.SetWeapon(0);
            gameplayForm.Fire();

        }

        public override void HitPos(float x, float y)
        {
            
        }
    }
}
