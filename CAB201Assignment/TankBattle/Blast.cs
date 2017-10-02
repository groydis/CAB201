using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Blast : Effect
    {
        private int explosionDamage;
        private int explosionRadius;
        private int earthDestructionRadius;

        private float x;
        private float y;
        private float blastLifeSpan;

        public Blast(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.earthDestructionRadius = earthDestructionRadius;
        }

        public void Ignite(float x, float y)
        {
            this.x = x;
            this.y = y;

            blastLifeSpan = 1.0f;
        }

        public override void Tick()
        {
            blastLifeSpan -= 0.05f;

            if (blastLifeSpan <= 0)
            {
                game.InflictDamage(x,y,explosionDamage,explosionRadius);

                Map thisMap = game.GetArena();
                thisMap.DestroyTerrain(x,y,earthDestructionRadius);

                game.RemoveEffect(this);

            }
        }

        public override void Display(Graphics graphics, Size displaySize)
        {
            //Answer has been given to us and just needs variables changed to private fields and 
            //Use variables that have been created in other scripts
            float disX = x * displaySize.Width / Map.WIDTH;
            float disY = y * displaySize.Height / Map.HEIGHT;
            float radius = displaySize.Width * (float)((1.0 - blastLifeSpan) * explosionRadius * 3.0 / 2.0) / Map.WIDTH;

            int alpha = 0, red = 0, green = 0, blue = 0;

            if (blastLifeSpan < 1.0 / 3.0)
            {
                red = 255;
                alpha = (int)(blastLifeSpan * 3.0 * 255);
            }
            else if (blastLifeSpan < 2.0 / 3.0)
            {
                red = 255;
                alpha = 255;
                green = (int)((blastLifeSpan * 3.0 - 1.0) * 255);
            }
            else
            {
                red = 255;
                alpha = 255;
                green = 255;
                blue = (int)((blastLifeSpan * 3.0 - 2.0) * 255);
            }

            RectangleF rect = new RectangleF(disX - radius, disY - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect); 
        }
    }
}
