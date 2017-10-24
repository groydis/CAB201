using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace TankBattle
{
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
