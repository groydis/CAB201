using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace TankBattle
{
    /// <summary>
    /// The Shell class is a type of Effect that represents the a projectile or shell launched by a BattleTank. 
    /// A Shell is launched at a certain angle and velocity and is affected by gravity and wind.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public class Shell : Effect
    {
        private float x;
        private float y;
        private float gravity;
        private float x_velocity;
        private float y_velocity;
        private Blast explosion;
        private Opponent player;
        
        private int wind;

        /// <summary>
        /// This method constructs a new Shell.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X position of the shell</param> 
        /// <param name="y">The y Position of teh shell</param> 
        /// <param name="angle">The angle in which the shell is firedy</param> 
        /// <param name="power">The power which assists in determining the shells speedy</param> 
        /// <param name="gravity">Gravity value affecting the shell</param>
        /// <param name="explosion">The blast effect to displayed</param>
        /// <param name="player">The assocated playery</param>
        public Shell(float x, float y, float angle, float power, float gravity, Blast explosion, Opponent player)
        {
            this.x = x;
            this.y = y;
            this.gravity = gravity;

            float angleRadians = (90 - angle) * (float)Math.PI / 180;
            float magnitude = power / 50;

            x_velocity = (float)Math.Cos(angleRadians) * magnitude;
            y_velocity = (float)Math.Sin(angleRadians) * -magnitude;

            this.explosion = explosion;

            this.player = player;
        }

        /// <summary>
        /// This method moves the given projectile according to its angle, power, gravity and the wind.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void Tick()
        {
            for (int i = 0; i < 10; i++)
            {
                x += x_velocity;
                y += y_velocity;

                wind = effectGame.GetWindSpeed();

                x += (wind / 1000.0f);

                if (x >= Map.WIDTH || x <= 0 || y >= Map.HEIGHT || y <= 0)
                {
                    effectGame.RemoveEffect(this);
                    return;
                }
                else if (effectGame.CheckHitTank(x, y))
                {
                    player.HitPos(x, y);
                    explosion.Ignite(x, y);
                    effectGame.AddWeaponEffect(explosion);
                    effectGame.RemoveEffect(this);
                }
                y_velocity += gravity;
            }
        }

        /// <summary>
        /// This method draws the Shell as a small white circle.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void Display(Graphics graphics, Size size)
        {
            float x = (float)this.x * size.Width / Map.WIDTH;
            float y = (float)this.y * size.Height / Map.HEIGHT;
            float s = size.Width / Map.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
