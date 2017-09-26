using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class BattleTank
    {
        private float angle;
        private int power;
        private int curr_weapon;

        private Opponent player;
        private int tankX;
        private int tankY;
        private Gameplay game;
        private TankModel tank;
        private Bitmap bmp;

        public BattleTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            player = this.player;

            tank = player.GetTank();

            angle = 0;
            power = 25;
            curr_weapon = 0;
            
            bmp = tank.CreateBMP(player.GetColour(), angle);

            game = this.game;
        }

        public Opponent GetPlayer()
        {
            throw new NotImplementedException();
        }

        public TankModel GetTank()
        {
            return player.GetTank();
        }

        public float GetTankAngle()
        {
            throw new NotImplementedException();
        }

        public void SetAngle(float angle)
        {
            throw new NotImplementedException();
        }

        public int GetCurrentPower()
        {
            throw new NotImplementedException();
        }

        public void SetPower(int power)
        {
            throw new NotImplementedException();
        }

        public int GetWeaponIndex()
        {
            throw new NotImplementedException();
        }
        public void SetWeapon(int newWeapon)
        {
            throw new NotImplementedException();
        }

        public void Display(Graphics graphics, Size displaySize)
        {
            throw new NotImplementedException();
        }

        public int GetX()
        {
            throw new NotImplementedException();
        }
        public int Y()
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public void InflictDamage(int damageAmount)
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public bool GravityStep()
        {
            throw new NotImplementedException();
        }
    }
}
