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
        private int currDurability;
        private Gameplay game;
        private TankModel tank;
        private Bitmap bmp;

        public BattleTank(Opponent player, int tankX, int tankY, Gameplay game)
        {
            player = this.player;

            tank = player.GetTank();
            currDurability = tank.GetTankArmour();

            angle = 0;
            power = 25;
            curr_weapon = 0;
            
            bmp = tank.CreateBMP(player.GetColour(), angle);

            game = this.game;
        }

        public Opponent GetPlayer()
        {
            return player;
        }

        public TankModel GetTank()
        {
            return player.GetTank();
        }

        public float GetTankAngle()
        {
            return angle;
                
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }

        public int GetCurrentPower()
        {
            return power;
        }

        public void SetPower(int power)
        {
            this.power = power;
        }

        public int GetWeaponIndex()
        {
            return curr_weapon;
        }
        public void SetWeapon(int newWeapon)
        {
            this.curr_weapon = newWeapon;
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
