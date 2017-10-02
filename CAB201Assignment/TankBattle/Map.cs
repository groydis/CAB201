using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Map
    {
        public const int WIDTH = 160;
        public const int HEIGHT = 120;

        private bool[,] thisMap = new bool[WIDTH, HEIGHT];

        Random rng = new Random();

        public Map()
        {
            for (int i = 0; i < WIDTH; i++)
            {
                thisMap[i, HEIGHT -1] = true;
                thisMap[i, 0] = false;
            }

            int newX, newY;
            for (int i = 0; i < 250; i++)
            {
                newX = rng.Next(0, 159);
                newY = rng.Next(0, 119);

                while (Get(newX, newY + 1) == false && newY <= 119)
                {
                    newY++;
                };

                thisMap[newX, newY] = true;
            }

        }

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

                        if (this.Get(x + ix, y + iy))
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

        public bool GravityStep()
        {
            bool mover = false;

            for (int y = 0; y < HEIGHT - 1; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    if (y < HEIGHT - 1)
                    {
                        if (Get(x, y) == true && Get(x, y + 1) == false)
                        {
                            thisMap[x, y + 1] = true;
                            thisMap[x, y] = false;
                            mover = true;
                        }
                    }
                }
            }         
                            
            return mover;
        }
    }
}
