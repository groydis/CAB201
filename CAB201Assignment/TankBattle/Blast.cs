using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TankBattle
{
    /// <summary>
    /// The Blast class is a type of Attack that represents the payload attached to a Shell. An Boom will inflict damage on tanks and destroy terrain within a radius.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public class Blast : Effect
    {
        private int explosionDamage;
        private int explosionRadius;
        private int earthDestructionRadius;

        private float x;
        private float y;
        private float blastLifeSpan;

        /// <summary>
        /// Blast takes the explosion damage, explosion radius and earth 
        /// destruction radius values it is passed and stores them as private fields.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="explosionDamage">The amount of damage the explosion does</param> 
        /// <param name="explosionReadius">The radius of the explosionk</param> 
        /// <param name="earthDestruction">The earth destruciton of the explosion</param> 
        public Blast(int explosionDamage, int explosionRadius, int earthDestructionRadius)
        {
            this.explosionDamage = explosionDamage;
            this.explosionRadius = explosionRadius;
            this.earthDestructionRadius = earthDestructionRadius;
        }


        /// <summary>
        /// This method detonates the Boom at the specified location.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X location to ignite</param> 
        /// <param name="y">The y location to ignite</param> 
        public void Ignite(float x, float y)
        {
            this.x = x;
            this.y = y;

            blastLifeSpan = 1.0f;
        }

        /// <summary>
        /// This method reduces the Blast's lifespan by 0.02, and if it reaches 0 (or lower) 
        /// Calls the GamePlay's InflictDamage() method with the Blaat's x and y coordinates, explosion damage and explosion radius.
        /// Calls the Gameplay's GetArena() to get a reference to the Map and then call DestroyTerrain() on it, this time passing in Blast's x and y coordinates and the earth destruction radius
        /// Calls the Gameplays's RemoveEffect(), passing in the this reference to remove the Blast from the list of active Attacks.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void Tick()
        {
            blastLifeSpan -= 0.02f;

            if (blastLifeSpan <= 0)
            {
                blastLifeSpan = 0;
                effectGame.InflictDamage(x,y,explosionDamage,explosionRadius);

                Map thisMap = effectGame.GetArena();

                thisMap.DestroyTerrain(x,y,earthDestructionRadius);

                effectGame.RemoveEffect(this);

            }
        }
        /// <summary>
        /// This method draws one frame of the Boom. The idea is to draw a circle that expands, cycling from yellow to red and then fading out.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public override void Display(Graphics graphics, Size displaySize)
        {
            //Answer has been given to us and just needs variables changed to private fields and 
            //Use variables that have been created in other scripts
            float x = (float)this.x * displaySize.Width / Map.WIDTH;
            float y = (float)this.y * displaySize.Height / Map.HEIGHT;
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

            RectangleF rect = new RectangleF(x - radius, y - radius, radius * 2, radius * 2);
            Brush b = new SolidBrush(Color.FromArgb(alpha, red, green, blue));

            graphics.FillEllipse(b, rect);
        }
    }
}
