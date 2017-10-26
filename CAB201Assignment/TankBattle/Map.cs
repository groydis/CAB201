using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TankBattle
{
    /// <summary>
    /// This class represents the landscape, the arena on which the tanks battle. The terrain is randomly 
    /// generated and can be destroyed during the round. A new Map with a newly-generated terrain is created for each round.
    /// Author Greyden Scott & Sean O'Connell October 2017
    /// Written, edited and tested by both team members
    /// </summary>
    public class Map
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;

        private bool[,] thisMap = new bool[WIDTH, HEIGHT];

        Random rng = new Random();

        /// <summary>
        /// This constructor randomly generates the terrain on which the tanks will battle. 
        /// It creates a two-dimensional array of bools which represent the terrain (where 'true' means there is terrain at that location). 
        /// The size of the array is in the constants Map.WIDTH and Map.HEIGHT
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        public Map()
        {
            

            int mapSize = rng.Next(75, 100);
            for (int i = mapSize; i > 0; i--)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    thisMap[j, HEIGHT - i] = true;
                }
            }

            int mapBomb = rng.Next(25, 75);
            for (int z = 0; z < mapBomb; z++)
            {
                float bX = rng.Next(0, 159);
                float bY = rng.Next(mapSize, 119);
                float rad = rng.Next(5, 10);

                DestroyTerrain(bX, bY, rad);
            }

            while (GravityStep())
            {
                GravityStep();
            }

            for (int i = 0; i < WIDTH; i++)
            {
                thisMap[i, HEIGHT - 1] = true;
                thisMap[i, HEIGHT - 2] = true;
            }
        }

        /// <summary>
        /// This returns whether there is any terrain at the given coordinates. 
        /// If there is, it returns true. Otherwise, it returns false.
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X location to check</param> 
        /// <param name="y">The Y location to check</param>
        /// <returns> If terrain exists returns true. Otherwise, it returns false. </returns>
        public bool Get(int x, int y)
        {
            if (x >= 0 && x <= WIDTH)
            {
                if (y >= 0 && y <= HEIGHT)
                {
                    if (thisMap[x,y] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This returns  whether there is room for a tank-sized object 
        /// (a tank is Tank.WIDTH wide and Tank.HEIGHT tall) at the given coordinates.
        /// The coordinates refer to the top left of the tank position
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X location to check</param> 
        /// <param name="y">The Y location to check</param>
        /// <returns> If tank can fit exists returns false. Otherwise, it returns true. </returns>
        public bool CheckTankCollide(int x, int y)
        {
            int tankBott = y + TankModel.HEIGHT;
            int tankR = x + TankModel.WIDTH;

            if (x >= 0 && y >= 0)
            {
                if (tankR <= WIDTH && tankBott <= HEIGHT)
                {
                    for (int i = x; i < tankR; i++)
                    {
                        for (int z = y; z < tankBott; z++)
                        {
                            if (Get(i, z) == true)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// This method takes the x coordinate of a tank and, using that, 
        /// finds the largest (lowest) y coordinate where a tank can go
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X location to check</param> 
        /// <returns> Returns the Y position </returns>
        public int TankYPosition(int x)
        {
            int lowestValid = 0;
            for (int y = 0; y <= HEIGHT - TankModel.HEIGHT; y++)
            {
                int colTiles = 0;
                for (int iy = 0; iy < TankModel.HEIGHT; iy++)
                {
                    for (int ix = 0; ix < TankModel.WIDTH; ix++)
                    {

                        if (Get(x + ix, y+ iy))
                        {
                            colTiles++;
                        }
                    }
                }
                if (colTiles == 0)
                {
                    lowestValid = y;
                }
            }
            return lowestValid;
        }

        /// <summary>
        /// This method destroys all terrain within a circle centred around destroyX, destroyY. 
        /// This method is called after a Shell explodes, destroying the terain
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="destroyX">The X location to destroy</param> 
        /// <param name="destroyY">The Y location to destroy</param> 
        /// <param name="radius">The radius of the destroy area</param> 
        public void DestroyTerrain(float destroyX, float destroyY, float radius)
        {
            float dist = 0;

            for (int y = 0; y < HEIGHT; y++)
            {                
                for (int x = 0; x < WIDTH; x++)
                {                    
                    dist = (float)Math.Sqrt(Math.Pow(x - destroyX, 2) + Math.Pow(y - destroyY, 2));

                    if (dist < radius)
                    {                        
                        thisMap[x, y] = false;
                    }
                }
            }
        }

        /// <summary>
        /// This method takes the x coordinate of a tank and, using that, 
        /// finds the largest (lowest) y coordinate where a tank can go
        /// Author Greyden Scott & Sean O'Connell October 2017
        /// Written, edited and tested by both team members
        /// </summary>
        /// <param name="x">The X location to check</param> 
        /// <returns> Returns the Y position </returns>
        public bool GravityStep()
        {
            bool mover = false;

            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = HEIGHT - 2; y > 0; y--)
                {
                    if (Get(x, y) == true && Get(x, y + 1) == false)
                    {
                        thisMap[x, y + 1] = true;
                        thisMap[x, y] = false;
                        mover = true;
                    }
                }
            }
            return mover;
        }
    }
}
