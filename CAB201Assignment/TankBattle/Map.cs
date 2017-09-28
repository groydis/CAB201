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
                thisMap[i, HEIGHT] = true;
            }
            for (int i = 0; i < WIDTH; i++)
            {
                thisMap[i, 0] = false;
            }

            int newX, newY;
            for (int i = 0; i < 250; i++)
            {
                newX = rng.Next(0, 160);
                newY = rng.Next(0, 119);

                while (Get(newX, newY + 1) == false)
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
            int tankBott = y + TankModel.HEIGHT, tankR = x + TankModel.WIDTH;

            if (x >= 0 && y >= 0)
            {
                if (x <= WIDTH - TankModel.WIDTH && y <= HEIGHT - TankModel.HEIGHT)
                {
                    if (Get(x, tankBott+ 1) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public int TankYPosition(int x)
        {
            //Not technically correct just trying to get past it and hopefully get it working later on
            int ans = 110;
            for (int y = HEIGHT; y > 0; y--)
            {
                for (int i = x; i <= WIDTH; i++)
                {
                    if (CheckTankCollide(i, y) == true)
                    {
                        return y;
                    }   
                }
            }
            return ans;
        }

        public void DestroyTerrain(float destroyX, float destroyY, float radius)
        {
            for (int y = 0; y > HEIGHT; y++)
            {
                for (int x = 0; x <= WIDTH; x++)
                {
                    double dist = Math.Sqrt(Math.Pow(x - destroyX, 2) + Math.Pow(y - destroyY, 2));
                    if (dist < radius)
                    {
                        thisMap[x, y] = false;
                    }
                }
            }
        }

        public bool GravityStep()
        {
            for (int y = 0; y > HEIGHT; y++)
            {
                for (int x = 0; x <= WIDTH; x++)
                {
                    if(thisMap[x, y] == true)
                    {
                       if(Get(x, y + 1) == false)
                       {
                            thisMap[x, y] = false;
                            thisMap[x, y + 1] = true;
                       }
                    }
                }
            }
            return false;
        }
    }
}
