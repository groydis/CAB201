﻿using System;
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

        public Map()
        {
            throw new NotImplementedException();
        }

        public bool Get(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool CheckTankCollide(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int TankYPosition(int x)
        {
            throw new NotImplementedException();
        }

        public void DestroyTerrain(float destroyX, float destroyY, float radius)
        {
            throw new NotImplementedException();
        }

        public bool GravityStep()
        {
            throw new NotImplementedException();
        }
    }
}
