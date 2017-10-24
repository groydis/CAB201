
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

        private GameplayForm gameplayForm;
        private Gameplay currentGame;

        private Random rng = new Random();
 

        private float prev_angle;
        private int prev_power;

        private int prev_x_hit;
        private int prev_y_hit;

        float angle;
        int power;
  

        public AIPlayer(string name, TankModel tank, Color colour) : base(name, tank, colour)
        {
            this.name = name;
            this.tank = tank;
            this.colour = colour;
            prev_angle = 0;
            prev_power = 0;
        }

        public override void CommenceRound()
        {

            
        }

        public override void NewTurn(GameplayForm gameplayForm, Gameplay currentGame)
        {
            this.gameplayForm = gameplayForm;
            this.currentGame = currentGame;


            if (prev_angle == 0)
            {
                angle = rng.Next(-90, 90);
                gameplayForm.SetAngle(angle);
                prev_angle = angle;
                gameplayForm.SetPower(20);
                prev_power = 20;
                gameplayForm.SetWeapon(0);
                gameplayForm.Fire();
            } else
            {
                
            }
            throw new NotImplementedException();
        }

        public override void HitPos(float x, float y)
        {
            
        }
    }
}
