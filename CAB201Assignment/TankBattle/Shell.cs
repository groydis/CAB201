using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

            this.player = player;
        }

        public override void Tick()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Started Loop");
                x = x + x_velocity;
                Console.WriteLine("x = {0}, x_vel = {1}", x, x_velocity);
                y = y + y_velocity;
                Console.WriteLine("y = {0}, y_vel = {1}", y, y_velocity);

                wind = game.GetWindSpeed();

                x = x + (wind / 1000.0f);

                Console.WriteLine("Got Wind");


                if (x >= Map.WIDTH || x <= 0 || y >= Map.HEIGHT || y <= 0)
                {
                    Console.WriteLine("Outside Bounds");
                    game.RemoveEffect(this);
                    return;
                }

                if (game.CheckHitTank(x, y) == true)
                {
                    Console.WriteLine("TankHit true");
                    player.HitPos(x, y);
                    Console.WriteLine("HitPos working");
                    explosion.Ignite(x, y);
                    Console.WriteLine("Ignite Working");
                    game.AddWeaponEffect(explosion);
                    Console.WriteLine("AddWeaponEffect Working");
                    game.RemoveEffect(this);
                    Console.WriteLine("RemoveEffect Working");
                }
                y = y + gravity;
            }
        }

        public override void Display(Graphics graphics, Size size)
        {
            x = x * size.Width / Map.WIDTH;
            y = y * size.Height / Map.HEIGHT;
            float s = size.Width / Map.WIDTH;

            RectangleF r = new RectangleF(x - s / 2.0f, y - s / 2.0f, s, s);
            Brush b = new SolidBrush(Color.WhiteSmoke);

            graphics.FillEllipse(b, r);
        }
    }
}
